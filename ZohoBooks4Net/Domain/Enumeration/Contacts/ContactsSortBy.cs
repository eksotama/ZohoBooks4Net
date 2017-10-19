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
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace ZohoBooks4Net.Domain.Enumeration.Contacts
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ContactsSortBy
    {
        [EnumMember(Value = "contact_name")]
        ContactName = 0,

        [EnumMember(Value = "first_name")]
        FirstName = 1,

        [EnumMember(Value = "last_name")]
        LastName = 2,

        [EnumMember(Value = "email")]
        Email = 3,

        [EnumMember(Value = "outstanding_receivable_amount")]
        OutstandingReceivableAmount = 4,

        [EnumMember(Value = "created_time")]
        CreatedTime = 5,

        [EnumMember(Value = "last_modified_time")]
        LastModifiedTime = 6
    }
}
