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
    public class CreditNote
    {
        [JsonProperty("creditnote_id")]
        public string CreditnoteId { get; set; }

        [JsonProperty("creditnote_number")]
        public string CreditnoteNumber { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("reference_number")]
        public string ReferenceNumber { get; set; }

        [JsonProperty("customer_id")]
        public string CustomerId { get; set; }

        [JsonProperty("is_taxable")]
        public bool IsTaxable { get; set; }

        [JsonProperty("contact_category")]
        public string ContactCategory { get; set; }

        [JsonProperty("gst_treatment")]
        public string GstTreatment { get; set; }

        [JsonProperty("tax_authority_id")]
        public string TaxAuthorityId { get; set; }

        [JsonProperty("tax_exemption_id")]
        public string TaxExemptionId { get; set; }

        [JsonProperty("tax_authority_name")]
        public string TaxAuthorityName { get; set; }

        [JsonProperty("tax_exemption_code")]
        public string TaxExemptionCode { get; set; }

        [JsonProperty("customer_name")]
        public string CustomerName { get; set; }

        [JsonProperty("contact_persons")]
        public IList<string> ContactPersons { get; set; }

        [JsonProperty("currency_id")]
        public string CurrencyId { get; set; }

        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }

        [JsonProperty("currency_symbol")]
        public string CurrencySymbol { get; set; }

        [JsonProperty("exchange_rate")]
        public int ExchangeRate { get; set; }

        [JsonProperty("price_precision")]
        public int PricePrecision { get; set; }

        [JsonProperty("template_id")]
        public string TemplateId { get; set; }

        [JsonProperty("template_name")]
        public string TemplateName { get; set; }

        [JsonProperty("template_type")]
        public string TemplateType { get; set; }

        [JsonProperty("page_width")]
        public string PageWidth { get; set; }

        [JsonProperty("page_height")]
        public string PageHeight { get; set; }

        [JsonProperty("orientation")]
        public string Orientation { get; set; }

        [JsonProperty("is_emailed")]
        public bool IsEmailed { get; set; }

        [JsonProperty("is_inclusive_tax")]
        public bool IsInclusiveTax { get; set; }

        [JsonProperty("discount")]
        public int Discount { get; set; }

        [JsonProperty("discount_applied_on_amount")]
        public int DiscountAppliedOnAmount { get; set; }

        [JsonProperty("is_discount_before_tax")]
        public bool IsDiscountBeforeTax { get; set; }

        [JsonProperty("discount_type")]
        public string DiscountType { get; set; }

        [JsonProperty("invoice_id")]
        public string InvoiceId { get; set; }

        [JsonProperty("invoice_number")]
        public string InvoiceNumber { get; set; }

        [JsonProperty("line_items")]
        public IList<LineItem> LineItems { get; set; }

        [JsonProperty("sub_total")]
        public double SubTotal { get; set; }

        [JsonProperty("tax_total")]
        public int TaxTotal { get; set; }

        [JsonProperty("shipping_charge")]
        public int ShippingCharge { get; set; }

        [JsonProperty("adjustment")]
        public int Adjustment { get; set; }

        [JsonProperty("adjustment_description")]
        public string AdjustmentDescription { get; set; }

        [JsonProperty("roundoff_value")]
        public int RoundoffValue { get; set; }

        [JsonProperty("transaction_rounding_type")]
        public string TransactionRoundingType { get; set; }

        [JsonProperty("total")]
        public double Total { get; set; }

        [JsonProperty("total_credits_used")]
        public double TotalCreditsUsed { get; set; }

        [JsonProperty("total_refunded_amount")]
        public int TotalRefundedAmount { get; set; }

        [JsonProperty("balance")]
        public int Balance { get; set; }

        [JsonProperty("taxes")]
        public IList<object> Taxes { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("terms")]
        public string Terms { get; set; }

        [JsonProperty("creditnote_refunds")]
        public IList<object> CreditnoteRefunds { get; set; }

        [JsonProperty("billing_address")]
        public Address BillingAddress { get; set; }

        [JsonProperty("shipping_address")]
        public Address ShippingAddress { get; set; }

        [JsonProperty("custom_fields")]
        public IList<CustomField> CustomFields { get; set; }

        [JsonProperty("custom_field_hash")]
        public CustomFieldHash CustomFieldHash { get; set; }

        [JsonProperty("created_time")]
        public DateTime CreatedTime { get; set; }

        [JsonProperty("created_by_id")]
        public string CreatedById { get; set; }

        [JsonProperty("last_modified_time")]
        public DateTime LastModifiedTime { get; set; }
    }
    public class ItemCustomField
    {
        [JsonProperty("customfield_id")]
        public string CustomfieldId { get; set; }

        [JsonProperty("is_active")]
        public bool IsActive { get; set; }

        [JsonProperty("show_in_all_pdf")]
        public bool ShowInAllPdf { get; set; }

        [JsonProperty("value_formatted")]
        public string ValueFormatted { get; set; }

        [JsonProperty("data_type")]
        public string DataType { get; set; }

        [JsonProperty("index")]
        public int Index { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("show_on_pdf")]
        public bool ShowOnPdf { get; set; }

        [JsonProperty("placeholder")]
        public string Placeholder { get; set; }

        [JsonProperty("value")]
        public int Value { get; set; }
    }

    public class CustomFieldHash
    {
        [JsonProperty("cf_reason_for_credit")]
        public string CfReasonForCredit { get; set; }
    }
}
