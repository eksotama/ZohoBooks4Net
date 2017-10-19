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

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZohoBooks4Net.Clients.Interfaces;
using ZohoBooks4Net.Domain.Entities;
using ZohoBooks4Net.Filters;
using ZohoBooks4Net.Requests;
using ZohoBooks4Net.Responses;
using ZohoBooks4Net.Responses.PaginatedResponses;

namespace ZohoBooks4Net.Clients
{
    public class Bills : ZohoBooksClientBase, IGetsPaginatedResponses<Bill>,
        ICreates<Bill>, IGets<Bill>, IUpdates<Bill>, IDeletes<Bill>
    {
        public Bills(Configuration configuration) : base(configuration)
        {
            BaseUri = "bills";
        }

        /// <summary>
        /// Create a bill received from your vendor. 
        /// </summary>
        /// <param name="newItem">The bill details to create.</param>
        /// <returns>A response with the bill data after it has passed through the Zoho service.</returns>
        public async Task<Bill> CreateAsync(Bill newItem)
        {
            var response = await PostDataAsync<Bill, ZohoBooksResponse<Bill>>(newItem, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Get the details of a bill. 
        /// </summary>
        /// <param name="id">The id of the bill to get.</param>
        /// <returns>A response with the requested bill.</returns>
        public async Task<Bill> GetAsync(string id)
        {
            var response = await GetDataAsync<ZohoBooksResponse<Bill>>(id, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Update a bill. To delete a line item just remove it from the line_items list.
        /// </summary>
        /// <param name="id">The id of the bill to update.</param>
        /// <param name="item">The new bill details to write to the selected bill.</param>
        /// <returns>A response with the updated bill details after passing though the Zoho service.</returns>
        public async Task<Bill> UpdateAsync(string id, Bill item)
        {
            var response = await PutDataAsync<Bill, ZohoBooksResponse<Bill>>(id, item, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Delete an existing bill. Bills which have payments applied cannot be deleted. 
        /// </summary>
        /// <param name="id">The id of the bill to delete.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> DeleteAsync(string id)
        {
            var response = await DeleteDataAsync<ZohoBooksMessage>(id, OrganizationIdFilter);
            return response.Code == 0;
        }

        public async Task<IList<Bill>> GetAllPagesAsync(IFilter filter)
        {
            var allPages = new List<Bill>();
            var currentPage = new PaginatedResponse<Bill>();
            var currentPageNum = currentPage.Context.Page;
            var contactsFilter = SetupFilter(filter, currentPage.Context.Page);

            while ((currentPage = await GetDataAsync<PaginatedResponse<Bill>>(contactsFilter)).Context.HasMorePage)
            {
                allPages.AddRange(currentPage.Resource);
                contactsFilter.Page = currentPageNum++;
            }
            return allPages;
        }

        public async Task<IList<Bill>> GetPageAsync(int page, int pageSize = 100, IFilter filter = null)
        {
            var result = await GetDataAsync<PaginatedResponse<Bill>>(SetupFilter(filter, page));
            return result.Resource;
        }

        public async Task<IList<Bill>> GetPageRangeAsync(int start, int end, int pageSize = 100, IFilter filter = null)
        {
            var pageRange = new List<Bill>();
            var currentPage = new PaginatedResponse<Bill>();
            var currentPageNum = currentPage.Context.Page;
            var contactsFilter = SetupFilter(filter, currentPageNum) as BillsFilter;

            while ((currentPage = await GetDataAsync<PaginatedResponse<Bill>>(contactsFilter)).Context.Page <= end)
            {
                pageRange.AddRange(currentPage.Resource);
                contactsFilter.Page = currentPageNum++;
            }

            return pageRange;
        }

        /// <summary>
        /// Mark a bill status as void. 
        /// </summary>
        /// <param name="id">The id of the bill to mark.</param>
        /// <returns>A response indicating if the request was successful.</returns>
        public async Task<bool> MarkABillAsVoidAsync(string id)
        {
            var response = await PostAsync<ZohoBooksMessage>(string.Format("{0}/status/void", id), OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Mark a void bill as open. 
        /// </summary>
        /// <param name="id">The id of the bill to mark.</param>
        /// <returns>A response indicating if the request was successful.</returns>
        public async Task<bool> MarkABillAsOpenAsync(string id)
        {
            var response = await PostAsync<ZohoBooksMessage>(string.Format("{0}/status/open", id), OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Updates the billing address for this bill. 
        /// </summary>
        /// <param name="billId">The ID of the billing address to update.</param>
        /// <param name="address">The address data to update.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> UpdateBillingAddressAsync(string billId, Address address)
        {
            var response = await PutDataAsync<Address, ZohoBooksMessage>(string.Format("{0}/address/billing"), address, OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Get the list of payments made for a bill. 
        /// </summary>
        /// <param name="id">The id of the bill to list payments for.</param>
        /// <returns>A response with the requested bill payments.</returns>
        public async Task<Payment> ListPaymentsAsync(string id)
        {
            var response = await GetDataAsync<ZohoBooksResponse<Payment>>(string.Format("{0}/payments", id), OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Apply the vendor credits from excess vendor payments to a bill. Multiple credits can be applied at once. 
        /// </summary>
        /// <param name="id">The id of the bill to apply credits to.</param>
        /// <param name="creditBillRequest">The request with the values and ids of the payments and credits to apply.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> ApplyCreditsAsync(string id, CreditBillRequest creditBillRequest)
        {
            var response = await PostDataAsync<CreditBillRequest, ZohoBooksMessage>
                (string.Format("{0}/credits", id), creditBillRequest, OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Delete a payment made to a bill. 
        /// </summary>
        /// <param name="bilId">The id of the bill associated with the payment.</param>
        /// <param name="billPaymentId">The payment id to delete.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> DeleteAPaymentAsync(string bilId, string billPaymentId)
        {
            var response = await DeleteDataAsync<ZohoBooksMessage>
                (string.Format("{0}/payments/{1}", bilId, billPaymentId), OrganizationIdFilter);
            return response.Code == 0;
        }

        // TODO: Attachments, get bill attachment, add attachment

        /// <summary>
        /// Delete the file attached to a bill.  
        /// </summary>
        /// <param name="bilId">The id of the bill whose attachment should be deleted.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> DeleteABillAttachmentAsync(string bilId)
        {
            var response = await DeleteDataAsync<ZohoBooksMessage>
                (string.Format("{0}/attachment", bilId), OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Get all bill comments. 
        /// </summary>
        /// <param name="billId">The id of the bil to list comments for.</param>
        /// <returns>A list of comments.</returns>
        public async Task<IList<Comment>> ListBillCommentsAsync(string billId)
        {
            var response = await GetDataAsync<ZohoBooksResponse<IList<Comment>>>(string.Format("{0}/comments", billId), OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Add a comment for a bill. 
        /// </summary>
        /// <param name="billId">The bill to add a comment to.</param>
        /// <param name="description">The description of the line items.</param>
        /// <param name="showCommentToClients">Boolean to show the comments to contacts in portal.</param>
        /// <returns>A response indicating whether the response was successful.</returns>
        public async Task<bool> AddCommentToPurchaseOrderAsync(string billId, string description = "", bool showCommentToClients = false)
        {
            var requestArgs = new JObject();

            if (!string.IsNullOrEmpty(description))
            {
                requestArgs["description"] = description;
            }

            if (showCommentToClients)
            {
                requestArgs["show_comment_to_clients"] = showCommentToClients;
            }

            IZohoBooksMessage response;

            if (requestArgs.HasValues)
            {
                response = await PostDataAsync<JObject, ZohoBooksMessage>(string.Format("{0}/comments"), requestArgs, OrganizationIdFilter);
            }

            response = await PostAsync<ZohoBooksMessage>(string.Format("{0}/comments"), OrganizationIdFilter);

            return response.Code == 0;
        }

        /// <summary>
        /// Delete a bill comment. 
        /// </summary>
        /// <param name="billId">The bill id the comment is for.</param>
        /// <param name="commentId">The comment to delete.</param>
        /// <returns>A response indicating whether the response was successful.</returns>
        public async Task<bool> DeleteCommentAsync(string billId, string commentId)
        {
            var response = await DeleteDataAsync<ZohoBooksMessage>(string.Format("{0}/comments/{1}", billId, commentId));
            return response.Code == 0;
        }

        /// <summary>
        /// A method that sets up the filter with the default arguments.
        /// </summary>
        /// <param name="filter">A reference to the filter to change.</param>
        /// <param name="page">The page number to get.</param>
        /// <param name="pageSize">The size of the page to return.</param>
        /// <returns>The new ContactsFilter.</returns>
        protected override IPaginationFilter SetupFilter(IFilter filter, int page, int pageSize = 100)
        {
            var pageFilter = (filter == null) ? new BillsFilter() : (BillsFilter)filter;
            pageFilter.Page = page;
            pageFilter.PerPage = pageSize;
            pageFilter.OrganizationId = OrganizationIdFilter.OrganizationId;

            return pageFilter;
        }
    }
}
