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

using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZohoBooks4Net.Clients.Interfaces;
using ZohoBooks4Net.Domain.Entities;
using ZohoBooks4Net.Filters;
using ZohoBooks4Net.Requests;
using ZohoBooks4Net.Responses;

namespace ZohoBooks4Net.Clients
{
    public class BankTransactions : ZohoBooksClientBase, IListsItemsWithFilters<BankTransaction>,
        ICreates<BankTransaction>, IGets<BankTransaction>, IUpdates<BankTransaction>, IDeletes<BankTransaction>
    {
        public BankTransactions(Configuration configuration) : base(configuration)
        {
            BaseUri = "banktransactions";
        }

        /// <summary>
        /// Create a bank transaction based on the allowed transaction types. 
        /// </summary>
        /// <param name="newItem">The new bank transaction details</param>
        /// <returns>A response with the new bank transaction after passing through the Zoho service.</returns>
        public async Task<BankTransaction> CreateAsync(BankTransaction newItem)
        {
            var response = await PostDataAsync<BankTransaction, ZohoBooksResponse<BankTransaction>>(newItem, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Fetch the details of a transaction by specifying the transaction_id. 
        /// </summary>
        /// <param name="id">The id of the bank transaction to get.</param>
        /// <returns>A response with the requested bank transaction.</returns>
        public async Task<BankTransaction> GetAsync(string id)
        {
            var response = await GetDataAsync<ZohoBooksResponse<BankTransaction>>(id, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Make changes in the applicable fields of a transaction and update it. 
        /// </summary>
        /// <param name="id">The id of the bank transaction to update.</param>
        /// <param name="item">The details of the bank transaction.</param>
        /// <returns>A response with the bank transaction after passing through the Zoho service.</returns>
        public async Task<BankTransaction> UpdateAsync(string id, BankTransaction item)
        {
            var response = await PutDataAsync<BankTransaction, ZohoBooksResponse<BankTransaction>>(id, item, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Delete a transaction from an account by specifying the transaction_id. 
        /// </summary>
        /// <param name="id">The id of the bank transaction to delete.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> DeleteAsync(string id)
        {
            var response = await DeleteDataAsync<ZohoBooksMessage>(id, OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Get all the transaction details involved in an account. 
        /// </summary>
        /// <param name="filter">The bank transaction filter.</param>
        /// <returns>A response with the filtered items.</returns>
        public async Task<IList<BankTransaction>> GetItemsAsync(IFilter filter)
        {
            var bankTransactionsFilter = filter as BankTransactionsFilter;
            bankTransactionsFilter.OrganizationId = OrganizationIdFilter.OrganizationId;

            var response = await GetDataAsync<ZohoBooksResponse<IList<BankTransaction>>>(filter);
            return response.Resource;
        }

        /// <summary>
        /// Provide criteria to search for matching uncategorised transactions. The list of transactions can also include 
        /// invoices/bills/credit-notes which will not be matched directly. Instead, a new (payment/refund) transaction is recorded
        /// and matched. 
        /// </summary>
        /// <param name="transactionId">The id of the transaction to find matching</param>
        /// <param name="filter">The bank transaction filter.</param>
        /// <returns>A response with the filtered items.</returns>
        public async Task<IList<BankTransaction>> GetMatchingItemsAsync(string transactionId, IFilter filter)
        {
            var matchingBankTransactionsFilter = (MatchingBankTransactionsFilter)filter;
            matchingBankTransactionsFilter.OrganizationId = OrganizationIdFilter.OrganizationId;

            var response = await GetDataAsync<ZohoBooksResponse<IList<BankTransaction>>>
                (string.Format("uncategorized/{0}/match", transactionId), matchingBankTransactionsFilter);
            return response.Resource;
        }

        /// <summary>
        /// Match an uncategorized transaction with an existing transaction in the account. 
        /// </summary>
        /// <param name="transactionId">The id of the transaction to match.</param>
        /// <param name="transactionMatchCriteria">The criteria to match the transaction with.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> MatchATransactionAsync(string transactionId, IList<TransactionMatchCriteria> transactionMatchCriteria)
        {
            var requestData = new JObject
            {
                ["transactions_to_be_matched"] = new JArray(transactionMatchCriteria)
            };

            var response = await PostDataAsync<JObject, ZohoBooksMessage>
                (string.Format("uncategorized/{0}/match", transactionId), requestData, OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Match an uncategorized transaction with an existing transaction in the account. 
        /// </summary>
        /// <param name="transactionId">The id of the transaction to unmatch.</param>
        /// <param name="accountId">An optional parameter for account id of the transactions.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> UnmatchAMatchedTransaction(string transactionId, string accountId = null)
        {
            var requestUri = string.Format("{0}/unmatch?organization_id={1}", transactionId, OrganizationIdFilter.OrganizationId);

            if (accountId != null)
            {
                requestUri += "&account_id=" + accountId;
            }

            var response = await PostAsync<ZohoBooksMessage>(requestUri);
            return response.Code == 0;
        }

        /// <summary>
        /// Restore an excluded transaction in your account. 
        /// </summary>
        /// <param name="transactionId">The id of the transaction to restore.</param>
        /// <param name="accountId">An optional parameter for account id of the transactions.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> RestoreATransaction(string transactionId, string accountId = null)
        {
            var requestUri = string.Format("{0}/restore?organization_id={1}", transactionId, OrganizationIdFilter.OrganizationId);

            if (accountId != null)
            {
                requestUri += "&account_id=" + accountId;
            }

            var response = await PostAsync<ZohoBooksMessage>(requestUri);
            return response.Code == 0;
        }

        /// <summary>
        /// Categorize an uncategorized transaction by creating a new transaction. 
        /// </summary>
        /// <param name="transactionId">The id of the transaction.</param>
        /// <param name="uncategorizedTransaction">The data of the uncategorized transaction to overwrite.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> CategorizeAnUncategorizedTransaction(string transactionId, BankTransaction uncategorizedTransaction)
        {
            var response = await PostDataAsync<BankTransaction, ZohoBooksMessage>
                (string.Format("uncategorized/{0}/categorize", transactionId), uncategorizedTransaction, OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Categorize an Uncategorized transaction as expense
        /// </summary>
        /// <param name="transactionId">The id of the transaction to restore.</param>
        /// <param name="expense">The expense data to use.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> CategorizeAsExpense(string transactionId, Expense expense)
        {
            // TODO Double check this for missing fields - https://www.zoho.com/books/api/v3/#Bank-Transactions_Categorize_as_expense
            var response = await PostDataAsync<Expense, ZohoBooksMessage>
                (string.Format("uncategorized/{0}/categorize/expenses", transactionId), expense, OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Categorize an uncategorized transaction by creating a new transaction. 
        /// </summary>
        /// <param name="transactionId">The id of the transaction.</param>
        /// <param name="accountId">An optional parameter for account id of the transaction.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> UncategorizeACategorizedTransaction(string transactionId, string accountId)
        {
            var requestUri = string.Format("{0}/restore?organization_id={1}", transactionId, OrganizationIdFilter.OrganizationId);

            if (accountId != null)
            {
                requestUri += "&account_id=" + accountId;
            }

            var response = await PostAsync<ZohoBooksMessage>(requestUri);
            return response.Code == 0;
        }

        /// <summary>
        /// Categorize an Uncategorized transaction as vendor payment.
        /// </summary>
        /// <param name="transactionId">The id of the transaction.</param>
        /// <param name="vendorPayment">The vendor payment data to use.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> CategorizeAsVendorPayment(string transactionId, VendorPayment vendorPayment)
        {
            // TODO Double check this for missing fields - https://www.zoho.com/books/api/v3/#Bank-Transactions_Categorize_as_expense
            var response = await PostDataAsync<VendorPayment, ZohoBooksMessage>
                (string.Format("uncategorized/{0}/categorize/expenses", transactionId), vendorPayment, OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Categorize an Uncategorized transaction as customer payment.
        /// </summary>
        /// <param name="transactionId">The id of the transaction.</param>
        /// <param name="customerPayment">The customer payment data to use.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> CategorizeAsCustomerPayment(string transactionId, CustomerPayment customerPayment)
        {
            var response = await PostDataAsync<CustomerPayment, ZohoBooksMessage>
                (string.Format("uncategorized/{0}/categorize/customerpayments", transactionId), customerPayment, OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Categorize an Uncategorized transaction as credit note refund.
        /// </summary>
        /// <param name="transactionId">The id of the transaction.</param>
        /// <param name="creditNoteRefund">The credit note refund data to use.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> CategorizeAsCreditNoteRefund(string transactionId, CreditNoteRefund creditNoteRefund)
        {
            var response = await PostDataAsync<CreditNoteRefund, ZohoBooksMessage>
                (string.Format("uncategorized/{0}/categorize/creditnoterefunds", transactionId), creditNoteRefund, OrganizationIdFilter);
            return response.Code == 0;
        }

        // TODO: After creating vendor credit refunds, vendor credit refunds, customer payment refunds api, a categorization methods for it here.

        /// <summary>
        /// Categorize an Uncategorized transaction as vendor payment refund.
        /// </summary>
        /// <param name="transactionId">The id of the transaction.</param>
        /// <param name="vendorPaymentRefund">The vendor payment refund data to use.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> CategorizeAsVendorCreditRefund(string transactionId, VendorPaymentRefund vendorPaymentRefund)
        {
            var response = await PostDataAsync<VendorPaymentRefund, ZohoBooksMessage>
                (string.Format("uncategorized/{0}/categorize/vendorpaymentrefunds", transactionId), vendorPaymentRefund, OrganizationIdFilter);
            return response.Code == 0;
        }
    }
}
