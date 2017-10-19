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
using ZohoBooks4Net.Domain.Enumeration.CustomerPayments;
using ZohoBooks4Net.Domain.Enumeration;

namespace ZohoBooks4Net.Filters
{
    public class CustomerPaymentsFilter : PaginationFilter
    {
        /// <summary>
        /// Search payments by customer name. Variants: customer_name_startswith and customer_name_contains. Max-len [100]
        /// </summary>
        public Tuple<SearchVariant, string> CustomerName { get; set; }

        /// <summary>
        /// Search payments by reference number. Variants: reference_number_startswith and reference_number_contains. Max-len [100]
        /// </summary>
        public Tuple<SearchVariant, string> ReferenceNumber { get; set; }

        /// <summary>
        /// Date on which payment is made. Format [yyyy-mm-dd]
        /// </summary>
        public DateTime? Date { get; set; }

        /// <summary>
        /// Search payments by payment amount. Variants: amount_less_than, amount_less_equals, amount_greater_than and amount_greater_equals
        /// </summary>
        public double? Amount { get; set; }

        /// <summary>
        /// Search payments by customer notes. Variants: notes_startswith and notes_contains
        /// </summary>
        public Tuple<SearchVariant, string> Notes { get; set; }

        /// <summary>
        /// Search payments by payment mode. Variants: payment_mode_startswith and payment_mode_contains
        /// </summary>
        public Tuple<SearchVariant, string> PaymentMode { get; set; }

        /// <summary>
        /// Customer ID of the customer involved in the payment.
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// Filter payments by mode.Allowed Values: PaymentMode.All, PaymentMode.Check, PaymentMode.Cash, PaymentMode.BankTransfer, 
        /// PaymentMode.Paypal, PaymentMode.CreditCard, PaymentMode.GoogleCheckout, PaymentMode.Credit, PaymentMode.Authorizenet,
        /// PaymentMode.BankRemittance, PaymentMode.Payflowpro, PaymentMode.Stripe, PaymentMode.TwoCheckout, PaymentMode.Braintree,
        /// and PaymentMode.Others
        /// </summary>
        public PaymentMode? FilterBy { get; set; }

        public CustomerPaymentsSortColumn? SortColumn { get; set; }

        public override void AddFilter(HttpRequestMessage message)
        {
            var filters = new Dictionary<string, string>();

            if (CustomerName != null)
            {
                filters.Add("customer_name" + SearchVariantValue(CustomerName.Item1), CustomerName.Item2);
            }

            if (ReferenceNumber!= null)
            {
                filters.Add("reference_number" + SearchVariantValue(ReferenceNumber.Item1), ReferenceNumber.Item2);
            }
            
            if (Date != null)
            {
                filters.Add("date", Date.Value.ToString());
            }

            if (Notes != null)
            {
                filters.Add("notes" + SearchVariantValue(Notes.Item1), Notes.Item2);
            }

            if (PaymentMode != null)
            {
                filters.Add("payment_mode" + SearchVariantValue(PaymentMode.Item1), PaymentMode.Item2);
            }

            if (CustomerId != null)
            {
                filters.Add("customer_id", CustomerId);
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
