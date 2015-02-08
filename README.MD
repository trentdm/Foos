Foos
====
Foos is an webapp dedicated to recording foosball scores and providing data for later use. It is built using AngularJS v12.16 and ServiceStack v4.

The restful api can be viewed at /metadata
Debugging logs can be viewed at /requestlogs

ToDo
===
track wins/losses of teams and players
normalize the teams, winners, players, tables (configured through orm or switch to writing sprocs)
foreign key off names (not a great ide a, but an okay stop gap)
use jira typeahead styling for entering team and players
add unified system for success/error messagesadd proper authentication
investigate a bundler for scripts, app js, and css directories. Minimize overhead of extending or publishing app (MVC was good at this)
change metadata, auth, register, etc paths to /api/{service}
customize metadata page, https://github.com/ServiceStack/ServiceStack/wiki/Metadata-page
consider adding local persistence for user