using System.ComponentModel.DataAnnotations;

namespace loan_management_system.Validations;

public class MinAmountAttribute : ValidationAttribute
{
    private readonly double _minAmount;
    public MinAmountAttribute(double minAmount)
    {
        _minAmount = minAmount;
    }

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value is double Amount)
        {
            if (Amount <= _minAmount)
            {
                return new ValidationResult($"Must be more then {_minAmount}$");
            }

        } 
        return ValidationResult.Success;
    }
}