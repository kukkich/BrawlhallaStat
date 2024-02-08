using BrawlhallaStat.Api.Exceptions;
using BrawlhallaStat.Domain;
using BrawlhallaStat.Domain.Context;
using MediatR;

namespace BrawlhallaStat.Api.BrawlhallaEntities.Requests;

public class AddLegendRequestHandler : IRequestHandler<AddLegendRequest, int>
{
    private readonly BrawlhallaStatContext _context;

    public AddLegendRequestHandler(BrawlhallaStatContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(AddLegendRequest request, CancellationToken cancellationToken)
    {
        var sameNameExist = _context.Legends.Any(x => x.Name == request.Name);
        if (sameNameExist)
        {
            throw new AlreadyExistException(
                who: nameof(Legend),
                propertyName: nameof(Legend.Name),
                value: request.Name
            );
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