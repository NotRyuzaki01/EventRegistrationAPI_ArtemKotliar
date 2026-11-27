namespace EventRegistrationAPI_ArtemKotliar.Services;

/// <summary>
/// RegistrationService custom exception that stores the code and the message. 
/// </summary>
public class RegistrationException : Exception
{
    public int StatusCode { get; }

    public RegistrationException(int statusCode, string message)
        : base(message)
    {
        StatusCode = statusCode;
    }
}