namespace AiHelper.Client.Exceptions;

public class AiHelperNotImplementedException : AiHelperErrorCodeException
{

    public AiHelperNotImplementedException() : base(501)
    {
    }

    public AiHelperNotImplementedException(Exception innerException) : base(501, innerException)
    {
    }


    public AiHelperNotImplementedException(string message) : base(501, message)
    {
    }

    public AiHelperNotImplementedException(string message, Exception innerException) : base(501, message, innerException)
    {
    }
}