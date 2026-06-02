using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Services;

public class ExpenseService
{
    public List<Expense> Expenses { get; } = new();

    public int CurrentId { get; set; } = 1;
}