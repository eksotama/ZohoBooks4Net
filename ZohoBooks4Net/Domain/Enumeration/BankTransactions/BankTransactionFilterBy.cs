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

namespace ZohoBooks4Net.Domain.Enumeration.BankTransactions
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum BankTransactionFilterBy
    {
        [EnumMember(Value = "Status.All")]
        All = 0,

        [EnumMember(Value = "Status.Uncategorized")]
        Uncategorized = 1,

        [EnumMember(Value = "Status.Categorized")]
        Categorized = 2,

        [EnumMember(Value = "Status.ManuallyAdded")]
        ManuallyAdded = 3,

        [EnumMember(Value = "Status.Excluded")]
        Excluded = 4,

        [EnumMember(Value = "Status.Matched")]
        Matched = 5
    }
}
