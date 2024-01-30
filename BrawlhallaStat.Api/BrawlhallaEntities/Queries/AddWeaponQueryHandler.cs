using BrawlhallaStat.Api.Exceptions;
using BrawlhallaStat.Domain;
using BrawlhallaStat.Domain.Context;
using MediatR;

namespace BrawlhallaStat.Api.BrawlhallaEntities.Queries;

public class AddWeaponQueryHandler : IRequestHandler<AddWeaponQuery, int>
{
    private readonly BrawlhallaStatContext _context;

    public AddWeaponQueryHandler(BrawlhallaStatContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(AddWeaponQuery request, CancellationToken cancellationToken)
    {
        var sameNameExist = _context.Weapons.Any(x => x.Name == request.Name);
        if (sameNameExist)
        {
            throw new AlreadyExistException(
                who: nameof(Weapon),
                propertyName: nameof(Weapon.Name),
                value: request.Name
            );
        }

        var weapon = new Weapon
        {
            Name = request.Name,
        };
        _context.Weapons.Add(weapon);

        await _context.SaveChangesAsync(cancellationToken);

        return weapon.Id;
    }
}