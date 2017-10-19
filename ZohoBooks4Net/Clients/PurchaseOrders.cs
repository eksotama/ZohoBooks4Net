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
using System.Collections.Generic;
using System.Threading.Tasks;
using ZohoBooks4Net.Clients.Interfaces;
using ZohoBooks4Net.Domain.Entities;
using ZohoBooks4Net.Filters;
using ZohoBooks4Net.Requests;
using ZohoBooks4Net.Responses;
using ZohoBooks4Net.Responses.PaginatedResponses;

namespace ZohoBooks4Net.Clients
{
    public class PurchaseOrders : ZohoBooksClientBase, IGetsPaginatedResponses<PurchaseOrder>,
        ICreates<PurchaseOrder>, IGets<PurchaseOrder>, IUpdates<PurchaseOrder>, IDeletes<PurchaseOrder>
    {
        public PurchaseOrders(Configuration configuration) : base(configuration)
        {
            BaseUri = "purchaseorders";
        }

        // TODO: Query params
        /// <summary>
        /// Create a purchase order for your vendor. 
        /// </summary>
        /// <param name="newItem">The details of the puchase order</param>
        /// <returns>The purchase order data after it has gone through the Zoho system.</returns>
        public async Task<PurchaseOrder> CreateAsync(PurchaseOrder newItem)
        {
            var response = await PostDataAsync<PurchaseOrder, ZohoBooksResponse<PurchaseOrder>>(newItem, OrganizationIdFilter);
            return response.Resource;
        }

        // TODO: Query params
        /// <summary>
        /// Get the details of a purchase order. 
        /// </summary>
        /// <param name="id">The id of the purchase order to get.</param>
        /// <returns>The requested purchase order.</returns>
        public async Task<PurchaseOrder> GetAsync(string id)
        {
            var response = await GetDataAsync<ZohoBooksResponse<PurchaseOrder>>(id, OrganizationIdFilter);
            return response.Resource;
        }

        // TODO: Query params
        /// <summary>
        /// Update an existing purchase order. 
        /// </summary>
        /// <param name="id">The id of the purchase order to update.</param>
        /// <param name="item">The new purchase order data to update with.</param>
        /// <returns>The purchase order after it's been run through the Zoho system.</returns>
        public async Task<PurchaseOrder> UpdateAsync(string id, PurchaseOrder item)
        {
            var response = await PutDataAsync<PurchaseOrder, ZohoBooksResponse<PurchaseOrder>>(id, item, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Delete an existing purchase order. 
        /// </summary>
        /// <param name="id">The id of the purchase order to delete.</param>
        /// <returns>A response indicating if the request was successful.</returns>
        public async Task<bool> DeleteAsync(string id)
        {
            var response = await DeleteDataAsync<ZohoBooksMessage>(id, OrganizationIdFilter);
            return response.Code == 0;
        }

        public async Task<IList<PurchaseOrder>> GetAllPagesAsync(IFilter filter)
        {
            var allPages = new List<PurchaseOrder>();
            var currentPage = new PaginatedResponse<PurchaseOrder>();
            var currentPageNum = currentPage.Context.Page;
            var purchaseordersFilter = SetupFilter(filter, currentPage.Context.Page) as PurchaseOrdersFilter;

            while ((currentPage = await GetDataAsync<PaginatedResponse<PurchaseOrder>>(purchaseordersFilter)).Context.HasMorePage)
            {
                allPages.AddRange(currentPage.Resource);
                purchaseordersFilter.Page = currentPageNum++;
            }
            return allPages;
        }

        public async Task<IList<PurchaseOrder>> GetPageAsync(int page, int pageSize = 100, IFilter filter = null)
        {
            var result = await GetDataAsync<PaginatedResponse<PurchaseOrder>>(SetupFilter(filter, page));
            return result.Resource;
        }

        public async Task<IList<PurchaseOrder>> GetPageRangeAsync(int start, int end, int pageSize = 100, IFilter filter = null)
        {
            var pageRange = new List<PurchaseOrder>();
            var currentPage = new PaginatedResponse<PurchaseOrder>();
            var currentPageNum = currentPage.Context.Page;
            var purchaseordersFilter = SetupFilter(filter, currentPageNum) as PurchaseOrdersFilter;

            while ((currentPage = await GetDataAsync<PaginatedResponse<PurchaseOrder>>(purchaseordersFilter)).Context.Page <= end)
            {
                pageRange.AddRange(currentPage.Resource);
                purchaseordersFilter.Page = currentPageNum++;
            }

            return pageRange;
        }

        /// <summary>
        /// Mark a draft purchase order as open. 
        /// </summary>
        /// <param name="purchaseOrderId">The id of the purchase order to change status of.</param>
        /// <returns>A response indicating whether the request was succesful.</returns>
        public async Task<bool> MarkPurchaseOrderAsOpenAsync(string purchaseOrderId)
        {
            var response = await PostAsync<ZohoBooksMessage>(string.Format("{0}/status/open", purchaseOrderId), OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Mark a draft purchase order as billed. 
        /// </summary>
        /// <param name="purchaseOrderId">The id of the purchase order to change status of.</param>
        /// <returns>A response indicating whether the request was succesful.</returns>
        public async Task<bool> MarkPurchaseOrderAsBilledAsync(string purchaseOrderId)
        {
            var response = await PostAsync<ZohoBooksMessage>(string.Format("{0}/status/billed", purchaseOrderId), OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Mark a draft purchase order as cancelled. 
        /// </summary>
        /// <param name="purchaseOrderId">The id of the purchase order to change status of.</param>
        /// <returns>A response indicating whether the request was succesful.</returns>
        public async Task<bool> MarkPurchaseOrderAsCancelledAsync(string purchaseOrderId)
        {
            var response = await PostAsync<ZohoBooksMessage>(string.Format("{0}/status/cancelled", purchaseOrderId), OrganizationIdFilter);
            return response.Code == 0;
        }

        // TODO: Attachments
        /// <summary>
        /// Email a purchase order to the vendor. Input json string is not mandatory. If input json string is empty, mail will be send with default mail content. 
        /// </summary>
        /// <param name="purchaseOrderId">The id of the purchase order to email.</param>
        /// <param name="emailContent">The email content to send.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> EmailPurchaseOrderAsync(string purchaseOrderId, EmailContent emailContent = null)
        {
            var response = await PostDataAsync<EmailContent, ZohoBooksMessage>(string.Format("{0}/email"), emailContent, OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Get the email content of a purchase order. 
        /// </summary>
        /// <param name="purchaseOrderId">The id of the purchase order to ge the email content of.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<EmailContent> GetEmailContentAsync(string purchaseOrderId)
        {
            var response = await GetDataAsync<ZohoBooksResponse<EmailContent>>(string.Format("{0}/email"), OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Updates the billing address for this purchase order alone. 
        /// </summary>
        /// <param name="billingAddress">The new data for the billing address.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> UpdateBillingAddressAsync(string invoiceId, Address billingAddress)
        {
            var response = await PutDataAsync<Address, ZohoBooksMessage>(string.Format("{0}/address/billing", invoiceId), billingAddress, OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Get all purchase order comments. 
        /// </summary>
        /// <param name="purchaseOrderId">The id of the purchase order to list comments for.</param>
        /// <returns>A list of comments.</returns>
        public async Task<IList<Comment>> ListPurchaseOrderCommentsAsync(string purchaseOrderId)
        {
            var response = await GetDataAsync<ZohoBooksResponse<IList<Comment>>>(string.Format("{0}/comments", purchaseOrderId), OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Add a comment for an purchase order. 
        /// </summary>
        /// <param name="purchaseOrderId">The purchase order to add a comment to.</param>
        /// <param name="description">The description of the line items.</param>
        /// <param name="showCommentToClients">Boolean to show the comments to contacts in portal.</param>
        /// <returns>A response indicating whether the response was successful.</returns>
        public async Task<bool> AddCommentToPurchaseOrderAsync(string purchaseOrderId, string description = "", bool showCommentToClients = false)
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
        /// Add a comment for an estimate. 
        /// </summary>
        /// <param name="purchaseOrderId">The purchase order the Comment is on.</param>
        /// <param name="commentId">The id of the Comment to update.</param>
        /// <param name="description">The description of the line items.</param>
        /// <param name="showCommentToClients">Boolean to show the comments to contacts in portal.</param>
        /// <returns>A response indicating whether the response was successful.</returns>
        public async Task<bool> UpdateCommentAsync(string purchaseOrderId, string commentId, string description = "", bool? showCommentToClients = false)
        {
            var requestArgs = new JObject();

            if (!string.IsNullOrEmpty(description))
            {
                requestArgs["description"] = description;
            }

            if (showCommentToClients != null)
            {
                requestArgs["show_comment_to_clients"] = showCommentToClients;
            }

            IZohoBooksMessage response;

            if (requestArgs.HasValues)
            {
                response = await PutDataAsync<JObject, ZohoBooksMessage>(string.Format("{0}/comments"), requestArgs, OrganizationIdFilter);
            }

            response = await PutAsync<ZohoBooksMessage>(string.Format("{0}/comments"), OrganizationIdFilter);

            return response.Code == 0;
        }

        /// <summary>
        /// Get all purchase order pdf templates. 
        /// </summary>
        /// <returns>A list of invoice templates.</returns>
        public async Task<IList<Template>> ListPurchaseOrderTemplatesAsync()
        {
            var response = await GetDataAsync<ZohoBooksResponse<IList<Template>>>("templates", OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Update the pdf template associated with the purchase order. 
        /// </summary>
        /// <param name="purchaseOrderId">The id of the purchase order to update.</param>
        /// <param name="templateId">The id of the template to update.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> UpdateInvoiceTemplateAsync(string purchaseOrderId, string templateId, Template template)
        {
            var response = await PutDataAsync<Template, ZohoBooksMessage>(string.Format("{0}/templates/{1}", purchaseOrderId, templateId), template, OrganizationIdFilter);
            return response.Code == 0;
        }

        // TODO: Get purchase order attachment
        // TODO: Add attachment to purchase order

        /// <summary>
        /// Set whether you want to send the attached file while emailing the purchase order. 
        /// </summary>
        /// <param name="id">The id of the purchase order to update.</param>
        /// <param name="canSendInEmail">Whether or not to allow sending the attached file when emailing the invoice.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> UpdateAttachmentPreferenceAsync(string id, bool canSendInEmail)
        {
            var response = await PutAsync<ZohoBooksMessage>
                (string.Format("{0}/attachment?organization_id={1}&can_send_in_email={2}",
                id, OrganizationIdFilter.OrganizationId, canSendInEmail.ToString().ToLower()));
            return response.Code == 0;
        }

        /// <summary>
        /// Delete the file attached to the purchase order.  
        /// </summary>
        /// <param name="id">The id of the purchase order whose attachment is to be deleted.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> DeleteAttachmentAsync(string id)
        {
            var response = await DeleteDataAsync<ZohoBooksMessage>(string.Format("{0}/attachment"), OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Delete a purchase order comment. 
        /// </summary>
        /// <param name="purchaseOrderId">The purchase order the comment is for.</param>
        /// <param name="commentId">The comment to delete.</param>
        /// <returns>A response indicating whether the response was successful.</returns>
        public async Task<bool> DeleteCommentAsync(string purchaseOrderId, string commentId)
        {
            var response = await DeleteDataAsync<ZohoBooksMessage>(string.Format("{0}/comments/{1}", purchaseOrderId, commentId));
            return response.Code == 0;
        }

        protected override IPaginationFilter SetupFilter(IFilter filter, int page, int pageSize = 100)
        {
            var pageFilter = (filter == null) ? new PurchaseOrdersFilter() : (PurchaseOrdersFilter)filter;
            pageFilter.Page = page;
            pageFilter.PerPage = pageSize;
            pageFilter.OrganizationId = OrganizationIdFilter.OrganizationId;
            return pageFilter;
        }
    }
}
