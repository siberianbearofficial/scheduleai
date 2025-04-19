namespace BmstuSchedule.Client.Exceptions;

public class BmstuScheduleConnectionException : BmstuScheduleException
{
    public BmstuScheduleConnectionException()
    {
    }

    public BmstuScheduleConnectionException(string message) : base(message)
    {
    }

    public BmstuScheduleConnectionException(string message, Exception innerException) : base(message, innerException)
    {
    }
}