namespace BmstuSchedule.Client.Exceptions;

public class BmstuScheduleErrorCodeException : BmstuScheduleException
{

    public int Code { get; }

    public BmstuScheduleErrorCodeException(int code) : base($"Api returns error code {code}")
    {
        Code = code;
    }

    public BmstuScheduleErrorCodeException(int code, Exception innerException) : base($"Api returns error code {code}", innerException)
    {
        Code = code;
    }


    public BmstuScheduleErrorCodeException(int code, string message) : base(message)
    {
        Code = code;
    }

    public BmstuScheduleErrorCodeException(int code, string message, Exception innerException) : base(message, innerException)
    {
        Code = code;
    }
}