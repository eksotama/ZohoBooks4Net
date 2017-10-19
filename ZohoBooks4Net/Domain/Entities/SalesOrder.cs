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
    public class SalesOrder
    {
        [JsonProperty("customer_id")]
        public string CustomerId { get; set; }

        [JsonProperty("billing_address_id")]
        public string BillingAddressId { get; set; }

        [JsonProperty("shipping_address_id")]
        public string ShippingAddressId { get; set; }

        [JsonProperty("is_pre_gst")]
        public bool IsPreGst { get; set; }

        [JsonProperty("gst_no")]
        public string GstNo { get; set; }

        [JsonProperty("gst_treatment")]
        public string GstTreatment { get; set; }

        [JsonProperty("place_of_supply")]
        public string PlaceOfSupply { get; set; }

        [JsonProperty("is_update_customer")]
        public bool IsUpdateCustomer { get; set; }

        [JsonProperty("contact_persons")]
        public IList<string> ContactPersons { get; set; }

        [JsonProperty("salesorder_number")]
        public string SalesorderNumber { get; set; }

        [JsonProperty("reference_number")]
        public string ReferenceNumber { get; set; }

        [JsonProperty("template_id")]
        public string TemplateId { get; set; }

        [JsonProperty("documents")]
        public IList<string> Documents { get; set; }

        [JsonProperty("exchange_rate")]
        public double ExchangeRate { get; set; }

        [JsonProperty("is_discount_before_tax")]
        public bool IsDiscountBeforeTax { get; set; }

        [JsonProperty("discount_type")]
        public string DiscountType { get; set; }

        [JsonProperty("salesperson_name")]
        public string SalespersonName { get; set; }

        [JsonProperty("tax_id")]
        public string TaxId { get; set; }

        [JsonProperty("is_inclusive_tax")]
        public bool IsInclusiveTax { get; set; }

        [JsonProperty("custom_fields")]
        public IList<CustomField> CustomFields { get; set; }

        [JsonProperty("line_items")]
        public IList<LineItem> LineItems { get; set; }

        [JsonProperty("shipping_charge")]
        public int ShippingCharge { get; set; }

        [JsonProperty("adjustment")]
        public double Adjustment { get; set; }

        [JsonProperty("adjustment_description")]
        public string AdjustmentDescription { get; set; }

        [JsonProperty("delivery_method")]
        public string DeliveryMethod { get; set; }

        [JsonProperty("date")]
        public DateTime? Date { get; set; }

        [JsonProperty("shipment_date")]
        public DateTime? ShipmentDate { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("terms")]
        public string Terms { get; set; }

        [JsonProperty("crm_owner_id")]
        public string CrmOwnerId { get; set; }

        [JsonProperty("crm_custom_reference_id")]
        public string CrmCustomReferenceId { get; set; }

        [JsonProperty("vat_treatment")]
        public string VatTreatment { get; set; }

        [JsonProperty("discount")]
        public string Discount { get; set; }

        [JsonProperty("notes_default")]
        public string NotesDefault { get; set; }

        [JsonProperty("terms_default")]
        public string TermsDefault { get; set; }

        [JsonProperty("estimate_id")]
        public string EstimateId { get; set; }

        [JsonProperty("pricebook_id")]
        public string PricebookId { get; set; }

        [JsonProperty("zcrm_potential_id")]
        public string ZcrmPotentialId { get; set; }

        [JsonProperty("Zcrm_potential_name")]
        public string ZcrmPotentialName { get; set; }
    }

}
