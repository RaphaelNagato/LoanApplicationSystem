using Core.Entities;

namespace Core.Spec;

public class LoanApplicationSpecification : BaseSpecification<LoanApplication>
{
    public LoanApplicationSpecification()
    {
        AddOrderByDescending(x => x.ApplicationDate);
    }

    public LoanApplicationSpecification(int id) : this()
    {
        AddCriteria(x => x.Id == id);
    }

    public LoanApplicationSpecification(string? searchTerm, LoanApplicationStatus? status, int page, int pageSize) : this()
    {
        // Apply search filter
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            AddCriteria(x => x.ApplicantName.ToLower().Contains(searchTerm.ToLower()));
        }

        // Apply status filter
        if (status.HasValue)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                AddCriteria(x => x.LoanStatus == status.Value);
            }
            else
            {
                // Combine search and status filters
                var existingCriteria = Criteria;
                AddCriteria(x => x.LoanStatus == status.Value && 
                    (existingCriteria == null || existingCriteria.Compile()(x)));
            }
        }

        // Apply pagination
        ApplyPaging((page - 1) * pageSize, pageSize);
    }

    public LoanApplicationSpecification(LoanApplicationStatus status) : this()
    {
        AddCriteria(x => x.LoanStatus == status);
    }
} 