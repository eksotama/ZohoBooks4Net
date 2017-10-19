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

namespace ZohoBooks4Net.Requests
{
    public class EmailContent
    {
        /// <summary>
        /// Boolean to trigger the email from the organization's email address
        /// </summary>
        [JsonProperty("send_from_org_email_id")]
        public bool SendFromOrgEmailId { get; set; }

        /// <summary>
        /// Array of email address of the recipients.
        /// </summary>
        [JsonProperty("to_mail_ids")]
        public IList<string> ToMailIds { get; set; }

        /// <summary>
        /// Array of email address of the recipients to be cced.
        /// </summary>
        [JsonProperty("cc_mail_ids")]
        public IList<string> CcMailIds { get; set; }

        /// <summary>
        /// Subject of an email has to be sent. Max-length [1000]
        /// </summary>
        [JsonProperty("subject")]
        public string Subject { get; set; }

        /// <summary>
        /// Body of an email has to be sent. Max-length [5000]
        /// </summary>
        [JsonProperty("body")]
        public string Body { get; set; }
    }

}
