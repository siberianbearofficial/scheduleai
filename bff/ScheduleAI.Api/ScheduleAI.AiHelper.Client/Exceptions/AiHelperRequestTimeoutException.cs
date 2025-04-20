namespace AiHelper.Client.Exceptions;

public class AiHelperRequestTimeoutException : AiHelperErrorCodeException
{

    public AiHelperRequestTimeoutException() : base(408)
    {
    }

    public AiHelperRequestTimeoutException(Exception innerException) : base(408, innerException)
    {
    }


    public AiHelperRequestTimeoutException(string message) : base(408, message)
    {
    }

    public AiHelperRequestTimeoutException(string message, Exception innerException) : base(408, message, innerException)
    {
    }
}