using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Services
{
    public class ExpenseService
    {
        private static readonly List<Expense> _expenses = new();
        private static int _nextId = 1;

        public List<Expense> GetAll() => _expenses.ToList();

        public Expense? GetById(int id) => _expenses.FirstOrDefault(e => e.Id == id);

        public Expense Create(Expense expense)
        {
            expense.Id = _nextId++;
            expense.CreatedAt = DateTime.UtcNow;
            _expenses.Add(expense);
            return expense;
        }

        public bool Delete(int id)
        {
            var expense = GetById(id);
            if (expense == null) return false;
            return _expenses.Remove(expense);
        }

        public ExpenseSummary GetSummary()
        {
            if (!_expenses.Any())
                return new ExpenseSummary { TotalAmount = 0, ExpenseCount = 0, CategoryBreakdown = new Dictionary<string, decimal>() };

            return new ExpenseSummary
            {
                TotalAmount = _expenses.Sum(e => e.Amount),
                ExpenseCount = _expenses.Count,
                CategoryBreakdown = _expenses
                    .GroupBy(e => e.Category)
                    .ToDictionary(g => g.Key, g => g.Sum(e => e.Amount))
            };
        }
    }

    public class ExpenseSummary
    {
        public decimal TotalAmount { get; set; }
        public int ExpenseCount { get; set; }
        public Dictionary<string, decimal> CategoryBreakdown { get; set; } = new();
    }
}