# TodosItAcadProject

## Introduction

Application for manage Todo list by using modern approaches to development on the ASP.NET platform .

## Architecture design patterns used

- RESTful
- Command
- Command query responsibility segregation
- Mediator
- Onion
- Repository
- Chain of responsibility
- Write-through cache

## Libraries

- .NET 8 
- ASP.NET 8
- AutoMapper 13
- FluentValidation 11
- MediatR 12
- Serilog 3
- EntityFrameworkCore 8
- xunit 2.7
- NetArchTest.Rules 1.3

## Projects

### Application

#### Application/Core

- Core.Users.Domain - User and user roles entity
- Core.Application - Core business logic abstractions and realizations 
- Core.Api - Common middlewares and api services configuration
- Core.Auth.Application - Common auth business logic abstractions and realizations
- Core.Auth.Api - Common auth realizations abstractions and realizations for Api projects


#### Application/Users

- Users.Application - Users management business logic

#### Application/Todos

- Todos.Domain - Todos entities
- Todos.Applications - Todos management business logic

#### Application/Auth

- Auth.Domain - Auth entities
- Auth.Application - Auth business logic

### Infrastructure

- Infrastructure.Persistence - Database connection realizations

### Apis

- Users.Api - Users management API
- Todos.Api - Todos management API
- Auth.Api - Auth API

### Tests

- Todos.UnitTests - Unit test for Todos.Applications
- Users.UnitTests - Unit test for Users.Applications
- Auth.UnitTests  - Unit test for Auth.Applications

#### Tests/Core

- Core.ArchitectureTests - Architecture tests
- Core.Tests - Common test utils

##  Configurations

### Users, Todos

```
{
  "ConnectionStrings": {
    "DefaultConnection": "" //Connection string to SQL server DB
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    },
    "LogsFolder": "" //Path to log folder
  },
  "AllowedHosts": "*",
  "Jwt": {
    "Key": "", // JWT key
    "Issuer": "Todos",
    "Audience": "Todos"
  }
}
```

### Auth.Api

```
{
  "ConnectionStrings": {
    "DefaultConnection": "" //Connection string to SQL server DB
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    },
    "LogsFolder": "" //Path to log folder
  },
  "AllowedHosts": "*",
  "Jwt": {
    "Key": "", // JWT key
    "Issuer": "Todos",
    "Audience": "Todos"
  },
  "TokensLifeTime": {
    "JwtToken" : 300, //Life time of JWT token in seconds
    "RefreshToken" : 72000  //Life time of refresh token in seconds
  }
}
```

## How to run

1. Install [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) (>=8.0.3)
2. Install and run [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) 
3. Insert **connection string** to database to **appsettings.json** files of apis projects. Example: 
```"DefaultConnection": "Server=.;database=Todos;Integrated Security=False;User Id=sa;Password=sqlServerPassword;MultipleActiveResultSets=True;Trust Server Certificate=true;"```
4. Insert logs folder path to **appsettings.json** files of apis projects. Example: ```"LogsFolder": "Logs/"```
5. Open target API project in terminal
6. Run command ```dotnet run```





