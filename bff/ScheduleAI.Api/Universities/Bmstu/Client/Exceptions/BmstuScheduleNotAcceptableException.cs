namespace BmstuSchedule.Client.Exceptions;

public class BmstuScheduleNotAcceptableException : BmstuScheduleErrorCodeException
{

    public BmstuScheduleNotAcceptableException() : base(406)
    {
    }

    public BmstuScheduleNotAcceptableException(Exception innerException) : base(406, innerException)
    {
    }


    public BmstuScheduleNotAcceptableException(string message) : base(406, message)
    {
    }

    public BmstuScheduleNotAcceptableException(string message, Exception innerException) : base(406, message, innerException)
    {
    }
}