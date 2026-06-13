using loan_management_system.DTOs;
using loan_management_system.Enums;
using loan_management_system.Model;

namespace loan_management_system.Services.Interfaces;

public interface ILoansService
{
    Task<LoanResponseDto> AddLoan(LoanDto loan, int customerId);
    Task<LoanResponseDto> GetLoan(int id);
    Task<List<LoanResponseDto>> GetAllLoans();
    Task<LoanStatus> GetLoanStatus(int id);
    Task DeleteLoan(int id);
}