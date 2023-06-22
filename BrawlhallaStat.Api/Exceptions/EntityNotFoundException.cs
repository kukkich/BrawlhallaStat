using BrawlhallaStat.Domain.Base;

namespace BrawlhallaStat.Api.Exceptions;

public class EntityNotFoundException<TEntity, TId> : ApiException
    where TEntity : IHaveId<TId>
{
    private readonly TId _id;

    public override string Message 
    {
        get
        {
            var name = typeof(TEntity).Name;

            return $"{name} with id {_id} wasn't found";
        }
    }

    public EntityNotFoundException(TId id)
    {
        _id = id;
    }
}