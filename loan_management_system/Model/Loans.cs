using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using loan_management_system.Enums;

namespace loan_management_system.Model;

public class Loans
{
    [Key]
    public int ID { get; set; }
    [Required]
    [ForeignKey("Customers")]
    public int CustomerID { get; set; }
    [Range(500, 50000, ErrorMessage = "Money must be between 500 and 50000")]
    public double Amount { get; set; }
    public double InterestRate { get; set; }
    [Range(6, 60, ErrorMessage = "Loan term must be between 6 and 60 months")]
    public int TermMonths { get; set; }
    public double MonthlyPayment { get; set; }
    public LoanStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public Customers Customers { get; set; }
    public ICollection<Payments> Payments { get; set; }
    public ICollection<LoanSchedule> LoanSchedule { get; set; }
}