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

namespace ZohoBooks4Net.Domain.Entities
{
    public class User
    {
        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("role_id")]
        public string RoleId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("user_role")]
        public string UserRole { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("is_current_user")]
        public bool IsCurrentUser { get; set; }

        [JsonProperty("photo_url")]
        public string PhotoUrl { get; set; }

        [JsonProperty("is_customer_segmented")]
        public bool IsCustomerSegmented { get; set; }

        [JsonProperty("is_vendor_segmented")]
        public bool IsVendorSegmented { get; set; }

        [JsonProperty("user_type")]
        public string UserType { get; set; }
    }
}
