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
using System.Threading.Tasks;
using ZohoBooks4Net.Clients.Interfaces;
using ZohoBooks4Net.Domain.Entities;
using ZohoBooks4Net.Filters;
using ZohoBooks4Net.Requests;
using ZohoBooks4Net.Responses;
using ZohoBooks4Net.Responses.PaginatedResponses;

namespace ZohoBooks4Net.Clients
{
    public class Contacts : ZohoBooksClientBase, IGetsPaginatedResponses<Contact>,
        IGets<Contact>, ICreates<Contact>, IUpdates<Contact>, IDeletes<Contact>
    {
        public Contacts(Configuration configuration) : base(configuration)
        {
            BaseUri = "contacts";
        }

        /// <summary>
        /// Get details of a contact. 
        /// </summary>
        /// <param name="id">The id of the contact to retrieve.</param>
        /// <returns>The requested Contact.</returns>
        public async Task<Contact> GetAsync(string id)
        {
            var result = await GetDataAsync<ZohoBooksResponse<Contact>>(id, OrganizationIdFilter);
            return result.Resource;
        }
        
        public async Task<IList<Contact>> GetAllPagesAsync(IFilter filter)
        {
            var allPages = new List<Contact>();
            var currentPage = new PaginatedResponse<Contact>();
            var currentPageNum = currentPage.Context.Page;
            var contactsFilter = SetupFilter(filter, currentPage.Context.Page) as ContactsFilter;

            while ((currentPage = await GetDataAsync<PaginatedResponse<Contact>>(contactsFilter)).Context.HasMorePage)
            {
                allPages.AddRange(currentPage.Resource);
                contactsFilter.Page = currentPageNum++;
            }
            return allPages;
        }

        public async Task<IList<Contact>> GetPageAsync(int page, int pageSize = 100, IFilter filter = null)
        {
            var result = await GetDataAsync<PaginatedResponse<Contact>>(SetupFilter(filter, page));
            return result.Resource;
        }

        public async Task<IList<Contact>> GetPageRangeAsync(int start, int end, int pageSize = 100, IFilter filter = null)
        {
            var pageRange = new List<Contact>();
            var currentPage = new PaginatedResponse<Contact>();
            var currentPageNum = currentPage.Context.Page;
            var contactsFilter = SetupFilter(filter, currentPageNum) as ContactsFilter;

            while ((currentPage = await GetDataAsync<PaginatedResponse<Contact>>(contactsFilter)).Context.Page <= end)
            {
                pageRange.AddRange(currentPage.Resource);
                contactsFilter.Page = currentPageNum++;
            }

            return pageRange;
        }

        /// <summary>
        /// Create a contact with given information. 
        /// </summary>
        /// <param name="newItem">A contact to insert into the database.</param>
        /// <returns>The new contact.</returns>
        public async Task<Contact> CreateAsync(Contact newItem)
        {
            return await PostDataAsync(newItem, OrganizationIdFilter);
        }

        /// <summary>
        /// Update an existing contact. To delete a contact person remove it from the contact_persons list.
        /// </summary>
        /// <param name="id">The id of the contact to delete.</param>
        /// <param name="item">The new contact data.</param>
        /// <returns>The Contact with updated fields.</returns>
        public async Task<Contact> UpdateAsync(string id, Contact item)
        {
            return await PutDataAsync(id.ToString(), item, OrganizationIdFilter);
        }

        /// <summary>
        /// Delete an existing contact. 
        /// </summary>
        /// <param name="id">The id of the contact to delete</param>
        /// <returns>A response indicating if the request was successful.</returns>
        public async Task<bool> DeleteAsync(string id)
        {
            var response = await DeleteDataAsync<ZohoBooksMessage>(id.ToString(), OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Mark a contact as active. 
        /// </summary>
        /// <param name="id">The id of the contact to mark active.</param>
        /// <returns>A response indicating if the request was successful.</returns>
        public async Task<bool> MarkAsActiveAsync(string id)
        {
            var response = await PostAsync<ZohoBooksMessage>(string.Format("{0}/active", id), OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Mark a contact as inactive. 
        /// </summary>
        /// <param name="id">The id of the contact to mark inactive.</param>
        /// <returns>A response indicating if the request was successful.</returns>
        public async Task<bool> MarkAsInactiveAsync(string id)
        {
            var response = await PostAsync<ZohoBooksMessage>(string.Format("{0}/inactive", id), OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Enable portal access for a contact.
        /// </summary>
        /// <param name="id">The id of the contact to enable portal access for.</param>
        /// <param name="contactIds">A list of contact ids to add.</param>
        /// <returns>A response indicating if the request was successful.</returns>
        public async Task<bool> EnablePortalAccessAsync(string id, IList<int> contactIds)
        {
            var contactIdData = new JObject();
            var contactPersons = new JArray();

            foreach (var contactId in contactIds)
            {
                var contact = new JObject();
                contact["contact_person_id"] = contactId;

                contactPersons.Add(contact);
            }

            contactIdData["contact_persons"] = contactPersons;

            var response = await PostDataAsync<JObject, ZohoBooksMessage>
                (string.Format("{0}/portal/enable", id), contactIdData, OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Enable automated payment reminders for a contact.
        /// </summary>
        /// <param name="id">The id of the contact to enable payment reminders for.</param>
        /// <returns>A response indicating if the request was successful.</returns>
        public async Task<bool> EnablePaymentRemindersAsync(string id)
        {
            var response = await PostAsync<ZohoBooksMessage>(string.Format("{0}/paymentreminder/enable", id), OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Disable automated payment reminders for a contact.
        /// </summary>
        /// <param name="id">The id of the contact to disable payment reminders for.</param>
        /// <returns>A response indicating if the request was successful.</returns>
        public async Task<bool> DisablePaymentRemindersAsync(string id)
        {
            var response = await PostAsync<ZohoBooksMessage>(string.Format("{0}/paymentreminder/disable", id), OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Email statement to the contact. If JSONString is not inputted, mail will be sent with the default mail content.
        /// </summary>
        /// <param name="id">The contact to send the email to.</param>
        /// <returns>A response indicating if the request was successful.</returns>
        public async Task<bool> EmailStatementAsync(string id, EmailContent emailContent)
        {
            var response = await PostDataAsync<EmailContent, ZohoBooksMessage>(string.Format("{0}/statements/email", id), emailContent, OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Get the statement mail content.
        /// </summary>
        /// <param name="id">The id of the statement to get.</param>
        /// <returns>A response with the data from the statement.</returns>
        public async Task<EmailResponseContent> GetStatementMailContentAsync(string id, DateTime? startDate = null, DateTime? endDate = null)
        {
            var dateFilter = "";

            if (startDate != null)
            {
                dateFilter += "&start_date=" + startDate.Value.ToString();
            }
            if (endDate != null)
            {
                dateFilter += "&end_date=" + endDate.Value.ToString();
            }

            var response = await GetDataAsync<EmailResponse>
                (string.Format("{0}/statements/email?organization_id={1}{2}", id, OrganizationIdFilter.OrganizationId, dateFilter));
            return response.Resource;
        }

        /// <summary>
        /// Send email to contact.
        /// </summary>
        /// <param name="id">Contact id to send the mail to.</param>
        /// <param name="toMailIds">Array of email address of the recipients.</param>
        /// <param name="subject">Subject of an email has to be sent. Max-length [1000]</param>
        /// <param name="body">Body of an email has to be sent. Max-length [5000]</param>
        /// <returns>A response indicating if the request was successful.</returns>
        public async Task<bool> EmailContactAsync(string id, IList<string> toMailIds, string subject, string body)
        {
            var request = new JObject();
            request["to_mail_ids"] = new JArray(toMailIds);
            request["subject"] = subject;
            request["body"] = body;

            var response = await PostDataAsync<JObject, ZohoBooksMessage>(string.Format("{0}/email", id), request, OrganizationIdFilter);
            return response.Code == 0;
        }
        
        /// <summary>
        /// List recent activities of a contact.
        /// </summary>
        /// <param name="contactId">Contact id of the comments to get.</param>
        /// <returns>A list of contact comments.</returns>
        public async Task<IList<ContactComment>> ListCommentsAsync(string contactId)
        {
            var response = await GetDataAsync<PaginatedResponse<ContactComment>>
                (string.Format("{0}/comments", contactId), OrganizationIdFilter);
            return response.Resource;
        }
        
        /// <summary>
        /// List the refund history of a contact.
        /// </summary>
        /// <param name="contactId">Contact id of the refunds to get.</param>
        /// <returns>A list of contact refunds.</returns>
        public async Task<IList<CreditNoteRefund>> ListRefundsAsync(string contactId)
        {
            var response = await GetDataAsync<PaginatedResponse<CreditNoteRefund>>(string.Format("{0}/refunds", contactId), OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Track a contact for 1099 reporting: (Note: This API is only available when the organization's country is U.S.A).
        /// </summary>
        /// <param name="contactId">Contact id of the 1099 to track.</param>
        /// <returns>A response indicating if the request was successful.</returns>
        public async Task<bool> Track1099Async(string contactId)
        {
            var response = await PostAsync<ZohoBooksMessage>(string.Format("{0}/track1099", contactId), OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Use this API to stop tracking payments to a vendor for 1099 reporting. (Note: This API is only available when the organization's country is U.S.A).
        /// </summary>
        /// <param name="contactId">Contact id of the 1099 to stop tracking.</param>
        /// <returns>A response indicating if the request was successful.</returns>
        public async Task<bool> Untrack1099Async(string contactId)
        {
            var response = await PostAsync<ZohoBooksMessage>(string.Format("{0}/untrack1099", contactId), OrganizationIdFilter);
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
            var pageFilter = (filter == null) ? new ContactsFilter() : (ContactsFilter)filter;

            pageFilter.Page = page;
            pageFilter.PerPage = pageSize;
            pageFilter.OrganizationId = OrganizationIdFilter.OrganizationId;

            return pageFilter;
        }
    }
}
