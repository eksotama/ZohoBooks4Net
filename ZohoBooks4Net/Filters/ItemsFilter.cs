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
using ZohoBooks4Net.Domain.Enumeration.Items;

namespace ZohoBooks4Net.Filters
{
    public class ItemsFilter : PaginationFilter
    {
        /// <summary>
        /// Search items by name. Max-length [100]. Variants: name_startswith and name_contains
        /// </summary>
        public Tuple<SearchVariant, string> Name { get; set; }

        /// <summary>
        /// Search items by description. Max-length [100]. Variants: description_startswith and description_contains
        /// </summary>
        public Tuple<SearchVariant, string> Description { get; set; }

        /// <summary>
        /// Search items by rate. Variants: rate_less_than, rate_less_equals, rate_greater_than and rate_greater_equals
        /// </summary>
        public Tuple<NumericalVariant, string> Rate { get; set; }

        /// <summary>
        /// Search items by tax id.
        /// </summary>
        public string TaxId { get; set; }

        public string TaxName { get; set; }

        /// <summary>
        /// ID of the account to which the item has to be associated with.
        /// </summary>
        public string AccountId { get; set; }

        /// <summary>
        /// Search items by name or description. Max-length [100]
        /// </summary>
        public string SearchText { get; set; }

        /// <summary>
        /// Filter items by status. Allowed Values: Status.All, Status.Active and Status.Inactive
        /// </summary>
        public ItemsFilterBy? FilterBy { get; set; }

        /// <summary>
        /// Sort items. Allowed Values: name, rate and tax_name
        /// </summary>
        public ItemsSortColumn? SortColumn { get; set; }

        public override void AddFilter(HttpRequestMessage message)
        {
            var filters = new Dictionary<string, string>();

            if (Name != null)
            {
                filters.Add("name" + SearchVariantValue(Name.Item1), Name.Item2);
            }

            if (Description != null)
            {
                filters.Add("description" + SearchVariantValue(Description.Item1), Description.Item2);
            }

            if (Rate != null)
            {
                filters.Add("rate" + JsonConvert.SerializeObject(Rate.Item1).Trim('\"'), Rate.Item2);
            }

            if (TaxId != null)
            {
                filters.Add("tax_id", TaxId);
            }

            if (TaxName != null)
            {
                filters.Add("tax_name", TaxName);
            }

            if (AccountId != null)
            {
                filters.Add("account_id", AccountId);
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
