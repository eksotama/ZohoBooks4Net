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

namespace ZohoBooks4Net.Requests
{
    public class ApplyCreditsContent
    {
        [JsonProperty("invoice_payments")]
        public IList<InvoicePayment> InvoicePayments { get; set; }

        [JsonProperty("apply_creditnotes")]
        public IList<ApplyCreditnote> ApplyCreditnotes { get; set; }

        [JsonProperty("payment_id")]
        public long PaymentId { get; set; }

        [JsonProperty("amount_applied")]
        public double AmountApplied { get; set; }

        [JsonProperty("creditnote_id")]
        public long CreditnoteId { get; set; }
    }

    public class InvoicePayment
    {
        [JsonProperty("payment_id")]
        public long PaymentId { get; set; }

        [JsonProperty("amount_applied")]
        public double AmountApplied { get; set; }
    }

    public class ApplyCreditnote
    {
        [JsonProperty("creditnote_id")]
        public long CreditnoteId { get; set; }

        [JsonProperty("amount_applied")]
        public double AmountApplied { get; set; }
    }

}
