#Foos#
Foos is an webapp dedicated to recording foosball scores and providing data for later use. It is built using AngularJS v12.16 and ServiceStack v4.

##Intent##
Foos is a small personal project, designed with just three purposes in mind:

* Find out who the best NCR in-house Foosball player is. (hint: it's not me).
* Familiarize myself with ServiceStack, particularly in contrast to Web Api 2
* Try out some different approaches to authentication and routing. Particularly, using ui.router instead of ngRoute, and extending ui.router to provide well integrated authentication.

##Technology##
Foos front-end is built upon AngularJS and AngularUI.
The server is built using ServiceStack. I am currently using ServiceStack.Ormlite for back-end persistence, but may change the database layer to something more customized, due to personal preference over ORMs in general.

##Api and Logging##
The restful api can be viewed at /metadata.

Debugging logs can be viewed at /requestlogs. This will likely be disabled if Foos ever gets hosted publicly and/or taken seriously.

##Open Source##
Foos is an open source project, licensed under the Apache License.

##ToDo##
* track wins/losses of teams and players
** normalize the teams, winners, players, tables (configured through orm or switch to writing sprocs)
** make team and player names unique, can then populate the joining tables on post
** since a model for posting a match and searching data may be different (search may include sums and averages), but SS is limited to 10 APIs and a post and search would count as two, will need to do the sums and averaging client side only.
** incorporate position into ui and player data
** may add third set of apis for team and players that includes summarized and averaged data in readable format, instead of putting mapping burden on client, or put into existing models but as a ignore on post property (so no data stored)
* consider adding local persistence for user, would use ss-id cookie for session id, but username is encrypted, so not sure how to restore that to the browser for viewing, and I dont want to maintain the auth state separately
* fix for requiring rootscope to broadcast alerts (should be able to $scope.$emit and have parent headCtrl receive (but my implementation is not compatible with ui.router seeing it as a parent/child relationship I think)
* investigate a bundler for scripts, app js, and css directories. Minimize overhead of extending or publishing app (MVC was good at this)
* change metadata, auth, register, etc paths to /api/{service}
* customize metadata page, https://github.com/ServiceStack/ServiceStack/wiki/Metadata-page
