--About
Foos is an webapp dedicated to recording foosball scores and providing data for later use. It is built using AngularJS v12.16 and ServiceStack v4.

The restful api can be viewed at ~/metadata.

--ToDo
normalize the teams, winners, players, tables (configured through orm or switch to writing sprocs)
foreign key off names (not a great idea, but an okay stop gap)
track wins/losses of teams and players
add unified system for success/error messages
change main navigation active class based on url
change signin/out based on auth
improve state handling, particularly for signing in/out
add proper authentication and local persistence
investigate using servicestack.bundler to simplify extending app (not having to manually add new js files to list)