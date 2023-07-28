namespace BrawlhallaStat.Domain.Base;

public interface IHaveId<T>
{
    //Todo remove set and remake dependent troubles
    public T Id { get; set; }
}