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
    public class Expenses : ZohoBooksClientBase, IGetsPaginatedResponses<Expense>,
        ICreates<Expense>, IGets<Expense>, IUpdates<Expense>, IDeletes<Expense>
    {
        // Not setting BaseUri here because Expenses also uses the claimant uri space.
        private new string BaseUri { get; set; } = "expenses";

        public Expenses(Configuration configuration) : base(configuration) { }

        /// <summary>
        /// Create billable or non-billable expense 
        /// </summary>
        /// <param name="newItem">The details of a new billable expense.</param>
        /// <returns>A response with the expense after it runs through the Zoho service.</returns>
        public async Task<Expense> CreateAsync(Expense newItem)
        {
            var response = await PostDataAsync<Expense, ZohoBooksResponse<Expense>>(BaseUri, newItem, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Get the details of the Expense 
        /// </summary>
        /// <param name="id">The id of the expense to get.</param>
        /// <returns>A response with the requested expense.</returns>
        public async Task<Expense> GetAsync(string id)
        {
            var response = await GetDataAsync<ZohoBooksResponse<Expense>>(string.Format("{0}/{1}", BaseUri, id), OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Update an existing Expense 
        /// </summary>
        /// <param name="id">The id of the expense to update.</param>
        /// <param name="item">The updated expense data.</param>
        /// <returns>A response with the expense data after it runs through the Zoho service.</returns>
        public async Task<Expense> UpdateAsync(string id, Expense item)
        {
            var response = await PutDataAsync<Expense, ZohoBooksResponse<Expense>>(string.Format("{0}/{1}", BaseUri, id), item, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Delete an existing expense. 
        /// </summary>
        /// <param name="id">The id of the expense to delete.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> DeleteAsync(string id)
        {
            var response = await DeleteDataAsync<ZohoBooksMessage>(string.Format("{0}/{1}", BaseUri, id), OrganizationIdFilter);
            return response.Code == 0;
        }

        public async Task<IList<Expense>> GetAllPagesAsync(IFilter filter)
        {
            var allPages = new List<Expense>();
            var currentPage = new PaginatedResponse<Expense>();
            var currentPageNum = currentPage.Context.Page;
            var expensesFilter = SetupFilter(filter, currentPage.Context.Page);

            while ((currentPage = await GetDataAsync<PaginatedResponse<Expense>>(BaseUri, expensesFilter)).Context.HasMorePage)
            {
                allPages.AddRange(currentPage.Resource);
                expensesFilter.Page = currentPageNum++;
            }
            return allPages;
        }

        public async Task<IList<Expense>> GetPageAsync(int page, int pageSize = 100, IFilter filter = null)
        {
            var result = await GetDataAsync<PaginatedResponse<Expense>>(BaseUri, SetupFilter(filter, page));
            return result.Resource;
        }

        public async Task<IList<Expense>> GetPageRangeAsync(int start, int end, int pageSize = 100, IFilter filter = null)
        {
            var pageRange = new List<Expense>();
            var currentPage = new PaginatedResponse<Expense>();
            var currentPageNum = currentPage.Context.Page;
            var expensesFilter = SetupFilter(filter, currentPageNum);

            while ((currentPage = await GetDataAsync<PaginatedResponse<Expense>>(BaseUri, expensesFilter)).Context.Page <= end)
            {
                pageRange.AddRange(currentPage.Resource);
                expensesFilter.Page = currentPageNum++;
            }

            return pageRange;
        }

        /// <summary>
        /// Get the complete history and comments of expense. 
        /// </summary>
        /// <param name="expenseId">The expense to get commments for.</param>
        /// <returns>A list of comments for the requested expense.</returns>
        public async Task<IList<Comment>> GetCommentsAsync(string expenseId)
        {
            return await GetDataAsync<IList<Comment>>(string.Format("{0}/{1}/comments", BaseUri, expenseId), OrganizationIdFilter);
        }

        /// <summary>
        /// Get the details of the claimant 
        /// </summary>
        /// <param name="id">The id of the claimant to get.</param>
        /// <returns>A response with the requested claimant.</returns>
        public async Task<Claimant> GetClaimantAsync(string id)
        {
            var response = await GetDataAsync<ZohoBooksResponse<Claimant>>(id, OrganizationIdFilter);
            return response.Resource;
        }

        public async Task<IList<Claimant>> GetClaimantsAsync(IFilter filter)
        {
            var allPages = new List<Claimant>();
            var currentPage = new PaginatedResponse<Claimant>();
            var currentPageNum = currentPage.Context.Page;
            var expensesFilter = SetupFilter(filter, currentPage.Context.Page) as ExpensesFilter;

            while ((currentPage = await GetDataAsync<PaginatedResponse<Claimant>>("claimants", expensesFilter)).Context.HasMorePage)
            {
                allPages.AddRange(currentPage.Resource);
                expensesFilter.Page = currentPageNum++;
            }
            return allPages;
        }

        /// <summary>
        /// Delete an existing claimant  
        /// </summary>
        /// <param name="id">The id of the claimant to delete.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> DeleteClaimantAsync(string id)
        {
            var response = await DeleteDataAsync<ZohoBooksMessage>(string.Format("claimants/{0}", id), OrganizationIdFilter);
            return response.Code == 0;
        }

        // TODO: Get an expense receipt, add an expense receipt (FILE)
        
        /// <summary>
        /// Delete an existing expense receipt.
        /// </summary>
        /// <param name="id">The id of the expense whose receipt should be deleted.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> DeleteReceiptAsync(string id)
        {
            var response = await DeleteDataAsync<ZohoBooksMessage>(string.Format("{0}/{1}/receipt", BaseUri, id), OrganizationIdFilter);
            return response.Code == 0;
        }

        protected override IPaginationFilter SetupFilter(IFilter filter, int page, int pageSize = 100)
        {
            var pageFilter = (filter == null) ? new ExpensesFilter() : (ExpensesFilter)filter;
            pageFilter.Page = page;
            pageFilter.PerPage = pageSize;
            pageFilter.OrganizationId = OrganizationIdFilter.OrganizationId;
            return pageFilter;
        }
    }
}
