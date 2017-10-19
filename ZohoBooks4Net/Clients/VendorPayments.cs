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
    public class VendorPayments : ZohoBooksClientBase, IGetsPaginatedResponses<VendorPayment>,
        ICreates<VendorPayment>, IGets<VendorPayment>, IUpdates<VendorPayment>, IDeletes<VendorPayment>
    {
        public VendorPayments(Configuration configuration) : base(configuration)
        {
            BaseUri = "vendorpayments";
        }

        /// <summary>
        /// Create a payment made to your vendor and you can also apply them to bills either partially or fully. 
        /// </summary>
        /// <param name="newItem">The vendor payment details data.</param>
        /// <returns>A response with the vendor payment details after running through the Zoho service.</returns>
        public async Task<VendorPayment> CreateAsync(VendorPayment newItem)
        {
            var response = await PostDataAsync<VendorPayment, ZohoBooksResponse<VendorPayment>>(newItem, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Get the details of a vendor payment. 
        /// </summary>
        /// <param name="id">The id of the vendor payment to get.</param>
        /// <returns>A response with the requested vendor payment.</returns>
        public async Task<VendorPayment> GetAsync(string id)
        {
            return await GetAsync(id);
        }
        
        /// <summary>
        /// Get the details of a vendor payment. 
        /// </summary>
        /// <param name="id">The id of the vendor payment to get.</param>
        /// <param name="fetchTaxInfo">Check if tax information should be fetched</param>
        /// <param name="fetchStatementLineInfo">Check if tax information should be fetched</param>
        /// <param name="isBillPaymentId">Check if the ID is Bill Payment or Vendor Payment</param>
        /// <returns>A response with the requested vendor payment.</returns>
        public async Task<VendorPayment> GetAsync(string id, bool fetchTaxInfo = false, bool fetchStatementLineInfo = false, bool isBillPaymentId = false)
        {
            var requestString = string.Format("{0}?organization_id={1}", id, OrganizationIdFilter.OrganizationId);

            if (fetchTaxInfo)
            {
                requestString += string.Format("&fetch_tax_info={0}", fetchTaxInfo.ToString().ToLower());
            }

            if (fetchStatementLineInfo)
            {
                requestString += string.Format("&fetch_statement_line_info={0}", fetchStatementLineInfo.ToString().ToLower());
            }

            if (isBillPaymentId)
            {
                requestString += string.Format("&is_bill_payment_id={0}", isBillPaymentId.ToString().ToLower());
            }

            var response = await GetDataAsync<ZohoBooksResponse<VendorPayment>>(id);
            return response.Resource;
        }

        /// <summary>
        /// Update an existing vendor payment. You can also modify the amount applied to the bills. 
        /// </summary>
        /// <param name="id">The id of the vendor payment to update.</param>
        /// <param name="item">The details of the vendor payment to update with.</param>
        /// <returns>A response with the updated resource after running through the Zoho service.</returns>
        public async Task<VendorPayment> UpdateAsync(string id, VendorPayment item)
        {
            var response = await PutDataAsync<VendorPayment, ZohoBooksResponse<VendorPayment>>(id, item, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Delete an existing vendor payment. 
        /// </summary>
        /// <param name="id">The id of the vendor payment to delete.</param>
        /// <returns>A response indicating if the request was successful.</returns>
        public async Task<bool> DeleteAsync(string id)
        {
            var response = await DeleteDataAsync<ZohoBooksMessage>(id, OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Obtain details of a particular refund of a vendor payment. 
        /// </summary>
        /// <param name="vendorPaymentId">The id of the vendor payment that has the refund.</param>
        /// <param name="vendorPaymentRefundId">The id of the vendor payment refund to get.</param>
        /// <returns>The requested vendor payment refund.</returns>
        public async Task<VendorPaymentRefund> GetVendorPaymentRefundAsync(string vendorPaymentId, string vendorPaymentRefundId)
        {
            var response = await GetDataAsync<ZohoBooksResponse<VendorPaymentRefund>>
                (string.Format("{0}/refunds/{1}", vendorPaymentId, vendorPaymentRefundId), OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// List all the refunds pertaining to an existing vendor payment. 
        /// </summary>
        /// <param name="id">The id of the vendor payment to get refunds for.</param>
        /// <returns>A response with the requested vendor payment refunds.</returns>
        public async Task<IList<VendorPaymentRefund>> GetVendorPaymentRefundsAsync(string id)
        {
            var response = await GetDataAsync<ZohoBooksResponse<IList<VendorPaymentRefund>>>(id, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Refund the excess amount paid to the vendor. 
        /// </summary>
        /// <param name="id">The id of the vendor payment to refund.</param>
        /// <param name="item">The vendor payment refund data to process.</param>
        /// <returns>A response with the refund data.</returns>
        public async Task<VendorPaymentRefund> RefundAnExcessVendorPaymentAsync(string id, VendorPaymentRefund item)
        {
            var response = await PostDataAsync<VendorPaymentRefund, ZohoBooksResponse<VendorPaymentRefund>>
                (string.Format("{0}/refunds", id), item, OrganizationIdFilter);
            return response.Resource;
        }

        public async Task<IList<VendorPayment>> GetPageRangeAsync(int start, int end, int pageSize = 100, IFilter filter = null)
        {
            var pageRange = new List<VendorPayment>();
            var currentPage = new PaginatedResponse<VendorPayment>();
            var currentPageNum = currentPage.Context.Page;
            var itemsFilter = SetupFilter(filter, currentPageNum) as VendorPaymentsFilter;

            while ((currentPage = await GetDataAsync<PaginatedResponse<VendorPayment>>(itemsFilter)).Context.Page <= end)
            {
                pageRange.AddRange(currentPage.Resource);
                itemsFilter.Page = currentPageNum++;
            }

            return pageRange;
        }

        public async Task<IList<VendorPayment>> GetPageAsync(int page, int pageSize = 100, IFilter filter = null)
        {
            var result = await GetDataAsync<PaginatedResponse<VendorPayment>>(SetupFilter(filter, page));
            return result.Resource;
        }

        public async Task<IList<VendorPayment>> GetAllPagesAsync(IFilter filter)
        {
            var allPages = new List<VendorPayment>();
            var currentPage = new PaginatedResponse<VendorPayment>();
            var currentPageNum = currentPage.Context.Page;
            var contactsFilter = SetupFilter(filter, currentPage.Context.Page) as VendorPaymentsFilter;

            while ((currentPage = await GetDataAsync<PaginatedResponse<VendorPayment>>(contactsFilter)).Context.HasMorePage)
            {
                allPages.AddRange(currentPage.Resource);
                contactsFilter.Page = currentPageNum++;
            }
            return allPages;
        }

        protected override IPaginationFilter SetupFilter(IFilter filter, int page, int pageSize = 100)
        {
            var pageFilter = (filter == null) ? new VendorPaymentsFilter() : (VendorPaymentsFilter)filter;
            pageFilter.Page = page;
            pageFilter.PerPage = pageSize;
            pageFilter.OrganizationId = OrganizationIdFilter.OrganizationId;
            return pageFilter;
        }
    }
}
