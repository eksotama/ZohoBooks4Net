#region License
/*
 * Copyright 2017 Brandon James
 *
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 */
#endregion

using System.Collections.Generic;
using System.Threading.Tasks;
using ZohoBooks4Net.Clients.Interfaces;
using ZohoBooks4Net.Domain.Entities;
using ZohoBooks4Net.Filters;
using ZohoBooks4Net.Responses;
using ZohoBooks4Net.Responses.PaginatedResponses;

namespace ZohoBooks4Net.Clients
{
    public class Users : ZohoBooksClientBase, IGetsPaginatedResponses<User>,
        ICreates<User>, IGets<User>, IUpdates<User>, IDeletes<User>
    {
        public Users(Configuration configuration) : base(configuration)
        {
            BaseUri = "users";
        }

        /// <summary>
        /// Create a new user. 
        /// </summary>
        /// <param name="newUser">The user data.</param>
        /// <returns>The user data after it has passed through the Zoho service.</returns>
        public async Task<User> CreateAsync(User newUser)
        {
            var response = await PostDataAsync<User, ZohoBooksResponse<User>>(newUser, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Details of an existing user. 
        /// </summary>
        /// <param name="id">The id of the user to get.</param>
        /// <returns>The requested user.</returns>
        public async Task<User> GetAsync(string id)
        {
            var response = await GetDataAsync<ZohoBooksResponse<User>>(id, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Update the details of an user. 
        /// </summary>
        /// <param name="id">The id of the user to update.</param>
        /// <param name="user">The user details to update the user with.</param>
        /// <returns>The updated user details after it goes through the Zoho service.</returns>
        public async Task<User> UpdateAsync(string id, User user)
        {
            var response = await PutDataAsync<User, ZohoBooksResponse<User>>(id, user, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Update the details of an user. 
        /// </summary>
        /// <param name="id">The id of the user to delete.</param>
        /// <returns>A response indicating if the request was successful.</returns>
        public async Task<bool> DeleteAsync(string id)
        {
            var response = await DeleteDataAsync<ZohoBooksMessage>(id, OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Mark an inactive user as active. 
        /// </summary>
        /// <param name="id">The id of the user.</param>
        /// <returns>A response indicating if the request was successful.</returns>
        public async Task<bool> MarkAsActiveAsync(string id)
        {
            var response = await PostAsync<ZohoBooksMessage>(string.Format("{0}/active", id), OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Mark an inactive user as inactive. 
        /// </summary>
        /// <param name="id">The id of the user.</param>
        /// <returns>A response indicating if the request was successful.</returns>
        public async Task<bool> MarkAsInActiveAsync(string id)
        {
            var response = await PostAsync<ZohoBooksMessage>(string.Format("{0}/inactive", id), OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Send invitation email to a user. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> InviteUserAsync(string id)
        {
            var response = await PostAsync<ZohoBooksResponse<IList<User>>>(string.Format("{0}/invite", id), OrganizationIdFilter);
            return response.Code == 0;
        }

        public async Task<IList<User>> GetPageRangeAsync(int start, int end, int pageSize = 100, IFilter filter = null)
        {
            var pageRange = new List<User>();
            var currentPage = new PaginatedResponse<User>();
            var currentPageNum = currentPage.Context.Page;
            var usersFilter = SetupFilter(filter, currentPageNum) as UsersFilter;

            while ((currentPage = await GetDataAsync<PaginatedResponse<User>>(usersFilter)).Context.Page <= end)
            {
                pageRange.AddRange(currentPage.Resource);
                usersFilter.Page = currentPageNum++;
            }

            return pageRange;
        }

        public async Task<IList<User>> GetPageAsync(int page, int pageSize = 100, IFilter filter = null)
        {
            var result = await GetDataAsync<PaginatedResponse<User>>(SetupFilter(filter, page));
            return result.Resource;
        }

        public async Task<IList<User>> GetAllPagesAsync(IFilter filter)
        {
            var allPages = new List<User>();
            var currentPage = new PaginatedResponse<User>();
            var currentPageNum = currentPage.Context.Page;
            var contactsFilter = SetupFilter(filter, currentPage.Context.Page) as UsersFilter;

            while ((currentPage = await GetDataAsync<PaginatedResponse<User>>(contactsFilter)).Context.HasMorePage)
            {
                allPages.AddRange(currentPage.Resource);
                contactsFilter.Page = currentPageNum++;
            }
            return allPages;
        }

        protected override IPaginationFilter SetupFilter(IFilter filter, int page, int pageSize = 100)
        {
            var pageFilter = (filter == null) ? new UsersFilter() : (UsersFilter)filter;
            pageFilter.Page = page;
            pageFilter.PerPage = pageSize;
            pageFilter.OrganizationId = OrganizationIdFilter.OrganizationId;
            return pageFilter;
        }
    }
}
