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
    public class CustomerPayments : ZohoBooksClientBase,
        ICreates<CustomerPayment>, IGets<CustomerPayment>, IUpdates<CustomerPayment>, IDeletes<CustomerPayment>,
        IListsItemsWithFilters<CustomerPayment>
    {
        public CustomerPayments(Configuration configuration) : base(configuration)
        {
            BaseUri = "customerpayments";
        }

        /// <summary>
        /// Create a new payment. 
        /// </summary>
        /// <param name="newItem">Payment details.</param>
        /// <returns>A response with the resource after it's run through the Zoho server.</returns>
        public async Task<CustomerPayment> CreateAsync(CustomerPayment newItem)
        {
            var response = await PostDataAsync<CustomerPayment, ZohoBooksResponse<CustomerPayment>>(newItem, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Delete an existing payment. 
        /// </summary>
        /// <param name="id">The id of the payment to delete.</param>
        /// <returns>A response indicating if the request was successful.</returns>
        public async Task<bool> DeleteAsync(string id)
        {
            var response = await DeleteDataAsync<ZohoBooksMessage>(id, OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Details of an existing payment. 
        /// </summary>
        /// <param name="id">The id of the payment to get.</param>
        /// <returns>The requested payment.</returns>
        public async Task<CustomerPayment> GetAsync(string id)
        {
            var response = await GetDataAsync<ZohoBooksResponse<CustomerPayment>>(id, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// List all the payments made by your customer 
        /// </summary>
        /// <param name="filter">Query parameters.</param>
        /// <returns>A response with payments made by your customer.</returns>
        public async Task<IList<CustomerPayment>> GetItemsAsync(IFilter filter)
        {
            SetupFilter(filter, 0);
            var response = await GetDataAsync<PaginatedResponse<CustomerPayment>>("", filter);
            return response.Resource;
        }
        
        /// <summary>
        /// Update an existing payment information. 
        /// </summary>
        /// <param name="id">The id of the customer payment to update.</param>
        /// <param name="item">The data to update the customer payment with.</param>
        /// <returns>A response with the resource after it's run through the Zoho server.</returns>
        public async Task<CustomerPayment> UpdateAsync(string id, CustomerPayment item)
        {
            var response = await PutDataAsync<CustomerPayment, ZohoBooksResponse<CustomerPayment>>(id, item, OrganizationIdFilter);
            return response.Resource;
        }
        
        /// <summary>
        /// List all the refunds made by your customer 
        /// </summary>
        /// <param name="filter">Query parameters.</param>
        /// <returns>A response with refunds made by your customer.</returns>
        public async Task<IList<CustomerPayment>> GetRefundsAsync(string id, IFilter filter)
        {
            SetupFilter(filter, 0);
            var response = await GetDataAsync<PaginatedResponse<CustomerPayment>>(string.Format("{0}/refunds", id), filter);
            return response.Resource;
        }

        /// <summary>
        /// Obtain details of a particular refund of a customer payment. 
        /// </summary>
        /// <param name="customerPaymentId">The customer payment the refund belongs to.</param>
        /// <param name="refundId">The refund id to get.</param>
        /// <returns>A response with refund made by your customer.</returns>
        public async Task<CustomerPayment> GetRefundAsync(string customerPaymentId, string refundId)
        {
            var response = await GetDataAsync<ZohoBooksResponse<CustomerPayment>>
                (string.Format("{0}/refunds/{1}", customerPaymentId, refundId), OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Refund an excess customer payment.
        /// </summary>
        /// <param name="paymentRefund">Payment refund details.</param>
        /// <returns>A response with the resource after it's run through the Zoho server.</returns>
        public async Task<IList<PaymentRefund>> RefundExcessPaymentAsync(PaymentRefundRequest refundRequest)
        {
            var response = await PostDataAsync<PaymentRefundRequest, ZohoBooksResponse<IList<PaymentRefund>>>
                (refundRequest, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Update an existing refund. 
        /// </summary>
        /// <param name="customerPaymentId">The id of the customer payment.</param>
        /// <param name="refundId">The id of the refund to update</param>
        /// <param name="item">The data to update the refund with.</param>
        /// <returns>A response with the resource after it's run through the Zoho server.</returns>
        public async Task<PaymentRefund> UpdateRefundAsync(string customerPaymentId, string refundId, PaymentRefund item)
        {
            var response = await PutDataAsync<PaymentRefund, ZohoBooksResponse<PaymentRefund>>
                (string.Format("{0}/refunds/{1}", customerPaymentId, refundId), item, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Delete an existing refund. 
        /// </summary>
        /// <param name="customerPaymentId">The id of the customer payment.</param>
        /// <param name="refundId">The id of the refund to delete</param>
        /// <returns>A response with the resource after it's run through the Zoho server.</returns>
        public async Task<bool> DeleteRefundAsync(string customerPaymentId, string refundId)
        {
            var response = await DeleteDataAsync<ZohoBooksMessage>
                (string.Format("{0}/refunds/{1}", customerPaymentId, refundId), OrganizationIdFilter);
            return response.Code == 0;
        }

        protected override IPaginationFilter SetupFilter(IFilter filter, int page, int pageSize = 100)
        {
            var pageFilter = (filter == null) ? new CustomerPaymentsFilter() : (CustomerPaymentsFilter)filter;
            pageFilter.Page = page;
            pageFilter.PerPage = pageSize;
            pageFilter.OrganizationId = OrganizationIdFilter.OrganizationId;
            return pageFilter;
        }
    }
}
