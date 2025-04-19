namespace BmstuSchedule.Client.Exceptions;

public class BmstuScheduleUnauthorizedException : BmstuScheduleErrorCodeException
{

    public BmstuScheduleUnauthorizedException() : base(401)
    {
    }

    public BmstuScheduleUnauthorizedException(Exception innerException) : base(401, innerException)
    {
    }


    public BmstuScheduleUnauthorizedException(string message) : base(401, message)
    {
    }

    public BmstuScheduleUnauthorizedException(string message, Exception innerException) : base(401, message, innerException)
    {
    }
}