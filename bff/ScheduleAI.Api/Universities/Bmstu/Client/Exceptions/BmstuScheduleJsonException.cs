namespace BmstuSchedule.Client.Exceptions;

public class BmstuScheduleJsonException : BmstuScheduleException
{
    public BmstuScheduleJsonException()
    {
    }

    public BmstuScheduleJsonException(string message) : base(message)
    {
    }

    public BmstuScheduleJsonException(string message, Exception innerException) : base(message, innerException)
    {
    }
}