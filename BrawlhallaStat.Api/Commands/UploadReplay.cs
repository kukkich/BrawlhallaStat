using BrawlhallaStat.Domain.Base;
using MediatR;

namespace BrawlhallaStat.Api.Commands;

public record UploadReplay(IUserIdentity User, IFormFile File) : IRequest<string>;
