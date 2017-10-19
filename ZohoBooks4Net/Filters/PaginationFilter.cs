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

using System.Collections.Generic;
using System.Net.Http;

namespace ZohoBooks4Net.Filters
{
    public class PaginationFilter : Filter, IPaginationFilter
    {
        /// <summary>
        /// The current page.
        /// </summary>
        public int? Page { get; set; }

        /// <summary>
        /// How many results to display per page.
        /// </summary>
        public int? PerPage { get; set; }

        public override void AddFilter(HttpRequestMessage message, IDictionary<string, string> filters)
        {
            if (Page != null)
            {
                filters.Add("page", Page.Value.ToString());
            }

            if (PerPage != null)
            {
                filters.Add("per_page", PerPage.Value.ToString());
            }

            base.AddFilter(message, filters);
        }
    }
}
