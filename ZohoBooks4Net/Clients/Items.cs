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
    public class Items : ZohoBooksClientBase, IGetsPaginatedResponses<Item>,
        ICreates<Item>, IGets<Item>, IUpdates<Item>, IDeletes<Item>
    {
        public Items(Configuration configuration) : base(configuration)
        {
            BaseUri = "items";
        }

        /// <summary>
        /// Create a new item. 
        /// </summary>
        /// <param name="newItem">The item data.</param>
        /// <returns>The item data after it has passed through the Zoho service.</returns>
        public async Task<Item> CreateAsync(Item newItem)
        {
            var response = await PostDataAsync<Item, ZohoBooksResponse<Item>>(newItem, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Details of an existing item. 
        /// </summary>
        /// <param name="id">The id of the item to get.</param>
        /// <returns>The requested item.</returns>
        public async Task<Item> GetAsync(string id)
        {
            var response = await GetDataAsync<ZohoBooksResponse<Item>>(id, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Update the details of an item. 
        /// </summary>
        /// <param name="id">The id of the item to update.</param>
        /// <param name="item">The item details to update the item with.</param>
        /// <returns>The updated item details after it goes through the Zoho service.</returns>
        public async Task<Item> UpdateAsync(string id, Item item)
        {
            var response = await PutDataAsync<Item, ZohoBooksResponse<Item>>(id, item, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Update the details of an item. 
        /// </summary>
        /// <param name="id">The id of the item to delete.</param>
        /// <returns>A response indicating if the request was successful.</returns>
        public async Task<bool> DeleteAsync(string id)
        {
            var response = await DeleteDataAsync<ZohoBooksMessage>(id, OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Mark an inactive item as active. 
        /// </summary>
        /// <param name="id">The id of the item.</param>
        /// <returns>A response indicating if the request was successful.</returns>
        public async Task<bool> MarkAsActiveAsync(string id)
        {
            var response = await PostAsync<ZohoBooksMessage>(string.Format("{0}/active", id), OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Mark an inactive item as inactive. 
        /// </summary>
        /// <param name="id">The id of the item.</param>
        /// <returns>A response indicating if the request was successful.</returns>
        public async Task<bool> MarkAsInActiveAsync(string id)
        {
            var response = await PostAsync<ZohoBooksMessage>(string.Format("{0}/inactive", id), OrganizationIdFilter);
            return response.Code == 0;
        }

        public async Task<IList<Item>> GetPageRangeAsync(int start, int end, int pageSize = 100, IFilter filter = null)
        {
            var pageRange = new List<Item>();
            var currentPage = new PaginatedResponse<Item>();
            var currentPageNum = currentPage.Context.Page;
            var itemsFilter = SetupFilter(filter, currentPageNum) as ItemsFilter;

            while ((currentPage = await GetDataAsync<PaginatedResponse<Item>>(itemsFilter)).Context.Page <= end)
            {
                pageRange.AddRange(currentPage.Resource);
                itemsFilter.Page = currentPageNum++;
            }

            return pageRange;
        }

        public async Task<IList<Item>> GetPageAsync(int page, int pageSize = 100, IFilter filter = null)
        {
            var result = await GetDataAsync<PaginatedResponse<Item>>(SetupFilter(filter, page));
            return result.Resource;
        }

        public async Task<IList<Item>> GetAllPagesAsync(IFilter filter)
        {
            var allPages = new List<Item>();
            var currentPage = new PaginatedResponse<Item>();
            var currentPageNum = currentPage.Context.Page;
            var contactsFilter = SetupFilter(filter, currentPage.Context.Page) as ItemsFilter;

            while ((currentPage = await GetDataAsync<PaginatedResponse<Item>>(contactsFilter)).Context.HasMorePage)
            {
                allPages.AddRange(currentPage.Resource);
                contactsFilter.Page = currentPageNum++;
            }
            return allPages;
        }

        protected override IPaginationFilter SetupFilter(IFilter filter, int page, int pageSize = 100)
        {
            var pageFilter = (filter == null) ? new ItemsFilter() : (ItemsFilter)filter;
            pageFilter.Page = page;
            pageFilter.PerPage = pageSize;
            pageFilter.OrganizationId = OrganizationIdFilter.OrganizationId;
            return pageFilter;
        }
    }
}
