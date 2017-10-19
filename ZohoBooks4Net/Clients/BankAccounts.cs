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

namespace ZohoBooks4Net.Clients
{
    public class BankAccounts : ZohoBooksClientBase, IListsItemsWithFilters<BankAccount>,
        ICreates<BankAccount>, IGets<BankAccount>, IUpdates<BankAccount>, IDeletes<BankAccount>
    {
        public BankAccounts(Configuration configuration) : base(configuration)
        {
            BaseUri = "bankaccounts";
        }

        /// <summary>
        /// Create a bank account or a credit card account for your organization. 
        /// </summary>
        /// <param name="newItem">The details of the new bank account.</param>
        /// <returns></returns>
        public async Task<BankAccount> CreateAsync(BankAccount newItem)
        {
            var response = await PostDataAsync<BankAccount, ZohoBooksResponse<BankAccount>>(newItem, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Get a detailed look of the account specified. 
        /// </summary>
        /// <param name="id">The id of the bank account.</param>
        /// <returns>A response with the bank account details.</returns>
        public async Task<BankAccount> GetAsync(string id)
        {
            var response = await GetDataAsync<ZohoBooksResponse<BankAccount>>(id, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Modify the account that was created. 
        /// </summary>
        /// <param name="id">The id of the bank account to update.</param>
        /// <param name="item">The details of the bank account to update with.</param>
        /// <returns>A response with the updated bank account data after it runs through the Zoho service.</returns>
        public async Task<BankAccount> UpdateAsync(string id, BankAccount item)
        {
            var response = await PutDataAsync<BankAccount, ZohoBooksResponse<BankAccount>>(id, item, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Delete a bank account from your organization. 
        /// </summary>
        /// <param name="id">The id of the bank account to delete.</param>
        /// <returns>A response indicating if the request was successful.</returns>
        public async Task<bool> DeleteAsync(string id)
        {
            var response = await DeleteDataAsync<ZohoBooksMessage>(id, OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// List all bank and credit card accounts for your organization. 
        /// </summary>
        /// <param name="filter">The bank accounts filter.</param>
        /// <returns>A response listing all the bank accounts.</returns>
        public async Task<IList<BankAccount>> GetItemsAsync(IFilter filter)
        {
            var bankAccountsFilter = SetupFilter(filter, 0) as BankAccountsFilter;
            var response = await GetDataAsync<ZohoBooksResponse<IList<BankAccount>>>(OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Make an account inactive. 
        /// </summary>
        /// <param name="bankAccountId">The id of the bank account to deactivate.</param>
        /// <returns>A response indicating if the request was successful.</returns>
        public async Task<bool> DeactivateAccountAsync(string bankAccountId)
        {
            var response = await PostAsync<ZohoBooksMessage>(string.Format("{0}/inactive", bankAccountId), OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Make an account active. 
        /// </summary>
        /// <param name="bankAccountId">The id of the bank account to activate.</param>
        /// <returns>A response indicating if the request was successful.</returns>
        public async Task<bool> ActivateAccountAsync(string bankAccountId)
        {
            var response = await PostAsync<ZohoBooksMessage>(string.Format("{0}/active", bankAccountId), OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Get the details of previously imported statement for the account. 
        /// </summary>
        /// <param name="bankAccountId">The id of the bank account whose statement should be returned.</param>
        /// <returns>The requested statement.</returns>
        public async Task<Statement> GetLastImportedStatementAsync(string bankAccountId)
        {
            var response = await GetDataAsync<ZohoBooksResponse<Statement>>
                (string.Format("{0}/statement/lastimported", bankAccountId), OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Delete the statement that was previously imported.  
        /// </summary>
        /// <param name="bankAccountId">The id of the bank account whose statement should be deleted.</param>
        /// <param name="statementId">The id of the statement to delete></param>
        /// <returns>The requested statement.</returns>
        public async Task<bool> DeleteLastImportedStatementAsync(string bankAccountId, string statementId)
        {
            var response = await DeleteDataAsync<ZohoBooksMessage>
                (string.Format("{0}/statement/{1}", bankAccountId, statementId), OrganizationIdFilter);
            return response.Code == 0;
        }
    }
}
