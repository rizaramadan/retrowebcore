# retrowebcore

This is a simple web app for scrum retropective, consisting Liked-Lacked-Learned-Longed for the card types. The features are:
- create board
- create card of each board
- liking card [wip]
- comment card [wip]
- merge card [wip]

This project is heavily depend on mediator pattern, using [MediatR](https://github.com/jbogard/MediatR) as the implementation of it.
Thus the decision on how to test is we don't actually test the controller or SignalR's hub because those are thin anyway. 
What we tests are the mediator handlers. 

This app are using:
- Asp.net Core 3.1 for the web framework
- SignalR Core for web socket
- Entity Framework for ORM on top of PostgreSQL 11
