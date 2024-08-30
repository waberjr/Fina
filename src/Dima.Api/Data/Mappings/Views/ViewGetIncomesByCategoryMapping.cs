using Dima.Core.Models.Reports;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappings.Views;

public class ViewGetIncomesByCategoryMapping : IEntityTypeConfiguration<IncomesByCategory>
{
    public void Configure(EntityTypeBuilder<IncomesByCategory> builder)
    {
        builder
            .HasNoKey()
            .ToView("vwGetIncomesByCategory");
    }
}