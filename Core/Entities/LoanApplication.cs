namespace Core.Entities;

public class LoanApplication
{
    public int Id { get; set; }
    public string ApplicantName { get; set; }
    public decimal LoanAmount { get; set; }
    public int LoanTermMonths { get; set; }
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
