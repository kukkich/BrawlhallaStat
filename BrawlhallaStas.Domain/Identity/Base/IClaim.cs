namespace BrawlhallaStat.Domain.Identity.Base;

public interface IClaim
{
    public string Name { get; set; }
    public string Value { get; set; }
}