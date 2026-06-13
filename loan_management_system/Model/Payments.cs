using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using loan_management_system.Validations;

namespace loan_management_system.Model;

public class Payments
{
    [Key]
    public int ID { get; set; }
    [Required]
    [ForeignKey("Loans")]
    public int LoansID { get; set; }
    [MinAmount(0)]
    public double  Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public Loans Loans { get; set; }
}