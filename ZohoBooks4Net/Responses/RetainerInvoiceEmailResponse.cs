using Newtonsoft.Json;
using System.Collections.Generic;

namespace ZohoBooks4Net.Responses
{
    public class RetainerInvoiceEmailResponse : ZohoBooksMessage
    {
        [JsonProperty("gateways_configured")]
        public bool GatewaysConfigured { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("error_list")]
        public IList<string> ErrorList { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("to_contacts")]
        public IList<ToContact> ToContacts { get; set; }

        [JsonProperty("attachment_name")]
        public string AttachmentName { get; set; }

        [JsonProperty("file_name")]
        public string FileName { get; set; }

        [JsonProperty("from_emails")]
        public IList<FromEmail> FromEmails { get; set; }

        [JsonProperty("customer_id")]
        public long CustomerId { get; set; }
    }
}
