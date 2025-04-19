namespace BmstuSchedule.Client.Exceptions;

public class BmstuScheduleBadGatewayException : BmstuScheduleErrorCodeException
{

    public BmstuScheduleBadGatewayException() : base(502)
    {
    }

    public BmstuScheduleBadGatewayException(Exception innerException) : base(502, innerException)
    {
    }


    public BmstuScheduleBadGatewayException(string message) : base(502, message)
    {
    }

    public BmstuScheduleBadGatewayException(string message, Exception innerException) : base(502, message, innerException)
    {
    }
}