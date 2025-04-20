namespace AiHelper.Client.Exceptions;

public class AiHelperInternalServerErrorException : AiHelperErrorCodeException
{

    public AiHelperInternalServerErrorException() : base(500)
    {
    }

    public AiHelperInternalServerErrorException(Exception innerException) : base(500, innerException)
    {
    }


    public AiHelperInternalServerErrorException(string message) : base(500, message)
    {
    }

    public AiHelperInternalServerErrorException(string message, Exception innerException) : base(500, message, innerException)
    {
    }
}