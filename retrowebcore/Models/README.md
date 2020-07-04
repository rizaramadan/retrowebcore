This folder contains our POCO for the logic of the app, the M (Model) in MVC. The file on this folder 
should not depend on any other namespace in this project and also should not depend on any persistence (ex. db) 
or presenting (ex. json) packages.

Having said that, we currently have a violation in Card.cs. It's a known issue that will be fixed soon.