using MediatR;

namespace BrawlhallaStat.Api.Commands;

public record TestMessageCommand(string Payload) : IRequest<int>;