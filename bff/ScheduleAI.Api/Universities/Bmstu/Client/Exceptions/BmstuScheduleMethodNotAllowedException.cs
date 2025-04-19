namespace BmstuSchedule.Client.Exceptions;

public class BmstuScheduleMethodNotAllowedException : BmstuScheduleErrorCodeException
{

    public BmstuScheduleMethodNotAllowedException() : base(405)
    {
    }

    public BmstuScheduleMethodNotAllowedException(Exception innerException) : base(405, innerException)
    {
    }


    public BmstuScheduleMethodNotAllowedException(string message) : base(405, message)
    {
    }

    public BmstuScheduleMethodNotAllowedException(string message, Exception innerException) : base(405, message, innerException)
    {
    }
}