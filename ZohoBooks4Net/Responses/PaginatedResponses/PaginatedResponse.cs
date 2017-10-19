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
using System.Collections.Generic;
using ZohoBooks4Net.JsonConverters;
using ZohoBooks4Net.Domain.Entities;

namespace ZohoBooks4Net.Responses.PaginatedResponses
{
    [JsonConverter(typeof(DynamicPropertyNameConverter))]
    public class PaginatedResponse<T> : ZohoBooksResponse<IList<T>>, IPaginatedResponse<T>
    {
        [JsonProperty("page_context")]
        public PageContext Context { get; set; }

        /// <summary>
        /// This will be a dictionary of every string returned by the service mapped to its expected return type.
        /// </summary>
        [JsonPropertyNameByType("bills", typeof(IList<Bill>))]
        [JsonPropertyNameByType("claimants", typeof(IList<Claimant>))]
        [JsonPropertyNameByType("contacts", typeof(IList<Contact>))]
        [JsonPropertyNameByType("comments", typeof(IList<Comment>))]
        [JsonPropertyNameByType("contact_comments", typeof(IList<ContactComment>))]
        [JsonPropertyNameByType("contact_persons", typeof(IList<ContactPerson>))]
        [JsonPropertyNameByType("creditnotes", typeof(IList<CreditNote>))]
        [JsonPropertyNameByType("creditnote_refunds", typeof(IList<CreditNoteRefund>))]
        [JsonPropertyNameByType("custeomerpayments", typeof(IList<CustomerPayment>))]
        [JsonPropertyNameByType("estimates", typeof(IList<Estimate>))]
        [JsonPropertyNameByType("expenses", typeof(IList<Expense>))]
        [JsonPropertyNameByType("invoices", typeof(IList<Invoice>))]
        [JsonPropertyNameByType("items", typeof(IList<Item>))]
        [JsonPropertyNameByType("journals", typeof(IList<Journal>))]
        [JsonPropertyNameByType("purchaseorders", typeof(IList<PurchaseOrder>))]
        [JsonPropertyNameByType("salesorders", typeof(IList<PurchaseOrder>))]
        [JsonPropertyNameByType("vendorpayments", typeof(IList<VendorPayment>))]
        public override IList<T> Resource { get; set; }
    }

    public class PageContext
    {
        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("per_page")]
        public int PerPage { get; set; }

        [JsonProperty("has_more_page")]
        public bool HasMorePage { get; set; }
    }
}
