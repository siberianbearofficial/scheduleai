namespace AiHelper.Client.Exceptions;

public class AiHelperBadRequestException : AiHelperErrorCodeException
{

    public AiHelperBadRequestException() : base(400)
    {
    }

    public AiHelperBadRequestException(Exception innerException) : base(400, innerException)
    {
    }


    public AiHelperBadRequestException(string message) : base(400, message)
    {
    }

    public AiHelperBadRequestException(string message, Exception innerException) : base(400, message, innerException)
    {
    }
}