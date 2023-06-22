using BrawlhallaStat.Api.Commands.BrawlhallaEntities;
using BrawlhallaStat.Api.Exceptions;
using BrawlhallaStat.Domain;
using BrawlhallaStat.Domain.Context;
using MediatR;

namespace BrawlhallaStat.Api.CommandHandlers.BrawlhallaEntities;

public class AddWeaponHandler : IRequestHandler<AddWeapon, int>
{
    private readonly BrawlhallaStatContext _context;

    public AddWeaponHandler(BrawlhallaStatContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(AddWeapon request, CancellationToken cancellationToken)
    {
        var sameNameExist = _context.Weapons.Any(x => x.Name == request.Name);
        if (sameNameExist)
        {
            throw new AlreadyExistException($"Weapon {request.Name} already exists");
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