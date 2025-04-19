namespace BmstuSchedule.Client.Exceptions;

public class BmstuScheduleNotFoundException : BmstuScheduleErrorCodeException
{

    public BmstuScheduleNotFoundException() : base(404)
    {
    }

    public BmstuScheduleNotFoundException(Exception innerException) : base(404, innerException)
    {
    }


    public BmstuScheduleNotFoundException(string message) : base(404, message)
    {
    }

    public BmstuScheduleNotFoundException(string message, Exception innerException) : base(404, message, innerException)
    {
    }
}