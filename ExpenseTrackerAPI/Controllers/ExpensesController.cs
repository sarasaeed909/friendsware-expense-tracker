using Microsoft.AspNetCore.Mvc;
using ExpenseTrackerAPI.Services;
using ExpenseTrackerAPI.Models;
using System.Linq;

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

        // GET: /api/expenses - Return all expenses
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_expenseService.GetAll());
        }

        // POST: /api/expenses - Create a new expense
        [HttpPost]
        public IActionResult Create([FromBody] Expense expense)
        {
            // Validate Title is not empty
            if (string.IsNullOrWhiteSpace(expense.Title))
                return BadRequest(new { error = "Title cannot be empty" });

            // Validate Amount is greater than zero
            if (expense.Amount <= 0)
                return BadRequest(new { error = "Amount must be greater than zero" });

            var created = _expenseService.Create(expense);
            return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
        }

        // DELETE: /api/expenses/{id} - Remove an expense by ID
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _expenseService.Delete(id);
            if (!deleted)
                return NotFound(new { error = $"Expense with id {id} not found" });

            return NoContent();
        }

        // GET: /api/expenses/summary - Returns total, count, and breakdown by category
        [HttpGet("summary")]
        public IActionResult GetSummary()
        {
            var expenses = _expenseService.GetAll();

            var totalAmount = expenses.Sum(e => e.Amount);
            var expenseCount = expenses.Count();
            var categoryBreakdown = expenses
                .GroupBy(e => e.Category)
                .ToDictionary(g => g.Key, g => g.Sum(e => e.Amount));

            return Ok(new
            {
                totalAmount,
                expenseCount,
                categoryBreakdown
            });
        }
    }
}