namespace AiHelper.Client.Exceptions;

public class AiHelperUnauthorizedException : AiHelperErrorCodeException
{

    public AiHelperUnauthorizedException() : base(401)
    {
    }

    public AiHelperUnauthorizedException(Exception innerException) : base(401, innerException)
    {
    }


    public AiHelperUnauthorizedException(string message) : base(401, message)
    {
    }

    public AiHelperUnauthorizedException(string message, Exception innerException) : base(401, message, innerException)
    {
    }
}