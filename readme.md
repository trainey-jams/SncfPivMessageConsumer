# SncfPivMessageConsumer

### General Description

The purpose of this service is to be the first point of ingestion for realtime pubh messages originating from SNCF Portail Information Voyageur (PIV) API. Please see the relevant [wiki pages](https://trainline.atlassian.net/wiki/spaces/UT/pages/95492455/PIV+-+Portail+Information+Voyager+-+SNCF+realtime+API) for details about the API and the source data contained therein.

## Operation
 - On startup it connects to the SNCF ApacheMQ broker and subscribes to the 'enriched circulation' topic to receive the message stream. 
 - It then maps each message received into English before pushing to an Amazon SNS Topic.
 - Note: the message stream contains all kinds of realtime updates not just for cancellations/delays.
 - We also receive advance information up to 90 days in the future.
 - Messages are received 24 hours a day, 7 days a week although the quantity can greatly differ.

### Developer Notes
 - In order to be more performant, the service utilizes multithreading and message [channels](https://learn.microsoft.com/en-us/dotnet/core/extensions/channels)
 - Pressing enter will trip the cancellation token and process any remaining messages before disconnecting from the broker.
 - Content Translation is done by the [JsonProperty] decorator when deserializing from the source bytearray. As SNS generally needs messages to be sent as strings, the service uses a custom JSON serializer in order to
   avoid translating the property names back to french. **Yes:** deserializing and then reserializing is far from ideal, we have not found another viable option.

### Local Development
- The service accesses AWS so you need to have it setup on your machine, a guide for which is [here.](https://trainline.atlassian.net/wiki/spaces/AWS/pages/69174230/AWS+CLI+Access).
- The password for the PIV test environment is in a keypass db on the development network drive.
- To see the contents of each message, simply add **"WriteToConsole": true** to appSettings.json

### Useful Links
 - [PIV main wiki page](https://trainline.atlassian.net/wiki/spaces/UT/pages/95492455/PIV+-+Portail+Information+Voyager+-+SNCF+realtime+API)
