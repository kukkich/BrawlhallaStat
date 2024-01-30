using BrawlhallaStat.Api.Exceptions;

namespace BrawlhallaStat.Api.Replays.Exceptions;

public abstract class ReplayUploadException : ApiException
{
    protected ReplayUploadException(string message)
        : base(message) 
    { }

    protected ReplayUploadException() {}
}