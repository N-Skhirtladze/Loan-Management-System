using loan_management_system.DTOs;
using loan_management_system.Enums;
using loan_management_system.Model;
using loan_management_system.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace loan_management_system.Services;

public class LoansService : ILoansService
{
    private readonly LoanContext context;

    public LoansService(LoanContext context)
    {
        this.context = context;
    }
    public async Task<LoanResponseDto> AddLoan(LoanDto loan, int customerId)
    {
        Customers? customer =  await context.Customers.FindAsync(customerId);
        if (customer == null)
            throw new KeyNotFoundException($"Customer with id {customerId} not found");

        if (customer.CreditScore < 300)
        {
            Loans newRejectedLoan = new Loans()
            {
                CustomerID = customerId,
                Amount = loan.Amount,
                InterestRate = 5.0,
                TermMonths = loan.TermInMonths,
                MonthlyPayment = loan.Amount / loan.TermInMonths,
                Status = LoanStatus.Rejected,
                CreatedAt = DateTime.Now,
            };
        
            await context.Loans.AddAsync(newRejectedLoan);
            await context.SaveChangesAsync();
            throw new InvalidOperationException("Credit score is too low to apply for a loan. Minimum score is 300.");
        }
        

        Loans newLoan = new Loans()
        {
            CustomerID = customerId,
            Amount = loan.Amount,
            InterestRate = 5.0,
            TermMonths = loan.TermInMonths,
            MonthlyPayment = loan.Amount / loan.TermInMonths,
            Status = LoanStatus.Approved,
            CreatedAt = DateTime.Now,
        };

        var startDate = DateTime.UtcNow;
        for (int i = 0; i < newLoan.TermMonths; i++)
        {
            newLoan.LoanSchedule.Add(new LoanSchedule()
            {
                PMT = newLoan.MonthlyPayment,
                DATE = startDate.AddMonths(i + 1)
            });
        }

        await context.Loans.AddAsync(newLoan); 
        await context.SaveChangesAsync();
        
        var loanDetails = await context.Loans.Include(e => e.Payments).Include(l => l.LoanSchedule).FirstOrDefaultAsync(c => c.CustomerID == customerId);

        return new LoanResponseDto()
        {
            ID = loanDetails.ID,
            CustomerID = customerId,
            Amount = loanDetails.Amount,
            InterestRate = loanDetails.InterestRate,
            TermMonths = loanDetails.TermMonths,
            MonthlyPayment = loanDetails.MonthlyPayment,
            Status = loanDetails.Status,
            CreatedAt = loanDetails.CreatedAt,
        };

    }

    public async Task<LoanResponseDto> GetLoan(int id)
    {
        Loans? loan = await context.Loans.FindAsync(id);
        
        if (loan == null)
            throw new KeyNotFoundException($"Loan with id {id} not found");

        return new LoanResponseDto()
        {
            ID = loan.ID,
            CustomerID = loan.CustomerID,
            Amount = loan.Amount,
            InterestRate = loan.InterestRate,
            TermMonths = loan.TermMonths,
            MonthlyPayment = loan.MonthlyPayment,
            Status = loan.Status,
            CreatedAt = DateTime.Now,
        };
    }
    
    public async Task<LoanStatus>  GetLoanStatus(int id)
    {
        Loans? loan = await context.Loans.FindAsync(id);

        if (loan == null)
            throw new KeyNotFoundException($"Loan with id {id} not found");
        
        return loan.Status;
    }

    public async Task<List<LoanResponseDto>> GetAllLoans()
    {
        List<Loans> loans = await context.Loans.ToListAsync();

        return loans.Select(loan => new LoanResponseDto()
        {
            ID = loan.ID,
            CustomerID = loan.CustomerID,
            Amount = loan.Amount,
            InterestRate = loan.InterestRate,
            TermMonths = loan.TermMonths,
            MonthlyPayment = loan.MonthlyPayment,
            Status = loan.Status,
            CreatedAt = loan.CreatedAt,
        }).ToList();
    }

    public async Task DeleteLoan(int id)
    {
        Loans? loan = await context.Loans.FindAsync(id);
        if (loan == null)
            throw new KeyNotFoundException($"Loan with id {id} not found");
        context.Loans.Remove(loan);
        await context.SaveChangesAsync();
    }
}