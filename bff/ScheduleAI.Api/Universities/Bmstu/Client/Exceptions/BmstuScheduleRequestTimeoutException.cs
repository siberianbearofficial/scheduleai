namespace BmstuSchedule.Client.Exceptions;

public class BmstuScheduleRequestTimeoutException : BmstuScheduleErrorCodeException
{

    public BmstuScheduleRequestTimeoutException() : base(408)
    {
    }

    public BmstuScheduleRequestTimeoutException(Exception innerException) : base(408, innerException)
    {
    }


    public BmstuScheduleRequestTimeoutException(string message) : base(408, message)
    {
    }

    public BmstuScheduleRequestTimeoutException(string message, Exception innerException) : base(408, message, innerException)
    {
    }
}