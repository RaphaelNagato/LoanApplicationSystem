public class LoanApplication
{
    [Key]
    public int Id { get; set; }
    [Required, MaxLength()]
    public string ApplicantName { get; set; }
    public decimal LoanAmount { get; set; }
    public int LoanTermMonths { get; set; }
    public decimal InterestRate { get; set; }
    public LoanApplicationStatus Status { get; set; }
    public DateTime ApplicationDate { get; set; }
}

public enum LoanApplicationStatus
{
    Pending,
    Approved,
    Rejected
}
