# SncfPivMessageConsumer

### General Description

The purpose of this service is being the first point of ingestion for realtime push messages originating from SNCF Portail Information Voyageur (PIV) API.
Please see the relevant [wiki pages](https://trainline.atlassian.net/wiki/spaces/UT/pages/95492455/PIV+-+Portail+Information+Voyager+-+SNCF+realtime+API) for details about the API and the source data contained therein.

## Operation
 - On startup it connects to the SNCF ApacheMQ broker and subscribes to the 'enriched circulation' topic to receive the message stream. 
 - It then maps each message received into English before pushing it to an Amazon SNS Topic.
 - Note: the message stream contains all kinds of realtime updates not just for cancellations/delays.
 - We also receive advance information up to 90 days in the future.
 - Messages are received 24 hours a day, 7 days a week although the quantity can greatly differ.

### Developer Notes
 - In order to be more performant, the service utilizes multithreading and message [channels.](https://learn.microsoft.com/en-us/dotnet/core/extensions/channels)
 - It utilizes a ApacheMQ feature called 'durable subscriptions'. This means that if the service goes down for some time, when it reconnects it will still receive messages that were sent while it was [offline.](https://activemq.apache.org/components/classic/documentation/manage-durable-subscribers).
 - In order to prevent received messages being lost during a system failure we do not [acknowledge](https://activemq.apache.org/components/nms/msdoc/1.6.0/vs2005/Output/html/T_Apache_NMS_AcknowledgementMode.htm) the message as being consumed until it has successfully been sent to SNS.
 - In order to gracefully shut the service down, press any key on the keyboard. This will trip the cancellation token and process any remaining messages before disconnecting from the broker.
 - Content Translation is done by the [JsonProperty] decorator when deserializing from the source bytearray. As SNS generally needs messages to be sent as strings, the service uses a custom JSON serializer in order to
   avoid translating the property names back to french. **Yes:** deserializing and then reserializing is far from ideal. **No** we have not found another viable option.

### Local Development
- The service accesses AWS so you need to have it setup on your machine, a guide for which is [here.](https://trainline.atlassian.net/wiki/spaces/AWS/pages/69174230/AWS+CLI+Access).
- aws-login
  - Account : **aws-sandbox-new**
  - Role : **adfsSandboxAdmin**
- The password for the PIV test environment is in a keypass db on the development network drive.
- To see the contents of each message, simply add **"WriteToConsole": true** to appSettings.json

### Environments
 - Todo............

### CI/CD Teamcity
 - Todo.............

### Telemetry/Logging
 - Todo..............

### Tests
 - Todo..............

### Useful Links
 - [PIV main wiki page](https://trainline.atlassian.net/wiki/spaces/UT/pages/95492455/PIV+-+Portail+Information+Voyager+-+SNCF+realtime+API)
