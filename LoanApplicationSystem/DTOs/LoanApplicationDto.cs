using System.ComponentModel.DataAnnotations;
using Core.Entities;

namespace LoanApplicationSystem.DTOs;

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
    public DateTime ApplicationDate { get; set; } = DateTime.Now;

    
    // Static mapper methods
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

    public static LoanApplication ToEntity(LoanApplicationDto dto)
    {
        return new LoanApplication
        {
            Id = dto.Id,
            ApplicantName = dto.ApplicantName,
            LoanAmount = dto.LoanAmount,
            LoanTermMonths = dto.LoanTermMonths,
            InterestRate = dto.InterestRate,
            LoanStatus = dto.LoanStatus,
            ApplicationDate = dto.ApplicationDate
        };
    }

    public static IEnumerable<LoanApplicationDto> FromEntities(IEnumerable<LoanApplication> entities)
    {
        return entities.Select(FromEntity);
    }
} 