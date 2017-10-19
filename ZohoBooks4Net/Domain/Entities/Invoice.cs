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
    public class Invoice
    {
        [JsonProperty("invoice_id")]
        public string InvoiceId { get; set; }

        [JsonProperty("ach_payment_initiated")]
        public bool AchPaymentInitiated { get; set; }

        [JsonProperty("customer_name")]
        public string CustomerName { get; set; }

        [JsonProperty("customer_id")]
        public string CustomerId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("invoice_number")]
        public string InvoiceNumber { get; set; }

        [JsonProperty("reference_number")]
        public string ReferenceNumber { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("due_date")]
        public string DueDate { get; set; }

        [JsonProperty("due_days")]
        public string DueDays { get; set; }

        [JsonProperty("currency_id")]
        public string CurrencyId { get; set; }

        [JsonProperty("schedule_time")]
        public string ScheduleTime { get; set; }

        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }

        [JsonProperty("is_viewed_by_client")]
        public bool IsViewedByClient { get; set; }

        [JsonProperty("has_attachment")]
        public bool HasAttachment { get; set; }

        [JsonProperty("client_viewed_time")]
        public string ClientViewedTime { get; set; }

        [JsonProperty("total")]
        public double Total { get; set; }

        [JsonProperty("balance")]
        public double Balance { get; set; }

        [JsonProperty("created_time")]
        public DateTime CreatedTime { get; set; }

        [JsonProperty("last_modified_time")]
        public DateTime LastModifiedTime { get; set; }

        [JsonProperty("is_emailed")]
        public bool IsEmailed { get; set; }

        [JsonProperty("reminders_sent")]
        public int RemindersSent { get; set; }

        [JsonProperty("last_reminder_sent_date")]
        public string LastReminderSentDate { get; set; }

        [JsonProperty("payment_expected_date")]
        public string PaymentExpectedDate { get; set; }

        [JsonProperty("last_payment_date")]
        public string LastPaymentDate { get; set; }

        [JsonProperty("custom_fields")]
        public IList<CustomField> CustomFields { get; set; }

        [JsonProperty("documents")]
        public string Documents { get; set; }

        [JsonProperty("salesperson_id")]
        public string SalespersonId { get; set; }

        [JsonProperty("salesperson_name")]
        public string SalespersonName { get; set; }

        [JsonProperty("shipping_charge")]
        public int ShippingCharge { get; set; }

        [JsonProperty("adjustment")]
        public int Adjustment { get; set; }

        [JsonProperty("write_off_amount")]
        public int WriteOffAmount { get; set; }

        [JsonProperty("exchange_rate")]
        public int ExchangeRate { get; set; }

        [JsonProperty("contact_persons")]
        public IList<string> ContactPersons { get; set; }

        [JsonProperty("place_of_supply")]
        public string PlaceOfSupply { get; set; }

        [JsonProperty("gst_treatment")]
        public string GstTreatment { get; set; }

        [JsonProperty("gst_no")]
        public string GstNo { get; set; }

        [JsonProperty("template_id")]
        public long TemplateId { get; set; }

        [JsonProperty("payment_terms")]
        public int PaymentTerms { get; set; }

        [JsonProperty("payment_terms_label")]
        public string PaymentTermsLabel { get; set; }

        [JsonProperty("discount")]
        public int Discount { get; set; }

        [JsonProperty("is_discount_before_tax")]
        public bool IsDiscountBeforeTax { get; set; }

        [JsonProperty("discount_type")]
        public string DiscountType { get; set; }

        [JsonProperty("is_inclusive_tax")]
        public bool IsInclusiveTax { get; set; }

        [JsonProperty("recurring_invoice_id")]
        public string RecurringInvoiceId { get; set; }

        [JsonProperty("invoiced_estimate_id")]
        public string InvoicedEstimateId { get; set; }

        [JsonProperty("project_id")]
        public string ProjectId { get; set; }

        [JsonProperty("line_items")]
        public IList<LineItem> LineItems { get; set; }

        [JsonProperty("allow_partial_payments")]
        public bool AllowPartialPayments { get; set; }

        [JsonProperty("custom_body")]
        public string CustomBody { get; set; }

        [JsonProperty("custom_subject")]
        public string CustomSubject { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("terms")]
        public string Terms { get; set; }

        [JsonProperty("adjustment_description")]
        public string AdjustmentDescription { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }

        [JsonProperty("tax_authority_id")]
        public long TaxAuthorityId { get; set; }

        [JsonProperty("tax_exemption_id")]
        public long TaxExemptionId { get; set; }

        [JsonProperty("tax_id")]
        public long TaxId { get; set; }

        [JsonProperty("item_id")]
        public long ItemId { get; set; }

        [JsonProperty("expense_id")]
        public string ExpenseId { get; set; }

        [JsonProperty("salesorder_item_id")]
        public string SalesorderItemId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("account_id")]
        public string AccountId { get; set; }

        [JsonProperty("rate")]
        public int Rate { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("time_entry_ids")]
        public string TimeEntryIds { get; set; }

        [JsonProperty("payment_gateways")]
        public IList<string> PaymentGateways { get; set; }

        [JsonProperty("gateway_name")]
        public string GatewayName { get; set; }

        [JsonProperty("additional_field1")]
        public string AdditionalField1 { get; set; }
    }

}
