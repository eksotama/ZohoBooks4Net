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
    public class Journal
    {
        [JsonProperty("journal_id")]
        public string JournalId { get; set; }

        [JsonProperty("entry_number")]
        public string EntryNumber { get; set; }

        /// <summary>
        /// Reference number for the journal.
        /// </summary>
        [JsonProperty("reference_number")]
        public string ReferenceNumber { get; set; }

        /// <summary>
        /// Notes for the journal.
        /// </summary>
        [JsonProperty("notes")]
        public string Notes { get; set; }

        /// <summary>
        /// ID of the Currency Associated with the Journal
        /// </summary>
        [JsonProperty("currency_id")]
        public string CurrencyId { get; set; }

        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }

        [JsonProperty("currency_symbol")]
        public string CurrencySymbol { get; set; }

        /// <summary>
        /// Exchange Rate between the Currencies
        /// </summary>
        [JsonProperty("exchange_rate")]
        public int ExchangeRate { get; set; }

        /// <summary>
        /// Date on which the journal to be recorded.
        /// </summary>
        [JsonProperty("journal_date")]
        public string JournalDate { get; set; }

        /// <summary>
        /// Type of the Journal. Allowed values: Cash and Both .
        /// </summary>
        [JsonProperty("journal_type")]
        public string JournalType { get; set; }

        [JsonProperty("line_items")]
        public IList<LineItem> LineItems { get; set; }

        [JsonProperty("line_item_total")]
        public int LineItemTotal { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("bcy_total")]
        public int BcyTotal { get; set; }

        [JsonProperty("price_precision")]
        public int PricePrecision { get; set; }

        [JsonProperty("taxes")]
        public IList<Tax> Taxes { get; set; }

        [JsonProperty("created_time")]
        public DateTime CreatedTime { get; set; }

        [JsonProperty("last_modified_time")]
        public DateTime LastModifiedTime { get; set; }

        [JsonProperty("custom_fields")]
        public IList<CustomField> CustomFields { get; set; }
    }

}
