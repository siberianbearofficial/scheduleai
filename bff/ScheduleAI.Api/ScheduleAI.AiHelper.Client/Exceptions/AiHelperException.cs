namespace AiHelper.Client.Exceptions;

public class AiHelperException : Exception
{
    public AiHelperException()
    {
    }

    public AiHelperException(string message) : base(message)
    {
    }

    public AiHelperException(string message, Exception innerException) : base(message, innerException)
    {
    }
}