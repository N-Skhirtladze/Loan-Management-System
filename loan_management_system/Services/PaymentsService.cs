using loan_management_system.DTOs;
using loan_management_system.Enums;
using loan_management_system.Model;
using loan_management_system.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace loan_management_system.Services;

public class PaymentsService : IPaymentsService
{
    private readonly LoanContext context;

    public PaymentsService(LoanContext context)
    {
        this.context = context;
    }
    
    public async Task<PaymentsResponseDto> Payment(PaymentsDto paymentsDto, int loanId)
    {
        Loans? loan = await context.Loans.FindAsync(loanId);
        if (loan == null)
            throw new KeyNotFoundException($"Loan with id {loanId} not found");
        
        Customers? customer = await context.Customers.FindAsync(loan?.CustomerID);
        if (customer == null)
            throw new KeyNotFoundException($"Customer with id {loan.CustomerID} not found");

        // List<LoanSchedule?> schedules = [await context.LoanSchedule.Include(e => e.Loans).FirstOrDefaultAsync(e => e.LoansID == loan!.ID)];
        int months = ((DateTime.UtcNow.Year - loan!.CreatedAt.Year) * 12) +
                     (DateTime.UtcNow.Month - loan!.CreatedAt.Month);
        
        // int monthDifference = ((schedules[months]!.DATE.Month -DateTime.UtcNow.Year) * 12) +
        //                       (schedules[months]!.DATE.Month - DateTime.UtcNow.Month);

        if (paymentsDto.Amount >= loan.Amount && months <= loan.TermMonths)
        {
            loan.Amount -= paymentsDto.Amount;
            customer!.CreditScore += 50;
            loan.Status = LoanStatus.Closed;
        }
        else if (paymentsDto.Amount < loan.Amount && months <= loan.TermMonths)
        {
            loan.Amount -= paymentsDto.Amount;
            customer!.CreditScore += 50;
        }
        else if (paymentsDto.Amount >= loan.Amount && months > loan.TermMonths)
        {
            loan.Amount = 0;
            loan.Status = LoanStatus.Closed;
            customer!.CreditScore -= (months - loan.TermMonths) * 50;
        }
        else
        {
            loan.Amount -=  paymentsDto.Amount;
            customer!.CreditScore -= (months - loan.TermMonths) * 50;
        }

        Payments newPayment = new Payments()
        {
            LoansID = loan.ID,
            Amount = paymentsDto.Amount,
            PaymentDate = DateTime.UtcNow,
        };
        await context.Payments.AddAsync(newPayment);
        await context.SaveChangesAsync();
        
        return new PaymentsResponseDto()
        {
            Id = newPayment.ID,
            LoanID = loanId,
            Amount = paymentsDto.Amount,
            PaymentDate = DateTime.UtcNow,
            // Loans = loan,
        };

    }
    
}