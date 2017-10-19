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

namespace ZohoBooks4Net.Domain.Entities
{
    public class VendorPayment
    {
        [JsonProperty("payment_id")]
        public string PaymentId { get; set; }

        [JsonProperty("vendor_id")]
        public string VendorId { get; set; }

        [JsonProperty("vendor_name")]
        public string VendorName { get; set; }

        [JsonProperty("payment_mode")]
        public string PaymentMode { get; set; }

        [JsonProperty("payment_number")]
        public int PaymentNumber { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("reference_number")]
        public string ReferenceNumber { get; set; }

        [JsonProperty("exchange_rate")]
        public double ExchangeRate { get; set; }

        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("balance")]
        public double Balance { get; set; }

        [JsonProperty("currency_id")]
        public string CurrencyId { get; set; }

        [JsonProperty("currency_symbol")]
        public string CurrencySymbol { get; set; }

        [JsonProperty("created_time")]
        public DateTime CreatedTime { get; set; }

        [JsonProperty("paid_through_account_id")]
        public string PaidThroughAccountId { get; set; }

        [JsonProperty("paid_through_account_name")]
        public string PaidThroughAccountName { get; set; }

        [JsonProperty("paid_through_account_type")]
        public string PaidThroughAccountType { get; set; }

        [JsonProperty("is_paid_via_print_check")]
        public bool IsPaidViaPrintCheck { get; set; }

        [JsonProperty("is_ach_payment")]
        public bool IsAchPayment { get; set; }

        [JsonProperty("check_details")]
        public CheckDetails CheckDetails { get; set; }

        [JsonProperty("billing_address")]
        public IList<Address> BillingAddress { get; set; }

        [JsonProperty("vendorpayment_refunds")]
        public IList<VendorPaymentRefund> VendorpaymentRefunds { get; set; }

        [JsonProperty("bills")]
        public IList<Bill> Bills { get; set; }

        [JsonProperty("documents")]
        public IList<string> Documents { get; set; }

        [JsonProperty("custom_fields")]
        public IList<CustomField> CustomFields { get; set; }
    }
}
