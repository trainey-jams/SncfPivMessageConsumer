using PIV_POC_Client.Models.PivMessage.Root;

namespace PIV_POC_Client.Models.Results
{
    public class MessageParseResult
    {
        public bool ParseSuccess { get; set; } = false;

        public PivMessageRoot MessageRoot {  get; set; } = new PivMessageRoot();
    }
}
