using Core.Entities;
using Infrastructure.DB;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

internal class LoanApplicationService(ILoanRepository loanRepository, ILogger<LoanApplicationService> logger) : ILoanApplicationService
{
    private readonly ILoanRepository _loanRepository = loanRepository;
    private readonly ILogger<LoanApplicationService> _logger = logger;

    public async Task<LoanApplication?> GetByIdAsync(int id, CancellationToken ct)
    {
        try
        {
            var result = await _loanRepository.GetByIdAsync(id, ct);
            if (result == null)
            {
                _logger.LogWarning("Loan application with ID {LoanId} not found", id);
            }
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving loan application with ID {LoanId}", id);
            throw;
        }
    }

    public async Task<IEnumerable<LoanApplication>> GetAllAsync(CancellationToken ct)
    {
        try
        {
            var result = await _loanRepository.GetAllAsync(ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all loan applications");
            throw;
        }
    }

    public async Task AddAsync(LoanApplication loanApplication, CancellationToken ct)
    {
        try
        {
            await _loanRepository.AddAsync(loanApplication, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding loan application for applicant: {ApplicantName}", loanApplication.ApplicantName);
            throw;
        }
    }

    public async Task UpdateAsync(LoanApplication loanApplication, CancellationToken ct)
    {
        try
        {
            await _loanRepository.UpdateAsync(loanApplication, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating loan application with ID: {LoanId}", loanApplication.Id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken ct)
    {
        try
        {
            await _loanRepository.DeleteAsync(id, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting loan application with ID: {LoanId}", id);
            throw;
        }
    }
}
