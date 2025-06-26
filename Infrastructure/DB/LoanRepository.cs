using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.DB;

internal class LoanRepository(LoanDbContext context, ILogger<LoanRepository> logger) : ILoanRepository
{
    private readonly LoanDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
    private readonly ILogger<LoanRepository> _logger = logger;

    public async Task<LoanApplication?> GetByIdAsync(int id, CancellationToken ct)
    {
        try
        {
            var result = await _context.LoanApplications.FindAsync([id], ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Database error while querying loan application with ID: {LoanId}", id);
            throw;
        }
    }

    public async Task<IEnumerable<LoanApplication>> GetAllAsync(CancellationToken ct)
    {
        try
        {
            var result = await _context.LoanApplications.ToListAsync(ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Database error while querying all loan applications");
            throw;
        }
    }

    public async Task AddAsync(LoanApplication loanApplication, CancellationToken ct)
    {
        try
        {
            await _context.LoanApplications.AddAsync(loanApplication, ct);
            await _context.SaveChangesAsync(ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Database error while adding loan application for applicant: {ApplicantName}", loanApplication.ApplicantName);
            throw;
        }
    }

    public async Task UpdateAsync(LoanApplication loanApplication, CancellationToken ct)
    {
        try
        {
            _context.LoanApplications.Update(loanApplication);
            await _context.SaveChangesAsync(ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Database error while updating loan application with ID: {LoanId}", loanApplication.Id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken ct)
    {
        try
        {
            var loanApplication = await GetByIdAsync(id, ct);
            if (loanApplication != null)
            {
                _context.LoanApplications.Remove(loanApplication);
                await _context.SaveChangesAsync(ct);
            }
            else
            {
                _logger.LogWarning("Attempted to delete loan application with ID {LoanId} but it was not found", id);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Database error while deleting loan application with ID: {LoanId}", id);
            throw;
        }
    }
}
