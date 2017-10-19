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
using ZohoBooks4Net.Domain.Enumeration.Variants;
using ZohoBooks4Net.Domain.Enumeration.PurchaseOrders;

namespace ZohoBooks4Net.Filters
{
    public class PurchaseOrdersFilter : PaginationFilter
    {
        /// <summary>
        /// Search purchase order by purchase order number. Variants: purchaseorder_number.startswith and
        /// purchaseorder_number.contains
        /// </summary>
        public Tuple<SearchVariant, string> PurchaseOrderNumber { get; set; }

        /// <summary>
        /// Search purchase order by reference number.Variants: reference_number.startswith and reference_number.contains
        /// </summary>
        public Tuple<SearchVariant, string> ReferenceNumber { get; set; }

        /// <summary>
        /// Search purchase order by purchase order item description. Variants: item_description.startswith and 
        /// item_description.contains
        /// </summary>
        public Tuple<SearchVariant, string> ItemDescription { get; set; }

        /// <summary>
        /// Search purchase order by vendor name. Variants: vendor_name.startswith and vendor_name.contains
        /// </summary>
        public Tuple<SearchVariant, string> VendorName { get; set; }

        /// <summary>
        /// Search purchase order by vendor id.
        /// </summary>
        public string VendorId { get; set; }

        /// <summary>
        /// Search purchase order by item id.
        /// </summary>
        public string ItemId { get; set; }

        /// <summary>
        /// Search purchase order by last modified time.
        /// </summary>
        public DateTime? LastModifiedTime { get; set; }

        /// <summary>
        /// The date the purchase order is created.
        /// </summary>
        public DateTime? Date { get; set; }

        /// <summary>
        /// Search purchase order by purchase order status. Allowed Values: draft, open, billed and cancelled
        /// </summary>
        public PurchaseOrderStatus? Status { get; set; }

        /// <summary>
        /// Search purchase order by purchase order total. Variants: total.start, total.end, total.less_than, total.less_equals, 
        /// total.greater_than and total.greater_equals
        /// </summary>
        public PurchaseOrderTotal? Total { get; set; }

        /// <summary>
        /// Filter purchase order by any status. Allowed Values: Status.All, Status.Draft, Status.Open, Status.Billed and Status.Cancelled.
        /// </summary>
        public PurchaseOrdersFilterBy? FilterBy { get; set; }

        /// <summary>
        /// Sort purchase orders. Allowed Values: vendor_name, purchaseorder_number, date, delivery_date, total and created_time.
        /// </summary>
        public PurchaseOrderSortColumn? SortColumn { get; set; }

        /// <summary>
        /// Search purchase order by purchase order number or reference number or vendor name.
        /// </summary>
        public string SearchText { get; set; }

        /// <summary>
        /// Search purchase order by purchase order’s custom field. Variants: custom_field_startswith, custom_field_contains
        /// </summary>
        public Tuple<SearchVariant, string> CustomField { get; set; }

        public override void AddFilter(HttpRequestMessage message)
        {
            var filters = new Dictionary<string, string>();

            if (PurchaseOrderNumber != null)
            {
                filters.Add("purchaseorder_number" + SearchVariantValue(PurchaseOrderNumber.Item1).Trim('\"'), PurchaseOrderNumber.Item2);
            }

            if (ReferenceNumber != null)
            {
                filters.Add("reference_number" + SearchVariantValue(ReferenceNumber.Item1).Trim('\"'), ReferenceNumber.Item2);
            }

            if (ItemDescription != null)
            {
                filters.Add("item_description" + SearchVariantValue(ItemDescription.Item1).Trim('\"'), ItemDescription.Item2);
            }

            if (VendorName != null)
            {
                filters.Add("vendor_name" + SearchVariantValue(VendorName.Item1).Trim('\"'), VendorName.Item2);
            }

            if (VendorId != null)
            {
                filters.Add("vendor_id", VendorId);
            }

            if (ItemId != null)
            {
                filters.Add("item_id", VendorId);
            }

            if (ItemId != null)
            {
                filters.Add("item_id", ItemId);
            }

            if (LastModifiedTime != null)
            {
                filters.Add("last_modified_time", LastModifiedTime.Value.ToString());
            }

            if (Date != null)
            {
                filters.Add("date", Date.Value.ToString());
            }

            if (Status != null)
            {
                filters.Add("status", JsonConvert.SerializeObject(Status.Value).Trim('\"'));
            }

            if (Total != null)
            {
                filters.Add("total", JsonConvert.SerializeObject(Total.Value).Trim('\"'));
            }

            if (FilterBy != null)
            {
                filters.Add("filter_by", JsonConvert.SerializeObject(FilterBy.Value).Trim('\"'));
            }

            if (SortColumn != null)
            {
                filters.Add("sort_column", JsonConvert.SerializeObject(SortColumn.Value).Trim('\"'));
            }

            if (SearchText != null)
            {
                filters.Add("search_text", SearchText);
            }

            if (CustomField != null)
            {
                filters.Add("custom_field" + SearchVariantValue(CustomField.Item1), CustomField.Item2);
            }

            base.AddFilter(message, filters);
        }
    }
}
