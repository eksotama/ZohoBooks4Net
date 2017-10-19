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
using ZohoBooks4Net.Domain.Enumeration.Invoices;

namespace ZohoBooks4Net.Filters
{
    public class InvoicesFilter : PaginationFilter
    {
        /// <summary>
        /// Search invoices by invoice number.Variants: invoice_number_startswith and invoice_number_contains. Max-length [100]
        /// </summary>
        public Tuple<SearchVariant, string> InvoiceNumber { get; set; }

        /// <summary>
        /// Search invoices by item name.Variants: item_name_startswith and item_name_contains. Max-length [100]
        /// </summary>
        public Tuple<SearchVariant, string> ItemName { get; set; }

        /// <summary>
        /// Search invoices by item id.
        /// </summary>
        public string ItemId { get; set; }

        /// <summary>
        /// Search invoices by item description.Variants: item_description_startswith and item_description_contains. Max-length [100]
        /// </summary>
        public Tuple<SearchVariant, string> ItemDescription { get; set; }

        /// <summary>
        /// The reference number of the invoice
        /// </summary>
        public string ReferenceNumber { get; set; }

        /// <summary>
        /// The name of the customer. Max-length [100]
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// ID of the recurring invoice from which the invoice is created.
        /// </summary>
        public string RecurringInvoiceId { get; set; }

        /// <summary>
        /// Search contacts by email id. Max-length [100]
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The total amount to be paid
        /// </summary>
        public double? Total { get; set; }

        /// <summary>
        /// The unpaid amount
        /// </summary>
        public double? Balance { get; set; }

        /// <summary>
        /// Search invoices by custom fields.Variants: custom_field_startswith and custom_field_contains
        /// </summary>
        public Tuple<SearchVariant, string> CustomField { get; set; }

        /// <summary>
        /// Search invoices by invoice date. Default date format is yyyy-mm-dd. Variants: due_date_start, due_date_end,
        /// due_date_before and due_date_after.
        /// </summary>
        public Tuple<DateVariant, DateTime> Date { get; set; }

        /// <summary>
        /// Search invoices by due date. Default date format is yyyy-mm-dd. Variants: due_date_start, due_date_end, due_date_before 
        /// and due_date_after 
        /// </summary>
        public Tuple<DateVariant, DateTime> DueDate { get; set; }

        /// <summary>
        /// Search invoices by invoice status.Allowed Values: sent, draft, overdue, paid, void, unpaid, partially_paid and viewed
        /// </summary>
        public InvoiceStatus? Status { get; set; }

        /// <summary>
        /// ID of the customer the invoice has to be created.
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// Filter invoices by any status or payment expected date.Allowed Values: Status.All, Status.Sent, Status.Draft, Status.OverDue, 
        /// Status.Paid, Status.Void, Status.Unpaid, Status.PartiallyPaid, Status.Viewed and Date.PaymentExpectedDate
        /// </summary>
        public InvoicesFilterBy? FilterBy { get; set; }

        /// <summary>
        /// Search invoices by invoice number or purchase order or customer name. Max-length [100]
        /// </summary>
        public string SearchText { get; set; }

        /// <summary>
        /// Sort invoices.Allowed Values: customer_name, invoice_number, date, due_date, total, balance and created_time
        /// </summary>
        public InvoiceSortColumn? SortColumn { get; set; }

        public override void AddFilter(HttpRequestMessage message)
        {
            var filters = new Dictionary<string, string>();

            if (InvoiceNumber != null)
            {
                filters.Add("invoice_number" + SearchVariantValue(InvoiceNumber.Item1), InvoiceNumber.Item2);
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
                filters.Add("reference_number", ReferenceNumber);
            }

            if (CustomerName != null)
            {
                filters.Add("customer_name", CustomerName);
            }

            if (RecurringInvoiceId != null)
            {
                filters.Add("recurring_invoice_id", RecurringInvoiceId);
            }

            if (Email != null)
            {
                filters.Add("email", Email);
            }

            if (Total != null)
            {
                filters.Add("total", Total.Value.ToString());
            }

            if (Balance != null)
            {
                filters.Add("balance", Balance.Value.ToString());
            }

            if (CustomField != null)
            {
                filters.Add("custom_field" + SearchVariantValue(CustomField.Item1), CustomField.Item2);
            }

            if (Date != null)
            {
                filters.Add("date" + DateVariantValue(Date.Item1), Date.Item2.ToString());
            }

            if (DueDate != null)
            {
                filters.Add("due_date" + DateVariantValue(DueDate.Item1), DueDate.Item2.ToString());
            }

            if (Status != null)
            {
                filters.Add("status", JsonConvert.SerializeObject(Status.Value).Trim('\"'));
            }

            if (CustomerId != null)
            {
                filters.Add("customer_id", CustomerId);
            }

            if (FilterBy != null)
            {
                filters.Add("filter_by", JsonConvert.SerializeObject(FilterBy.Value).Trim('\"'));
            }

            if (SearchText != null)
            {
                filters.Add("search_text", SearchText);
            }

            if (SortColumn != null)
            {
                filters.Add("sort_column", JsonConvert.SerializeObject(SortColumn.Value));
            }

            base.AddFilter(message, filters);
        }
    }
}
