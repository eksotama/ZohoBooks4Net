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
    public class Journals : ZohoBooksClientBase, IGetsPaginatedResponses<Journal>,
        ICreates<Journal>, IGets<Journal>, IUpdates<Journal>, IDeletes<Journal>
    {
        public Journals(Configuration configuration) : base(configuration)
        {
            BaseUri = "journals";
        }

        /// <summary>
        /// Create a journal 
        /// </summary>
        /// <param name="newItem">Journal details.</param>
        /// <returns>The new journal after it has run through the Zoho service.</returns>
        public async Task<Journal> CreateAsync(Journal newItem)
        {
            var response = await PostDataAsync<Journal, ZohoBooksResponse<Journal>>(newItem, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Get the details of the journal 
        /// </summary>
        /// <param name="id">The id of the journal to get.</param>
        /// <returns></returns>
        public async Task<Journal> GetAsync(string id)
        {
            var response = await GetDataAsync<ZohoBooksResponse<Journal>>(id, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Updates the journal with given information 
        /// </summary>
        /// <param name="id">The id of the journal to update.</param>
        /// <param name="item">The updated journal details.</param>
        /// <returns>The resource after being updated through the Zoho service.</returns>
        public async Task<Journal> UpdateAsync(string id, Journal item)
        {
            var response = await PutDataAsync<Journal, ZohoBooksResponse<Journal>>(id, item, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Deletes the given journal 
        /// </summary>
        /// <param name="id">The id of the journal to delete.</param>
        /// <returns>A response indicating if the request was successful.</returns>
        public async Task<bool> DeleteAsync(string id)
        {
            var response = await DeleteDataAsync<ZohoBooksMessage>(id, OrganizationIdFilter);
            return response.Code == 0;
        }

        public async Task<IList<Journal>> GetPageAsync(int page, int pageSize = 100, IFilter filter = null)
        {
            var result = await GetDataAsync<PaginatedResponse<Journal>>((SetupFilter(filter, page) as JournalsFilter));
            return result.Resource;
        }

        public async Task<IList<Journal>> GetPageRangeAsync(int start, int end, int pageSize = 100, IFilter filter = null)
        {
            var pageRange = new List<Journal>();
            var currentPage = new PaginatedResponse<Journal>();
            var currentPageNum = currentPage.Context.Page;
            var journalsFilter = SetupFilter(filter, currentPageNum) as JournalsFilter;

            while ((currentPage = await GetDataAsync<PaginatedResponse<Journal>>(journalsFilter)).Context.Page <= end)
            {
                pageRange.AddRange(currentPage.Resource);
                journalsFilter.Page = currentPageNum++;
            }

            return pageRange;
        }

        public async Task<IList<Journal>> GetAllPagesAsync(IFilter filter)
        {
            var allPages = new List<Journal>();
            var currentPage = new PaginatedResponse<Journal>();
            var currentPageNum = currentPage.Context.Page;
            var journalsFilter = SetupFilter(filter, currentPage.Context.Page) as JournalsFilter;

            while ((currentPage = await GetDataAsync<PaginatedResponse<Journal>>(journalsFilter)).Context.HasMorePage)
            {
                allPages.AddRange(currentPage.Resource);
                journalsFilter.Page = currentPageNum++;
            }
            return allPages;
        }

        protected override IPaginationFilter SetupFilter(IFilter filter, int page, int pageSize = 100)
        {
            var pageFilter = (filter == null) ? new JournalsFilter() : (JournalsFilter)filter;
            pageFilter.Page = page;
            pageFilter.PerPage = pageSize;
            pageFilter.OrganizationId = OrganizationIdFilter.OrganizationId;
            return pageFilter;
        }
    }
}
