using BrawlhallaStat.Domain.Identity.Base;
using MediatR;

namespace BrawlhallaStat.Api.Replays.Commands;

public record UploadReplayCommand(IUserIdentity User, IFormFile File) : IRequest<string>;
