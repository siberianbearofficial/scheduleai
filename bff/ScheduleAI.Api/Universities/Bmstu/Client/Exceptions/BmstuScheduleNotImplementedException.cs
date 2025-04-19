namespace BmstuSchedule.Client.Exceptions;

public class BmstuScheduleNotImplementedException : BmstuScheduleErrorCodeException
{

    public BmstuScheduleNotImplementedException() : base(501)
    {
    }

    public BmstuScheduleNotImplementedException(Exception innerException) : base(501, innerException)
    {
    }


    public BmstuScheduleNotImplementedException(string message) : base(501, message)
    {
    }

    public BmstuScheduleNotImplementedException(string message, Exception innerException) : base(501, message, innerException)
    {
    }
}