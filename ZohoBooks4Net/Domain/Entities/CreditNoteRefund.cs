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
    public class CreditNoteRefund
    {
        [JsonProperty("creditnote_refund_id")]
        public long CreditnoteRefundId { get; set; }

        [JsonProperty("creditnote_id")]
        public long CreditnoteId { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("refund_mode")]
        public string RefundMode { get; set; }

        [JsonProperty("reference_number")]
        public int ReferenceNumber { get; set; }

        [JsonProperty("creditnote_number")]
        public string CreditnoteNumber { get; set; }

        [JsonProperty("customer_name")]
        public string CustomerName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("amount_bcy")]
        public double AmountBcy { get; set; }

        [JsonProperty("amount_fcy")]
        public double AmountFcy { get; set; }
    }
    
}
