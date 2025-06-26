using Core.Entities;
using Core.Spec;
using Infrastructure.Spec;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.DB;

internal class LoanRepository(LoanDbContext context, ILogger<LoanRepository> logger) : ILoanRepository
{
    public async Task<LoanApplication?> GetByIdAsync(int id, CancellationToken ct)
    {
        try
        {
            var result = await context.LoanApplications.FindAsync([id], ct);
            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Database error while querying loan application with ID: {LoanId}", id);
            throw;
        }
    }

    public async Task<IEnumerable<LoanApplication>> GetAllAsync(CancellationToken ct)
    {
        try
        {
            // No tracking since this is read-only
            var result = await context.LoanApplications.AsNoTracking().ToListAsync(ct);
            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Database error while querying all loan applications");
            throw;
        }
    }

    public async Task<IEnumerable<LoanApplication>> GetAsync(BaseSpecification<LoanApplication> spec, CancellationToken ct)
    {
        try
        {
            var query = SpecificationEvaluator<LoanApplication>.GetQuery(context.LoanApplications.AsNoTracking(), spec);
            return await query.ToListAsync(ct);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Database error while querying loan applications with specification");
            throw;
        }
    }

    public async Task<int> CountAsync(BaseSpecification<LoanApplication> spec, CancellationToken ct)
    {
        try
        {
            var query = SpecificationEvaluator<LoanApplication>.GetQuery(context.LoanApplications.AsNoTracking(), spec);
            return await query.CountAsync(ct);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Database error while counting loan applications with specification");
            throw;
        }
    }

    public async Task AddAsync(LoanApplication loanApplication, CancellationToken ct)
    {
        try
        {
            await context.LoanApplications.AddAsync(loanApplication, ct);
            await context.SaveChangesAsync(ct);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Database error while adding loan application for applicant: {ApplicantName}", loanApplication.ApplicantName);
            throw;
        }
    }

    public async Task UpdateAsync(LoanApplication loanApplication, CancellationToken ct)
    {
        try
        {
            var trackedEntity = context.ChangeTracker
                .Entries<LoanApplication>()
                .FirstOrDefault(e => e.Entity.Id == loanApplication.Id);

            if (trackedEntity == null)
            {
                context.LoanApplications.Attach(loanApplication);
            }
            else if (!ReferenceEquals(trackedEntity.Entity, loanApplication))
            {
                trackedEntity.CurrentValues.SetValues(loanApplication);
            }

            await context.SaveChangesAsync(ct);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Database error while updating loan application with ID: {LoanId}", loanApplication.Id);
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
                context.LoanApplications.Remove(loanApplication);
                await context.SaveChangesAsync(ct);
            }
            else
            {
                logger.LogWarning("Attempted to delete loan application with ID {LoanId} but it was not found", id);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Database error while deleting loan application with ID: {LoanId}", id);
            throw;
        }
    }
}

