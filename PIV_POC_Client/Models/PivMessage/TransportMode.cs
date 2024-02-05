using Amazon.DynamoDBv2.DataModel;
using Newtonsoft.Json;
using PIV_POC_Client.AWS.Utility;
using PIV_POC_Client.Models.Enums;

namespace PIV_POC_Client.Models.PivMessage
{
    public class TransportMode
    {
        [DynamoDBProperty(typeof(DynamoEnumStringConverter<TransportModeCode>))]
        [JsonProperty("codeMode")]
        public TransportModeCode? Code { get; set; }

        [JsonProperty("libelleMode")]
        public string Label { get; set; } = string.Empty;

        [DynamoDBProperty(typeof(DynamoEnumStringConverter<TransportModSubCode>))]
        [JsonProperty("codeSousMode")]
        public TransportModSubCode? SubCode { get; set; }

        [JsonProperty("libelleSousMode")]
        public string SubLabel { get; set; } = string.Empty;

        [DynamoDBProperty(typeof(DynamoEnumStringConverter<TransportModeType>))]
        [JsonProperty("typeMode")]
        public TransportModeType? Type { get; set; }
    }
}
