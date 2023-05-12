using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;

namespace project.Models
{
    public class MoreThan2WordsAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string? s = value as string;
            if (s == null || s.Trim().IndexOf(' ') < 1)
                return new ValidationResult("Deve contenere almeno 2 parole");
            return ValidationResult.Success;
        }
    }
}
