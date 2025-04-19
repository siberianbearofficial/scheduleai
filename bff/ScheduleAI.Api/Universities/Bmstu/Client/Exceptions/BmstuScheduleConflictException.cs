namespace BmstuSchedule.Client.Exceptions;

public class BmstuScheduleConflictException : BmstuScheduleErrorCodeException
{

    public BmstuScheduleConflictException() : base(409)
    {
    }

    public BmstuScheduleConflictException(Exception innerException) : base(409, innerException)
    {
    }


    public BmstuScheduleConflictException(string message) : base(409, message)
    {
    }

    public BmstuScheduleConflictException(string message, Exception innerException) : base(409, message, innerException)
    {
    }
}