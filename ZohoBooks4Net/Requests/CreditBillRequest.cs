﻿#region License
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
    public class BillPayment
    {
        [JsonProperty("payment_id")]
        public string PaymentId { get; set; }

        [JsonProperty("amount_applied")]
        public double AmountApplied { get; set; }
    }

    public class ApplyVendorCredit
    {
        [JsonProperty("vendor_credit_id")]
        public string VendorCreditId { get; set; }

        [JsonProperty("amount_applied")]
        public double AmountApplied { get; set; }
    }

    public class CreditBillRequest
    {
        [JsonProperty("bill_payments")]
        public IList<BillPayment> BillPayments { get; set; }

        [JsonProperty("apply_vendor_credits")]
        public IList<ApplyVendorCredit> ApplyVendorCredits { get; set; }
    }
}
