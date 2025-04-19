namespace BmstuSchedule.Client.Exceptions;

public class BmstuScheduleException : Exception
{
    public BmstuScheduleException()
    {
    }

    public BmstuScheduleException(string message) : base(message)
    {
    }

    public BmstuScheduleException(string message, Exception innerException) : base(message, innerException)
    {
    }
}