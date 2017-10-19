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
    public class Invoices : ZohoBooksClientBase, IGetsPaginatedResponses<Invoice>,
        ICreates<Invoice>, IGets<Invoice>, IUpdates<Invoice>, IDeletes<Invoice>
    {
        public Invoices(Configuration configuration) : base(configuration)
        {
            BaseUri = "invoices";
        }

        /// <summary>
        /// Create an invoice for your customer. 
        /// </summary>
        /// <param name="newItem">The invoice data to insert.</param>
        /// <returns>A response with the created invoice.</returns>
        public async Task<Invoice> CreateAsync(Invoice newItem)
        {
            var response = await PostDataAsync<Invoice, ZohoBooksResponse<Invoice>>(newItem, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Get the details of an invoice. 
        /// </summary>
        /// <param name="id">The ID of the invoice to get.</param>
        /// <returns>The requested invoice.</returns>
        public async Task<Invoice> GetAsync(string id)
        {
            var response = await GetDataAsync<ZohoBooksResponse<Invoice>>("id", OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Update an existing invoice. To delete a line item just remove it from the line_items list. 
        /// </summary>
        /// <param name="id">The id of the invoice to update.</param>
        /// <param name="item">The invoice data to update with.</param>
        /// <returns>The invoice after being updated in the Zoho database.</returns>
        public async Task<Invoice> UpdateAsync(string id, Invoice item)
        {
            var response = await PutDataAsync<Invoice, ZohoBooksResponse<Invoice>>(item, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Delete an existing invoice. Invoices which have payment or credits note applied cannot be deleted. 
        /// </summary>
        /// <param name="id">The id of the invoice to delete.</param>
        /// <returns>A response indicating if the request was successful.</returns>
        public async Task<bool> DeleteAsync(string id)
        {
            var response = await DeleteDataAsync<ZohoBooksMessage>(id, OrganizationIdFilter);
            return response.Code == 0;
        }

        public async Task<IList<Invoice>> GetAllPagesAsync(IFilter filter)
        {
            var allPages = new List<Invoice>();
            var currentPage = new PaginatedResponse<Invoice>();
            var currentPageNum = currentPage.Context.Page;
            var invoicesFilter = SetupFilter(filter, currentPage.Context.Page);

            while ((currentPage = await GetDataAsync<PaginatedResponse<Invoice>>(invoicesFilter)).Context.HasMorePage)
            {
                allPages.AddRange(currentPage.Resource);
                invoicesFilter.Page = currentPageNum++;
            }
            return allPages;
        }

        public async Task<IList<Invoice>> GetPageAsync(int page, int pageSize = 100, IFilter filter = null)
        {
            var result = await GetDataAsync<PaginatedResponse<Invoice>>(SetupFilter(filter, page));
            return result.Resource;
        }

        public async Task<IList<Invoice>> GetPageRangeAsync(int start, int end, int pageSize = 100, IFilter filter = null)
        {
            var pageRange = new List<Invoice>();
            var currentPage = new PaginatedResponse<Invoice>();
            var currentPageNum = currentPage.Context.Page;
            var invoicesFilter = SetupFilter(filter, currentPageNum);

            while ((currentPage = await GetDataAsync<PaginatedResponse<Invoice>>(invoicesFilter)).Context.Page <= end)
            {
                pageRange.AddRange(currentPage.Resource);
                invoicesFilter.Page = currentPageNum++;
            }

            return pageRange;
        }

        /// <summary>
        /// Mark a draft invoice as sent.
        /// </summary>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> MarkInvoiceAsSentAsync(string id)
        {
            var response = await PostAsync<ZohoBooksMessage>(string.Format("{0}/status/sent", id), OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Mark an invoice status as void. Upon voiding, the payments and credits associated with the invoices will be unassociated and
        /// will be under customer credits. 
        /// </summary>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> VoidInvoiceAsync(string id)
        {
            var response = await PostAsync<ZohoBooksMessage>(string.Format("{0}/status/void", id), OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Mark a voided invoice as draft.  
        /// </summary>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> MarkInvoiceAsDraftAsync(string id)
        {
            var response = await PostAsync<ZohoBooksMessage>(string.Format("{0}/status/draft", id), OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Email an invoice to the customer. Input json string is not mandatory. If input json string is empty, mail will be send with default mail content. 
        /// </summary>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> EmailInvoiceAsync(string id, EmailContent email)
        {
            var response = await PostDataAsync<EmailContent, ZohoBooksMessage>(string.Format("{0}/email", id), email, OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Send invoices to your customers by email. Maximum of 10 invoices can be sent at once. 
        /// </summary>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> EmailInvoicesAsync(string contactId, IEnumerable<string> contacts = null, IEnumerable<string> invoiceIds = null)
        {
            var request = new JObject();
            request["contact_id"] = contactId;

            if (contacts != null)
            {
                request["contacts"] = new JArray(contacts);
            }
            if (invoiceIds != null)
            {
                request["invoice_ids"] = new JArray(invoiceIds);
            }

            var response = await PostDataAsync<JObject, ZohoBooksMessage>(string.Format("email"), request, OrganizationIdFilter);
            return response.Code == 0;
        }

        // TODO: Get invoice email content

        // TODO: File upload support
        /// <summary>
        /// Remind your customer about an unpaid invoice by email. Reminder will be sent, only for the invoices which are in open or 
        /// overdue status. 
        /// </summary>
        /// <param name="invoiceId">The invoice to send a reminder for.</param>
        /// <param name="email">The content of the email to send to the customer.</param>
        /// <returns>A response indicating whether the response was successful.</returns>
        public async Task<bool> RemindCustomerAsync(string invoiceId, EmailContent email)
        {
            var response = await PostDataAsync<EmailContent, ZohoBooksMessage>
                (string.Format("{0}/paymentreminder", invoiceId), email, OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Remind your customer about an unpaid invoices by email. Reminder mail will be send, only for the invoices is in open or 
        /// overdue status. Maximum 10 invoices can be reminded at once.  
        /// </summary>
        /// <param name="invoiceIds">The invoices to send a reminder for.</param>
        /// <returns>A response indicating whether the response was successful.</returns>
        public async Task<bool> BulkInvoiceReminderAsync(IEnumerable<string> invoiceIds)
        {
            var invoiceIdsParameter = string.Join(",", invoiceIds);

            var response = await PostAsync<ZohoBooksMessage>
                (string.Format("paymentreminder?organization_id={0}&invoice_ids={1}", 
                    OrganizationIdFilter.OrganizationId, invoiceIdsParameter));
            return response.Code == 0;
        }

        /// <summary>
        /// Get the mail content of the payment reminder. 
        /// </summary>
        /// <param name="id">The id of the payment reminder to get the mail content for.</param>
        /// <returns>The emamil response.</returns>
        public async Task<EmailResponseContent> GetPaymentReminderMailContentAsync(string id)
        {
            var response = await GetDataAsync<EmailResponse>(string.Format("{0}/paymentreminder", id));
            return response.Resource;
        }

        // TODO: Bulk Export Invoices
        // TODO: Bulk Print Invoices

        /// <summary>
        /// Disable automated payment reminders for an invoice. 
        /// </summary>
        /// <param name="id">Id of the invoice to disable payment reminders for.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> DisablePaymentReminderAsync(string id)
        {
            var response = await PostAsync<ZohoBooksMessage>(string.Format("{0}/paymentreminder/disable", id), OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Enable automated payment reminders for an invoice. 
        /// </summary>
        /// <param name="id">Id of the invoice to disable payment reminders for.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> EnablePaymentReminderAsync(string id)
        {
            var response = await PostAsync<ZohoBooksMessage>(string.Format("{0}/paymentreminder/enable", id), OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Write off the invoice balance amount of an invoice. 
        /// </summary>
        /// <param name="id">The id of the invoice to write off.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> WriteOffInvoiceAsync(string id)
        {
            var response = await PostAsync<ZohoBooksMessage>(string.Format("{0}/writeoff", id), OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Cancel the write off amount of an invoice.  
        /// </summary>
        /// <param name="id">The id of the invoice to cancel the write off.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> CancelWriteOffInvoiceAsync(string id)
        {
            var response = await PostAsync<ZohoBooksMessage>(string.Format("{0}/writeoff/cancel", id), OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Updates the billing address for this invoice alone. 
        /// </summary>
        /// <param name="billingAddress">The new data for the billing address.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> UpdateBillingAddressAsync(string invoiceId, Address billingAddress)
        {
            var response = await PutDataAsync<Address, ZohoBooksMessage>(string.Format("{0}/address/billing", invoiceId), billingAddress, OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Updates the shipping address for this invoice alone. 
        /// </summary>
        /// <param name="shippingAddress">The new data for the shipping address.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> UpdateShippingAddressAsync(string invoiceId, Address shippingAddress)
        {
            var response = await PutDataAsync<Address, ZohoBooksMessage>(string.Format("{0}/address/shipping", invoiceId), shippingAddress, OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Get all invoice pdf templates. 
        /// </summary>
        /// <returns>A list of invoice templates.</returns>
        public async Task<IList<Template>> ListInvoiceTemplatesAsync()
        {
            var response = await GetDataAsync<ZohoBooksResponse<IList<Template>>>("templates", OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Update the pdf template associated with the invoice. 
        /// </summary>
        /// <param name="invoiceId">The id of the invoice to update.</param>
        /// <param name="templateId">The id of the template to update.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> UpdateInvoiceTemplateAsync(string invoiceId, string templateId, Template template)
        {
            var response = await PutDataAsync<Template, ZohoBooksMessage>(string.Format("{0}/templates/{1}", invoiceId, templateId), template, OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Get the list of payments made for an invoice. 
        /// </summary>
        /// <param name="invoiceId">The id of the invoice to get payments for.</param>
        /// <returns>A list of payments.</returns>
        public async Task<IList<Payment>> ListInvoicePaymentsAsync(string invoiceId)
        {
            var response = await GetDataAsync<ZohoBooksResponse<IList<Payment>>>(string.Format("{0}/payments", invoiceId), OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Get the list of credits applied for an invoice. 
        /// </summary>
        /// <param name="id">The id of the invoice to get the applied credits for.</param>
        /// <returns>A list of credit notes.</returns>
        public async Task<IList<CreditNote>> ListCreditsAppliedAsync(string id)
        {
            var response = await GetDataAsync<ZohoBooksResponse<IList<CreditNote>>>(string.Format("{0}/creditsapplied"), OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Apply the customer credits either from credit notes or excess customer payments to an invoice. Multiple credits can be applied 
        /// at once. 
        /// </summary>
        /// <param name="id">The id of the invoice to apply credits to.</param>
        /// <param name="applyCreditsContent">The payments and creditnotes to apply the credits to.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> ApplyCreditsAsync(string id, ApplyCreditsContent applyCreditsContent)
        {
            var response = await PostDataAsync<ApplyCreditsContent, ZohoBooksMessage>(string.Format("{0}/credits", id), applyCreditsContent, OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Delete a payment made to an invoice. 
        /// </summary>
        /// <param name="id">The id of the invoice to delete the payment for.</param>
        /// <param name="paymentId">The id of the payment to delete.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> DeletePaymentAsync(string id, string paymentId)
        {
            var response = await DeleteDataAsync<ZohoBooksMessage>(string.Format("{0}/payments/{1}", id, paymentId), OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Delete a particular credit applied to an invoice. 
        /// </summary>
        /// <param name="id">The id of the invoice to delete the payment for.</param>
        /// <param name="creditNotesId">The id of the credit to delete.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> DeleteAppliedCreditAsync(string id, string creditNotesId)
        {
            var response = await DeleteDataAsync<ZohoBooksMessage>(string.Format("{0}/payments/{1}", id, creditNotesId), OrganizationIdFilter);
            return response.Code == 0;
        }

        // TODO: Get invoice attachment
        // TODO: Add attachment to invoice

        /// <summary>
        /// Set whether you want to send the attached file while emailing the invoice. 
        /// </summary>
        /// <param name="id">The id of the invoice to update.</param>
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
        /// Delete the file attached to the invoice.  
        /// </summary>
        /// <param name="id">The id of the invoice whose attachment is to be deleted.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> DeleteAttachmentAsync(string id)
        {
            var response = await DeleteDataAsync<ZohoBooksMessage>(string.Format("{0}/attachment"), OrganizationIdFilter);
            return response.Code == 0;
        }

        protected override IPaginationFilter SetupFilter(IFilter filter, int page, int pageSize = 100)
        {
            var pageFilter = (filter == null) ? new InvoicesFilter() : (InvoicesFilter)filter;
            pageFilter.Page = page;
            pageFilter.PerPage = pageSize;
            pageFilter.OrganizationId = OrganizationIdFilter.OrganizationId;
            return pageFilter;
        }
    }
}
