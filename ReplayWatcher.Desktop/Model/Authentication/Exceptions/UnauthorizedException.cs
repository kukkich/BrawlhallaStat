namespace ReplayWatcher.Desktop.Model.Authentication.Exceptions;

public class UnauthorizedException : Exception
{
    public override string Message => "Request rejected cause unauthorized";
}