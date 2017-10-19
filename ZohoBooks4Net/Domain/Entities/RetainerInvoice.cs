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

namespace ZohoBooks4Net.Domain.Entities
{
    public class RetainerInvoice
    {
        [JsonProperty("retainerinvoice_id")]
        public long RetainerinvoiceId { get; set; }

        [JsonProperty("customer_name")]
        public string CustomerName { get; set; }

        [JsonProperty("retainerinvoice_number")]
        public string RetainerinvoiceNumber { get; set; }

        [JsonProperty("customer_id")]
        public long CustomerId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("reference_number")]
        public string ReferenceNumber { get; set; }

        [JsonProperty("project_or_estimate_name")]
        public string ProjectOrEstimateName { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("currency_id")]
        public long CurrencyId { get; set; }

        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }

        [JsonProperty("is_viewed_by_client")]
        public bool IsViewedByClient { get; set; }

        [JsonProperty("client_viewed_time")]
        public bool ClientViewedTime { get; set; }

        [JsonProperty("total")]
        public double Total { get; set; }

        [JsonProperty("balance")]
        public double Balance { get; set; }

        [JsonProperty("created_time")]
        public DateTime CreatedTime { get; set; }

        [JsonProperty("last_modified_time")]
        public DateTime LastModifiedTime { get; set; }

        [JsonProperty("is_emailed")]
        public bool IsEmailed { get; set; }

        [JsonProperty("last_payment_date")]
        public string LastPaymentDate { get; set; }

        [JsonProperty("has_attachment")]
        public bool HasAttachment { get; set; }
    }
}
