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
    public class RetainerInvoices : ZohoBooksClientBase, IGetsPaginatedResponses<RetainerInvoice>,
        ICreates<RetainerInvoice>, IGets<RetainerInvoice>, IUpdates<RetainerInvoice>, IDeletes<RetainerInvoice>
    {
        public RetainerInvoices(Configuration configuration) : base(configuration)
        {
            BaseUri = "retainerinvoices";
        }

        /// <summary>
        /// Create a retainer invoice for your customer. 
        /// </summary>
        /// <param name="newItem">Retainer invoice details.</param>
        /// <returns>A response with the reource after it's been processed through Zoho.</returns>
        public async Task<RetainerInvoice> CreateAsync(RetainerInvoice newItem)
        {
            var response = await PostDataAsync<RetainerInvoice, ZohoBooksResponse<RetainerInvoice>>(newItem, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Get the details of a retainer invoice. 
        /// </summary>
        /// <param name="id">The id of the retainer invoice to get.</param>
        /// <returns>The requested retainer invoice.</returns>
        public async Task<RetainerInvoice> GetAsync(string id)
        {
            var response = await GetDataAsync<ZohoBooksResponse<RetainerInvoice>>(id, OrganizationIdFilter);
            return response.Resource;
        }

        public async Task<IList<RetainerInvoice>> GetAllPagesAsync(IFilter filter)
        {
            var allPages = new List<RetainerInvoice>();
            var currentPage = new PaginatedResponse<RetainerInvoice>();
            var currentPageNum = currentPage.Context.Page;
            var retainerInvoicesFilter = SetupFilter(filter, currentPage.Context.Page) as RetainerInvoicesFilter;

            while ((currentPage = await GetDataAsync<PaginatedResponse<RetainerInvoice>>(retainerInvoicesFilter)).Context.HasMorePage)
            {
                allPages.AddRange(currentPage.Resource);
                retainerInvoicesFilter.Page = currentPageNum++;
            }
            return allPages;
        }

        public async Task<IList<RetainerInvoice>> GetPageAsync(int page, int pageSize = 100, IFilter filter = null)
        {
            var result = await GetDataAsync<PaginatedResponse<RetainerInvoice>>(SetupFilter(filter, page));
            return result.Resource;
        }

        public async Task<IList<RetainerInvoice>> GetPageRangeAsync(int start, int end, int pageSize = 100, IFilter filter = null)
        {
            var pageRange = new List<RetainerInvoice>();
            var currentPage = new PaginatedResponse<RetainerInvoice>();
            var currentPageNum = currentPage.Context.Page;
            var retainerInvoicesFilter = SetupFilter(filter, currentPageNum) as RetainerInvoicesFilter;

            while ((currentPage = await GetDataAsync<PaginatedResponse<RetainerInvoice>>(retainerInvoicesFilter)).Context.Page <= end)
            {
                pageRange.AddRange(currentPage.Resource);
                retainerInvoicesFilter.Page = currentPageNum++;
            }

            return pageRange;
        }

        /// <summary>
        /// Update the pdf template associated with the retainer invoice. 
        /// </summary>
        /// <param name="id">The id of the retainer invoice to update.</param>
        /// <param name="item">The retainer invoice details.</param>
        /// <returns>The updated retainer invoice details.</returns>
        public async Task<RetainerInvoice> UpdateAsync(string id, RetainerInvoice item)
        {
            var response = await PutDataAsync<RetainerInvoice, ZohoBooksResponse<RetainerInvoice>>(id, item, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Delete an existing retainer invoice. Invoices which have payment or credits note applied cannot be deleted. 
        /// </summary>
        /// <param name="id">The id of the retainer invoice to delete.</param>
        /// <returns>A response indicating if the request was successful.</returns>
        public async Task<bool> DeleteAsync(string id)
        {
            var response = await DeleteDataAsync<ZohoBooksMessage>(id, OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Mark a draft retainer invoice as sent. 
        /// </summary>
        /// <param name="id">The id of the retainer invoice to update.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> MarkARetainerInvoiceAsSentAsync(string id)
        {
            var response = await PostAsync<ZohoBooksMessage>(string.Format("{0}/status/sent", id), OrganizationIdFilter);
            return response.Code == 0;
        }

        // TODO: Update a retainer invoice template

        /// <summary>
        /// Mark an invoice status as void. Upon voiding, the payments and credits associated with the retainer invoices will be 
        /// unassociated and will be under customer credits.  
        /// </summary>
        /// <param name="id">The id of the retainer invoice to update.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> MarkARetainerInvoiceAsVoidAsync(string id)
        {
            var response = await PostAsync<ZohoBooksMessage>(string.Format("{0}/status/void", id), OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Mark a voided retainer invoice as draft.  
        /// </summary>
        /// <param name="id">The id of the retainer invoice to update.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> MarkARetainerInvoiceAsDraftAsync(string id)
        {
            var response = await PostAsync<ZohoBooksMessage>(string.Format("{0}/status/draft", id), OrganizationIdFilter);
            return response.Code == 0;
        }

        // TODO: Attachments
        /// <summary>
        /// Email a retainer invoice to the customer. Input json string is not mandatory. If input json string is empty, mail will be send 
        /// with default mail content. 
        /// </summary>
        /// <param name="id">The id of the retainer invoice requested.</param>
        /// <param name="emailContent">The content of the email to send.</param>
        /// <param name="sendCustomerStatement">Send customer statement pdf a with email.</param>
        /// <param name="sendAttachment">Send the retainer invoice attachment a with the email.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> EmailARetainerInvoiceAsync(string id, EmailContent emailContent, bool sendCustomerStatement = false, bool sendAttachment = false)
        {
            var response = await PostDataAsync<EmailContent, ZohoBooksMessage>(string.Format("{0}/email", id), emailContent, OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Get the email content of a retainer invoice. 
        /// </summary>
        /// <param name="retainerInvoiceId">The id of the retainer invoice to get.</param>
        /// <param name="emailTemplateId">Get the email content based on a specific email template. If this param is not inputted, then 
        /// the content will be based on the email template associated with the customer. If no template is associated with the customer, 
        /// then default template will be used.</param>
        /// <returns>The requested retainer invoice.</returns>
        public async Task<RetainerInvoiceEmailResponse> GetRetainerInvoiceEmailResponseAsync(string retainerInvoiceId, string emailTemplateId = "")
        {
            var uri = string.Format("{0}?organization_id={1}", retainerInvoiceId, OrganizationIdFilter.OrganizationId);

            if (string.IsNullOrEmpty(emailTemplateId))
            {
                uri = string.Format("{0}&email_template_id={1}", uri, emailTemplateId);
            }

            var response = await GetDataAsync<RetainerInvoiceEmailResponse>(uri);
            return response;
        }

        /// <summary>
        /// Updates the billing address for this retainer invoice alone. 
        /// </summary>
        /// <param name="billingAddress">The new data for the billing address.</param>
        /// <returns>A </returns>
        public async Task<bool> UpdateBillingAddressAsync(string invoiceId, Address billingAddress)
        {
            var response = await PutDataAsync<Address, ZohoBooksMessage>(string.Format("{0}/address/billing", invoiceId), billingAddress, OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Updates the shipping address for this retainer invoice alone. 
        /// </summary>
        /// <param name="shippingAddress">The new data for the shipping address.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> UpdateShippingAddressAsync(string invoiceId, Address shippingAddress)
        {
            var response = await PutDataAsync<Address, ZohoBooksMessage>(string.Format("{0}/address/shipping", invoiceId), shippingAddress, OrganizationIdFilter);
            return response.Code == 0;
        }
        
        /// <summary>
        /// Get all retainer invoice pdf templates. 
        /// </summary>
        /// <returns>A list of invoice templates.</returns>
        public async Task<IList<Template>> ListInvoiceTemplatesAsync()
        {
            var response = await GetDataAsync<ZohoBooksResponse<IList<Template>>>("templates", OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Get the list of payments made for a retainer invoices.  
        /// </summary>
        /// <returns>A list of invoice payments.</returns>
        public async Task<IList<Payment>> ListRetainerPaymentsAsync()
        {
            var response = await GetDataAsync<ZohoBooksResponse<IList<Payment>>>("payments", OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Delete a payment made to a retainer invoice. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteRetainerPaymentAsync(string id)
        {
            var response = await DeleteDataAsync<ZohoBooksMessage>(string.Format("customerpayments/{0}", id), OrganizationIdFilter);
            return response.Code == 0;
        }

        // TODO: Attachments

        /// <summary>
        /// Delete the file attached to the retainer invoice. 
        /// </summary>
        /// <param name="retainerInvoiceId">The id of the retainer invoice the document is in.</param>
        /// <param name="documentId">The attachment id to delete.</param>
        /// <returns>A response indicating if the request was successful.</returns>
        public async Task<bool> DeleteAttachmentAsync(string retainerInvoiceId, string documentId)
        {
            var response = await DeleteDataAsync<ZohoBooksMessage>(string.Format("{0}/documents/{1}", retainerInvoiceId, documentId), OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Get all retainer invoice pdf templates. 
        /// </summary>
        /// <param name="estimateId">The id of the retainer invoice to list templates for.</param>
        /// <returns>A list of email templates.</returns>
        public async Task<IList<RetainerInvoiceComment>> ListRetainerInvoiceCommentsAsync(string estimateId)
        {
            var response = await GetDataAsync<ZohoBooksResponse<IList<RetainerInvoiceComment>>>(string.Format("{0}/comments", estimateId), OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Add a comment for an retainer invoice. 
        /// </summary>
        /// <param name="estimateId">The retainer invoice to add a comment to.</param>
        /// <param name="description">The description of the line items.</param>
        /// <param name="showCommentToClients">Boolean to show the comments to contacts in portal.</param>
        /// <returns>A response indicating whether the response was successful.</returns>
        public async Task<bool> AddCommentToRetainerInvoiceAsync(string estimateId, string description = "", bool showCommentToClients = false)
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
        /// <param name="retainerInvoiceId">The Estimate the Comment is on.</param>
        /// <param name="commentId">The id of the Comment to update.</param>
        /// <param name="description">The description of the line items.</param>
        /// <param name="showCommentToClients">Boolean to show the comments to contacts in portal.</param>
        /// <returns>A response indicating whether the response was successful.</returns>
        public async Task<bool> UpdateCommentAsync(string retainerInvoiceId, string commentId, string description = "", bool? showCommentToClients = false)
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
        /// Delete a retainer invoice comment. 
        /// </summary>
        /// <param name="retainerInvoiceId">The retainer invoice the comment is for.</param>
        /// <param name="commentId">The comment to delete.</param>
        /// <returns>A response indicating whether the response was successful.</returns>
        public async Task<bool> DeleteCommentAsync(string retainerInvoiceId, string commentId)
        {
            var response = await DeleteDataAsync<ZohoBooksMessage>(string.Format("{0}/comments/{1}", retainerInvoiceId, commentId));
            return response.Code == 0;
        }

        protected override IPaginationFilter SetupFilter(IFilter filter, int page, int pageSize = 100)
        {
            var pageFilter = (filter == null) ? new RetainerInvoicesFilter() : (RetainerInvoicesFilter)filter;
            pageFilter.Page = page;
            pageFilter.PerPage = pageSize;
            pageFilter.OrganizationId = OrganizationIdFilter.OrganizationId;
            return pageFilter;
        }
    }
}
