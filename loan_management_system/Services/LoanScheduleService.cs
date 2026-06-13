using loan_management_system.Model;
using loan_management_system.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace loan_management_system.Services;

public class LoanScheduleService : ILoanScheduleService
{
    private readonly LoanContext context;

    public LoanScheduleService(LoanContext context)
    {
        this.context = context;
    }
    public async Task<List<LoanSchedule>> GetLoanSchedules(int loanId)
    {
        List<LoanSchedule> loanSchedules = await context.LoanSchedule.Where(e => e.LoansID == loanId).ToListAsync();

        if (loanSchedules == null)
            throw new KeyNotFoundException($"Loan schedule with id {loanId} not found");
        
        return loanSchedules!;
    }
}