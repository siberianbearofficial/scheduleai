namespace BmstuSchedule.Client.Exceptions;

public class BmstuScheduleInternalServerErrorException : BmstuScheduleErrorCodeException
{

    public BmstuScheduleInternalServerErrorException() : base(500)
    {
    }

    public BmstuScheduleInternalServerErrorException(Exception innerException) : base(500, innerException)
    {
    }


    public BmstuScheduleInternalServerErrorException(string message) : base(500, message)
    {
    }

    public BmstuScheduleInternalServerErrorException(string message, Exception innerException) : base(500, message, innerException)
    {
    }
}