using loan_management_system.Model;

namespace loan_management_system.DTOs;

public class PaymentsDto
{
    public double Amount { get; set; }
}

public class PaymentsResponseDto
{
    public int Id { get; set; }
    public int LoanID { get; set; }
    public double Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    // public Loans Loans { get; set; }
}