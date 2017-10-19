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
using ZohoBooks4Net.Domain.Enumeration.Journals;
using ZohoBooks4Net.Domain.Enumeration.Variants;

namespace ZohoBooks4Net.Filters
{
    public class JournalsFilter : PaginationFilter
    {
        /// <summary>
        /// Search journals by journal entry number. Variants: entry_number_startswith and entry_number_contains
        /// </summary>
        public Tuple<SearchVariant, string> EntryNumber { get; set; }

        /// <summary>
        /// Search journals by journal reference number. Variants: reference_number_startswith and reference_number_contains
        /// </summary>
        public Tuple<SearchVariant, string> ReferenceNumber { get; set; }

        /// <summary>
        /// Search journals by journal date. Variants: date_start, date_end, date_before and date_after
        /// </summary>
        public Tuple<DateVariant, DateTime> Date { get; set; }

        /// <summary>
        /// Search journals by journal notes. Variants: notes_startswith and notes_contains
        /// </summary>
        public Tuple<SearchVariant, string> Notes { get; set; }

        /// <summary>
        /// Search the journals using Last Modified Time
        /// </summary>
        public DateTime LastModifiedTime { get; set; }

        /// <summary>
        /// Search journals by journal total. Variants: total_less_than, total_less_equals, total_greater_than and
        /// total_greater_equals

        /// </summary>
        public Tuple<NumericalVariant, double> Total { get; set; }

        /// <summary>
        /// Search Journals using Customer ID
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// Filter journals by journal date. Allowed Values: JournalDate.All, JournalDate.Today, JournalDate.ThisWeek, 
        /// JournalDate.ThisMonth, JournalDate.ThisQuarter and JournalDate.ThisYear
        /// </summary>
        public JournalsFilterBy? FilterBy { get; set; }

        /// <summary>
        /// Sort journal list. Allowed Values: journal_date, entry_number, reference_number and total
        /// </summary>
        public JournalsSortColumn? SortColumn { get; set; }

        public override void AddFilter(HttpRequestMessage message)
        {
            var filters = new Dictionary<string, string>();

            if (EntryNumber != null)
            {
                filters.Add("entry_number" + SearchVariantValue(EntryNumber.Item1), EntryNumber.Item2);
            }

            if (ReferenceNumber != null)
            {
                filters.Add("reference_number" + SearchVariantValue(ReferenceNumber.Item1), ReferenceNumber.Item2);
            }

            if (Date != null)
            {
                filters.Add("date" + DateVariantValue(Date.Item1), Date.Item2.ToString());
            }

            if (Notes != null)
            {
                filters.Add("notes" + SearchVariantValue(Notes.Item1), Notes.Item2);
            }

            if (LastModifiedTime != null)
            {
                filters.Add("last_modified_time", LastModifiedTime.ToString());
            }

            if (Total != null)
            {
                filters.Add("total" + JsonConvert.SerializeObject(Total).Trim('\"'), Total.ToString());
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
