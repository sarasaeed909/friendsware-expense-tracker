using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Services
{
    public class ExpenseService
    {
        private static List<Expense> _expenses = new List<Expense>();
        private static int _nextId = 1;

        // GET all expenses
        public List<Expense> GetAll() => _expenses;

        // GET expense by ID
        public Expense? GetById(int id) => _expenses.FirstOrDefault(e => e.Id == id);

        // CREATE a new expense
        public Expense Create(Expense expense)
        {
            expense.Id = _nextId++;
            expense.CreatedAt = DateTime.UtcNow;
            _expenses.Add(expense);
            return expense;
        }

        // DELETE an expense
        public bool Delete(int id)
        {
            var expense = GetById(id);
            if (expense == null) return false;
            return _expenses.Remove(expense);
        }
    }
}