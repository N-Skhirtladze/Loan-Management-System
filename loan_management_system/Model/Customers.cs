using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;
using loan_management_system.Validations;

namespace loan_management_system.Model;

public class Customers
{
    [Key]
    public int ID { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string  LastName { get; set; }
    public string PersonNumber { get; set; }
    [MinAge(18)]
    public DateTime BirthDate { get; set; }
    public int CreditScore { get; set; }
    public ICollection<Loans> Loans { get; set; }
}