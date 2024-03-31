using BrawlhallaStat.Domain.Identity.Base;
using MediatR;

namespace BrawlhallaStat.Api.Statistics.Requests;

public record DeleteFilterRequest(IUserIdentity Actor, string FilterId) : IRequest;
