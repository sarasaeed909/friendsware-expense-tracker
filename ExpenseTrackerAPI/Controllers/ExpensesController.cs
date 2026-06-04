using Microsoft.AspNetCore.Mvc;
using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Services;

namespace ExpenseTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpensesController : ControllerBase
    {
        private readonly ExpenseService _expenseService;

        public ExpensesController(ExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        // GET: api/expenses?category=Food&page=1&pageSize=5 (category filter + pagination)
        [HttpGet]
        public IActionResult GetAll([FromQuery] string? category = null, [FromQuery] int page = 1, [FromQuery] int pageSize = 5)
        {
            // Validate pagination parameters
            if (page < 1)
                return BadRequest(new { error = "Page must be greater than or equal to 1" });

            if (pageSize < 1)
                return BadRequest(new { error = "PageSize must be greater than or equal to 1" });

            var expenses = _expenseService.GetAll();

            // Category filter (case-insensitive)
            if (!string.IsNullOrWhiteSpace(category))
            {
                expenses = expenses
                    .Where(e => e.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Calculate pagination metadata
            var total = expenses.Count;
            var totalPages = (int)Math.Ceiling(total / (double)pageSize);

            // Apply pagination
            var paginatedExpenses = expenses
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Return response with metadata envelope
            return Ok(new
            {
                total,
                page,
                pageSize,
                totalPages,
                expenses = paginatedExpenses
            });
        }

        // POST: api/expenses
        [HttpPost]
        public IActionResult Create([FromBody] Expense expense)
        {
            // Validate Title
            if (string.IsNullOrWhiteSpace(expense.Title))
                return BadRequest(new { error = "Title cannot be empty" });

            // Validate Amount
            if (expense.Amount <= 0)
                return BadRequest(new { error = "Amount must be greater than zero" });

            // Set default category if not provided
            if (string.IsNullOrWhiteSpace(expense.Category))
                expense.Category = "Uncategorized";

            var created = _expenseService.Create(expense);
            return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
        }

        // PUT: api/expenses/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Expense updatedExpense)
        {
            // Validate Title
            if (string.IsNullOrWhiteSpace(updatedExpense.Title))
                return BadRequest(new { error = "Title cannot be empty" });

            // Validate Amount
            if (updatedExpense.Amount <= 0)
                return BadRequest(new { error = "Amount must be greater than zero" });

            // Get existing expense
            var existingExpense = _expenseService.GetById(id);
            if (existingExpense == null)
                return NotFound(new { error = $"Expense with id {id} not found" });

            // Update the expense (keep original Id and CreatedAt)
            existingExpense.Title = updatedExpense.Title;
            existingExpense.Amount = updatedExpense.Amount;
            existingExpense.Category = updatedExpense.Category ?? "Uncategorized";

            return Ok(existingExpense);
        }

        // DELETE: api/expenses/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _expenseService.Delete(id);

            if (!deleted)
                return NotFound(new { error = $"Expense with id {id} not found" });

            return NoContent();
        }

        // GET: api/expenses/summary
        [HttpGet("summary")]
        public IActionResult GetSummary()
        {
            var summary = _expenseService.GetSummary();
            return Ok(summary);
        }
    }
}