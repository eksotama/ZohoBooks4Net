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
    public class CreditNotes : ZohoBooksClientBase, IGetsPaginatedResponses<CreditNote>,
        ICreates<CreditNote>, IGets<CreditNote>, IUpdates<CreditNote>, IDeletes<CreditNote>
    {
        public CreditNotes(Configuration configuration) : base(configuration)
        {
            BaseUri = "creditnotes";
        }

        public async Task<IList<CreditNote>> GetPageRangeAsync(int start, int end, int pageSize = 100, IFilter filter = null)
        {
            var pageRange = new List<CreditNote>();
            var currentPage = new PaginatedResponse<CreditNote>();
            var currentPageNum = currentPage.Context.Page;
            var contactsFilter = SetupFilter(filter, currentPageNum) as CreditNotesFilter;

            while ((currentPage = await GetDataAsync<PaginatedResponse<CreditNote>>(contactsFilter)).Context.Page <= end)
            {
                pageRange.AddRange(currentPage.Resource);
                contactsFilter.Page = currentPageNum++;
            }

            return pageRange;
        }

        public async Task<IList<CreditNote>> GetPageAsync(int page, int pageSize = 100, IFilter filter = null)
        {
            var result = await GetDataAsync<PaginatedResponse<CreditNote>>(SetupFilter(filter, page));
            return result.Resource;
        }

        public async Task<IList<CreditNote>> GetAllPagesAsync(IFilter filter)
        {
            var allPages = new List<CreditNote>();
            var currentPage = new PaginatedResponse<CreditNote>();
            var currentPageNum = currentPage.Context.Page;
            var contactsFilter = SetupFilter(filter, currentPage.Context.Page) as CreditNotesFilter;

            while ((currentPage = await GetDataAsync<PaginatedResponse<CreditNote>>(contactsFilter)).Context.HasMorePage)
            {
                allPages.AddRange(currentPage.Resource);
                contactsFilter.Page = currentPageNum++;
            }
            return allPages;
        }

        /// <summary>
        /// Create a credit note
        /// </summary>
        /// <param name="newItem">Details of an existing creditnote.</param>
        /// <returns>A credit note after going through the Zoho database.</returns>
        public async Task<CreditNote> CreateAsync(CreditNote newItem)
        {
            var response = await PostDataAsync<CreditNote, ZohoBooksResponse<CreditNote>>(newItem, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Delete an existing credit note 
        /// </summary>
        /// <param name="id">Id of the credit note to delete.</param>
        /// <returns>A response indicating if the request was successful.</returns>
        public async Task<bool> DeleteAsync(string id)
        {
            var response = await DeleteDataAsync<ZohoBooksMessage>(id, OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Details of an existing creditnote. 
        /// </summary>
        /// <param name="id">The id of the credit note to get.</param>
        /// <returns>The requested credit note.</returns>
        public async Task<CreditNote> GetAsync(string id)
        {
            var response = await GetDataAsync<ZohoBooksResponse<CreditNote>>(id, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Update a credit note
        /// </summary>
        /// <param name="id">The id of the credit note to update.</param>
        /// <param name="item">The updated data of a credit note to send.</param>
        /// <returns>A response with the updated credit note.</returns>
        public async Task<CreditNote> UpdateAsync(string id, CreditNote item)
        {
            var response = await PutDataAsync<CreditNote, ZohoBooksResponse<CreditNote>>(id, item, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Void a credit note.
        /// </summary>
        /// <param name="id">The id of the creditnote to void.</param>
        /// <returns>A response indicating if the request was successful.</returns>
        public async Task<bool> VoidCreditNoteAsync(string id)
        {
            var response = await PostAsync<ZohoBooksMessage>(string.Format("{0}/void"), OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Open a voided credit note
        /// </summary>
        /// <param name="id">The id of the creditnote to open.</param>
        /// <returns>A response indicating if the request was successful.</returns>
        public async Task<bool> OpenVoidedCreditNoteAsync(string id)
        {
            var response = await PostAsync<ZohoBooksMessage>(string.Format("{0}/converttoopen"), OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Open a voided credit note
        /// </summary>
        /// <param name="id">The id of the creditnote to open.</param>
        /// <returns>A response indicating if the request was successful.</returns>
        public async Task<IList<MailHistory>> GetEmailHistoryAsync(string id)
        {
            var response = await PostAsync<ZohoBooksResponse<IList<MailHistory>>>(string.Format("{0}/converttoopen"), OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Get email content of a credit note. 
        /// </summary>
        /// <param name="id">The id of the credit note to get the email content for.</param>
        /// <param name="emailTemplateId">Get the email content based on a specific email template. If this param is not inputted, then 
        /// the content will be based on the email template associated with the customer. If no template is associated with the customer, 
        /// then default template will be used.</param>
        /// <returns>The email content.</returns>
        public async Task<EmailResponseContent> GetEmailContentAsync(string id, string emailTemplateId = null)
        {
            EmailResponse response;

            if (emailTemplateId == null)
            {
                response = await GetDataAsync<EmailResponse>(string.Format("{0}/email", id), OrganizationIdFilter);
            }
            else
            {
                response = await GetDataAsync<EmailResponse>
                    (string.Format("{0}/email?email_template_id={1}&organization_id={2}", id, emailTemplateId, OrganizationIdFilter.OrganizationId));
            }
            return response.Resource;
        }

        /// <summary>
        /// Updates the billing address for this invoice alone. 
        /// </summary>
        /// <param name="billingAddress">The new data for the billing address.</param>
        /// <returns>A </returns>
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
        /// Get all invoice pdf credit note. 
        /// </summary>
        /// <returns>A list of credit note templates.</returns>
        public async Task<IList<Template>> ListCreditnoteTemplatesAsync()
        {
            var response = await GetDataAsync<ZohoBooksResponse<IList<Template>>>("templates", OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Update the pdf template associated with the credit note. 
        /// </summary>
        /// <param name="invoiceId">The id of the credit note to update.</param>
        /// <param name="templateId">The id of the template to update.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> UpdateCreditnoteTemplateAsync(string invoiceId, string templateId, Template template)
        {
            var response = await PutDataAsync<Template, ZohoBooksMessage>(string.Format("{0}/templates/{1}", invoiceId, templateId), template, OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// List invoices to which the credit note is applied. 
        /// </summary>
        /// <param name="id">The id of the credit note to get credited invoices.</param>
        /// <returns>A list of credited invoices.</returns>
        public async Task<IList<InvoiceCredited>> GetInvoicesCreditedAsync(string id)
        {
            var response = await GetDataAsync<ZohoBooksResponse<IList<InvoiceCredited>>>(string.Format("{0}/invoices"));
            return response.Resource;
        }

        /// <summary>
        /// Apply credit note to existing invoices. 
        /// </summary>
        /// <param name="id">The id of the invoice to credit.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> CreditAnInvoiceAsync(string id, CreditAnInvoiceRequest request)
        {
            var response = await PostDataAsync<CreditAnInvoiceRequest, ZohoBooksMessage>
                (string.Format("{0}/invoices", id), request, OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Delete the credits applied to an invoice. 
        /// </summary>
        /// <param name="creditnoteId">The id of the credit not where the invoice is.</param>
        /// <param name="invoiceId">The id of the invoice to delete.</param>
        /// <returns>A response indicating if the request was successful.</returns>
        public async Task<bool> DeleteInvoicesAsync(string creditnoteId, string invoiceId)
        {
            var response = await DeleteDataAsync<ZohoBooksMessage>(string.Format("{0}/invoices/{1}", creditnoteId, invoiceId), OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Get history and comments of a credit note. 
        /// </summary>
        /// <param name="creditnoteId">The credit note to get commments for.</param>
        /// <returns>A list of comments for the requested sales order.</returns>
        public async Task<IList<Comment>> GetCommentsAsync(string creditnoteId)
        {
            return await GetDataAsync<IList<Comment>>(string.Format("{0}/comments", creditnoteId), OrganizationIdFilter);
        }

        /// <summary>
        /// Add a comment for a credit note. 
        /// </summary>
        /// <param name="creditnoteId">The credit note to add a comment to.</param>
        /// <param name="comment">The comment data to add. Only the description will be accepted.</param>
        /// <returns>A response with the contents of the comment.</returns>
        public async Task<Comment> AddCommentToCreditNoteAsync(string creditnoteId, Comment comment)
        {
            var response = await PostDataAsync<Comment, ZohoBooksResponse<Comment>>
                (string.Format("{0}/comments", creditnoteId), comment, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Update existing comment of a credit note. 
        /// </summary>
        /// <param name="creditnoteId">The credit note of the comment to update.</param>
        /// <param name="comment">The id of the comment to update.</param>
        /// <returns>The comment after it has been updated in the database.</returns>
        public async Task<Comment> UpdateCommentOnSalesOrderAsync(string creditnoteId, string commentId, Comment comment)
        {
            var response = await PostDataAsync<Comment, ZohoBooksResponse<Comment>>
                (string.Format("{0}/comments/{1}", creditnoteId, commentId), comment, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Update existing comment of a credit note. 
        /// </summary>
        /// <param name="creditnoteId">The credit note of the comment to delete.</param>
        /// <param name="comment">The ID of the comment to delete.</param>
        /// <returns>The comment after it has been updated in the database.</returns>
        public async Task<bool> DeleteCommentToSalesOrderAsync(string creditnoteId, string commentId, Comment comment)
        {
            var response = await DeleteDataAsync<ZohoBooksMessage>(string.Format("{0}/comments/{1}", creditnoteId, commentId), OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// List all refunds of an existing credit note. 
        /// </summary>
        /// <param name="creditnoteId">The credit note to list refunds for.</param>
        /// <param name="page">The page of credit note refunds to get.</param>
        /// <returns>The credit note's refunds.</returns>
        public async Task<IList<CreditNoteRefund>> ListCreditNoteRefundsAsync(string creditnoteId, int page = 1)
        {
            var response = await GetDataAsync<PaginatedResponse<CreditNoteRefund>>
                (string.Format("{0}/refunds?organization_id={1}&page={2}", OrganizationIdFilter.OrganizationId, creditnoteId));
            return response.Resource;
        }

        /// <summary>
        /// Get refund of a particular credit note. 
        /// </summary>
        /// <param name="creditnoteId">The credit note to get a refund for.</param>
        /// <returns>The credit note's refunds.</returns>
        public async Task<CreditNoteRefund> GetCreditNoteRefundAsync(string creditnoteId)
        {
            var response = await GetDataAsync<ZohoBooksResponse<CreditNoteRefund>> (string.Format("{0}/refunds", creditnoteId), OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Refund credit note amount. 
        /// </summary>
        /// <param name="creditnoteId">The id of the credit note to refund.</param>
        /// <param name="creditNoteRefund">The credit note data to create.</param>
        /// <returns>The credit note after it is refunded.</returns>
        public async Task<CreditNoteRefund> RefundCreditNoteAsync(string creditnoteId, CreditNoteRefund creditNoteRefund)
        {
            var response = await PostDataAsync<CreditNoteRefund, ZohoBooksResponse<CreditNoteRefund>>
                (string.Format("{0}/refunds", creditnoteId), creditNoteRefund, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Update the refunded transaction. 
        /// </summary>
        /// <param name="creditnoteId">The id of the credit note to refund.</param>
        /// <param name="creditNoteRefundId">The id of the refund to change.</param>
        /// <param name="creditnoteRefund">Credit note data to update with.</param>
        /// <returns>The credit note after it is refunded.</returns>
        public async Task<CreditNoteRefund> UpdateCreditNoteRefundAsync(string creditnoteId, string creditnoteRefundId, CreditNoteRefund creditNoteRefund)
        {
            var response = await PutDataAsync<CreditNoteRefund, ZohoBooksResponse<CreditNoteRefund>>
                (string.Format("{0}/refunds/{1}", creditnoteId, creditnoteRefundId), creditNoteRefund, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Update the refunded transaction. 
        /// </summary>
        /// <param name="creditnoteId">The id of the credit note to refund.</param>
        /// <param name="creditNoteRefundId">The id of the refund to change.</param>
        /// <returns>The credit note after it is refunded.</returns>
        public async Task<bool> DeleteCreditNoteRefundAsync(string creditnoteId, string creditnoteRefundId)
        {
            var response = await DeleteDataAsync<ZohoBooksMessage>
                (string.Format("{0}/refunds/{1}", creditnoteId, creditnoteRefundId), OrganizationIdFilter);
            return response.Code == 0;
        }
    }
}
