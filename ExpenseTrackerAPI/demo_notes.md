# Expense Tracker API - Demo Script

## Video Overview

This document outlines the 3-minute demonstration video of my Expense Tracker API built with ASP.NET Core Web API and C#.

---

### Introduction 

Assalam-o-Alaikum. My name is Sara, and today I'll be presenting my Expense Tracker API built using ASP.NET Core Web API and C#.

---

### Code Overview 

First, let's briefly look at the project structure.

**Expense.cs** - This is the Expense model, which contains the properties Id, Title, Amount, Category, and CreatedAt.

**ExpenseService.cs** - This service manages expense data using an in-memory list and provides CRUD operations.

**ExpensesController.cs** - The controller exposes API endpoints for creating, retrieving, updating, deleting expenses, and generating expense summaries. Input validation is also implemented here.

**Program.cs** - Program.cs contains the application configuration, Swagger setup, and middleware registration.

**Middleware** - I have implemented custom middleware for input sanitisation (trims whitespace from all incoming strings) and global exception handling (catches unhandled errors and returns clean JSON responses).

**Key Concepts Covered** - This project demonstrates RESTful API development, CRUD operations, Service-Based Architecture, middleware implementation, input validation, error handling, pagination, filtering, Swagger documentation, and Postman API testing.

---

### API Demonstration 

Now let's move to the API demonstration using Swagger.

As you can see, all endpoints are available through Swagger UI.

I executed the GET All Expenses endpoint. The API successfully returns the available expenses with a Status 200 OK response, confirming that the endpoint is working correctly.

I have also tested all remaining endpoints, including PUT, DELETE, Summary, Pagination, and Category Filtering. The results of all test cases are available in the attached screenshots folder.

---

### Postman Testing 

In addition to Swagger testing, I have also tested the API using Postman.

The Postman collection is included in the repository root as `ExpenseTracker.postman_collection.json`. It contains 17 requests covering both success and failure cases, including:

- GET All Expenses (200 OK)
- POST Create Expense (201 Created)
- POST Empty Title (400 Bad Request)
- POST Zero Amount (400 Bad Request)
- PUT Update Expense (200 OK)
- PUT Invalid ID (404 Not Found)
- DELETE Existing ID (204 No Content)
- DELETE Non-existent ID (404 Not Found)
- GET Summary (200 OK)
- Pagination and Category Filter tests

The collection can be imported into Postman to run all tests automatically.

---

### Conclusion 

This completes the demonstration of my Expense Tracker API.

Thank you for your time.

---

## Validation Error Scenario

The video demonstrates that the API handles validation errors properly through the code overview (input validation mentioned in ExpensesController). The Postman collection includes specific validation failure requests.

