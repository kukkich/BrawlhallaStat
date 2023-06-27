using BrawlhallaStat.Api.Commands.BrawlhallaEntities;
using BrawlhallaStat.Api.Exceptions;
using BrawlhallaStat.Domain;
using BrawlhallaStat.Domain.Context;
using MediatR;

namespace BrawlhallaStat.Api.CommandHandlers.BrawlhallaEntities;

public class AddLegendHandler : IRequestHandler<AddLegend, int>
{
    private readonly BrawlhallaStatContext _context;

    public AddLegendHandler(BrawlhallaStatContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(AddLegend request, CancellationToken cancellationToken)
    {
        var sameNameExist = _context.Legends.Any(x => x.Name == request.Name);
        if (sameNameExist)
        {
            throw new AlreadyExistException($"LegendDetails {request.Name} already exists");
        }

        var legend = new Legend
        {
            Name = request.Name,
            FirstWeaponId = request.FirstWeaponId,
            SecondWeaponId = request.SecondWeaponId
        };
        _context.Legends.Add(legend);

        await _context.SaveChangesAsync(cancellationToken);

        return legend.Id;
    }
}