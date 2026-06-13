using loan_management_system.DTOs;
using loan_management_system.Model;
using loan_management_system.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace loan_management_system.Services;

public class CustomersService : ICustomersService
{
    private readonly LoanContext context;

    public CustomersService(LoanContext context)
    {
        this.context = context;
    }
    public async Task<CustomersResponseDto> GetCustomer(int id)
    {
        Customers? customer = await context.Customers.Include(e => e.Loans).FirstOrDefaultAsync(e => e.ID == id);

        if (customer == null)
            throw new KeyNotFoundException($"Customer with id {id} not found");
        
        return new CustomersResponseDto()
        {
            ID = customer.ID,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            PersonNumber = customer.PersonNumber,
            BirthDate = customer.BirthDate,
            CreditScore = customer.CreditScore,
        };
    }

    public async Task<CustomersResponseDto> AddCustomers(CustomersDto customers)
    {
        Customers newCustomer = new Customers()
        {
            FirstName = customers.FirstName,
            LastName = customers.LastName,
            PersonNumber = customers.PersonNumber,
            BirthDate = customers.BirthDate,
            CreditScore = 300
        };
        
        await context.Customers.AddAsync(newCustomer);
        await context.SaveChangesAsync();
        
        return new CustomersResponseDto()
        {
            ID = newCustomer.ID,
            FirstName = newCustomer.FirstName,
            LastName = newCustomer.LastName,
            PersonNumber = newCustomer.PersonNumber,
            BirthDate = newCustomer.BirthDate,
            CreditScore = newCustomer.CreditScore,
        };
    }

    public async Task<List<LoanResponseDto>> GetLoansByCustomerId(int id)
    {
        Customers? customer = await context.Customers.Include(e => e.Loans).FirstOrDefaultAsync(e => e.ID == id);

        if (customer == null)
            throw new KeyNotFoundException($"Customer with id {id} not found");

        List<Loans> loans = customer.Loans.ToList();
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

    public async Task<List<CustomersResponseDto>> GetAllCustomers()
    {
        List<Customers> customers = await context.Customers.ToListAsync();
        
        return customers.Select(customer => new CustomersResponseDto()
        {
            ID = customer.ID,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            BirthDate = customer.BirthDate,
            CreditScore = customer.CreditScore,
        }).ToList();
    }

    public async Task DeleteCustomer(int id)
    {
        Customers? customer = await context.Customers.FindAsync(id);
        if (customer == null)
            throw new KeyNotFoundException($"Customer with id {id} not found");
        
        context.Customers.Remove(customer);
        await context.SaveChangesAsync();
    }
}