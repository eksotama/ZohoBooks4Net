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

namespace ZohoBooks4Net.Domain.Enumeration.ChartOfAccounts
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ChartOfAccountsFilterBy
    {
        [EnumMember(Value = "AccountType.All")]
        All = 0,

        [EnumMember(Value = "AccountType.Active")]
        Active = 1,

        [EnumMember(Value = "AccountType.Inactive")]
        Inactive = 2,

        [EnumMember(Value = "AccountType.Asset")]
        Asset = 3,

        [EnumMember(Value = "AccountType.Liability")]
        Liability = 4,

        [EnumMember(Value = "AccountType.Equity")]
        Equity = 5,

        [EnumMember(Value = "AccountType.Income")]
        Income = 6,

        [EnumMember(Value = "AccountType.Expense")]
        Expense = 7,
    }
}
