namespace AiHelper.Client.Exceptions;

public class AiHelperMethodNotAllowedException : AiHelperErrorCodeException
{

    public AiHelperMethodNotAllowedException() : base(405)
    {
    }

    public AiHelperMethodNotAllowedException(Exception innerException) : base(405, innerException)
    {
    }


    public AiHelperMethodNotAllowedException(string message) : base(405, message)
    {
    }

    public AiHelperMethodNotAllowedException(string message, Exception innerException) : base(405, message, innerException)
    {
    }
}