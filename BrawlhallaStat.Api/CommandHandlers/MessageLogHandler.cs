using BrawlhallaStat.Api.Commands;
using MediatR;

namespace BrawlhallaStat.Api.CommandHandlers;

public class MessageLogHandler : IRequestHandler<TestMessageCommand, int>
{
    public Task<int> Handle(TestMessageCommand request, CancellationToken cancellationToken)
    {
        var id = Random.Shared.Next();
        Console.WriteLine($"{request.Payload} id:{id}");
        return Task.FromResult(id);
    }
}