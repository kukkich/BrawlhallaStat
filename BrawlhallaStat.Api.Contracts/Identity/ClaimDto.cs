using BrawlhallaStat.Domain.Identity.Base;

namespace BrawlhallaStat.Api.Contracts.Identity;

public class ClaimDto : IClaim
{
    public string Name { get; set; } = null!;
    public string Value { get; set; } = null!;
}