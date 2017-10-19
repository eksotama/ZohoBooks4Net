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
using ZohoBooks4Net.Domain.Entities;

namespace ZohoBooks4Net.Responses
{
    public class EstimateEmailResponse : ZohoBooksMessage
    {
        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("error_list")]
        public IList<object> ErrorList { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("emailtemplates")]
        public IList<Template> Emailtemplates { get; set; }

        [JsonProperty("to_contacts")]
        public IList<ToContact> ToContacts { get; set; }

        [JsonProperty("file_name")]
        public string FileName { get; set; }

        [JsonProperty("from_emails")]
        public IList<FromEmail> FromEmails { get; set; }

        [JsonProperty("customer_id")]
        public string CustomerId { get; set; }
    }

}
