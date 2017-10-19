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
    public class Payment
    {
        [JsonProperty("payment_id")]
        public string PaymentId { get; set; }

        [JsonProperty("documents")]
        public IList<string> Documents { get; set; }

        [JsonProperty("customer_id")]
        public long CustomerId { get; set; }

        [JsonProperty("customer_name")]
        public string CustomerName { get; set; }

        [JsonProperty("retainerinvoice_id")]
        public long RetainerinvoiceId { get; set; }

        [JsonProperty("payment_mode")]
        public string PaymentMode { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("currency_id")]
        public long CurrencyId { get; set; }

        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }

        [JsonProperty("exchange_rate")]
        public int ExchangeRate { get; set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("unused_amount")]
        public int UnusedAmount { get; set; }

        [JsonProperty("bank_charges")]
        public int BankCharges { get; set; }

        [JsonProperty("is_client_review_settings_enabled")]
        public bool IsClientReviewSettingsEnabled { get; set; }

        [JsonProperty("tax_amount_withheld")]
        public int TaxAmountWithheld { get; set; }

        [JsonProperty("discount_amount")]
        public int DiscountAmount { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("reference_number")]
        public string ReferenceNumber { get; set; }

        [JsonProperty("online_transaction_id")]
        public string OnlineTransactionId { get; set; }

        [JsonProperty("invoices")]
        public IList<Invoice> Invoices { get; set; }

        [JsonProperty("retainerinvoice")]
        public RetainerInvoice RetainerInvoice { get; set; }

        [JsonProperty("payment_refunds")]
        public IList<PaymentRefund> PaymentRefunds { get; set; }

        [JsonProperty("last_four_digits")]
        public string LastFourDigits { get; set; }

        [JsonProperty("html_string")]
        public string HtmlString { get; set; }

        [JsonProperty("template_id")]
        public long TemplateId { get; set; }

        [JsonProperty("template_name")]
        public string TemplateName { get; set; }

        [JsonProperty("page_width")]
        public string PageWidth { get; set; }

        [JsonProperty("page_height")]
        public string PageHeight { get; set; }

        [JsonProperty("orientation")]
        public string Orientation { get; set; }

        [JsonProperty("template_type")]
        public string TemplateType { get; set; }

        [JsonProperty("attachment_name")]
        public string AttachmentName { get; set; }

        [JsonProperty("can_send_in_mail")]
        public bool CanSendInMail { get; set; }

        [JsonProperty("is_payment_drawn_details_required")]
        public bool IsPaymentDrawnDetailsRequired { get; set; }

        [JsonProperty("is_single_bill_payment")]
        public bool IsSingleBillPayment { get; set; }

        [JsonProperty("custom_fields")]
        public IList<CustomField> CustomFields { get; set; }
    }
}
