using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PIV_POC_Client.Interfaces;
using PIV_POC_Client.Models.Config;
using PIV_POC_Client.Models.Enums;
using PIV_POC_Client.Models.PivMessage.Root;

namespace PIV_POC_Client.Processor
{
    public class MessageProcessor : IMessageProcessor
    {
        private readonly MessageProcessorConfiguration ProcessorConfiguration;

        public MessageProcessor(

                                    IOptions<MessageProcessorConfiguration> processorConfiguration)
        {
            ProcessorConfiguration = processorConfiguration.Value;
        }

        private string ParseStompMessageToJson(string rawMessage)
        {
            var metaDataString = "\"MESSAGEHEADERS\": ";
            var bodyString = "\"MESSAGEBODY\" :";

            int bodyStartIndex = rawMessage.IndexOf("\n\n") + 2;

            bodyString += rawMessage.Substring(bodyStartIndex, rawMessage.Length - bodyStartIndex);

            string rawMetaData = rawMessage.Substring(0, bodyStartIndex).Replace("MESSAGE\n", "").Replace("\n\n", "");

            Dictionary<string, string> metaDataPairs = rawMetaData.Split("\n")
                   .Select(value => value.Split(':'))
                   .ToDictionary(
                    pair => pair[0],
                    pair => pair[1].Contains('\\') ? pair[1].Replace('\\', '-') : pair[1]);

            metaDataString += JsonConvert.SerializeObject(metaDataPairs);

            string message = $"{{{metaDataString},{bodyString}}}";

            return message;
        }

        private void WriteMessageToFile(string docPath, string fileName, string message)
        {
            // Write the text to a new file named "WriteFile.txt".
            File.AppendAllText(Path.Combine(docPath, fileName), message + Environment.NewLine);
        }

        public async Task Process(Guid subscriptionId, string rawMessage)
        {
            PivMessageRoot messageRoot = new PivMessageRoot();

            if (rawMessage.StartsWith(ServerFrames.MESSAGE.ToString()))
            {
                string messageJson = ParseStompMessageToJson(rawMessage);

                Console.WriteLine(messageJson);

                WriteMessageToFile("C:\\Users\\Administrator\\Documents\\PIV_Message_Files", "PIV_Messages.json", messageJson);
            }

            if (rawMessage.StartsWith(ServerFrames.ERROR.ToString()))
            {
                WriteMessageToFile("C:\\Users\\Administrator\\Documents\\PIV_Error_Files", "PIV_Errors.json", rawMessage);
            }

            if (rawMessage.StartsWith(ServerFrames.RECEIPT.ToString()))
            {
                //TODO
            }
        }
    }
}
