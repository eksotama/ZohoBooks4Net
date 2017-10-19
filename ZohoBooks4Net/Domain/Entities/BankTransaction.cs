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
    public class BankTransaction
    {
        [JsonProperty("transaction_id")]
        public string TransactionId { get; set; }

        [JsonProperty("from_account_id")]
        public string FromAccountId { get; set; }

        [JsonProperty("from_account_name")]
        public string FromAccountName { get; set; }

        [JsonProperty("to_account_id")]
        public string ToAccountId { get; set; }

        [JsonProperty("to_account_name")]
        public string ToAccountName { get; set; }

        [JsonProperty("transaction_type")]
        public string TransactionType { get; set; }

        [JsonProperty("currency_id")]
        public string CurrencyId { get; set; }

        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }

        [JsonProperty("payment_mode")]
        public string PaymentMode { get; set; }

        [JsonProperty("exchange_rate")]
        public double ExchangeRate { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("customer_id")]
        public string CustomerId { get; set; }

        [JsonProperty("vendor_id")]
        public string VendorId { get; set; }

        [JsonProperty("vendor_name")]
        public string VendorName { get; set; }

        [JsonProperty("reference_number")]
        public string ReferenceNumber { get; set; }

        [JsonProperty("bank_charges")]
        public int BankCharges { get; set; }

        [JsonProperty("documents")]
        public IList<string> Documents { get; set; }

        [JsonProperty("is_inclusive_tax")]
        public bool IsInclusiveTax { get; set; }

        [JsonProperty("tax_percentage")]
        public int TaxPercentage { get; set; }

        [JsonProperty("tax_amount")]
        public int TaxAmount { get; set; }

        [JsonProperty("sub_total")]
        public int SubTotal { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("bcy_total")]
        public int BcyTotal { get; set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("imported_transactions")]
        public IList<ImportedTransaction> ImportedTransactions { get; set; }

        [JsonProperty("tags")]
        public IList<string> Tags { get; set; }

        [JsonProperty("line_items")]
        public IList<LineItem> LineItems { get; set; }
    }

    public class ImportedTransaction
    {
        [JsonProperty("imported_transaction_id")]
        public string ImportedTransactionId { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("payee")]
        public string Payee { get; set; }

        [JsonProperty("reference_number")]
        public string ReferenceNumber { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("account_id")]
        public string AccountId { get; set; }
    }
}
