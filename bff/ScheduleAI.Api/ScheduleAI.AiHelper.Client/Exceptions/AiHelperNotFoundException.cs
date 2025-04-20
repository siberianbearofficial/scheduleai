namespace AiHelper.Client.Exceptions;

public class AiHelperNotFoundException : AiHelperErrorCodeException
{

    public AiHelperNotFoundException() : base(404)
    {
    }

    public AiHelperNotFoundException(Exception innerException) : base(404, innerException)
    {
    }


    public AiHelperNotFoundException(string message) : base(404, message)
    {
    }

    public AiHelperNotFoundException(string message, Exception innerException) : base(404, message, innerException)
    {
    }
}