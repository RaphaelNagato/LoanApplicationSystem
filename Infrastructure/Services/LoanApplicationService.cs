using Core.Entities;
using Core.DTOs;
using Core.Spec;
using Infrastructure.DB;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

internal class LoanApplicationService(ILoanRepository loanRepository, ILogger<LoanApplicationService> logger) : ILoanApplicationService
{
    private readonly ILoanRepository _loanRepository = loanRepository;
    private readonly ILogger<LoanApplicationService> _logger = logger;

    public async Task<LoanApplicationDto?> GetByIdAsync(int id, CancellationToken ct)
    {
        try
        {
            var entity = await _loanRepository.GetByIdAsync(id, ct);
            if (entity == null)
            {
                _logger.LogWarning("Loan application with ID {LoanId} not found", id);
                return null;
            }
            return LoanApplicationDto.FromEntity(entity);
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

    public async Task<PaginationResult<LoanApplicationDto>> GetPaginatedAsync(string? searchTerm, LoanApplicationStatus? status, int page, int pageSize, CancellationToken ct)
    {
        try
        {
            var spec = new LoanApplicationSpecification(searchTerm, status, page, pageSize);
            var countSpec = new LoanApplicationSpecification(searchTerm, status, 1, int.MaxValue);

            var entities = await _loanRepository.GetAsync(spec, ct);
            var totalCount = await _loanRepository.CountAsync(countSpec, ct);

            var dtos = LoanApplicationDto.FromEntities(entities);
            return new PaginationResult<LoanApplicationDto>(dtos, totalCount, page, pageSize);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving paginated loan applications");
            throw;
        }
    }

    public async Task<LoanApplicationDto> AddAsync(LoanApplicationDto dto, CancellationToken ct)
    {
        try
        {
            var entity = dto.ToEntity();
            await _loanRepository.AddAsync(entity, ct);
            return LoanApplicationDto.FromEntity(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding loan application for applicant: {ApplicantName}", dto.ApplicantName);
            throw;
        }
    }

    public async Task<LoanApplicationDto> UpdateAsync(int id, LoanApplicationDto dto, CancellationToken ct)
    {
        try
        {
            var entity = await _loanRepository.GetByIdAsync(id, ct);
            if (entity == null)
            {
                throw new InvalidOperationException($"Loan application with ID {id} not found.");
            }

            entity.ApplicantName = dto.ApplicantName;
            entity.LoanAmount = dto.LoanAmount;
            entity.LoanTermMonths = dto.LoanTermMonths;
            entity.InterestRate = dto.InterestRate;
            entity.LoanStatus = dto.LoanStatus;
            entity.ApplicationDate = dto.ApplicationDate;

            await _loanRepository.UpdateAsync(entity, ct);
            return LoanApplicationDto.FromEntity(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating loan application with ID: {LoanId}", id);
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
