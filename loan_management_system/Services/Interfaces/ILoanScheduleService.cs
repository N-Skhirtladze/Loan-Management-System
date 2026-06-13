using loan_management_system.Model;

namespace loan_management_system.Services.Interfaces;

public interface ILoanScheduleService
{
    Task<List<LoanSchedule>> GetLoanSchedules(int id);
}