using BrawlhallaStat.Domain.Identity.Base;

namespace BrawlhallaStat.Domain.Identity.Dto;

public class ClaimDto : IClaim
{
    public string Name { get; set; } = null!;
    public string Value { get; set; } = null!;
}