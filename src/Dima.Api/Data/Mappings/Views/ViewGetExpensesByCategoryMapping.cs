using Dima.Core.Models;
using Dima.Core.Models.Reports;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappings.Views;

public class ViewGetExpensesByCategoryMapping : IEntityTypeConfiguration<ExpensesByCategory>
{
    public void Configure(EntityTypeBuilder<ExpensesByCategory> builder)
    {
        builder
            .HasNoKey()
            .ToView("vwGetExpensesByCategory");
    }
}