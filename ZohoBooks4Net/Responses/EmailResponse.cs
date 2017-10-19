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

namespace ZohoBooks4Net.Responses
{
    public class EmailResponse : ZohoBooksResponse<EmailResponseContent>
    {
        [JsonProperty("data")]
        public override EmailResponseContent Resource { get; set; }
    }

    public class EmailResponseContent
    {
        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("to_contacts")]
        public IList<ToContact> ToContacts { get; set; }

        [JsonProperty("file_name")]
        public string FileName { get; set; }

        [JsonProperty("from_emails")]
        public IList<FromEmail> FromEmails { get; set; }

        [JsonProperty("contact_id")]
        public long ContactId { get; set; }
    }

    public class ToContact
    {
        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("selected")]
        public bool Selected { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("contact_person_id")]
        public long ContactPersonId { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("salutation")]
        public string Salutation { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }
    }

    public class FromEmail
    {
        [JsonProperty("user_name")]
        public string UserName { get; set; }

        [JsonProperty("selected")]
        public bool Selected { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
