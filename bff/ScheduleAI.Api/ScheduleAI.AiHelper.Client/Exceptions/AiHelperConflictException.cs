namespace AiHelper.Client.Exceptions;

public class AiHelperConflictException : AiHelperErrorCodeException
{

    public AiHelperConflictException() : base(409)
    {
    }

    public AiHelperConflictException(Exception innerException) : base(409, innerException)
    {
    }


    public AiHelperConflictException(string message) : base(409, message)
    {
    }

    public AiHelperConflictException(string message, Exception innerException) : base(409, message, innerException)
    {
    }
}