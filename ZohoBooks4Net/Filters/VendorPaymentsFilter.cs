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
using System.Text;
using ZohoBooks4Net.Domain.Enumeration;
using ZohoBooks4Net.Domain.Enumeration.Variants;
using ZohoBooks4Net.Domain.Enumeration.VendorPayments;

namespace ZohoBooks4Net.Filters
{
    public class VendorPaymentsFilter : PaginationFilter
    {
        /// <summary>
        /// Search payments by vendor name. Variants: vendor_name_startswith and vendor_name_contains.
        /// </summary>
        public Tuple<SearchVariant, string> VendorName { get; set; }

        /// <summary>
        /// Search payments by reference number. Variants: reference_number_startswith and reference_number_contains. In refunds, reference
        /// number for the refund recorded.
        /// </summary>
        public Tuple<SearchVariant, string> ReferenceNumber { get; set; }

        /// <summary>
        /// Search with Payment Number. Variant: payment_number_startswith, payment_number_contains
        /// </summary>
        public Tuple<SearchVariant, string> PaymentNumber { get; set; }

        /// <summary>
        /// Date the payment is made. Search payments by payment made date. Variants: date_start, date_end, date_before and date_after.
        /// </summary>
        public Tuple<DateVariant, DateTime> Date { get; set; }

        /// <summary>
        /// Payment amount made to the vendor. Search payments by payment amount. Variants: amount_less_than, amount_less_equals, 
        /// amount_greater_than and amount_greater_equals. In refunds, Amount refunded from the vendor payment.

        /// </summary>
        public Tuple<NumericalVariant, double> Amount { get; set; }

        /// <summary>
        /// Search payments by payment mode. Variants: payment_mode_startswith and payment_mode_contains.
        /// </summary>
        public Tuple<SearchVariant, string> PaymentMode { get; set; }

        /// <summary>
        /// Search with Payment Notes. Variant: notes_startswith, notes_contains
        /// </summary>
        public Tuple<SearchVariant, string> Notes { get; set; }

        /// <summary>
        /// ID of the vendor. Search payments by vendor id.
        /// </summary>
        public string VendorId { get; set; }

        /// <summary>
        /// Search with the Last Modified Time of the Vendor Payment
        /// </summary>
        public DateTime? LastModifiedTime { get; set; }

        /// <summary>
        /// Search payments by Bill ID.
        /// </summary>
        public string BillId { get; set; }

        /// <summary>
        /// Search payments by description. Variants: description_startswith and description_contains.
        /// </summary>
        public Tuple<SearchVariant, string> Description { get; set; }

        /// <summary>
        /// Search payments by reference number or vendor name or payment description.
        /// </summary>
        public string SearchText { get; set; }

        /// <summary>
        /// Filter payments by mode. Allowed Values: PaymentMode.All, PaymentMode.Check, PaymentMode.Cash, PaymentMode.BankTransfer, 
        /// PaymentMode.Paypal, PaymentMode.CreditCard, PaymentMode.GoogleCheckout, PaymentMode.Credit, PaymentMode.Authorizenet, 
        /// PaymentMode.BankRemittance, PaymentMode.Payflowpro and PaymentMode.Others.
        /// </summary>
        public PaymentMode? FilterBy { get; set; }

        /// <summary>
        /// Sort the payment list. Allowed Values: vendor_name, date, reference_number, amount and balance.
        /// </summary>
        public VendorPaymentsSortColumn? SortColumn { get; set; }

        public override void AddFilter(HttpRequestMessage message)
        {
            var filters = new Dictionary<string, string>();

            if (VendorName != null)
            {
                filters.Add("vendor_name" + SearchVariantValue(VendorName.Item1), VendorName.Item2);
            }

            if (ReferenceNumber != null)
            {
                filters.Add("reference_number" + SearchVariantValue(ReferenceNumber.Item1), ReferenceNumber.Item2);
            }

            if (PaymentNumber != null)
            {
                filters.Add("payment_number" + SearchVariantValue(PaymentNumber.Item1), PaymentNumber.Item2);
            }

            if (Date != null)
            {
                filters.Add("date" + DateVariantValue(Date.Item1), Date.Item2.ToString());
            }

            if (Amount != null)
            {
                filters.Add("amount" + JsonConvert.SerializeObject(PaymentNumber.Item1).Trim('\"'), PaymentNumber.Item2);
            }

            if (Notes != null)
            {
                filters.Add("notes" + SearchVariantValue(Notes.Item1), Notes.Item2);
            }

            if (VendorId != null)
            {
                filters.Add("payment_number", VendorId);
            }

            if (LastModifiedTime != null)
            {
                filters.Add("last_modified_time", LastModifiedTime.Value.ToString());
            }

            if (BillId != null)
            {
                filters.Add("bill_id", BillId);
            }

            if (Description != null)
            {
                filters.Add("description" + SearchVariantValue(Description.Item1), Description.Item2);
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
