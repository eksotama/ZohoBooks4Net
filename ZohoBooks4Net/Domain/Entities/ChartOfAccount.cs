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
    public class ChartOfAccount
    {
        [JsonProperty("account_id")]
        public string AccountId { get; set; }

        /// <summary>
        /// Name of the account
        /// </summary>
        [JsonProperty("account_name")]
        public string AccountName { get; set; }

        /// <summary>
        /// Type of the account. Allowed Values: other_asset, other_current_asset, cash, bank, fixed_asset, other_current_liability, 
        /// credit_card, long_term_liability, other_liability, equity, income, other_income, expense, cost_of_goods_sold, other_expense,
        /// accounts_receivable and accounts_payable.
        /// </summary>
        [JsonProperty("account_type")]
        public string AccountType { get; set; }

        /// <summary>
        /// ID of the account currency.
        /// </summary>
        [JsonProperty("currency_id")]
        public string CurrencyId { get; set; }

        /// <summary>
        /// ID of the Parent Account
        /// </summary>
        [JsonProperty("parent_account_id")]
        public string ParentAccountId { get; set; }

        [JsonProperty("is_user_created")]
        public bool IsUserCreated { get; set; }

        [JsonProperty("is_system_account")]
        public bool IsSystemAccount { get; set; }

        [JsonProperty("is_active")]
        public bool IsActive { get; set; }

        [JsonProperty("can_show_in_ze")]
        public bool CanShowInZe { get; set; }

        [JsonProperty("is_involved_in_transaction")]
        public bool IsInvolvedInTransaction { get; set; }

        [JsonProperty("has_attachment")]
        public bool HasAttachment { get; set; }

        [JsonProperty("documents")]
        public IList<string> Documents { get; set; }

        [JsonProperty("created_time")]
        public DateTime CreatedTime { get; set; }

        [JsonProperty("last_modified_time")]
        public DateTime LastModifiedTime { get; set; }
    }
}
