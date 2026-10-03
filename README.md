# EmployeeApp

A layered .NET Framework 4.8 solution for managing companies and their employees. I built it as coursework to practise n-tier structure: one business-logic layer and one data-access layer shared by a WPF desktop client, an ASP.NET MVC site and an ASP.NET Web API.

It's unfinished. The commit history says "GUI handling incomplete" and that's still true (details below).

## Projects

| Project | Type | Role |
|---|---|---|
| `DTO` | Class library | Shared data transfer objects (`Company`, `Employee`) |
| `EmployeeDataAccess` | Class library | Entity Framework context, entities, repositories and entity↔DTO mappers |
| `BusinessLogic` | Class library | `CompanyBLL` and `EmployeeBLL`, which the clients call |
| `WebAPI` | ASP.NET Web API 2 | REST endpoints (`CompanyApiController`, `EmployeeApiController`) |
| `WebGUI` | ASP.NET MVC 5 | Web frontend (`Company`, `Employee` and `Home` controllers) |
| `EmployeeWPF` | WPF | Desktop client |
| `TestApp` | WPF | Throwaway harness I used to poke the Web API over HTTP |

There's also a `testAPI` folder, which is an unused Web API project template and isn't part of the solution.

## Architecture

```
EmployeeWPF (WPF)      WebGUI (MVC)      WebAPI (REST)  <──HTTP──  TestApp
        \                   |                 /
         \                  |                /
          BusinessLogic (CompanyBLL, EmployeeBLL)   referenced in-process
                            |
          EmployeeDataAccess (EF, repositories + mappers)
                            |
          SQL Server Express (code-first, seeded by EmployeeInitializer)
```

All three front ends reference `BusinessLogic` directly and call it in-process. The WPF client and the MVC controllers do `new EmployeeBLL()` themselves; they don't go through the Web API. The Web API is a third front end over the same layers, and the only thing that calls it is `TestApp`.

The parts I'd point at are the repository pattern and the mapping between EF entities and DTOs: nothing above `EmployeeDataAccess` sees an EF entity, and only the DTOs cross layer boundaries.

## What works and what doesn't

- WebGUI can look up a company or employee by id, create new ones, and the home page lists all of both.
- The WPF client is lookup-only: get an employee or a company by id, and see a company's employees.
- The Web API exposes get-by-id and create for both. Routes include the action, e.g. `GET api/employeeapi/getemployee/1` and `POST api/employeeapi/addemployee`. An unknown id gives a 404.
- Editing and deleting aren't implemented. `CompanyBLL.editCompany` / `deleteCompany` and `EmployeeBLL.editEmployee` are empty `TODO` stubs, there's no employee delete at all, and `addEmployeeToCompany` exists in the BLL but nothing in the UI calls it.
- `TestApp` calls a hard-coded `https://localhost:44367` (WebAPI's IIS Express port), so WebAPI has to be running from Visual Studio first.
- There are no automated tests.

## Tech stack

- C# on .NET Framework 4.8
- ASP.NET Web API 2 and ASP.NET MVC 5
- Entity Framework 6, code-first, with a `CreateDatabaseIfNotExists` initializer that seeds sample data
- WPF
- SQL Server Express

## Running it

This needs Windows, Visual Studio (or Rider) and a local SQL Server Express instance.

1. Open `EmployeeApp.sln`.
2. Each runnable project has its own `Employees` connection string (`Web.config` for WebGUI and WebAPI, `App.config` for the WPF apps). They all point at `localhost\SQLEXPRESS`, so change `Data Source` if your instance is named differently. The database is `Employees18`, and EF creates and seeds it on first use.
3. Run `WebGUI` or `EmployeeWPF` on its own. Neither needs the Web API running. Start `WebAPI` only if you want to hit the REST endpoints.

Employees need `YearsEmployed` between 1 and 45 (validated on the DTO).
