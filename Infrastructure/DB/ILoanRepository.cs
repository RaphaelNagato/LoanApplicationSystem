using Core.Entities;
using Core.Spec;

namespace Infrastructure.DB;

internal interface ILoanRepository
{
    Task AddAsync(LoanApplication loanApplication, CancellationToken ct);
    Task DeleteAsync(int id, CancellationToken ct);
    Task<IEnumerable<LoanApplication>> GetAllAsync(CancellationToken ct);
    Task<LoanApplication?> GetByIdAsync(int id, CancellationToken ct);
    Task UpdateAsync(LoanApplication loanApplication, CancellationToken ct);
    Task<IEnumerable<LoanApplication>> GetAsync(BaseSpecification<LoanApplication> spec, CancellationToken ct);
    Task<int> CountAsync(BaseSpecification<LoanApplication> spec, CancellationToken ct);
}