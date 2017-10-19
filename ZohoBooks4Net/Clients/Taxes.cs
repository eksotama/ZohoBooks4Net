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
using System.Threading.Tasks;
using ZohoBooks4Net.Clients.Interfaces;
using ZohoBooks4Net.Domain.Entities;
using ZohoBooks4Net.Filters;
using ZohoBooks4Net.Responses;
using ZohoBooks4Net.Responses.PaginatedResponses;

namespace ZohoBooks4Net.Clients
{
    public class Taxes : ZohoBooksClientBase, IGetsPaginatedResponses<Tax>,
        ICreates<Tax>, IGets<Tax>, IUpdates<Tax>, IDeletes<Tax>
    {
        public new string BaseUri { get; set; } = "settings/taxes";

        public Taxes(Configuration configuration) : base(configuration) { }

        /// <summary>
        /// Create a tax which can be associated with an item. 
        /// </summary>
        /// <param name="newItem"></param>
        /// <returns>A response with the tax after it passes through the Zoho service.</returns>
        public async Task<Tax> CreateAsync(Tax newItem)
        {
            var response = await PostDataAsync<Tax, ZohoBooksResponse<Tax>>(BaseUri, newItem, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Get the details of a simple or compound tax. 
        /// </summary>
        /// <param name="id">The id of the tax to get.</param>
        /// <returns>A response with the requested tax.</returns>
        public async Task<Tax> GetAsync(string id)
        {
            var response = await GetDataAsync<ZohoBooksResponse<Tax>>(string.Format("{0}/{1}", BaseUri, id), OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Get the details of a simple or compound tax. 
        /// </summary>
        /// <param name="id">The id of the tax to update.</param>
        /// <param name="item">The data to update the selected tax with.</param>
        /// <returns>A response with the updated tax details after is passes through the Zoho service.</returns>
        public async Task<Tax> UpdateAsync(string id, Tax item)
        {
            var response = await PutDataAsync<Tax, ZohoBooksResponse<Tax>>(string.Format("{0}/{1}", BaseUri, id), item, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Delete a simple or compound tax. 
        /// </summary>
        /// <param name="id">The id of the tax to delete.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> DeleteAsync(string id)
        {
            var response = await DeleteDataAsync<ZohoBooksMessage>(string.Format("{0}/{1}", BaseUri, id), OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Create a tax group associating multiple taxes.. 
        /// </summary>
        /// <param name="newItem">The new tax group details.</param>
        /// <returns>The tax group details after it passes through the Zoho service.</returns>
        public async Task<TaxGroup> CreateTaxGroupAsync(TaxGroup newItem)
        {
            var response = await PostDataAsync<TaxGroup, ZohoBooksResponse<TaxGroup>>("settings/taxgroups", newItem, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Get the details of a tax group. 
        /// </summary>
        /// <param name="id">The id of the tax group to get.</param>
        /// <returns>A response with the requested tax group.</returns>
        public async Task<TaxGroup> GetTaxGroupAsync(string id)
        {
            var response = await GetDataAsync<ZohoBooksResponse<TaxGroup>>(string.Format("settings/taxgroups/{0}", id), OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Update the details of the tax group. 
        /// </summary>
        /// <param name="id">The id of the tax group to update.</param>
        /// <param name="item">The details of the tax group to update with.</param>
        /// <returns>A response with the updated tax group details after it passes through the Zoho service.</returns>
        public async Task<TaxGroup> UpdateTaxGroupAsync(string id, TaxGroup item)
        {
            var response = await PutDataAsync<TaxGroup, ZohoBooksResponse<TaxGroup>>(string.Format("settings/taxgroups/{0}", id), item, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Delete a tax group. Tax group that is associated to transactions cannot be deleted. 
        /// </summary>
        /// <param name="id">The id of the tax group to delete.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> DeleteTaxGroupAsync(string id)
        {
            var response = await DeleteDataAsync<ZohoBooksMessage>(string.Format("settings/taxgroups/{0}", id), OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Get the details of a tax authority. 
        /// </summary>
        /// <param name="newItem">The details of the new tax authority.</param>
        /// <returns>A response with the new tax authority details after it passes through the Zoho service.</returns>
        public async Task<TaxAuthority> CreateTaxAuthorityAsync(TaxAuthority newItem)
        {
            var response = await PostDataAsync<TaxAuthority, ZohoBooksResponse<TaxAuthority>>("settings/taxauthorities", newItem, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// List of tax authorities 
        /// </summary>
        /// <returns>A response with the requested tax authorities.</returns>
        public async Task<IList<TaxAuthority>> ListTaxAuthoritiesAsync()
        {
            var response = await GetDataAsync<ZohoBooksResponse<IList<TaxAuthority>>>("settings/taxauthorities", OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Delete a tax authority.
        /// </summary>
        /// <param name="id">The id of the tax authority to delete.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> DeleteTaxAuthorityAsync(string id)
        {
            var response = await DeleteDataAsync<ZohoBooksMessage>(string.Format("settings/taxauthorities/{0}", id), OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Create a tax exemption. 
        /// </summary>
        /// <param name="newItem">The new tax exemption details.</param>
        /// <returns>The tax exemption details after they have passed through the Zoho service.</returns>
        public async Task<TaxExemption> CreateTaxExemptionAsync(TaxExemption newItem)
        {
            var response = await PostDataAsync<TaxExemption, ZohoBooksResponse<TaxExemption>>("settings/taxexemptions", newItem, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Create a tax exemption. 
        /// </summary>
        /// <param name="id">The id of the tax exemption to get.</param>
        /// <returns>A response with the data of the requested tax exemption.</returns>
        public async Task<TaxExemption> GetTaxExemptionAsync(string id)
        {
            var response = await GetDataAsync<ZohoBooksResponse<TaxExemption>>(string.Format("settings/taxexemptions/{0}", id), OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// List of tax exemptions. 
        /// </summary>
        /// <returns>A list of tax exemptions.</returns>
        public async Task<IList<TaxExemption>> ListTaxExemptionsAsync()
        {
            var response = await GetDataAsync<ZohoBooksResponse<IList<TaxExemption>>>(string.Format("settings/taxexemptions"), OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Update the details of a tax exemption. 
        /// </summary>
        /// <param name="id">The id of the tax exemption to update.</param>
        /// <param name="item">The tax exemption details to update the selected tax exemption with.</param>
        /// <returns>The tax exemption details after they have passed through the Zoho service.</returns>
        public async Task<TaxExemption> UpdateTaxExemptionAsync(string id, TaxExemption item)
        {
            var response = await PostDataAsync<TaxExemption, ZohoBooksResponse<TaxExemption>>
                (string.Format("settings/taxexemptions/{0}", id), item, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Delete a tax exemption. 
        /// </summary>
        /// <param name="id">The id of the tax exemption to delete.</param>
        /// <returns>A response indicating if the request was successful.</returns>
        public async Task<bool> DeleteTaxExemptionAsync(string id)
        {
            var response = await DeleteDataAsync<ZohoBooksMessage>(string.Format("settings/taxexemptions/{0}", id), OrganizationIdFilter);
            return response.Code == 0;
        }

        public async Task<IList<Tax>> GetAllPagesAsync(IFilter filter)
        {
            var allPages = new List<Tax>();
            var currentPage = new PaginatedResponse<Tax>();
            var currentPageNum = currentPage.Context.Page;
            var taxesFilter = SetupFilter(filter, currentPage.Context.Page);

            while ((currentPage = await GetDataAsync<PaginatedResponse<Tax>>(taxesFilter)).Context.HasMorePage)
            {
                allPages.AddRange(currentPage.Resource);
                taxesFilter.Page = currentPageNum++;
            }
            return allPages;
        }

        public async Task<IList<Tax>> GetPageAsync(int page, int pageSize = 100, IFilter filter = null)
        {
            var result = await GetDataAsync<PaginatedResponse<Tax>>(SetupFilter(filter, page));
            return result.Resource;
        }

        public async Task<IList<Tax>> GetPageRangeAsync(int start, int end, int pageSize = 100, IFilter filter = null)
        {
            var pageRange = new List<Tax>();
            var currentPage = new PaginatedResponse<Tax>();
            var currentPageNum = currentPage.Context.Page;
            var taxesFilter = SetupFilter(filter, currentPageNum);

            while ((currentPage = await GetDataAsync<PaginatedResponse<Tax>>(taxesFilter)).Context.Page <= end)
            {
                pageRange.AddRange(currentPage.Resource);
                taxesFilter.Page = currentPageNum++;
            }

            return pageRange;
        }
    }
}
