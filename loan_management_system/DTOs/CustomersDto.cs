using loan_management_system.Model;

namespace loan_management_system.DTOs;

public class CustomersDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PersonNumber { get; set; }
    public DateTime BirthDate { get; set; }
}

public class CustomersResponseDto
{
    public int ID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PersonNumber { get; set; }
    public DateTime BirthDate { get; set; }
    public int CreditScore { get; set; }
}