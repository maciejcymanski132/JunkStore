using JunkStore.Models;
using JunkStore.Models.Types;
using System.ComponentModel.DataAnnotations;

public class GreaterThanZeroAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var order = (Order)validationContext.ObjectInstance;
        if (order.OrderStatus == OrderStatus.Completed && (decimal)value <= 0)
        {
            return new ValidationResult(ErrorMessage);
        }
        return ValidationResult.Success;
    }
}