using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DB;

internal class LoanRepository(LoanDbContext context) : ILoanRepository
{
    private readonly LoanDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
    public async Task<LoanApplication?> GetByIdAsync(int id, CancellationToken ct)
    {
        return await _context.LoanApplications.FindAsync([id], ct);
    }
    public async Task<IEnumerable<LoanApplication>> GetAllAsync(CancellationToken ct)
    {
        return await _context.LoanApplications.ToListAsync(ct);
    }
    public async Task AddAsync(LoanApplication loanApplication, CancellationToken ct)
    {
        await _context.LoanApplications.AddAsync(loanApplication, ct);
        await _context.SaveChangesAsync(ct);
    }
    public async Task UpdateAsync(LoanApplication loanApplication, CancellationToken ct)
    {
        _context.LoanApplications.Update(loanApplication);
        await _context.SaveChangesAsync(ct);
    }
    public async Task DeleteAsync(int id, CancellationToken ct)
    {
        var loanApplication = await GetByIdAsync(id, ct);
        if (loanApplication != null)
        {
            _context.LoanApplications.Remove(loanApplication);
            await _context.SaveChangesAsync(ct);
        }
    }
}
