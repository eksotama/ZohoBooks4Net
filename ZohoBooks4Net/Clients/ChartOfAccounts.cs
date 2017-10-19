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
    public class ChartOfAccounts : ZohoBooksClientBase, IGetsPaginatedResponses<ChartOfAccount>,
        ICreates<ChartOfAccount>, IGets<ChartOfAccount>, IUpdates<ChartOfAccount>, IDeletes<ChartOfAccount>
    {
        public ChartOfAccounts(Configuration configuration) : base(configuration)
        {
            BaseUri = "chartofaccounts";
        }

        /// <summary>
        /// Creates an account with the given account type. 
        /// </summary>
        /// <param name="newItem">The details of the new account.</param>
        /// <returns>A response with the details after rhe data runs through the Zoho service.</returns>
        public async Task<ChartOfAccount> CreateAsync(ChartOfAccount newItem)
        {
            var response = await PostDataAsync<ChartOfAccount, ZohoBooksResponse<ChartOfAccount>>(newItem, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Gets the details of an account 
        /// </summary>
        /// <param name="id">The id of the account to get.</param>
        /// <returns>The requested account.</returns>
        public async Task<ChartOfAccount> GetAsync(string id)
        {
            var response = await GetDataAsync<ZohoBooksResponse<ChartOfAccount>>(id, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Updates the account information. 
        /// </summary>
        /// <param name="id">The id of the account to update.</param>
        /// <param name="item">The details of the account to update.</param>
        /// <returns>A response with the updated details of the account.</returns>
        public async Task<ChartOfAccount> UpdateAsync(string id, ChartOfAccount item)
        {
            var response = await PutDataAsync<ChartOfAccount, ZohoBooksResponse<ChartOfAccount>>(id, item, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Deletes the given account. Accounts associated in any transaction/products could not be deleted. 
        /// </summary>
        /// <param name="id">The id of the account to delete.</param>
        /// <returns>A response indicating if the request was succesful.</returns>
        public async Task<bool> DeleteAsync(string id)
        {
            var response = await DeleteDataAsync<ZohoBooksMessage>(id, OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Updates the account status as active. 
        /// </summary>
        /// <param name="id">The id of the account to mark as active.</param>
        /// <returns>A response indicating if the request was successful.</returns>
        public async Task<bool> MarkAccountAsActiveAsync(string id)
        {
            var response = await PostAsync<ZohoBooksMessage>(string.Format("{0}/active", id), OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Updates the account status as inactive. 
        /// </summary>
        /// <param name="id">The id of the account to mark as active.</param>
        /// <returns>A response indicating if the request was successful.</returns>
        public async Task<bool> MarkAccountAsInactiveAsync(string id)
        {
            var response = await PostAsync<ZohoBooksMessage>(string.Format("{0}/inactive", id), OrganizationIdFilter);
            return response.Code == 0;
        }

        public async Task<IList<ChartOfAccount>> GetPageAsync(int page, int pageSize = 100, IFilter filter = null)
        {
            var result = await GetDataAsync<PaginatedResponse<ChartOfAccount>>(SetupFilter(filter, page));
            return result.Resource;
        }

        public async Task<IList<ChartOfAccount>> GetPageRangeAsync(int start, int end, int pageSize = 100, IFilter filter = null)
        {
            var pageRange = new List<ChartOfAccount>();
            var currentPage = new PaginatedResponse<ChartOfAccount>();
            var currentPageNum = currentPage.Context.Page;
            var journalsFilter = SetupFilter(filter, currentPageNum) as ChartOfAccountsFilter;

            while ((currentPage = await GetDataAsync<PaginatedResponse<ChartOfAccount>>(journalsFilter)).Context.Page <= end)
            {
                pageRange.AddRange(currentPage.Resource);
                journalsFilter.Page = currentPageNum++;
            }

            return pageRange;
        }

        public async Task<IList<ChartOfAccount>> GetAllPagesAsync(IFilter filter)
        {
            var allPages = new List<ChartOfAccount>();
            var currentPage = new PaginatedResponse<ChartOfAccount>();
            var currentPageNum = currentPage.Context.Page;
            var contactsFilter = SetupFilter(filter, currentPage.Context.Page) as ChartOfAccountsFilter;

            while ((currentPage = await GetDataAsync<PaginatedResponse<ChartOfAccount>>(contactsFilter)).Context.HasMorePage)
            {
                allPages.AddRange(currentPage.Resource);
                contactsFilter.Page = currentPageNum++;
            }
            return allPages;
        }

        protected override IPaginationFilter SetupFilter(IFilter filter, int page, int pageSize = 100)
        {
            var pageFilter = (filter == null) ? new ChartOfAccountsFilter() : (ChartOfAccountsFilter)filter;
            pageFilter.Page = page;
            pageFilter.PerPage = pageSize;
            pageFilter.OrganizationId = OrganizationIdFilter.OrganizationId;
            return pageFilter;
        }
    }
}
