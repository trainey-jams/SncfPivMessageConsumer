# SncfPivMessageConsumer

### Description

The purpose of this service is to be the first point of ingestion for realtime information stream origination from SNCF Portail Information Voyageur (PIV) API.
 - It connects to the SNCF ApacheMQ broker and then maps each message received into English before pushing to an Amazon SNS Topic.
 - Note: the message stream contains all kinds of realtime updates not just for cancellations/delays.
 - We also receive advance information up to 90 days in the future.

### Local Development
- The service accesses AWS so you need to have it setup on your machine, a guide for which is [here.](https://trainline.atlassian.net/wiki/spaces/AWS/pages/69174230/AWS+CLI+Access).
- The password for the PIV test environment is in a keypass db on the development network drive. 

### Useful Links
 - [PIV main wiki page](https://trainline.atlassian.net/wiki/spaces/UT/pages/95492455/PIV+-+Portail+Information+Voyager+-+SNCF+realtime+API)
