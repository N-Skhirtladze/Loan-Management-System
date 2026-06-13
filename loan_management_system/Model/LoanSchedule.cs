using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace loan_management_system.Model;

public class LoanSchedule
{
    [Key]
    public int ID { get; set; }
    [Required]
    [ForeignKey("Loans")]
    public int LoansID  { get; set; }
    public double PMT { get; set; }
    public DateTime DATE { get; set; }
    public Loans Loans { get; set; }
    
}