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
using ZohoBooks4Net.Domain.Enumeration.Expenses;
using ZohoBooks4Net.Domain.Enumeration.Variants;

namespace ZohoBooks4Net.Filters
{
    public class ExpensesFilter : PaginationFilter
    {
        /// <summary>
        /// Search expenses by description.Variants description_startswith and description_contains. Max-length [100]
        /// </summary>
        public Tuple<SearchVariant, string> Description { get; set; }

        /// <summary>
        /// Search expenses by reference number. Variants reference_number_startswith and reference_number_contains. Max-length [100]
        /// </summary>
        public Tuple<SearchVariant, string> ReferenceNumber { get; set; }

        /// <summary>
        /// Search expenses by expense status. Allowed Values unbilled, invoiced, reimbursed, non-billable and billable
        /// </summary>
        public ExpenseStatus? Status { get; set; }

        /// <summary>
        /// Search expenses by expense date. Variants date_start, date_end, date_before and date_after. Format [yyyy-mm-dd]
        /// </summary>
        public Tuple<DateVariant, DateTime> Date { get; set; }

        /// <summary>
        /// Search expenses by amount. Variants: amount_less_than, amount_less_equals, amount_greater_than and amount_greater_than
        /// </summary>
        public Tuple<NumericalVariant, double> Amount { get; set; }

        /// <summary>
        /// Search expenses by expense account name. Variants account_name_startswith and account_name_contains. Max-length [100]
        /// </summary>
        public Tuple<SearchVariant, string> AccountName { get; set; }

        /// <summary>
        /// Search expenses by customer name. Variants: customer_name_startswith and customer_name_contains. Max-length [100]
        /// </summary>
        public Tuple<SearchVariant, string> CustomerName { get; set; }

        /// <summary>
        /// Search expenses by vendor name. Variants: vendor_name_startswith and vendor_name_contains
        /// </summary>
        public Tuple<SearchVariant, string> VendorName { get; set; }

        /// <summary>
        /// ID of the expense account.
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// ID of the vendor the expense is made.
        /// </summary>
        public string VendorId { get; set; }

        /// <summary>
        /// Search expenses by recurring expense id.
        /// </summary>
        public string RecurringExpenseId { get; set; }

        /// <summary>
        /// Search expenses by paid through account id.
        /// </summary>
        public string PaidThroughAccountId { get; set; }

        public string SearchText { get; set; }

        /// <summary>
        /// Sort expenses.Allowed Values date, account_name, total, bcy_total, reference_number, customer_name and created_time
        /// </summary>
        public ExpensesSortColumn? SortColumn { get; set; }

        /// <summary>
        /// Filter expenses by expense status. Allowed Values Status.All, Status.Billable, Status.Nonbillable, Status.Reimbursed, 
        /// Status.Invoiced, Status.Unbilled
        /// </summary>
        public ExpensesFilterBy? FilterBy { get; set; }

        public override void AddFilter(HttpRequestMessage message)
        {
            var filters = new Dictionary<string, string>();

            if (Description != null)
            {
                filters.Add("description" + SearchVariantValue(Description.Item1), Description.Item2);
            }

            if (ReferenceNumber != null)
            {
                filters.Add("reference_number" + SearchVariantValue(ReferenceNumber.Item1), ReferenceNumber.Item2);
            }

            if (Status != null)
            {
                filters.Add("status", JsonConvert.SerializeObject(Status).Trim('\"'));
            }

            if (Date != null)
            {
                filters.Add("date" + DateVariantValue(Date.Item1), Date.Item2.ToString());
            }

            if (Amount != null)
            {
                filters.Add("amount" + JsonConvert.SerializeObject(Amount.Item1).Trim('\"'), Amount.Item2.ToString());
            }

            if (AccountName != null)
            {
                filters.Add("account_name" + SearchVariantValue(AccountName.Item1), AccountName.Item2);
            }

            if (CustomerName != null)
            {
                filters.Add("customer_name" + SearchVariantValue(CustomerName.Item1), CustomerName.Item2);
            }

            if (VendorName != null)
            {
                filters.Add("vendor_name" + SearchVariantValue(VendorName.Item1), VendorName.Item2);
            }

            if (CustomerId != null)
            {
                filters.Add("customer_id", CustomerId);
            }

            if (VendorId != null)
            {
                filters.Add("vendor_id", VendorId);
            }

            if (RecurringExpenseId != null)
            {
                filters.Add("recurring_expense_id", RecurringExpenseId);
            }

            if (PaidThroughAccountId != null)
            {
                filters.Add("paid_through_account_id", PaidThroughAccountId);
            }

            if (SearchText != null)
            {
                filters.Add("seach_text", SearchText);
            }

            if (FilterBy != null)
            {
                filters.Add("filter_by", JsonConvert.SerializeObject(FilterBy).Trim('\"'));
            }

            if (SortColumn != null)
            {
                filters.Add("sort_column", JsonConvert.SerializeObject(SortColumn).Trim('\"'));
            }

            base.AddFilter(message, filters);
        }
    }
}
