namespace BrawlhallaStat.Domain.Base;

public interface IHaveId<out T>
{
    public T Id { get; }
}