namespace AiHelper.Client.Exceptions;

public class AiHelperErrorCodeException : AiHelperException
{

    public int Code { get; }

    public AiHelperErrorCodeException(int code) : base($"Api returns error code {code}")
    {
        Code = code;
    }

    public AiHelperErrorCodeException(int code, Exception innerException) : base($"Api returns error code {code}", innerException)
    {
        Code = code;
    }


    public AiHelperErrorCodeException(int code, string message) : base(message)
    {
        Code = code;
    }

    public AiHelperErrorCodeException(int code, string message, Exception innerException) : base(message, innerException)
    {
        Code = code;
    }
}