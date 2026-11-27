namespace EventRegistrationAPI_ArtemKotliar.Services;

public class RegistrationException : Exception
{
    public int StatusCode { get; }

    public RegistrationException(int statusCode, string message)
        : base(message)
    {
        StatusCode = statusCode;
    }
}