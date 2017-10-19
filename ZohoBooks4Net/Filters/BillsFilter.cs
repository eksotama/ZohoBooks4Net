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
using ZohoBooks4Net.Domain.Enumeration.Bills;
using ZohoBooks4Net.Domain.Enumeration.Variants;

namespace ZohoBooks4Net.Filters
{
    public class BillsFilter : PaginationFilter
    {
        /// <summary>
        /// Search bills by bill number. Variants: bill_number_startswith and bill_number_contains
        /// </summary>
        public string BillNumber { get; set; }

        /// <summary>
        /// Search bills by reference_number number. Variants: reference_number_startswith and reference_number_contains
        /// </summary>
        public Tuple<SearchVariant, string> ReferenceNumber { get; set; }

        /// <summary>
        /// Search bills by bill date. Variants: date_start, date_end, date_before and date.after
        /// </summary>
        public Tuple<DateVariant, DateTime> Date { get; set; }

        /// <summary>
        /// Search bills by bill status. Allowed Values: paid, open, overdue, void and partially_paid
        /// </summary>
        public BillStatus? Status { get; set; }

        /// <summary>
        /// Search bills by description. Variants: description_startswith and description_contains
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Search bills by vendor name. Variants: vendor_name_startswith and vendor_name_contains
        /// </summary>
        public Tuple<SearchVariant, string> VendorName { get; set; }

        /// <summary>
        /// Search bills by bill total. Variants: total_less_than, total_less_equals, total_greater_than and total_greater_equals
        /// </summary>
        public Tuple<NumericalVariant, double> Total { get; set; }

        /// <summary>
        /// Search bills by Vendor ID
        /// </summary>
        public string VendorId { get; set; }

        /// <summary>
        /// Search bills by Item ID
        /// </summary>
        public string ItemId { get; set; }

        /// <summary>
        /// Search bills by Recurring Bill ID
        /// </summary>
        public string RecurringBillId { get; set; }

        /// <summary>
        /// Search bills by Purchase Order ID
        /// </summary>
        public string PurchaseOrderId { get; set; }

        /// <summary>
        /// Search bills by Last Modified Time
        /// </summary>
        public DateTime? LastModifiedTime { get; set; }

        /// <summary>
        /// Search bills by bill number or reference number or vendor name.
        /// </summary>
        public string SearchText { get; set; }

        /// <summary>
        /// Filter bills by any status. Allowed Values: Status.All, Status.PartiallyPaid, Status.Paid, Status.Overdue, Status.Void and 
        /// Status.Open.
        /// </summary>
        public BillFilterBy? FilterBy { get; set; }

        /// <summary>
        /// Sort bills. Allowed Values: vendor_name, bill_number, date, due_date, total, balance and created_time.
        /// </summary>
        public BillSortColumn? SortColumn { get; set; }

        public override void AddFilter(HttpRequestMessage message)
        {
            var filters = new Dictionary<string, string>();

            if (BillNumber != null)
            {
                filters.Add("bill_number", BillNumber);
            }

            if (ReferenceNumber != null)
            {
                filters.Add("reference_number" + SearchVariantValue(ReferenceNumber.Item1), ReferenceNumber.Item2);
            }

            if (Date != null)
            {
                filters.Add("date" + DateVariantValue(Date.Item1), Date.Item2.ToString());
            }

            if (Status != null)
            {
                filters.Add("status", JsonConvert.SerializeObject(Status).Trim('\"'));
            }

            if (Description != null)
            {
                filters.Add("description", Description);
            }

            if (VendorName != null)
            {
                filters.Add("vendor_name" + SearchVariantValue(VendorName.Item1), VendorName.Item2);
            }

            if (Total != null)
            {
                filters.Add("total" + JsonConvert.SerializeObject(Total.Item1).Trim('\"'), Total.Item2.ToString());
            }

            if (VendorId != null)
            {
                filters.Add("vendor_id", VendorId);
            }

            if (ItemId != null)
            {
                filters.Add("item_id", ItemId);
            }

            if (RecurringBillId != null)
            {
                filters.Add("recurring_bill_id", RecurringBillId);
            }

            if (PurchaseOrderId != null)
            {
                filters.Add("purchase_order_id", PurchaseOrderId);
            }

            if (LastModifiedTime != null)
            {
                filters.Add("last_modified_time", LastModifiedTime.Value.ToString());
            }

            if (SearchText != null)
            {
                filters.Add("search_text", SearchText);
            }

            if (FilterBy != null)
            {
                filters.Add("filter_by", JsonConvert.SerializeObject(FilterBy.Value).Trim('\"'));
            }

            if (SortColumn != null)
            {
                filters.Add("sort_column", JsonConvert.SerializeObject(SortColumn.Value).Trim('\"'));
            }

            base.AddFilter(message, filters);
        }
    }
}
