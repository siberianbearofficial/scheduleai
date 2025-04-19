namespace BmstuSchedule.Client.Exceptions;

public class BmstuScheduleBadRequestException : BmstuScheduleErrorCodeException
{

    public BmstuScheduleBadRequestException() : base(400)
    {
    }

    public BmstuScheduleBadRequestException(Exception innerException) : base(400, innerException)
    {
    }


    public BmstuScheduleBadRequestException(string message) : base(400, message)
    {
    }

    public BmstuScheduleBadRequestException(string message, Exception innerException) : base(400, message, innerException)
    {
    }
}