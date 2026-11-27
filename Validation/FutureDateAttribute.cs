using System.ComponentModel.DataAnnotations;

namespace EventRegistrationAPI_ArtemKotliar.Validation;

public class FutureDateAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is not DateTime date)
        {
            return false;
        }

        if (date.Kind == DateTimeKind.Unspecified)
        {
            date = DateTime.SpecifyKind(date, DateTimeKind.Utc);
        }
        else
        {
            date = date.ToUniversalTime();
        }

        return date > DateTime.UtcNow;
    }
}