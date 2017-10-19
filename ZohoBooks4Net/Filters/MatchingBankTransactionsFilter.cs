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

using System;
using System.Collections.Generic;
using System.Net.Http;

namespace ZohoBooks4Net.Filters
{
    public class MatchingBankTransactionsFilter : Filter
    {
        /// <summary>
        /// ID of the Transaction
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        /// Transaction Type of the transaction
        /// </summary>
        public string TransactionType { get; set; }

        /// <summary>
        /// Date after which Transactions are to be filtered
        /// </summary>
        public DateTime? DateAfter { get; set; }

        /// <summary>
        /// Date before which Transactions are to be filtered
        /// </summary>
        public DateTime? DateBefore { get; set; }

        /// <summary>
        /// Starting amout with which transactions are to be filtered
        /// </summary>
        public double? AmountStart { get; set; }

        /// <summary>
        /// Starting amout with which transactions are to be filtered
        /// </summary>
        public double? AmountEnd { get; set; }

        /// <summary>
        /// Contact person name, involved in the transaction.
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        /// Reference Number of the transaction
        /// </summary>
        public string ReferenceNumber { get; set; }

        /// <summary>
        /// Check if all transactions must be shown
        /// </summary>
        public bool? ShowAllTransactions { get; set; }

        public override void AddFilter(HttpRequestMessage message)
        {
            var filters = new Dictionary<string, string>();

            if (TransactionId != null)
            {
                filters.Add("transaction_id", TransactionId);
            }

            if (TransactionType != null)
            {
                filters.Add("transaction_type", TransactionType);
            }

            if (DateAfter != null)
            {
                filters.Add("date_after", DateAfter.Value.ToString());
            }

            if (DateBefore != null)
            {
                filters.Add("date_before", DateBefore.Value.ToString());
            }

            if (AmountStart != null)
            {
                filters.Add("amount_start", AmountStart.ToString());
            }

            if (AmountEnd != null)
            {
                filters.Add("amount_end", AmountEnd.ToString());
            }

            if (Contact != null)
            {
                filters.Add("contact", Contact);
            }

            if (ReferenceNumber != null)
            {
                filters.Add("reference_number", ReferenceNumber);
            }

            if (ShowAllTransactions != null)
            {
                filters.Add("show_all_transactions", ShowAllTransactions.Value.ToString().ToLower());
            }

            base.AddFilter(message, filters);
        }
    }
}
