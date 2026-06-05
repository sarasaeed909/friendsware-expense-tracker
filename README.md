# Expense Tracker API

A simple RESTful API for managing personal expenses built with ASP.NET Core Web API and C#.

Table of contents
- [Technology](#technology)
- [Features](#features)
- [Bonus Features](#bonus-features)
- [Validation & Error Responses](#validation--error-responses)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Run the app](#run-the-app)
- [API Endpoints](#api-endpoints)
- [Postman Collection](#postman-collection)
- [Project Structure](#project-structure)
- [Screenshots](#screenshots)
- [Known Issues](#known-issues)
- [Author](#author)

## Technology

- ASP.NET Core Web API (.NET 10)
- C#
- Swagger / OpenAPI
- LINQ
- Postman (for testing)

## Features

- GET /api/expenses — Returns all expenses (supports pagination & category filter)
- POST /api/expenses — Creates a new expense with validation
- PUT /api/expenses/{id} — Updates an existing expense
- DELETE /api/expenses/{id} — Deletes an expense
- GET /api/expenses/summary — Returns total amount, count, and category breakdown

## Bonus Features

| Bonus | Description |
|-------|-------------|
| B1 - Pagination | ?page=1&pageSize=5 with metadata (total, page, pageSize, totalPages) |
| B2 - Category Filter | ?category=Food (case-insensitive) |
| B3 - PUT Update | Update existing expenses with validation |
| B4 - Input Sanitisation | Automatically trims whitespace from all string fields |
| B5 - Global Exception Handler | Catches unhandled exceptions and returns a clean JSON error response |

## Validation & Error Responses

| Scenario | Status | Response example |
|----------|--------|------------------|
| Empty title | 400 Bad Request | { "error": "Title cannot be empty" } |
| Amount ≤ 0 | 400 Bad Request | { "error": "Amount must be greater than zero" } |
| DELETE / PUT non-existent id | 404 Not Found | { "error": "Expense with id {id} not found" } |
| Invalid page / pageSize | 400 Bad Request | { "error": "Page must be greater than or equal to 1" } |
| Unhandled exception | 500 Internal Server Error | { "error": "An unexpected error occurred.", "detail": "...", "timestamp": "..." } |

## Getting Started

### Prerequisites

- .NET 10 SDK
- Visual Studio 2022/2026 or VS Code (optional)
- Postman (optional, for testing)

### Installation

1. Clone the repository:

```sh
git clone https://github.com/sarasaeed909/friendsware-expense-tracker.git
cd friendsware-expense-tracker/ExpenseTrackerAPI
```

2. Restore and build:

```sh
dotnet restore
dotnet build
```

### Run the app

From the project folder:

```sh
dotnet run
```

By default Swagger UI will be available when the app is running. The exact URL (scheme/port) may vary; common local URLs include:

- https://localhost:44356/swagger (example)
- https://localhost:{PORT}/swagger

Running in Visual Studio

1. Open ExpenseTrackerAPI.sln in Visual Studio.
2. Press F5 or run the project.

## API Endpoints

Summary of available endpoints:

| Method | Endpoint | Description | Query Parameters |
|--------|----------|-------------|------------------|
| GET | /api/expenses | Get all expenses | category, page, pageSize |
| GET | /api/expenses/summary | Get spending summary | — |
| POST | /api/expenses | Create a new expense | — |
| PUT | /api/expenses/{id} | Update an existing expense | — |
| DELETE | /api/expenses/{id} | Delete an expense | — |

Request and response schemas are documented in the Swagger UI when the app is running.

## Postman Collection

A Postman collection is included at the repository root: ExpenseTracker.postman_collection.json

To use it:

1. Import the collection into Postman.
2. Ensure the API is running locally.
3. Run the requests (the collection includes success and failure scenarios).

## Project Structure

```
ExpenseTrackerAPI/
├── Controllers/
│   └── ExpensesController.cs
├── Models/
│   └── Expense.cs
├── Services/
│   └── ExpenseService.cs
├── Middlewares/
│   ├── InputSanitisationMiddleware.cs
│   └── GlobalExceptionMiddleware.cs
├── screenshots/
│   ├── Swagger UI.png
│   ├── GET all expenses.png
│   ├── Post_Expense.png
│   ├── pagination_B1.png
│   ├── Category Filter_B2.png
│   ├── update_B3.png
│   ├── summary_endpoint.png
│   └── postman.png
├── Program.cs
├── appsettings.json
├── README.md
├── demo_notes.md
└── ExpenseTracker.postman_collection.json
```

## Screenshots

| Feature | Screenshot |
|---------|------------|
| Swagger UI | ![Swagger UI](screenshots/Swagger%20UI.png) |
| GET All Expenses | ![GET All Expenses](screenshots/GET%20all%20expenses.png) |
| POST Create Expense | ![POST Expense](screenshots/Post_Expense.png) |
| Pagination (B1) | ![Pagination](screenshots/pagination_B1.png) |
| Category Filter (B2) | ![Category Filter](screenshots/Category%20Filter_B2.png) |
| PUT Update (B3) | ![PUT Update](screenshots/update_B3.png) |
| Summary Endpoint | ![Summary](screenshots/summary_endpoint.png) |
| Postman Collection | ![Postman](screenshots/postman.png) |

## Known Issues

- None. All core and bonus features are implemented. Note that in-memory storage is used — data will be lost on application stop.

## About

This repository contains all the work and project deliverables provided to Friendsware Solutions over the course of the internship.

## Author

Sara Saeed — Backend Developer Intern

**Task:** ASP.NET Core Web API for managing expenses
Repository: https://github.com/sarasaeed909/friendsware-expense-tracker
