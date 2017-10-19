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
    public class PurchaseOrder
    {
        /// <summary>
        /// ID of the vendor the purchase order has to be created.
        /// </summary>
        [JsonProperty("vendor_id")]
        public string VendorId { get; set; }

        /// <summary>
        /// Array of contact person(s) for whom purchase order has to be sent.

        /// </summary>
        [JsonProperty("contact_persons")]
        public IList<string> ContactPersons { get; set; }

        /// <summary>
        /// Mandatory if auto number generation is disabled.
        /// </summary>
        [JsonProperty("purchaseorder_number")]
        public string PurchaseorderNumber { get; set; }

        /// <summary>
        /// Enter pricebook ID
        /// </summary>
        [JsonProperty("pricebook_id")]
        public string PricebookId { get; set; }

        /// <summary>
        /// Reference number of purchase order.
        /// </summary>
        [JsonProperty("reference_number")]
        public string ReferenceNumber { get; set; }

        /// <summary>
        /// ID of the Billing Address
        /// </summary>
        [JsonProperty("billing_address_id")]
        public string BillingAddressId { get; set; }

        /// <summary>
        /// ID of the pdf template associated with the purchase order.
        /// </summary>
        [JsonProperty("template_id")]
        public string TemplateId { get; set; }

        /// <summary>
        /// The date the purchase order is created.
        /// </summary>
        [JsonProperty("date")]
        public DateTime? Date { get; set; }

        /// <summary>
        /// Date of delivery of the purchase order
        /// </summary>
        [JsonProperty("delivery_date")]
        public DateTime? DeliveryDate { get; set; }

        /// <summary>
        /// Exchange rate of the currency.

        /// </summary>
        [JsonProperty("exchange_rate")]
        public int ExchangeRate { get; set; }

        /// <summary>
        /// Delivery note for vendor.
        /// </summary>
        [JsonProperty("notes")]
        public string Notes { get; set; }

        /// <summary>
        /// Terms for the purchase order
        /// </summary>
        [JsonProperty("terms")]
        public string Terms { get; set; }

        /// <summary>
        /// ID of the sales order
        /// </summary>
        [JsonProperty("salesorder_id")]
        public string SalesorderId { get; set; }

        /// <summary>
        /// Line items of purchase order.
        /// </summary>
        [JsonProperty("line_items")]
        public IList<LineItem> LineItems { get; set; }

        [JsonProperty("custom_fields")]
        public IList<CustomField> CustomFields { get; set; }

        [JsonProperty("documents")]
        public IList<string> Documents { get; set; }
    }
}
