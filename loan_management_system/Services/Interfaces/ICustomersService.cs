using loan_management_system.DTOs;
using  loan_management_system.Model;

namespace loan_management_system.Services.Interfaces;

public interface ICustomersService
{
   Task<CustomersResponseDto> AddCustomers(CustomersDto customers);
   Task<CustomersResponseDto> GetCustomer(int id);
   Task<List<LoanResponseDto>> GetLoansByCustomerId(int id);
   Task<List<CustomersResponseDto>> GetAllCustomers();
   Task DeleteCustomer(int id);
}