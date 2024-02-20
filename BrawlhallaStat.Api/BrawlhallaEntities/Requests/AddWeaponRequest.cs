using MediatR;

namespace BrawlhallaStat.Api.BrawlhallaEntities.Requests;

public record AddWeaponRequest(int Id, string Name) : IRequest;