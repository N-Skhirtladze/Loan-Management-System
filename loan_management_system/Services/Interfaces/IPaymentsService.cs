using loan_management_system.DTOs;

namespace loan_management_system.Services.Interfaces;

public interface IPaymentsService
{
    Task<PaymentsResponseDto> Payment(PaymentsDto paymentsDto, int loanId);
}