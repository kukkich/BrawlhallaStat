using MediatR;

namespace BrawlhallaStat.Api.Commands;

public record RegisterUser(string Login) : IRequest<string>;
