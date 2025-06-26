using Core.DTOs;
using Core.Entities;
using Microsoft.AspNetCore.Components;

namespace LoanApplicationSystem.Components.Pages
{
    public partial class LoanApplications : IDisposable
    {
        private PaginationResult<LoanApplicationDto>? paginationResult;
        private LoanApplicationDto currentDto = new();
        private bool showForm = false;
        private bool editMode = false;
        private bool isLoading = true;
        private CancellationTokenSource _cancellationTokenSource = new();

        // Filtering and pagination parameters
        private string? searchTerm;
        private LoanApplicationStatus? selectedStatus;
        private int currentPage = 1;
        private int pageSize = 10;
        private Timer? searchTimer;

        private string? SearchTerm
        {
            get => searchTerm;
            set
            {
                if (searchTerm != value)
                {
                    searchTerm = value;
                    searchTimer?.Dispose();
                    searchTimer = new Timer(async _ =>
                    {
                        await InvokeAsync(HandleSearch);
                    }, null, 500, Timeout.Infinite);
                }
            }
        }
        
        private LoanApplicationStatus? SelectedStatus
        {
            get => selectedStatus;
            set
            {
                if (selectedStatus != value)
                {
                    selectedStatus = value;
                    _ = HandleSearch();
                }
            }
        }

        private int PageSize
        {
            get => pageSize;
            set
            {
                if (pageSize != value)
                {
                    pageSize = value;
                    _ = HandlePageSizeChange();
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {
            Logger.LogDebug("Initializing Loan Applications page");
            await LoadLoans();
        }

        private async Task LoadLoans()
        {
            try
            {
                Logger.LogDebug("Loading loan applications with page: {CurrentPage}, size: {PageSize}, term: '{SearchTerm}', status: {SelectedStatus}", currentPage, pageSize, searchTerm, selectedStatus);
                isLoading = true;
                paginationResult = await LoanService.GetPaginatedAsync(searchTerm, selectedStatus, currentPage, pageSize, _cancellationTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                Logger.LogDebug("Loan loading operation was cancelled");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error loading loan applications");
            }
            finally
            {
                isLoading = false;
                await InvokeAsync(StateHasChanged);
            }
        }

        private async Task HandleSearch()
        {
            currentPage = 1; 
            await LoadLoans();
        }

        private async Task HandlePageSizeChange()
        {
            currentPage = 1;
            await LoadLoans();
        }

        private async Task ChangePage(int page)
        {
            if (page >= 1 && page <= (paginationResult?.TotalPages ?? 1))
            {
                currentPage = page;
                await LoadLoans();
            }
        }

        private void ShowAddForm()
        {
            Logger.LogDebug("User initiated add loan application form");
            currentDto = new LoanApplicationDto();
            editMode = false;
            showForm = true;
        }

        private void EditLoan(LoanApplicationDto loan)
        {
            Logger.LogDebug("User initiated edit for loan application ID: {LoanId}", loan.Id);
            currentDto = loan;
            editMode = true;
            showForm = true;
        }

        private async Task DeleteLoan(int id)
        {
            try
            {
                Logger.LogDebug("User initiated delete for loan application ID: {LoanId}", id);
                await LoanService.DeleteAsync(id, _cancellationTokenSource.Token);
                await LoadLoans();
            }
            catch (OperationCanceledException)
            {
                Logger.LogDebug("Delete operation was cancelled for loan application ID: {LoanId}", id);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error deleting loan application ID: {LoanId}", id);
            }
        }

        private async Task HandleValidSubmit()
        {
            try
            {
                if (editMode)
                {
                    Logger.LogDebug("User submitted update for loan application ID: {LoanId}", currentDto.Id);
                    await LoanService.UpdateAsync(currentDto.Id, currentDto, _cancellationTokenSource.Token);
                }
                else
                {
                    Logger.LogDebug("User submitted new loan application for applicant: {ApplicantName}", currentDto.ApplicantName);
                    await LoanService.AddAsync(currentDto, _cancellationTokenSource.Token);
                }
                showForm = false;
                await LoadLoans();
            }
            catch (OperationCanceledException)
            {
                Logger.LogDebug("Form submission operation was cancelled");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error processing loan application form submission");
            }
        }

        private void CancelForm()
        {
            Logger.LogDebug("User cancelled loan application form");
            showForm = false;
        }

        public void Dispose()
        {
            Logger.LogDebug("Disposing Loan Applications component");
            searchTimer?.Dispose();
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }
    }
} 