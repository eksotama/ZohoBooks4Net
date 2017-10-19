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

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using ZohoBooks4Net.Domain.Enumeration.Contacts;
using ZohoBooks4Net.Domain.Enumeration.Variants;

namespace ZohoBooks4Net.Filters
{
    public class ContactsFilter : PaginationFilter
    {
        /// <summary>
        /// Search contacts by contact name. Max-length [100] Variants: contact_name_startswith and contact_name_contains. Max-length [100]
        /// </summary>
        public Tuple<SearchVariant, string> ContactName { get; set; }

        /// <summary>
        /// Search contacts by company name. Max-length [100] Variants: company_name_startswith and company_name_contains
        /// </summary>
        public Tuple<SearchVariant, string> CompanyName { get; set; }

        /// <summary>
        /// Search contacts by first name of the contact person. Max-length [100] Variants: first_name_startswith and first_name_contains
        /// </summary>
        public Tuple<SearchVariant, string> FirstName { get; set; }

        /// <summary>
        /// Search contacts by last name of the contact person. Max-length [100] Variants: last_name_startswith and last_name_contains
        /// </summary>
        public Tuple<SearchVariant, string> LastName { get; set; }

        /// <summary>
        /// Search contacts by any of the address fields. Max-length [100] Variants: address_startswith and address_contains
        /// </summary>
        public Tuple<SearchVariant, string> Address { get; set; }

        /// <summary>
        /// Search contacts by email of the contact person. Max-length [100] Variants: email_startswith and email_contains
        /// </summary>
        public Tuple<SearchVariant, string> Email { get; set; }

        /// <summary>
        /// Search contacts by phone number of the contact person. Max-length [100] Variants: phone_startswith and phone_contains
        /// </summary>
        public Tuple<SearchVariant, string> Phone { get; set; }

        /// <summary>
        /// Search contacts by contact name or notes. Max-length [100]
        /// </summary>
        public string SearchText { get; set; }

        /// <summary>
        /// Filter contacts by status. Allowed Values: Status.All, Status.Active, Status.Inactive, Status.Duplicate and Status.Crm
        /// </summary>
        public ContactsFilterBy? FilterBy { get; set; }

        /// <summary>
        /// Sort contacts. Allowed Values: contact_name, first_name, last_name, email, outstanding_receivable_amount, created_time and last_modified_time
        /// </summary>
        public ContactsSortBy? SortBy { get; set; }

        public override void AddFilter(HttpRequestMessage message)
        {
            var filters = new Dictionary<string, string>();

            if (ContactName != null)
            {
                filters.Add("contact_name" + SearchVariantValue(ContactName.Item1), ContactName.Item2);
            }

            if (CompanyName != null)
            {
                filters.Add("company_name" + SearchVariantValue(CompanyName.Item1), CompanyName.Item2);
            }

            if (FirstName != null)
            {
                filters.Add("first_name" + SearchVariantValue(FirstName.Item1), FirstName.Item2);
            }

            if (LastName != null)
            {
                filters.Add("last_name" + SearchVariantValue(LastName.Item1), LastName.Item2);
            }

            if (Address != null)
            {
                filters.Add("address" + SearchVariantValue(Address.Item1), LastName.Item2);
            }

            if (Email != null)
            {
                filters.Add("email" + SearchVariantValue(Email.Item1), Email.Item2);
            }

            if (Phone != null)
            {
                filters.Add("phone" + SearchVariantValue(Phone.Item1), Phone.Item2);
            }

            if (SearchText != null)
            {
                filters.Add("search_text", SearchText);
            }

            if (FilterBy != null)
            {
                filters.Add("filter_by", JsonConvert.SerializeObject(FilterBy.Value.ToString().Trim('\"')));
            }

            if (SortBy != null)
            {
                filters.Add("sort_by", JsonConvert.SerializeObject(SortBy.Value.ToString().Trim('\"')));
            }

            base.AddFilter(message, filters);
        }
    }
}
