# TodoApp

TodoApp is a web application that allows users to register an account, login and add, read, edit and delete their own todos. There is also an admin panel for managing users and todos.


## Tech Stack

The application uses the following technologies:

- **.NET 7** for the backend, which is a cross-platform framework for building web applications using C# and ASP.NET Core.
- **Angular 16** for the frontend, which is a popular framework for building single-page applications using TypeScript and HTML.
- **JWT** for authentication and authorization, which is a standard for securely transmitting information between parties as JSON objects.
- **Postman** as an API testing tool, which is a platform for designing, testing and documenting APIs.
- **SQL Server** as a database, which is a relational database management system that supports various data types and queries.
- **Docker** as a container platform, which is a software that allows developers to package and run applications in isolated environments.


## Installation

To install and run the application, you need to have the following prerequisites:

- **Docker Desktop** installed on your machine.
- **Git** installed on your machine.
- **User secrets** initialized in .NET and enter `ConnectionStrings:TodoDbConnetionString` as a key and your own connection string as value.
- **Jwt:Key** added as a key and your desired value of this.

Then, follow these steps:

1. Clone the repository from GitHub using the command `git clone https://github.com/<your_username>/TodoApp.git`.
2. Navigate to the `TodoAppBackend` folder.
3. To add your secrets run the commands `dotnet user-secrets init` , `dotnet user-secrets set "ConnectionStrings:TodoDbConnetionString" "YOUR_CONNECTION_STRING"` and `dotnet user-secrets set "Jwt:Key" "YOUR_SECRET_KEY"`
4. Finally run the command `docker-compose up -d` to build and run the backend containers.
5. Navigate to the `TodoAppUI` folder and run the command `docker build -t todoappui .` to build the frontend image.
6. Run the command `docker run -p 4200:80 todoappui` to run the frontend container.
7. Open your browser and go to `http://localhost:4200` to access the application.

## Usage

The application has two types of users: regular users and admin users.

- Regular users can register an account, login and manage their own todos.
- Admin users can login and manage their page.

You can use Postman or Swagger to test the API endpoints of the backend.
