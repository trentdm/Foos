# Foos
Foos is a webapp dedicated to recording foosball scores and providing data for later use. It is built using AngularJS v12.16 and ServiceStack v4.

## Intent
Foos is a small personal project, designed with just three purposes in mind:

* Find out who the best NCR in-house Foosball player is. (hint: it's not me).
* Familiarize myself with ServiceStack, particularly in contrast to Web Api 2
* Try out some different approaches to authentication and routing. Particularly, using ui.router instead of ngRoute, and extending ui.router to provide well integrated authentication.

## Technology
Foos front-end is built upon AngularJS and AngularUI.
The server is built using ServiceStack. I am currently using ServiceStack.Ormlite for back-end persistence, but may change the database layer to something more customized, due to personal preference over ORMs in general.

## Api and Logging
The restful api can be viewed at /metadata.

## Open Source
Foos is an open source project, licensed under the Apache License.

## ToDo
* Hyperlinks to different data sets across review pages
* Improve synchronization of auth state between server and client (https://github.com/ServiceStack/ServiceStack/wiki/Authentication-and-authorization)
* Signout needs to remove serverside session
* Rethink match, team, and player apis - room for simplification if 10 api limit and orm functionality don't hinder
* Investigate a integrated bundler to minimize payloads (MVC was good at this, or https://github.com/ServiceStack/Bundler if sticking with SS)
* Change metadata, auth, register, etc., paths to /api/{service}
* Customize metadata page, https://github.com/ServiceStack/ServiceStack/wiki/Metadata-page
* Use modernizr to detect if a client's browser may be unsupported (and notify with alert)
