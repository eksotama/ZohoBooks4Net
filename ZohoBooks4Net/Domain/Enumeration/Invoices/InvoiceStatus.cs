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

namespace ZohoBooks4Net.Domain.Enumeration.Invoices
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum InvoiceStatus
    {
        [EnumMember(Value = "sent")]
        Sent = 0,

        [EnumMember(Value = "draft")]
        Draft = 1,

        [EnumMember(Value = "overdue")]
        Overdue = 2,

        [EnumMember(Value = "paid")]
        Paid = 3,

        [EnumMember(Value = "void")]
        Void = 3,

        [EnumMember(Value = "unpaid")]
        Unpaid = 4,

        [EnumMember(Value = "partially_paid")]
        PartiallyPaid = 5,

        [EnumMember(Value = "viewed")]
        Viewed = 6
    }
}
