using System.ComponentModel.DataAnnotations;
using Core.Entities;

namespace Core.DTOs;

public class LoanApplicationDto
{
    public int Id { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "Applicant name cannot exceed 100 characters")]
    public string ApplicantName { get; set; } = string.Empty;

    [Required]
    [Range(100, 1000000, ErrorMessage = "Loan amount must be between $100 and $1,000,000")]
    public decimal LoanAmount { get; set; }

    [Required]
    [Range(1, 360, ErrorMessage = "Loan term must be between 1 and 360 months")]
    public int LoanTermMonths { get; set; }

    [Required]
    [Range(0.1, 25.0, ErrorMessage = "Interest rate must be between 0.1% and 25%")]
    public decimal InterestRate { get; set; }

    public LoanApplicationStatus LoanStatus { get; set; } = LoanApplicationStatus.Pending;

    public DateTimeOffset ApplicationDate { get; set; } = DateTimeOffset.Now;

    public LoanApplication ToEntity()
    {
        return new LoanApplication
        {
            Id = this.Id,
            ApplicantName = this.ApplicantName,
            LoanAmount = this.LoanAmount,
            LoanTermMonths = this.LoanTermMonths,
            InterestRate = this.InterestRate,
            LoanStatus = this.LoanStatus,
            ApplicationDate = this.ApplicationDate
        };
    }

    public static LoanApplicationDto FromEntity(LoanApplication entity)
    {
        return new LoanApplicationDto
        {
            Id = entity.Id,
            ApplicantName = entity.ApplicantName,
            LoanAmount = entity.LoanAmount,
            LoanTermMonths = entity.LoanTermMonths,
            InterestRate = entity.InterestRate,
            LoanStatus = entity.LoanStatus,
            ApplicationDate = entity.ApplicationDate
        };
    }

    public static IEnumerable<LoanApplicationDto> FromEntities(IEnumerable<LoanApplication> entities)
    {
        return entities.Select(FromEntity);
    }
} 