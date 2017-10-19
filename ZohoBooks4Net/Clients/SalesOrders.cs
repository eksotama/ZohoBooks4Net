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
using ZohoBooks4Net.Requests;
using ZohoBooks4Net.Responses;
using ZohoBooks4Net.Responses.PaginatedResponses;

namespace ZohoBooks4Net.Clients
{
    public class SalesOrders : ZohoBooksClientBase, IGetsPaginatedResponses<SalesOrder>,
        ICreates<SalesOrder>, IGets<SalesOrder>, IUpdates<SalesOrder>, IDeletes<SalesOrder>
    {
        public SalesOrders(Configuration configuration) : base(configuration)
        {
            BaseUri = "salesorders";
        }

        /// <summary>
        /// Create a sales order for your customer. 
        /// </summary>
        /// <param name="newItem">The sales order to be created</param>
        /// <returns>A sales order after being passed through the Zoho database.</returns>
        public async Task<SalesOrder> CreateAsync(SalesOrder newItem)
        {
            var response = await PostDataAsync<SalesOrder, ZohoBooksResponse<SalesOrder>>(newItem, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Delete an existing sales order. Invoiced sales order cannot be deleted. 
        /// </summary>
        /// <param name="id">The ID of the sales order to be deleted.</param>
        /// <returns>A response indicating whether the request was successful</returns>
        public async Task<bool> DeleteAsync(string id)
        {
            var response = await DeleteDataAsync<ZohoBooksMessage>(id, OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Get the details of a sales order. 
        /// </summary>
        /// <param name="id">The ID of the sales order to get.</param>
        /// <returns>The requested sales order.</returns>
        public async Task<SalesOrder> GetAsync(string id)
        {
            var response = await GetDataAsync<ZohoBooksResponse<SalesOrder>>(id, OrganizationIdFilter);
            return response.Resource;
        }

        public async Task<IList<SalesOrder>> GetAllPagesAsync(IFilter filter)
        {
            var allPages = new List<SalesOrder>();
            var currentPage = new PaginatedResponse<SalesOrder>();
            var currentPageNum = currentPage.Context.Page;
            var salesOrdersFilter = SetupFilter(filter, currentPage.Context.Page) as SalesOrdersFilter;

            while ((currentPage = await GetDataAsync<PaginatedResponse<SalesOrder>>(salesOrdersFilter)).Context.HasMorePage)
            {
                allPages.AddRange(currentPage.Resource);
                salesOrdersFilter.Page = currentPageNum++;
            }
            return allPages;
        }

        public async Task<IList<SalesOrder>> GetPageAsync(int page, int pageSize = 100, IFilter filter = null)
        {
            var result = await GetDataAsync<PaginatedResponse<SalesOrder>>(SetupFilter(filter, page));
            return result.Resource;
        }

        public async Task<IList<SalesOrder>> GetPageRangeAsync(int start, int end, int pageSize = 100, IFilter filter = null)
        {
            var pageRange = new List<SalesOrder>();
            var currentPage = new PaginatedResponse<SalesOrder>();
            var currentPageNum = currentPage.Context.Page;
            var salesOrdersFilter = SetupFilter(filter, currentPageNum) as SalesOrdersFilter;

            while ((currentPage = await GetDataAsync<PaginatedResponse<SalesOrder>>(salesOrdersFilter)).Context.Page <= end)
            {
                pageRange.AddRange(currentPage.Resource);
                salesOrdersFilter.Page = currentPageNum++;
            }

            return pageRange;
        }

        /// <summary>
        /// Update an existing sales order. To delete a line item just remove it from the line_items list.
        /// </summary>
        /// <param name="id">The ID of the sales order to update.</param>
        /// <param name="item">The new data to place in the sales order.</param>
        /// <returns>A response with the updated salse order.</returns>
        public async Task<SalesOrder> UpdateAsync(string id, SalesOrder item)
        {
            var response = await PutDataAsync<SalesOrder, ZohoBooksResponse<SalesOrder>>(item, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Mark a draft sales order as open. 
        /// </summary>
        /// <param name="id">The id of the sales order to mark as open.</param>
        /// <returns>A response indicating whether or not </returns>
        public async Task<bool> MarkSalesOrderAsOpenAsync(string id)
        {
            var response = await PostAsync<ZohoBooksMessage>(string.Format("{0}/status/open"), OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Mark a sales order as void. 
        /// </summary>
        /// <param name="id">The id of the sales order to mark as void.</param>
        /// <returns>A response indicating whether or not </returns>
        public async Task<bool> MarkSalesOrderAsVoidAsync(string id)
        {
            var response = await PostAsync<ZohoBooksMessage>(string.Format("{0}/status/void"), OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Email a sales order to the customer. Input json string is not mandatory. If input json string is empty, mail will be send with
        /// default mail content. 
        /// </summary>
        /// <param name="emailContent">The contents of the email to send.</param>
        /// <returns>A response indicating whether the reponse was succesful.</returns>
        public async Task<bool> EmailSalesOrderAsync(SalesOrderEmailContent emailContent)
        {
            var response = await PostDataAsync<SalesOrderEmailContent, ZohoBooksMessage>(emailContent, OrganizationIdFilter);
            return response.Code == 0;
        }

        // TODO: Add the functionality to download attachment files from requests.
        /// <summary>
        /// Get the email content of a sales order. 
        /// </summary>
        /// <param name="salesOrderId">The ID of the sales order to get.</param>
        /// <returns>The requeted email content in PDF format.</returns>
        public async Task<EmailResponse> GetSalesOrderEmailContentAsync(string salesOrderId, string emailTemplateId = null)
        {
            if (!string.IsNullOrEmpty(emailTemplateId))
            {
                return await GetDataAsync<EmailResponse>
                    (string.Format("{0}/email?organization_id={1}&email_template_id={2}",
                        salesOrderId, OrganizationIdFilter.OrganizationId, emailTemplateId));
            }

            return await GetDataAsync<EmailResponse>(string.Format("{0}/email", salesOrderId), OrganizationIdFilter);
        }

        // TODO: Add the functionality to download attachment files from requests.
        /// <summary>
        /// Maximum of 25 sales orders can be exported in a single pdf. 
        /// </summary>
        /// <param name="salesOrderIds">Array of sales order IDs which are to be export as pdf.</param>
        /// <returns>A pdf export of the sales orders.</returns>
        public async Task<bool> BulkExportSalesOrdersAsync(IList<string> salesOrderIds)
        {
            if (salesOrderIds.Count > 25)
            {
                for (int i = 25; i < salesOrderIds.Count; i++)
                {
                    salesOrderIds.RemoveAt(i);
                }
            }
            var salesOrdersParams = string.Join(",", salesOrderIds);

            var response = await GetDataAsync<ZohoBooksMessage>
                (string.Format("pdf?organization_id={0}&salesorder_ids={1}", OrganizationIdFilter.OrganizationId, salesOrdersParams));
            return response.Code == 0;
        }

        /// <summary>
        /// Proxy method for easily exporting a single sales order.
        /// </summary>
        /// <param name="salesOrderId">The sales order ID to get.</param>
        /// <returns>A pdf export of the sales order.</returns>
        public async Task<bool> BulkExportSalesOrdersAsync(string salesOrderId)
        {
            return await BulkExportSalesOrdersAsync(new List<string> { salesOrderId });
        }

        /// <summary>
        /// Export sales orders as pdf and print them. Maximum of 25 sales orders can be printed. 
        /// </summary>
        /// <param name="salesOrderIds">The sales order IDs to get.</param>
        /// <returns>A pdf export of the sales order.</returns>
        public async Task<bool> BulkPrintSalesOrdersAsync(IList<string> salesOrderIds)
        {
            if (salesOrderIds.Count > 25)
            {
                for (int i = 25; i < salesOrderIds.Count; i++)
                {
                    salesOrderIds.RemoveAt(i);
                }
            }
            var salesOrdersParams = string.Join(",", salesOrderIds);

            var response = await GetDataAsync<ZohoBooksMessage>(string.Format("print?salesorder_ids={0}", salesOrdersParams));
            return response.Code == 0;
        }

        /// <summary>
        /// Proxy method for easily printing a single sales order.
        /// </summary>
        /// <param name="salesOrderId">The sales order ID to get.</param>
        /// <returns>A pdf export of the sales order.</returns>
        public async Task<bool> BulkPrintSalesOrdersAsync(string salesOrderId)
        {
            return await BulkPrintSalesOrdersAsync(new List<string> { salesOrderId });
        }

        /// <summary>
        /// Updates the billing address for this sales order alone. 
        /// </summary>
        /// <param name="salesOrderId">The sales order whose billing address to update.</param>
        /// <returns>A response indicating whether the response was successful.</returns>
        public async Task<bool> UpdateBillingAddressAsync(string salesOrderId, Address billingAddress)
        {
            var response = await PutDataAsync<Address, ZohoBooksMessage>
                (string.Format("{0}/address/billing", salesOrderId), billingAddress, OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Updates the shipping address for this sales order alone. 
        /// </summary>
        /// <param name="salesOrderId">The sales order whose shipping address to update.</param>
        /// <returns>A response indicating whether the response was successful.</returns>
        public async Task<bool> UpdateShippingAddressAsync(string salesOrderId, Address billingAddress)
        {
            var response = await PutDataAsync<Address, ZohoBooksMessage>
                (string.Format("{0}/address/shipping", salesOrderId), billingAddress, OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Get all sales order pdf templates. 
        /// </summary>
        /// <returns>A list of sales order templates.</returns>
        public async Task<IList<Template>> ListSalesOrderTemplatesAsync()
        {
            var response = await GetDataAsync<ZohoBooksResponse<IList<Template>>>("templates", OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Update the pdf template associated with the sales order. 
        /// </summary>
        /// <param name="salesOrderId">The sales order ID to get.</param>
        /// <param name="templateId">The template ID within the sales order to get.</param>
        /// <param name="template">The new template fields.</param>
        /// <returns>A response indicating if the response was successful.</returns>
        public async Task<bool> UpdateSalesOrderTemplateAsync(string salesOrderId, string templateId, Template template)
        {
            var response = await PutDataAsync<Template, ZohoBooksMessage>
                (string.Format("{0}/templates/{1}", salesOrderId, templateId), template, OrganizationIdFilter);
            return response.Code == 0;
        }

        // TODO: File support & 3 more dealing with attachments
        /// <summary>
        /// Returns the file attached to the sales order. 
        /// </summary>
        /// <param name="salesOrderId">The sales order ID to get the attachment for.</param>
        /// <returns>A response indicating if the response was successful.</returns>
        public async Task<bool> GetSalesOrderAttachmentAsync(string salesOrderId)
        {
            var response = await GetDataAsync<ZohoBooksMessage>(string.Format("{0}/attachment", salesOrderId), OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Get the complete history and comments of sales order. 
        /// </summary>
        /// <param name="salesOrderId">The sales order to get commments for.</param>
        /// <returns>A list of comments for the requested sales order.</returns>
        public async Task<IList<Comment>> GetCommentsAsync(string salesOrderId)
        {
            return await GetDataAsync<IList<Comment>>(string.Format("{0}/comments", salesOrderId), OrganizationIdFilter);
        }

        /// <summary>
        /// Add a comment for a sales order. 
        /// </summary>
        /// <param name="salesOrderId">The sales order to add a comment to.</param>
        /// <param name="comment">The comment data to add. Only the description will be accepted.</param>
        /// <returns>A response with the contents of the comment.</returns>
        public async Task<Comment> AddCommentToSalesOrderAsync(string salesOrderId, Comment comment)
        {
            var response = await PostDataAsync<Comment, ZohoBooksResponse<Comment>>
                (string.Format("{0}/comments", salesOrderId), comment, OrganizationIdFilter);
            return response.Resource;
        }
        
        /// <summary>
        /// Update existing comment of a sales order. 
        /// </summary>
        /// <param name="salesOrderId">The sales order of the comment to update.</param>
        /// <param name="comment">The ID of the comment to update.</param>
        /// <returns>The comment after it has been updated in the database.</returns>
        public async Task<Comment> UpdateCommentOnSalesOrderAsync(string salesOrderId, string commentId, Comment comment)
        {
            var response = await PostDataAsync<Comment, ZohoBooksResponse<Comment>>
                (string.Format("{0}/comments/{1}", salesOrderId, commentId), comment, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Update existing comment of a sales order. 
        /// </summary>
        /// <param name="salesOrderId">The sales order of the comment to delete.</param>
        /// <param name="comment">The ID of the comment to delete.</param>
        /// <returns>The comment after it has been updated in the database.</returns>
        public async Task<bool> DeleteCommentToSalesOrderAsync(string salesOrderId, string commentId, Comment comment)
        {
            var response = await DeleteDataAsync<ZohoBooksMessage>(string.Format("{0}/comments/{1}", salesOrderId, commentId), OrganizationIdFilter);
            return response.Code == 0;
        }

        protected override IPaginationFilter SetupFilter(IFilter filter, int page, int pageSize = 100)
        {
            var pageFilter = (filter == null) ? new SalesOrdersFilter() : (SalesOrdersFilter)filter;
            pageFilter.Page = page;
            pageFilter.PerPage = pageSize;
            pageFilter.OrganizationId = OrganizationIdFilter.OrganizationId;
            return pageFilter;
        }
    }
}
