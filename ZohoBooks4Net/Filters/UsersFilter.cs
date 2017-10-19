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
using System.Collections.Generic;
using System.Net.Http;
using ZohoBooks4Net.Domain.Enumeration.Users;

namespace ZohoBooks4Net.Filters
{
    public class UsersFilter : PaginationFilter
    {
        public UsersFilterBy? FilterBy { get; set; }

        public UsersSortColumn? SortColumn { get; set; }

        public override void AddFilter(HttpRequestMessage message)
        {
            var filters = new Dictionary<string, string>();

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
