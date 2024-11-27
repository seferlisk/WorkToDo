using System;
using System.ComponentModel.DataAnnotations;

namespace WorkToDo.Helper
{
    public class DateNotPastAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is DateTime dateValue)
        {
            if (dateValue < DateTime.Now.Date)
            {
                return new ValidationResult(ErrorMessage ?? "The date cannot be in the past.");
            }
        }
        return ValidationResult.Success;
    }
}
}
