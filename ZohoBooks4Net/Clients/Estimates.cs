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
using ZohoBooks4Net.Responses.PaginatedResponses;

namespace ZohoBooks4Net.Clients
{
    public class Estimates : ZohoBooksClientBase, IGetsPaginatedResponses<Estimate>,
        ICreates<Estimate>, IGets<Estimate>, IUpdates<Estimate>, IDeletes<Estimate>
    {
        public Estimates(Configuration configuration) : base(configuration)
        {
            BaseUri = "estimates";
        }

        /// <summary>
        /// Get the details of an estimate. 
        /// </summary>
        /// <param name="id">The id of the estimate to get.</param>
        /// <returns>The requested estimate.</returns>
        public async Task<Estimate> GetAsync(string id)
        {
            var response = await GetDataAsync<ZohoBooksResponse<Estimate>>(id, OrganizationIdFilter);
            return response.Resource;
        }
        
        public async Task<IList<Estimate>> GetAllPagesAsync(IFilter filter)
        {
            var allPages = new List<Estimate>();
            var currentPage = new PaginatedResponse<Estimate>();
            var currentPageNum = currentPage.Context.Page;
            var contactsFilter = SetupFilter(filter, currentPage.Context.Page);

            while ((currentPage = await GetDataAsync<PaginatedResponse<Estimate>>(contactsFilter)).Context.HasMorePage)
            {
                allPages.AddRange(currentPage.Resource);
                contactsFilter.Page = currentPageNum++;
            }
            return allPages;
        }

        public async Task<IList<Estimate>> GetPageAsync(int page, int pageSize = 100, IFilter filter = null)
        {
            var result = await GetDataAsync<PaginatedResponse<Estimate>>(SetupFilter(filter, page));
            return result.Resource;
        }

        public async Task<IList<Estimate>> GetPageRangeAsync(int start, int end, int pageSize = 100, IFilter filter = null)
        {
            var pageRange = new List<Estimate>();
            var currentPage = new PaginatedResponse<Estimate>();
            var currentPageNum = currentPage.Context.Page;
            var contactsFilter = SetupFilter(filter, currentPageNum);

            while ((currentPage = await GetDataAsync<PaginatedResponse<Estimate>>(contactsFilter)).Context.Page <= end)
            {
                pageRange.AddRange(currentPage.Resource);
                contactsFilter.Page = currentPageNum++;
            }

            return pageRange;
        }

        /// <summary>
        /// Create an estimate for your customer. 
        /// </summary>
        /// <param name="newItem">The new Estimate to create.</param>
        /// <returns>The estimate after being created by the server.</returns>
        public async Task<Estimate> CreateAsync(Estimate newItem)
        {
            var response = await PostDataAsync<Estimate, ZohoBooksResponse<Estimate>>(newItem, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Update an existing estimate. To delete a line item just remove it from the line_items list. 
        /// </summary>
        /// <param name="id">The id of the Estimate to update.</param>
        /// <param name="item">The content for the update of the estimate.</param>
        /// <returns>The updated estimate.</returns>
        public async Task<Estimate> UpdateAsync(string id, Estimate item)
        {
            var response = await PutDataAsync<Estimate, ZohoBooksResponse<Estimate>>(id, item, OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Delete an existing estimate 
        /// </summary>
        /// <param name="id">The ID of the Estimate to delete.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> DeleteAsync(string id)
        {
            var response = await DeleteDataAsync<ZohoBooksMessage>(id, OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Mark a draft estimate as sent. 
        /// </summary>
        /// <param name="id">The id of the Estimate draft to mark as sent.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> MarkEstimateAsSentAsync(string id)
        {
            var response = await PostAsync<ZohoBooksMessage>(string.Format("{0}/status/sent", id), OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Mark a sent estimate as accepted if the customer has accepted it. 
        /// </summary>
        /// <param name="id">The id of the sent Estimate to mark as accepted.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> MarkEstimateAsAcceptedAsync(string id)
        {
            var response = await PostAsync<ZohoBooksMessage>(string.Format("{0}/status/accepted", id), OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Mark a sent estimate as accepted if the customer has accepted it. 
        /// </summary>
        /// <param name="id">The id of the sent Estimate to mark as accepted.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> MarkEstimateAsDeclinedAsync(string id)
        {
            var response = await PostAsync<ZohoBooksMessage>(string.Format("{0}/status/declined", id), OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Email an estimate to the customer. Input json string is not mandatory. If input json string is empty, mail will be send with default mail content. 
        /// </summary>
        /// <param name="emailContent">The content of the email to send.</param>
        /// <param name="id">The ID of the Estimate to email about.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> EmailEstimateAsync(EmailContent emailContent, string id)
        {
            var response = await PostDataAsync<EmailContent, ZohoBooksMessage>(string.Format("{0}/email", id), emailContent, OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Send estimates to your customers by email.Maximum of 10 estimates can be sent at once.
        /// </summary>
        /// <param name="emailContent">The content of the email to send.</param>
        /// <param name="ids">An array of Estimate IDs to email about.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<EstimateEmailResponse> EmailMultipleEstimateAsync(EmailContent emailContent, IList<string> estimateIdsToEmail)
        {
            var response = await PostDataAsync<EmailContent, EstimateEmailResponse>
                (string.Format("{0}/email", string.Join(",", estimateIdsToEmail)), emailContent, OrganizationIdFilter);
            return response;
        }

        /// <summary>
        /// Get the email content of an estimate. 
        /// </summary>
        /// <param name="estimateId">The ID of the estimate to get.</param>
        /// <param name="emailTemplateId">Get the email content based on a specific email template. If this param is not inputted, then 
        /// the content will be based on the email template associated with the customer. If no template is associated with the customer,
        /// then default template will be used.</param>
        /// <returns>Email content of the requested estimate.</returns>
        public async Task<EstimateEmailResponse> GetEstimateEmailContentAsync(string estimateId, string emailTemplateId)
        {
            return await GetDataAsync<EstimateEmailResponse>
                (string.Format("{0}/email?organization_id={1}&estimateId={2}", estimateId, OrganizationIdFilter.OrganizationId, estimateId));
        }

        /// <summary>
        /// Maximum of 25 estimates can be exported in a single pdf. 
        /// </summary>
        /// <param name="estimateIds">An array of estimate IDs to export as PDF.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> BulkExportEstimatesAsync(IList<string> estimateIds)
        {
            var response = await GetDataAsync<ZohoBooksMessage>("/estimates/pdf", OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Export estimates as pdf and print them. Maximum of 25 estimates can be printed.  
        /// </summary>
        /// <param name="estimateIds">An array of estimate IDs to export as PDF.</param>
        /// <returns>A response indicating wWhether the request was successful.</returns>
        public async Task<bool> BulkPrintEstimatesAsync(IList<string> estimateIds)
        {
            var response = await GetDataAsync<ZohoBooksMessage>("/estimates/print", OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Updates the billing address for this estimate alone. 
        /// </summary>
        /// <param name="estimateId">The ID of the billing address to update.</param>
        /// <param name="address">The address data to update.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> UpdateBillingAddressAsync(string estimateId, Address address)
        {
            var response = await PutDataAsync<Address, ZohoBooksMessage>(string.Format("{0}/address/billing"), address, OrganizationIdFilter);
            return response.Code == 0;
        }

        /// <summary>
        /// Updates the shipping address for an existing estimate alone. 
        /// </summary>
        /// <param name="estimateId">The ID of the billing address to update.</param>
        /// <param name="address">The address data to update.</param>
        /// <returns>A response indicating whether the request was successful.</returns>
        public async Task<bool> UpdateShippingAddressAsync(string estimateId, Address address)
        {
            var response = await PutDataAsync<Address, ZohoBooksMessage>(string.Format("{0}/address/shipping"), address, OrganizationIdFilter);
            return response.Code == 0;
        }
        
        /// <summary>
        /// Get all estimate pdf templates. 
        /// </summary>
        /// <param name="estimateId">The ID of the estimate to list templates for.</param>
        /// <returns>A list of email templates.</returns>
        public async Task<IList<Template>> ListEstimateTemplatesAsync(string estimateId)
        {
            var response = await GetDataAsync<ZohoBooksResponse<IList<Template>>>(string.Format("{0}/templates", estimateId), OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Update the pdf template associated with the estimate. 
        /// </summary>
        /// <param name="estimateId">The ID of the estimate.</param>
        /// <param name="templateId">The ID of the templates to update.</param>
        /// <returns>A list of email templates.</returns>
        public async Task<bool> UpdateEstimateTemplateAsync(string estimateId, string templateId)
        {
            var response = await GetDataAsync<ZohoBooksMessage>
                (string.Format("{0}/templates/{1}", estimateId, templateId), OrganizationIdFilter);
            return response.Code == 1;
        }

        /// <summary>
        /// Get all estimate pdf templates. 
        /// </summary>
        /// <param name="estimateId">The ID of the estimate to list templates for.</param>
        /// <returns>A list of email templates.</returns>
        public async Task<IList<EstimateComment>> ListEstimateCommentsAsync(string estimateId)
        {
            var response = await GetDataAsync<ZohoBooksResponse<IList<EstimateComment>>>(string.Format("{0}/comments", estimateId), OrganizationIdFilter);
            return response.Resource;
        }

        /// <summary>
        /// Add a comment for an estimate. 
        /// </summary>
        /// <param name="estimateId">The Estimate to add a Comment to</param>
        /// <param name="description">The description of the line items.</param>
        /// <param name="showCommentToClients">Boolean to show the comments to contacts in portal.</param>
        /// <returns>A response indicating whether the response was successful.</returns>
        public async Task<bool> AddCommentToEstimateAsync(string estimateId, string description = "", bool? showCommentToClients = false)
        {
            var requestArgs = new JObject();

            if (!string.IsNullOrEmpty(description))
            {
                requestArgs["description"] = description;
            }

            if (showCommentToClients != null)
            {
                requestArgs["show_comment_to_clients"] = showCommentToClients;
            }

            IZohoBooksMessage response;

            if (requestArgs.HasValues)
            {
                response = await PostDataAsync<JObject, ZohoBooksMessage>(string.Format("{0}/comments"), requestArgs, OrganizationIdFilter);
            }

            response = await PostAsync<ZohoBooksMessage>(string.Format("{0}/comments"), OrganizationIdFilter);

            return response.Code == 0;
        }

        /// <summary>
        /// Add a comment for an estimate. 
        /// </summary>
        /// <param name="estimateId">The Estimate the Comment is on.</param>
        /// <param name="commentId">The id of the Comment to update.</param>
        /// <param name="description">The description of the line items.</param>
        /// <param name="showCommentToClients">Boolean to show the comments to contacts in portal.</param>
        /// <returns>A response indicating whether the response was successful.</returns>
        public async Task<bool> UpdateCommentAsync(string estimateId, string commentId, string description = "", bool? showCommentToClients = false)
        {
            var requestArgs = new JObject();

            if (!string.IsNullOrEmpty(description))
            {
                requestArgs["description"] = description;
            }

            if (showCommentToClients != null)
            {
                requestArgs["show_comment_to_clients"] = showCommentToClients;
            }

            IZohoBooksMessage response;

            if (requestArgs.HasValues)
            {
                response = await PutDataAsync<JObject, ZohoBooksMessage>(string.Format("{0}/comments"), requestArgs, OrganizationIdFilter);
            }

            response = await PutAsync<ZohoBooksMessage>(string.Format("{0}/comments"), OrganizationIdFilter);

            return response.Code == 0;
        }

        /// <summary>
        /// Delete an estimate comment. 
        /// </summary>
        /// <param name="estimateId">The estimate the comment is for.</param>
        /// <param name="commentId">The comment to delete.</param>
        /// <returns>A response indicating whether the response was successful.</returns>
        public async Task<bool> DeleteCommentAsync(string estimateId, string commentId)
        {
            var response = await DeleteDataAsync<ZohoBooksMessage>(string.Format("{0}/comments/{1}", estimateId, commentId));
            return response.Code == 0;
        }
    }
}
