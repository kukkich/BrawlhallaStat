using BrawlhallaStat.Domain.Identity.Base;

namespace BrawlhallaStat.Api.Contracts.Identity;

public class RoleDto : IRole
{
    public string Name { get; set; } = null!; 
}
