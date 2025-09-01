using System.ComponentModel.DataAnnotations;

namespace Education.Domain;

public class DateInFutureAttribute : ValidationAttribute
{
    private readonly Func<DateTime> _datetimeNowProvider;

    public DateInFutureAttribute():this(() => DateTime.Now) {}

    public DateInFutureAttribute(Func<DateTime> datetimeNowProvider)
    {
        _datetimeNowProvider = datetimeNowProvider;
        ErrorMessage = "The date must be in the future.";
    }



    public override bool IsValid(object? value)
    {
        bool isValid = false;
        if (value is DateTime dateTime)
        {
            isValid = dateTime > _datetimeNowProvider();
        }

        return isValid;
    }




    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is DateTime dateTime)
        {
            if (dateTime > _datetimeNowProvider())
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(ErrorMessage);
        }
        return new ValidationResult("Invalid date format.");
    }

}
