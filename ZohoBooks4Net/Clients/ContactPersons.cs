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
    public class ContactPersons : ZohoBooksClientBase, IGetsPaginatedResponses<ContactPerson>,
        ICreates<ContactPerson>, IGets<ContactPerson>, IUpdates<ContactPerson>, IDeletes<ContactPerson>
    {
        public ContactPersons(Configuration configuration) : base(configuration)
        {
            BaseUri = "contacts/contact_persons";
        }

        /// <summary>
        /// Create a contact person for contact. 
        /// </summary>
        /// <param name="newItem">The data for the new contact person.</param>
        /// <returns>A response with the new contact person's data.</returns>
        public async Task<ContactPerson> CreateAsync(ContactPerson newItem)
        {
            var response = await PostDataAsync<ContactPerson, ZohoBooksResponse<ContactPerson>>("", newItem, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Get the contact person details. 
        /// </summary>
        /// <param name="id">The id of the contact to get.</param>
        /// <returns>A response with the contact details.</returns>
        public async Task<ContactPerson> GetAsync(string id)
        {
            var response = await GetDataAsync<ZohoBooksResponse<ContactPerson>>(id, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Update an existing contact person. 
        /// </summary>
        /// <param name="id">The ID of the contact person to update.</param>
        /// <param name="item">The data to replace for the selected contact.</param>
        /// <returns>A response with the updated contact person's data.</returns>
        public async Task<ContactPerson> UpdateAsync(string id, ContactPerson item)
        {
            var response = await PutDataAsync<ContactPerson, ZohoBooksResponse<ContactPerson>>(id, item, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Delete an existing contact person. 
        /// </summary>
        /// <param name="id">The id of the contact person to delete.</param>
        /// <returns>A response indicating if the request was successful.</returns>
        public async Task<bool> DeleteAsync(string id)
        {
            var response = await DeleteDataAsync<ZohoBooksMessage>(id);
            return response.Code == 0;
        }

        public async Task<IList<ContactPerson>> GetAllPagesAsync(IFilter filter)
        {
            var allPages = new List<ContactPerson>();
            var currentPage = new PaginatedResponse<ContactPerson>();
            var currentPageNum = currentPage.Context.Page;
            var contactsFilter = SetupFilter(filter, currentPage.Context.Page);

            while ((currentPage = await GetDataAsync<PaginatedResponse<ContactPerson>>(contactsFilter)).Context.HasMorePage)
            {
                allPages.AddRange(currentPage.Resource);
                contactsFilter.Page = currentPageNum++;
            }
            return allPages;
        }

        public async Task<IList<ContactPerson>> GetPageAsync(int page, int pageSize = 100, IFilter filter = null)
        {
            var result = await GetDataAsync<PaginatedResponse<ContactPerson>>(SetupFilter(filter, page));
            return result.Resource;
        }

        public async Task<IList<ContactPerson>> GetPageRangeAsync(int start, int end, int pageSize = 100, IFilter filter = null)
        {
            var pageRange = new List<ContactPerson>();
            var currentPage = new PaginatedResponse<ContactPerson>();
            var currentPageNum = currentPage.Context.Page;
            var contactsFilter = SetupFilter(filter, currentPageNum);

            while ((currentPage = await GetDataAsync<PaginatedResponse<ContactPerson>>(contactsFilter)).Context.Page <= end)
            {
                pageRange.AddRange(currentPage.Resource);
                contactsFilter.Page = currentPageNum++;
            }

            return pageRange;
        }

        /// <summary>
        /// Mark a contact person as primary for the contact. 
        /// </summary>
        /// <param name="id">The ID of the contact person to mark.</param>
        /// <returns>A response indicating if the request was successful.</returns>
        public async Task<bool> MarkAsPrimaryContactPersonAsync(string id)
        {
            var response = await PostAsync<ZohoBooksMessage>(string.Format("{0}/primary"));
            return response.Code == 0;
        }
    }
}
