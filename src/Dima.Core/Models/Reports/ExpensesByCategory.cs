namespace Dima.Core.Models.Reports;

public record ExpensesByCategory(string UserEmail, string Category, int Year, decimal Expenses);