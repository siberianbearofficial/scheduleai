namespace AiHelper.Client.Exceptions;

public class AiHelperNotAcceptableException : AiHelperErrorCodeException
{

    public AiHelperNotAcceptableException() : base(406)
    {
    }

    public AiHelperNotAcceptableException(Exception innerException) : base(406, innerException)
    {
    }


    public AiHelperNotAcceptableException(string message) : base(406, message)
    {
    }

    public AiHelperNotAcceptableException(string message, Exception innerException) : base(406, message, innerException)
    {
    }
}