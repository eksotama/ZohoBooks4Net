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

namespace ZohoBooks4Net.Domain.Entities
{
    public class TaxGroup
    {
        [JsonProperty("tax_group_id")]
        public string TaxGroupId { get; set; }

        [JsonProperty("tax_group_name")]
        public string TaxGroupName { get; set; }

        [JsonProperty("tax_group_percentage")]
        public double TaxGroupPercentage { get; set; }

        [JsonProperty("taxes")]
        public IList<Tax> Taxes { get; set; }
    }
}
