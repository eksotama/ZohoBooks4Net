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
    public class Item
    {
        /// <summary>
        /// Name of the item. Max-length [100]
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Price of the item.
        /// </summary>
        [JsonProperty("rate")]
        public int Rate { get; set; }

        /// <summary>
        /// Description for the item. Max-length [2000]
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Percent of the tax.
        /// </summary>
        [JsonProperty("tax_percentage")]
        public string TaxPercentage { get; set; }

        /// <summary>
        /// SKU value of item,should be unique throughout the product
        /// </summary>
        [JsonProperty("sku")]
        public string Sku { get; set; }

        /// <summary>
        /// Specify the type of an item. Allowed values: goods or service or digital_service.
        /// </summary>
        [JsonProperty("product_type")]
        public string ProductType { get; set; }

        /// <summary>
        /// ID of the account to which the item has to be associated with.
        /// </summary>
        [JsonProperty("account_id")]
        public string AccountId { get; set; }

        /// <summary>
        /// Type of the item. Allowed values: sales,purchases,sales_and_purchases and inventory. Default value will be sales.
        /// </summary>
        [JsonProperty("item_type")]
        public string ItemType { get; set; }

        /// <summary>
        /// Purchase description for the item
        /// </summary>
        [JsonProperty("purchase_description")]
        public string PurchaseDescription { get; set; }

        /// <summary>
        /// Purchase price of the item.
        /// </summary>
        [JsonProperty("purchase_rate")]
        public string PurchaseRate { get; set; }

        /// <summary>
        /// ID of the COGS account to which the item has to be associated with. Mandatory, if item_type is purchase / sales and 
        /// purchase / inventory.
        /// </summary>
        [JsonProperty("purchase_account_id")]
        public string PurchaseAccountId { get; set; }

        /// <summary>
        /// ID of the stock account to which the item has to be associated with. Mandatory, if item_type is inventory.
        /// </summary>
        [JsonProperty("inventory_account_id")]
        public string InventoryAccountId { get; set; }

        /// <summary>
        /// Preferred vendor ID.
        /// </summary>
        [JsonProperty("vendor_id")]
        public string VendorId { get; set; }

        /// <summary>
        /// Reorder level of the item.
        /// </summary>
        [JsonProperty("reorder_level")]
        public string ReorderLevel { get; set; }

        /// <summary>
        /// Opening stock of the item.
        /// </summary>
        [JsonProperty("initial_stock")]
        public string InitialStock { get; set; }

        /// <summary>
        /// Unit price of the opening stock.
        /// </summary>
        [JsonProperty("initial_stock_rate")]
        public string InitialStockRate { get; set; }

        [JsonProperty("warehouses")]
        public IList<Warehouse> Warehouses { get; set; }
    }

}
