# EmployeeApp

A classic n-tier .NET solution for managing companies and their employees, demonstrating how multiple client applications (WPF desktop, ASP.NET MVC web) can share the same business logic and data access layers through a REST Web API.

## Solution Structure

| Project | Type | Role |
|---|---|---|
| `DTO` | Class library | Shared data transfer objects (`Company`, `Employee`) |
| `EmployeeDataAccess` | Class library | Entity Framework context, entities, repositories, and entity↔DTO mappers |
| `BusinessLogic` | Class library | Business rules (`CompanyBLL`, `EmployeeBLL`) |
| `WebAPI` | ASP.NET Web API | REST endpoints (`CompanyApiController`, `EmployeeApiController`) |
| `WebGUI` | ASP.NET MVC | Web frontend consuming the API (`Company`/`Employee`/`Home` controllers) |
| `EmployeeWPF` | WPF | Desktop client |
| `TestApp` / `testAPI` | WPF / Web API | Test harness clients |

## Architecture

```
EmployeeWPF (desktop)      WebGUI (ASP.NET MVC)
        \                     /
         \                   /
          WebAPI  (ASP.NET Web API, REST)
                |
          BusinessLogic (BLL)
                |
          EmployeeDataAccess (EF, repository + mapper pattern)
                |
          SQL Server (Entity Framework, seeded via EmployeeInitializer)
```

Key patterns: **repository pattern**, **DTO mapping** between persistence entities and transport objects, and strict layering — clients never touch the database directly.

## Tech Stack

- C# / .NET Framework 4.8
- ASP.NET Web API 2 & ASP.NET MVC 5
- Entity Framework (code-first with database initializer/seeding)
- WPF
- IIS Express / Visual Studio or JetBrains Rider

## Running

1. Open `EmployeeApp.sln` in Visual Studio or Rider.
2. Set `WebAPI` as the startup project and run it (IIS Express). EF creates and seeds the database on first run.
3. Start `WebGUI` and/or `EmployeeWPF` to use the web or desktop client against the API.
