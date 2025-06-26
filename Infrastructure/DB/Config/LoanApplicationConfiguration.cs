using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DB.Config;

internal class LoanApplicationConfiguration : IEntityTypeConfiguration<LoanApplication>
{
    public void Configure(EntityTypeBuilder<LoanApplication> builder)
    {
        builder.ToTable("LoanApplications");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.ApplicantName).HasMaxLength(255);
        builder.Property(x => x.LoanAmount).IsRequired().HasPrecision(18,2);
        builder.Property(x => x.LoanTermMonths).IsRequired();
        builder.Property(x => x.InterestRate).IsRequired().HasPrecision(5,2);
        builder.Property(x => x.LoanStatus).HasConversion(
            s => s.ToString(),
            s => Enum.Parse<LoanApplicationStatus>(s)).IsRequired().HasMaxLength(50);
        builder.Property(x => x.ApplicationDate).IsRequired().HasConversion(
            v => v.ToUniversalTime(),
            v => v);
    }
}
