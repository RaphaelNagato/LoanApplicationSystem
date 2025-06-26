using Core.DTOs;
using Core.Entities;

namespace Infrastructure.Services;

public interface ILoanApplicationService
{
    Task<LoanApplicationDto> AddAsync(LoanApplicationDto dto, CancellationToken ct);
    Task DeleteAsync(int id, CancellationToken ct);
    Task<LoanApplicationDto?> GetByIdAsync(int id, CancellationToken ct);
    Task<LoanApplicationDto> UpdateAsync(int id, LoanApplicationDto dto, CancellationToken ct);
    Task<PaginationResult<LoanApplicationDto>> GetPaginatedAsync(string? searchTerm, LoanApplicationStatus? status, int page, int pageSize, CancellationToken ct);
}