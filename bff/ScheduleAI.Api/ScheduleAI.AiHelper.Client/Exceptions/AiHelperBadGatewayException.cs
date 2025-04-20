namespace AiHelper.Client.Exceptions;

public class AiHelperBadGatewayException : AiHelperErrorCodeException
{

    public AiHelperBadGatewayException() : base(502)
    {
    }

    public AiHelperBadGatewayException(Exception innerException) : base(502, innerException)
    {
    }


    public AiHelperBadGatewayException(string message) : base(502, message)
    {
    }

    public AiHelperBadGatewayException(string message, Exception innerException) : base(502, message, innerException)
    {
    }
}