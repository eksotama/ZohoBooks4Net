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
    public class Comment
    {
        [JsonProperty("comment_id")]
        public long CommentId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("commented_by_id")]
        public long CommentedById { get; set; }

        [JsonProperty("commented_by")]
        public string CommentedBy { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("date_description")]
        public string DateDescription { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("transaction_id")]
        public long TransactionId { get; set; }

        [JsonProperty("transaction_type")]
        public string TransactionType { get; set; }

        [JsonProperty("operation_type")]
        public string OperationType { get; set; }
    }
}
