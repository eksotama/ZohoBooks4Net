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
using ZohoBooks4Net.JsonConverters;
using ZohoBooks4Net.Domain.Entities;
using System.Collections.Generic;

namespace ZohoBooks4Net.Responses
{
    [JsonConverter(typeof(DynamicPropertyNameConverter))]
    public class ZohoBooksResponse<T> : ZohoBooksMessage
    {
        /// <summary>
        /// Comprises the invoked API’s Data.
        /// </summary>
        [JsonPropertyNameByType("bankaccount", typeof(BankAccount))]
        [JsonPropertyNameByType("bankaccounts", typeof(IList<BankAccount>))]
        [JsonPropertyNameByType("banktransaction", typeof(BankTransaction))]
        [JsonPropertyNameByType("banktransactions", typeof(IList<BankTransaction>))]
        [JsonPropertyNameByType("bill", typeof(Bill))]
        [JsonPropertyNameByType("claimant", typeof(Claimant))]
        [JsonPropertyNameByType("comments", typeof(IList<Comment>))]
        [JsonPropertyNameByType("contact", typeof(Contact))]
        [JsonPropertyNameByType("contact_person", typeof(ContactPerson))]
        [JsonPropertyNameByType("creditnote", typeof(CreditNote))]
        [JsonPropertyNameByType("creditnote_refund", typeof(CreditNoteRefund))]
        [JsonPropertyNameByType("email_history", typeof(IList<MailHistory>))]
        [JsonPropertyNameByType("estimate", typeof(Estimate))]
        [JsonPropertyNameByType("expense", typeof(Expense))]
        [JsonPropertyNameByType("invoice", typeof(Invoice))]
        [JsonPropertyNameByType("invoices_credited", typeof(IList<InvoiceCredited>))]
        [JsonPropertyNameByType("item", typeof(IList<Item>))]
        [JsonPropertyNameByType("journal", typeof(IList<Journal>))]
        [JsonPropertyNameByType("matching_transactions", typeof(IList<BankTransaction>))]
        [JsonPropertyNameByType("opening_balance", typeof(OpeningBalance))]
        [JsonPropertyNameByType("payment", typeof(Payment))]
        [JsonPropertyNameByType("payments", typeof(IList<Payment>))]
        [JsonPropertyNameByType("users", typeof(IList<User>))]
        [JsonPropertyNameByType("payment_refunds", typeof(Payment))]
        [JsonPropertyNameByType("purchaseorder", typeof(PurchaseOrder))]
        [JsonPropertyNameByType("salesorder", typeof(PurchaseOrder))]
        [JsonPropertyNameByType("tax", typeof(Tax))]
        [JsonPropertyNameByType("taxauthority", typeof(TaxAuthority))]
        [JsonPropertyNameByType("taxauthorities", typeof(IList<TaxAuthority>))]
        [JsonPropertyNameByType("taxexemption", typeof(TaxExemption))]
        [JsonPropertyNameByType("taxexemptions", typeof(IList<TaxExemption>))]
        [JsonPropertyNameByType("taxgroup", typeof(TaxGroup))]
        [JsonPropertyNameByType("taxgroups", typeof(IList<TaxGroup>))]
        [JsonPropertyNameByType("template", typeof(Template))]
        [JsonPropertyNameByType("templates", typeof(IList<Template>))]
        [JsonPropertyNameByType("vendorpayment", typeof(VendorPayment))]
        [JsonPropertyNameByType("vendorpayment_refund", typeof(VendorPaymentRefund))]
        [JsonPropertyNameByType("vendorpayment_refunds", typeof(IList<VendorPaymentRefund>))]
        public virtual T Resource { get; set; }
    }
}
