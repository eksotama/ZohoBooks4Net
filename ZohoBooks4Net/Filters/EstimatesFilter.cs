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
using ZohoBooks4Net.Domain.Enumeration.Estimates;

namespace ZohoBooks4Net.Filters
{
    public class EstimatesFilter : PaginationFilter
    {
        /// <summary>
        /// Search estimates by estimate number.Variantsestimate_number_startswith and estimate_number_contains
        /// </summary>
        public Tuple<SearchVariant, string> EstimateNumber { get; set; }

        /// <summary>
        /// Search estimates by reference number.Variants reference_number_startswith and reference_number_contains
        /// </summary>
        public Tuple<SearchVariant, string> ReferenceNumber { get; set; }

        /// <summary>
        /// Search estimates by customer name.Variants customer_name_startswith and customer_name_contains
        /// </summary>
        public Tuple<SearchVariant, string> CustomerName { get; set; }

        /// <summary>
        /// Search estimates by estimate total.Variants total_less_than, total_less_equals, total_greater_than and total_greater_equals
        /// </summary>
        public Tuple<NumericalVariant, double> Total { get; set; }

        /// <summary>
        /// Search estimates by customer id..
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// ID of the item.
        /// </summary>
        public string ItemId { get; set; }

        /// <summary>
        /// Search estimates by item name.Variants item_name_startswith and item_name_contains
        /// </summary>
        public Tuple<SearchVariant, string> ItemName { get; set; }

        /// <summary>
        /// Search estimates by item description.Variantsitem_description_startswith and item_description_contains
        /// </summary>
        public Tuple<SearchVariant, string> ItemDescription { get; set; }

        /// <summary>
        /// Search estimates by custom field.Variantscustom_field_startswith and custom_field_contains
        /// </summary>
        public Tuple<SearchVariant, string> CustomField { get; set; }

        /// <summary>
        /// The date of expiration of the estimates
        /// </summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// Search estimates by estimate date.Variants date_start, date_end, date_before and date_after
        /// </summary>
        public Tuple<DateVariant, DateTime> Date { get; set; }

        /// <summary>
        /// Search estimates by status.Allowed Valuesdraft, sent, invoiced , accepted, declined and expired
        /// </summary>
        public EstimateStatus? Status { get; set; }

        /// <summary>
        /// Filter estimates by status.Allowed Values Status.All, Status.Sent, Status.Draft, Status.Invoiced, Status.Accepted,
        /// Status.Declined and Status.Expired
        /// </summary>
        public EstimatesFilterBy? FilterBy { get; set; }

        /// <summary>
        /// Search estimates by estimate number or reference or customer name.
        /// </summary>
        public string SearchText { get; set; }

        /// <summary>
        /// Sort estimates. Allowed Values customer_name, estimate_number, date, total and created_time
        /// </summary>
        public EstimatesSortColumn? SortColumn { get; set; }

        public override void AddFilter(HttpRequestMessage message)
        {
            var filters = new Dictionary<string, string>();

            if (EstimateNumber != null)
            {
                filters.Add("estimate_number" + SearchVariantValue(EstimateNumber.Item1).Trim('\"'), EstimateNumber.Item2);
            }

            if (ReferenceNumber != null)
            {
                filters.Add("reference_number" + SearchVariantValue(ReferenceNumber.Item1).Trim('\"'), ReferenceNumber.Item2);
            }

            if (CustomerName != null)
            {
                filters.Add("customer_name" + SearchVariantValue(CustomerName.Item1).Trim('\"'), CustomerName.Item2);
            }

            if (ItemName != null)
            {
                filters.Add("item_name" + SearchVariantValue(ItemName.Item1).Trim('\"'), ItemName.Item2);
            }

            if (ItemDescription != null)
            {
                filters.Add("item_description" + SearchVariantValue(ItemDescription.Item1).Trim('\"'), ItemName.Item2);
            }

            if (CustomField != null)
            {
                filters.Add("custom_field" + SearchVariantValue(CustomField.Item1).Trim('\"'), CustomField.Item2);
            }

            if (ExpiryDate != null)
            {
                filters.Add("expiry_date", ExpiryDate.Value.ToString());
            }

            if (Date != null)
            {
                filters.Add("date" + DateVariantValue(Date.Item1), Date.Item2.ToString());
            }

            if (Status != null)
            {
                filters.Add("status", JsonConvert.SerializeObject(Status));
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
                filters.Add("sort_column", JsonConvert.SerializeObject(SortColumn.Value).Trim('\"'));
            }

            base.AddFilter(message, filters);
        }
    }
}
