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

using System;
using ZohoBooks4Net.Filters;

namespace ZohoBooks4Net.Clients
{
    /// <summary>
    /// This class exists because the OrganizationIdFilter should be in every client.
    /// </summary>
    public abstract class ZohoBooksClientBase : ClientBase
    {
        /// <summary>
        /// Adds the OrganizationId to the request.
        /// </summary>
        public Filter OrganizationIdFilter { get; private set; }

        public ZohoBooksClientBase(Configuration configuration) : base(configuration)
        {
            configuration.BaseUri = new Uri("https://books.zoho.com/api/v3/", UriKind.Absolute);
            AuthenticationDetails = new Tuple<string, string>("Zoho-authtoken", configuration.UserApiKey);
            OrganizationIdFilter = new Filter { OrganizationId = configuration.UserName };
        }

        /// <summary>
        /// A method that sets up the filter with the default arguments.
        /// </summary>
        /// <param name="filter">A reference to the filter to change.</param>
        /// <param name="page">The page number to get.</param>
        /// <param name="pageSize">The size of the page to return.</param>
        /// <returns>The new ContactsFilter.</returns>
        protected virtual IPaginationFilter SetupFilter(IFilter filter, int page, int pageSize = 100)
        {
            var pageFilter = (filter == null) ? new PaginationFilter() : (PaginationFilter)filter;
            pageFilter.Page = page;
            pageFilter.PerPage = pageSize;
            pageFilter.OrganizationId = OrganizationIdFilter.OrganizationId;

            return pageFilter;
        }
    }
}
