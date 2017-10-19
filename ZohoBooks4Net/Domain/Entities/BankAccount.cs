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
    public class BankAccount
    {
        [JsonProperty("account_id")]
        public string AccountId { get; set; }

        [JsonProperty("account_name")]
        public string AccountName { get; set; }

        [JsonProperty("currency_id")]
        public string CurrencyId { get; set; }

        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }

        [JsonProperty("currency_symbol")]
        public string CurrencySymbol { get; set; }

        [JsonProperty("price_precision")]
        public int PricePrecision { get; set; }

        [JsonProperty("account_type")]
        public string AccountType { get; set; }

        [JsonProperty("account_number")]
        public string AccountNumber { get; set; }

        [JsonProperty("uncategorized_transactions")]
        public int UncategorizedTransactions { get; set; }

        [JsonProperty("total_unprinted_checks")]
        public int TotalUnprintedChecks { get; set; }

        [JsonProperty("is_active")]
        public bool IsActive { get; set; }

        [JsonProperty("is_feeds_subscribed")]
        public bool IsFeedsSubscribed { get; set; }

        [JsonProperty("is_feeds_active")]
        public bool IsFeedsActive { get; set; }

        [JsonProperty("balance")]
        public int Balance { get; set; }

        [JsonProperty("bank_balance")]
        public int BankBalance { get; set; }

        [JsonProperty("bcy_balance")]
        public int BcyBalance { get; set; }

        [JsonProperty("bank_name")]
        public string BankName { get; set; }

        [JsonProperty("routing_number")]
        public string RoutingNumber { get; set; }

        [JsonProperty("is_primary_account")]
        public bool IsPrimaryAccount { get; set; }

        [JsonProperty("is_paypal_account")]
        public bool IsPaypalAccount { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("is_system_account")]
        public bool IsSystemAccount { get; set; }

        [JsonProperty("is_show_warning_for_feeds_refresh")]
        public bool IsShowWarningForFeedsRefresh { get; set; }
    }
}
