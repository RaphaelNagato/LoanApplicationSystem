using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class LoanApplication
{
    [Key]
    public int Id { get; set; }
    [Required, MaxLength(255)]
    public string ApplicantName { get; set; }
    [Range(5, 1000000)]
    public decimal LoanAmount { get; set; }
    public int LoanTermMonths { get; set; }
    [Range(0, 100)]
    public decimal InterestRate { get; set; }
    public LoanApplicationStatus LoanStatus { get; set; }
    public DateTime ApplicationDate { get; set; }
}

public enum LoanApplicationStatus
{
    Pending,
    Approved,
    Rejected
}
