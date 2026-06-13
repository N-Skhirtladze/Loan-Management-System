using loan_management_system.Enums;
using loan_management_system.Model;

namespace loan_management_system.DTOs;

public class LoanDto
{
    public double Amount { get; set; }
    public int TermInMonths { get; set; }
}

public class LoanResponseDto
{
    public int ID { get; set; }
    public int CustomerID { get; set; }
    public double Amount { get; set; }
    public double InterestRate { get; set; }
    public int TermMonths { get; set; }
    public double MonthlyPayment { get; set; }
    public LoanStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }

}
