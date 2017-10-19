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

namespace ZohoBooks4Net.Domain.Enumeration
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PaymentMode
    {
        [EnumMember(Value = "PaymentMode.All")]
        All = 0,

        [EnumMember(Value = "PaymentMode.Check")]
        Check = 1,

        [EnumMember(Value = "PaymentMode.Cash")]
        Cash = 2,

        [EnumMember(Value = "PaymentMode.BankTransfer")]
        BankTransfer = 3,

        [EnumMember(Value = "PaymentMode.Paypal")]
        Paypal = 4,

        [EnumMember(Value = "PaymentMode.CreditCard")]
        CreditCard = 5,

        [EnumMember(Value = "PaymentMode.GoogleCheckout")]
        GoogleCheckout = 6,

        [EnumMember(Value = "PaymentMode.Credit")]
        Credit = 7,

        [EnumMember(Value = "PaymentMode.Authorizenet")]
        Authorizenet = 8,

        [EnumMember(Value = "PaymentMode.BankRemittance")]
        BankRemittance = 9,

        [EnumMember(Value = "PaymentMode.Payflowpro")]
        Payflowpro = 10,

        [EnumMember(Value = "PaymentMode.Stripe")]
        Stripe = 11,

        [EnumMember(Value = "PaymentMode.TwoCheckout")]
        TwoCheckout = 12,

        [EnumMember(Value = "PaymentMode.Braintree")]
        Braintree = 13,

        [EnumMember(Value = "PaymentMode.Others")]
        Others = 14
    }
}
