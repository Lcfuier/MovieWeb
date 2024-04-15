# [MovieWeb]()

BookStore is a web built with ASP.NET Core MVC, using Entity Framework Core and Microsoft SQL Server as the backend technologies. The project uses ASP.NET Core Identity for authentication and authorization, and follows the repository and unit of work pattern for data access. The project is structured using n-tier architecture, with separate projects for the Presentation, Model, and Data Access.
# Project Description

MovieWeb allows user to browse and watching movie. The project consists of two roles: User,  and Admin. User can watching movie,rate and comment the movie. Admin can add movie, edit or delete existing movie,actor,.., create account, assign permissions to users and lock/unlock account.

Default admin account : admin@gmail.com 
                        Admin@123
# Project Structure

The MovieWeb project follows a typical n-tier architecture structure with the following layers:

1. **Presentation Layer**: This layer represents the user interfaces, such as MVC or API controllers, views, or command-line interfaces. It interacts with the application through use cases, typically via dependency injection.This layer contains the application logic and orchestrates the flow of data


3. **DataAccess Layer**: This layer is responsible for interacting with the database and performing CRUD (create, read, update, delete) operations on the data. The DataAccess Layer in the MovieWeb project is implemented as a separate project and uses ASP.NET Core EF Core for ORM (object-relational mapping) and MSSQL for database management.

4. **Model Layer**: This layer contains Entity, classes and functions that are used across the application

# Technologies Used 
- **Programming Language**: C# 11 (.NET 7)
- **Web Framework**: ASP.NET Core MVC
- **Database**: Entity Framework Core SQL Server 7.0.13 (ORM)
- **Identity Provider**: Microsoft AspNetCore Identity 7.0.13
- **Front-End Libraries**: Bootstrap, jQuery, DataTables, Ajax
- **Several external technologies**: Toastr, Stripe
# License
The MovieWeb project is open-source and released under the [MIT License](https://opensource.org/licenses/MIT). You are free to use, modify, and distribute the codebase as per the license terms.






  
