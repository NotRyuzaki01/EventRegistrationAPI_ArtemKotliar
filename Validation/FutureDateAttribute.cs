using System.ComponentModel.DataAnnotations;

namespace EventRegistrationAPI_ArtemKotliar.Validation;

/// <summary>
/// Custom validation attribute that checks whether the passed object is of type DateTime and if so checks if its a data set in the future from now. 
/// </summary>
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