using Core.Entities;

namespace Infrastructure.Services;

public interface ILoanApplicationService
{
    Task AddAsync(LoanApplication loanApplication, CancellationToken ct);
    Task DeleteAsync(int id, CancellationToken ct);
    Task<IEnumerable<LoanApplication>> GetAllAsync(CancellationToken ct);
    Task<LoanApplication?> GetByIdAsync(int id, CancellationToken ct);
    Task UpdateAsync(LoanApplication loanApplication, CancellationToken ct);
}