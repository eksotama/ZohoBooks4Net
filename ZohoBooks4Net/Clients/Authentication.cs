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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ZohoBooks4Net.Filters;

namespace ZohoBooks4Net.Clients
{
    public class Authentication : ClientBase
    {
        /// <summary>
        /// Adds the OrganizationId to the request.
        /// </summary>
        public Filter OrganizationIdFilter { get; private set; }

        public Authentication(Configuration configuration) : base(configuration) { }
        
        public async Task<bool> Authenticate(string username, string password)
        {
            var tempBaseUri = Configuration.BaseUri;
            var tempAuthentication = AuthenticationDetails;
            var tempOrganizationIdFilter = OrganizationIdFilter;

            AuthenticationDetails = null;
            OrganizationIdFilter = null;
            Configuration.BaseUri = new Uri("https://accounts.zoho.com/", UriKind.Absolute);

            var response = await PostDataAsync("apiauthtoken/nb/create",
                string.Format("?SCOPE=ZohoBooks/booksapi&EMAIL_ID={0}&PASSWORD={1}&organization_id={2}",
                    username, password, OrganizationIdFilter.OrganizationId), 
                null,
                RequestFormat.FormUrlEncoded);
            
            var result = Regex.Match(response, @"RESULT=(TRUE|FALSE)");
            Configuration.BaseUri = tempBaseUri;

            if (!result.Success)
            {
                // Set these back to the way they were if the request was unsuccessful (unsure if they were previously set)
                AuthenticationDetails = tempAuthentication;
                OrganizationIdFilter = tempOrganizationIdFilter;
                return false;
            }

            var isSuccess = result.Groups[0].Value.Substring("RESULT=".Length - 1) == "TRUE";

            Configuration.UserName = username;
            Configuration.UserApiKey = Regex.Match(response, @"AUTHTOKEN=.*").Value.Substring("AUTHTOKEN=".Length - 1);

            AuthenticationDetails = new Tuple<string, string>("Zoho-authtoken", Configuration.UserApiKey);
            OrganizationIdFilter = new Filter { OrganizationId = Configuration.UserName };

            return true;
        }
    }
}
