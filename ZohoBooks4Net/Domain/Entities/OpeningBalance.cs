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

namespace ZohoBooks4Net.Domain.Entities
{
    public class OpeningBalance
    {
        [JsonProperty("opening_balance_id")]
        public string OpeningBalanceId { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("price_precision")]
        public int PricePrecision { get; set; }

        [JsonProperty("accounts")]
        public IList<Account> Accounts { get; set; }

        [JsonProperty("total")]
        public double Total { get; set; }
    }

    public class Account
    {
        [JsonProperty("acount_split_id")]
        public string AcountSplitId { get; set; }

        [JsonProperty("account_id")]
        public string AccountId { get; set; }

        [JsonProperty("account_name")]
        public string AccountName { get; set; }

        [JsonProperty("debit_or_credit")]
        public string DebitOrCredit { get; set; }

        [JsonProperty("exchange_rate")]
        public int ExchangeRate { get; set; }

        [JsonProperty("currency_id")]
        public string CurrencyId { get; set; }

        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }

        [JsonProperty("bcy_amount")]
        public int BcyAmount { get; set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }
    }
}
