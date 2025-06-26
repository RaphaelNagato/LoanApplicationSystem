using Core.Entities;
using Infrastructure.DB;

namespace Infrastructure.Services;

internal class LoanApplicationService(ILoanRepository loanRepository) : ILoanApplicationService
{
    private readonly ILoanRepository _loanRepository = loanRepository;

    public async Task<LoanApplication?> GetByIdAsync(int id, CancellationToken ct)
    {
        return await _loanRepository.GetByIdAsync(id, ct);
    }

    public async Task<IEnumerable<LoanApplication>> GetAllAsync(CancellationToken ct)
    {
        return await _loanRepository.GetAllAsync(ct);
    }

    public async Task AddAsync(LoanApplication loanApplication, CancellationToken ct)
    {
        await _loanRepository.AddAsync(loanApplication, ct);
    }

    public async Task UpdateAsync(LoanApplication loanApplication, CancellationToken ct)
    {
        await _loanRepository.UpdateAsync(loanApplication, ct);
    }

    public async Task DeleteAsync(int id, CancellationToken ct)
    {
        await _loanRepository.DeleteAsync(id, ct);
    }
}
