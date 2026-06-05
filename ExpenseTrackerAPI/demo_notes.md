# Expense Tracker API - Demo Script

## 5-Step Walkthrough 

### Step 1: Code Overview
Show the project structure in Visual Studio Solution Explorer and explain:
- Expense.cs model (Id, Title, Amount, Category, CreatedAt)
- ExpenseService.cs (in-memory storage with auto-increment)
- ExpensesController.cs (GET, POST, PUT, DELETE, Summary endpoints)
- Program.cs (Swagger and middleware setup)

### Step 2: Start API and Show Swagger UI
- Navigate to https://localhost:44356/swagger
- Show all available endpoints (GET, POST, PUT, DELETE, Summary)

### Step 3: Show Successful GET Request
- Show Status 200 OK with expenses data in response

### Step 4: Show Features
- Click GET /api/expenses/summary → Try it out → Execute
- Show Status 200 OK with total amount and category breakdown

### Step 5: Show Postman Collection
- Open Postman
- Show the "Expense Tracker API" collection
- Note that the collection includes both success and failure cases

---

## Validation Error Scenario (Tested in Postman)

The API handles validation errors properly. During Postman testing, I sent a POST request with an empty title:

**Request:**
```json
{
  "title": "",
  "amount": 10,
  "category": "Food"
}
