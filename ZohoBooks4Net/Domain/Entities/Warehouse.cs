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

    public class Warehouse
    {
        [JsonProperty("warehouse_id")]
        public string WarehouseId { get; set; }

        [JsonProperty("warehouse_name")]
        public string WarehouseName { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("is_primary")]
        public bool IsPrimary { get; set; }

        [JsonProperty("warehouse_stock_on_hand")]
        public string WarehouseStockOnHand { get; set; }

        [JsonProperty("warehouse_available_stock")]
        public string WarehouseAvailableStock { get; set; }

        [JsonProperty("warehouse_actual_available_stock")]
        public string WarehouseActualAvailableStock { get; set; }
    }
}
