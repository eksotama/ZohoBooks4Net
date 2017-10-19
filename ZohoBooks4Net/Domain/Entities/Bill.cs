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
    public class Bill
    {
        [JsonProperty("bill_id")]
        public string BillId { get; set; }

        [JsonProperty("purchaseorder_ids")]
        public IList<object> PurchaseorderIds { get; set; }

        [JsonProperty("vendor_id")]
        public string VendorId { get; set; }

        [JsonProperty("source_of_supply")]
        public string SourceOfSupply { get; set; }

        [JsonProperty("destination_of_supply")]
        public string DestinationOfSupply { get; set; }

        [JsonProperty("gst_no")]
        public string GstNo { get; set; }

        [JsonProperty("gst_treatment")]
        public string GstTreatment { get; set; }

        [JsonProperty("is_pre_gst")]
        public bool IsPreGst { get; set; }

        [JsonProperty("pricebook_id")]
        public long PricebookId { get; set; }

        [JsonProperty("is_reverse_charge_applied")]
        public bool IsReverseChargeApplied { get; set; }

        [JsonProperty("unused_credits_payable_amount")]
        public int UnusedCreditsPayableAmount { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("bill_number")]
        public string BillNumber { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("due_date")]
        public string DueDate { get; set; }

        [JsonProperty("payment_terms")]
        public int PaymentTerms { get; set; }

        [JsonProperty("payment_terms_label")]
        public string PaymentTermsLabel { get; set; }

        [JsonProperty("reference_number")]
        public string ReferenceNumber { get; set; }

        [JsonProperty("due_in_days")]
        public int DueInDays { get; set; }

        [JsonProperty("currency_id")]
        public string CurrencyId { get; set; }

        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }

        [JsonProperty("documents")]
        public IList<string> Documents { get; set; }

        [JsonProperty("price_precision")]
        public int PricePrecision { get; set; }

        [JsonProperty("exchange_rate")]
        public double ExchangeRate { get; set; }

        [JsonProperty("custom_fields")]
        public IList<CustomField> CustomFields { get; set; }

        [JsonProperty("is_item_level_tax_calc")]
        public bool IsItemLevelTaxCalc { get; set; }

        [JsonProperty("is_inclusive_tax")]
        public bool IsInclusiveTax { get; set; }

        [JsonProperty("line_items")]
        public IList<LineItem> LineItems { get; set; }

        [JsonProperty("sub_total")]
        public int SubTotal { get; set; }

        [JsonProperty("tax_total")]
        public int TaxTotal { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("payment_made")]
        public int PaymentMade { get; set; }

        [JsonProperty("vendor_credits_applied")]
        public int VendorCreditsApplied { get; set; }

        [JsonProperty("is_line_item_invoiced")]
        public bool IsLineItemInvoiced { get; set; }

        [JsonProperty("purchaseorders")]
        public IList<PurchaseOrder> PurchaseOrders { get; set; }

        [JsonProperty("taxes")]
        public IList<Tax> Taxes { get; set; }

        [JsonProperty("acquisition_vat_summary")]
        public IList<Tax> AcquisitionVatSummary { get; set; }

        [JsonProperty("reverse_charge_vat_summary")]
        public IList<Tax> ReverseChargeVatSummary { get; set; }

        [JsonProperty("balance")]
        public double? Balance { get; set; }

        [JsonProperty("billing_address")]
        public Address BillingAddress { get; set; }

        [JsonProperty("payments")]
        public IList<Payment> Payments { get; set; }

        [JsonProperty("vendor_credits")]
        public IList<VendorCredit> VendorCredits { get; set; }

        [JsonProperty("created_time")]
        public DateTime? CreatedTime { get; set; }

        [JsonProperty("created_by_id")]
        public string CreatedById { get; set; }

        [JsonProperty("last_modified_time")]
        public DateTime? LastModifiedTime { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("terms")]
        public string Terms { get; set; }

        [JsonProperty("open_purchaseorders_count")]
        public int OpenPurchaseordersCount { get; set; }
    }
}
