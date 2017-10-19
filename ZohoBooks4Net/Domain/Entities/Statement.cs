using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZohoBooks4Net.Domain.Entities
{
    public class Statement
    {
        [JsonProperty("statement_id")]
        public string StatementId { get; set; }

        [JsonProperty("from_date")]
        public DateTime FromDate { get; set; }

        [JsonProperty("to_date")]
        public DateTime ToDate { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("transactions")]
        public IList<Transaction> Transactions { get; set; }
    }
}
