# ProjectAPI
A vehicle rental store API created using ASP.Net Core. The application follows onion architecture with EF Core, Hangfire integration for background jobs and
Web scraping components.

## Functionality

1. Web scraping competition price rates
2. A weekly job to obtain, read and update fraudulent account details
3. Manage accounts - View, Add, update, delete, ban.
4. Manage vehicles - View, Add, update, delete
5. Manage equipment - View, Add, update, delete
6. Manage inquiries - View, Add, delete, email, responses
7. Manage bookings - View, Add, update, delete

## Architecture

**Onion Architecture**

Each layer has its own responsibility and tasks. The core can only be accessed by going through the outermost 
layers. The direction of data flow is from the outermost layer  to the innermost layer.

## Design Patterns
To ensure single responsibility principle is followed there have been several design patterns utilized.

1. Transferable Object Design – This is the design when multiple attributes are required to 
be sent to the view a single POJO class is created to handle this. 
2. Factory Design Pattern – Multiple repositories each handling an entity. To simplify 
repository creation this design pattern is utilized.
3. Option Design Pattern – This pattern is used to group configuration details with a layer of 
abstraction.
4. Repository Pattern – This abstracts the data layer and centralizes the object handling 
process. All the while following SOLID principles.
5. Dependency Injection – This is used to make a class independent of the dependencies.

## Libraries integraed
- **EF Core** - Object Relational mapper integrated with an SQL server in order to simplify database interactions
- **Hangfire** - To perform automated jobs 
- **HtmlAgilityPack** - Web scraping library to read and write DOM Elements from a given URL  
