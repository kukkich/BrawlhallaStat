using BrawlhallaStat.Domain.Identity.Base;
using MediatR;

namespace BrawlhallaStat.Api.Replays.Requests;

public record UploadReplayRequest(IUserIdentity User, IFormFile File) : IRequest<string>;
