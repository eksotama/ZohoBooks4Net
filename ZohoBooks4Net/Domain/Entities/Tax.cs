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

namespace ZohoBooks4Net.Domain.Entities
{
    public class Tax
    {
        [JsonProperty("tax_name")]
        public string TaxName { get; set; }

        [JsonProperty("tax_percentage")]
        public double TaxPercentage { get; set; }

        [JsonProperty("tax_type")]
        public string TaxType { get; set; }

        [JsonProperty("tax_authority_name")]
        public string TaxAuthorityName { get; set; }

        [JsonProperty("tax_authority_id")]
        public string TaxAuthorityId { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("is_value_added")]
        public bool IsValueAdded { get; set; }

        [JsonProperty("update_recurring_invoice")]
        public bool UpdateRecurringInvoice { get; set; }

        [JsonProperty("update_recurring_expense")]
        public bool UpdateRecurringExpense { get; set; }

        [JsonProperty("update_draft_invoice")]
        public bool UpdateDraftInvoice { get; set; }

        [JsonProperty("update_recurring_bills")]
        public bool UpdateRecurringBills { get; set; }

        [JsonProperty("update_draft_so")]
        public bool UpdateDraftSo { get; set; }

        [JsonProperty("update_subscription")]
        public bool UpdateSubscription { get; set; }

        [JsonProperty("update_project")]
        public bool UpdateProject { get; set; }

        [JsonProperty("is_editable")]
        public bool IsEditable { get; set; }
    }
}
