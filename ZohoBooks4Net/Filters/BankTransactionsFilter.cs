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
using System.Net.Http;
using ZohoBooks4Net.Domain.Enumeration.BankTransactions;
using ZohoBooks4Net.Domain.Enumeration.Variants;

namespace ZohoBooks4Net.Filters
{
    public class BankTransactionsFilter : Filter
    {
        /// <summary>
        /// Mandatory Account id for which transactions are to be listed.
        /// </summary>
        public string AccountId { get; set; }

        /// <summary>
        /// Transaction Type of the transaction
        /// </summary>
        public string TransactionType { get; set; }

        /// <summary>
        /// Start and end date, to provide a range within which the transaction date exist. Variants: date_start and date_end
        /// </summary>
        public Tuple<DateVariant, DateTime> Date { get; set; }

        /// <summary>
        /// Start and end amount, to provide a range within which the transaction amount exist. Variants: amount_start and amount_end
        /// </summary>
        public Tuple<NumericalVariant, double> Amount { get; set; }

        /// <summary>
        /// Search using Reference Number of the transaction
        /// </summary>
        public string ReferenceNumber { get; set; }

        /// <summary>
        /// Search Transactions by contact name or description
        /// </summary>
        public string SearchText { get; set; }

        /// <summary>
        /// Transaction status wise list view - All, uncategorized, manually_added, matched, excluded, categorized
        /// </summary>
        public BankTransactionStatus? Status { get; set; }

        /// <summary>
        /// Transaction status wise list view - All, uncategorized, manually_added, matched, excluded, categorized
        /// </summary>
        public BankTransactionFilterBy? FilterBy { get; set; }

        public override void AddFilter(HttpRequestMessage message)
        {
            var filters = new Dictionary<string, string>();

            if (AccountId != null)
            {
                filters.Add("account_id", AccountId);
            }

            if (TransactionType != null)
            {
                filters.Add("transaction_type", TransactionType);
            }

            if (Date != null)
            {
                filters.Add("date" + DateVariantValue(Date.Item1), Date.Item2.ToString());
            }

            if (Amount != null)
            {
                filters.Add("amount" + JsonConvert.SerializeObject(Amount.Item1).Trim('\"'), Amount.Item2.ToString());
            }
            
            if (ReferenceNumber != null)
            {
                filters.Add("reference_number", ReferenceNumber);
            }
            
            if (SearchText != null)
            {
                filters.Add("search_text", SearchText);
            }
            
            if (Status != null)
            {
                filters.Add("status", JsonConvert.SerializeObject(Status.Value).Trim('\"'));
            }
            
            if (FilterBy != null)
            {
                filters.Add("filter_by", JsonConvert.SerializeObject(FilterBy.Value).Trim('\"'));
            }

            base.AddFilter(message, filters);
        }
    }
}
