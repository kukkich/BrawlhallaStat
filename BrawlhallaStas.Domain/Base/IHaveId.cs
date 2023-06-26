namespace BrawlhallaStat.Domain.Base;

public interface IHaveId<T>
{
    public T Id { get; set; }
}