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

namespace ZohoBooks4Net.Responses
{
    public class ZohoBooksMessage : IZohoBooksMessage
    {
        /// <summary>
        /// Zoho Books error code. This will be zero for a success response and non-zero in case of an error.
        /// </summary>
        [JsonProperty("code")]
        public int Code { get; set; }
        
        /// <summary>
        /// Message for the invoked API.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
