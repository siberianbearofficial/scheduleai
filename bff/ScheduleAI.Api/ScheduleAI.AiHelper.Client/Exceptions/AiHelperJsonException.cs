namespace AiHelper.Client.Exceptions;

public class AiHelperJsonException : AiHelperException
{
    public AiHelperJsonException()
    {
    }

    public AiHelperJsonException(string message) : base(message)
    {
    }

    public AiHelperJsonException(string message, Exception innerException) : base(message, innerException)
    {
    }
}