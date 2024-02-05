using Amazon.DynamoDBv2.DataModel;
using Newtonsoft.Json;
using PIV_POC_Client.AWS.Utility;
using PIV_POC_Client.Models.Enums;

namespace PIV_POC_Client.Models.PivMessage
{
    public class CommericalStatus
    {
        [DynamoDBProperty(typeof(DynamoEnumStringConverter<CommericalStatusCode>))]
        [JsonProperty("codeStatut")]
        public CommericalStatusCode? StatusCode { get; set; }

        [JsonProperty("libelleStatut")]
        public string StatusLabel { get; set; } = string.Empty;
    }
}
