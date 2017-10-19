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
using ZohoBooks4Net.Domain.Enumeration.SalesOrders;

namespace ZohoBooks4Net.Filters
{
    public class SalesOrdersFilter : PaginationFilter
    {
        /// <summary>
        /// Sort sales orders. Allowed Values: customer_name, salesorder_number, shipment_date, total, date and created_time.
        /// </summary>
        public SalesOrderSortColumn? SortColumn { get; set; }

        /// <summary>
        /// Search sales order by sales order number or reference number or customer name.
        /// </summary>
        public string SearchText { get; set; }

        /// <summary>
        /// Filter sales order by status. Allowed Values: Status.All, Status.Open, Status.Draft, Status.OverDue, Status.PartiallyInvoiced,
        /// Status.Invoiced, Status.Void and Status.Closed
        /// </summary>
        public SalesOrdersFilterBy? FilterBy { get; set; }

        /// <summary>
        /// Search sales order by sales order Number. Variants: salesorder_number.startswith, salesorder_number.contains.
        /// Maximum Length : 100 
        /// </summary>
        public Tuple<SearchVariant, string> SalesOrderNumber { get; set; }

        /// <summary>
        /// Search sales order by item name. Variants: item_name.startswith and item_name.contains. Maximum Length : 100
        /// </summary>
        public Tuple<SearchVariant, string> ItemName { get; set; }

        /// <summary>
        /// Search sales order Based on Item ID.
        /// </summary>
        public string ItemId { get; set; }

        /// <summary>
        /// Search sales order by item description. Variants: item_description.startswith and item_description.contains. Maximum Length: 100 
        /// </summary>
        public Tuple<SearchVariant, string> ItemDescription { get; set; }

        /// <summary>
        /// Search sales order by reference number. Variants: reference_number.startswith and reference_number.contains.
        /// </summary>
        public Tuple<SearchVariant, string> ReferenceNumber { get; set; }

        /// <summary>
        /// Search sales order by customer name. Variants: customer_name.startswith and customer_name.contains. Maximum Length : 100
        /// </summary>
        public Tuple<SearchVariant, string> CustomerName { get; set; }

        /// <summary>
        /// Search sales order by sales order total. Variants: total.start, total.end, total.less_than, total.less_equals, 
        /// total.greater_than and total.greater_equals.
        /// </summary>
        public Tuple<NumericalVariant, double> Total { get; set; }

        /// <summary>
        /// Search sales order by sales order date. Variants: date.start, date.end, date.before and date.after. 
        /// Default date format : yyyy-mm-dd
        /// </summary>
        public Tuple<DateVariant, DateTime> Date { get; set; }

        /// <summary>
        /// Search sales order by sales order shipment date. Variants: shipment_date.start, shipment_date.end, shipment_date.before and 
        /// shipment_date.after. Default date format : yyyy-mm-dd
        /// </summary>
        public Tuple<DateVariant, DateTime> ShipmentDate { get; set; }

        /// <summary>
        /// Search sales order by sales order status. Allowed Values: draft, open, invoiced, partially_invoiced, void and overdue.
        /// </summary>
        public SalesOrderStatus? Status { get; set; }

        /// <summary>
        /// Search sales order based on customer_id
        /// </summary>
        public string CustomerId { get; set; }

        public override void AddFilter(HttpRequestMessage message)
        {
            var filters = new Dictionary<string, string>();

            if (SortColumn != null)
            {
                filters.Add("sort_column", JsonConvert.SerializeObject(SortColumn.Value).Trim('\"'));
            }

            if (SearchText != null)
            {
                filters.Add("search_text", SearchText);
            }

            if (FilterBy != null)
            {
                filters.Add("filter_by", JsonConvert.SerializeObject(FilterBy.Value));
            }

            if (SalesOrderNumber != null)
            {
                filters.Add("sales_order_number" + SearchVariantValue(SalesOrderNumber.Item1), SalesOrderNumber.Item2);
            }

            if (ItemName != null)
            {
                filters.Add("item_name" + SearchVariantValue(ItemName.Item1), ItemName.Item2);
            }

            if (ItemId != null)
            {
                filters.Add("item_id", ItemId);
            }

            if (ItemDescription != null)
            {
                filters.Add("item_description" + SearchVariantValue(ItemDescription.Item1), ItemDescription.Item2);
            }

            if (ReferenceNumber != null)
            {
                filters.Add("reference_number" + SearchVariantValue(ReferenceNumber.Item1), ReferenceNumber.Item2);
            }

            if (CustomerName != null)
            {
                filters.Add("customer_name" + SearchVariantValue(CustomerName.Item1), CustomerName.Item2);
            }

            if (Total != null)
            {
                filters.Add("total" + JsonConvert.SerializeObject(Total.Item1).Trim('\"'), Total.Item2.ToString());
            }

            if (Date != null)
            {
                filters.Add("date" + DateVariantValue(Date.Item1), Date.Item2.ToString());
            }

            if (ShipmentDate != null)
            {
                filters.Add("shipment_date" + DateVariantValue(ShipmentDate.Item1), Date.Item2.ToString());
            }

            if (Status != null)
            {
                filters.Add("status", JsonConvert.SerializeObject(Status).Trim('\"'));
            }

            if (CustomerId != null)
            {
                filters.Add("customer_id", CustomerId);
            }

            base.AddFilter(message, filters);
        }
    }
}
