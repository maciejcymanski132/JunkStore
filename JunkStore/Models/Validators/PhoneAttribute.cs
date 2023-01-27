using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class PhoneAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        string phoneNumber = (string)value;

        if (!string.IsNullOrWhiteSpace(phoneNumber))
        {
            // Use regular expression to check if phone number is in valid format
            var regex = new Regex(@"^\+?[0-9]{10,12}$");
            if (!regex.IsMatch(phoneNumber))
            {
                return new ValidationResult("Invalid phone number format. Phone number should be in format +1234567890 or 1234567890");
            }
        }

        return ValidationResult.Success;
    }
}
