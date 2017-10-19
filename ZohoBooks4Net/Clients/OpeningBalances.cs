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

using System.Threading.Tasks;
using ZohoBooks4Net.Clients.Interfaces;
using ZohoBooks4Net.Domain.Entities;
using ZohoBooks4Net.Responses;

namespace ZohoBooks4Net.Clients
{
    public class OpeningBalances : ZohoBooksClientBase,
        ICreates<OpeningBalance>, IGets<OpeningBalance>, IUpdates<OpeningBalance>, IDeletes<OpeningBalance>
    {
        public OpeningBalances(Configuration configuration) : base(configuration)
        {
            BaseUri = "settings/openingbalances";
        }

        /// <summary>
        /// Creates opening balance with the given information. 
        /// </summary>
        /// <param name="newItem"></param>
        /// <returns></returns>
        public async Task<OpeningBalance> CreateAsync(OpeningBalance newItem)
        {
            var response = await PostDataAsync<OpeningBalance, ZohoBooksResponse<OpeningBalance>>(newItem, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Get opening balance. 
        /// </summary>
        /// <param name="id">The id of the opening balance to get.</param>
        /// <returns>The requested opening balance.</returns>
        public async Task<OpeningBalance> GetAsync(string id)
        {
            var response = await GetDataAsync<ZohoBooksResponse<OpeningBalance>>(id, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Updates the existing opening balance information 
        /// </summary>
        /// <param name="id">The id of the opening balance to update.</param>
        /// <param name="item">The opening balance details to update with.</param>
        /// <returns>The opening balance details after going through the Zoho service.</returns>
        public async Task<OpeningBalance> UpdateAsync(string id, OpeningBalance item)
        {
            var response = await PutDataAsync<OpeningBalance, ZohoBooksResponse<OpeningBalance>>(id, item, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Delete the entered opening balance 
        /// </summary>
        /// <param name="id">The id of the opening balance to delete.</param>
        /// <returns>A response indicating if the request was succesful.</returns>
        public async Task<bool> DeleteAsync(string id)
        {
            var response = await DeleteDataAsync<ZohoBooksMessage>(id, OrganizationIdFilter);
            return response.Code == 0;
        }
    }
}
