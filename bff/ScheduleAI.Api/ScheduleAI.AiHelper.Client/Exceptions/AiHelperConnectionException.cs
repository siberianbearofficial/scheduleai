namespace AiHelper.Client.Exceptions;

public class AiHelperConnectionException : AiHelperException
{
    public AiHelperConnectionException()
    {
    }

    public AiHelperConnectionException(string message) : base(message)
    {
    }

    public AiHelperConnectionException(string message, Exception innerException) : base(message, innerException)
    {
    }
}