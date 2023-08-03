using BrawlhallaStat.Domain.Identity.Base;

namespace BrawlhallaStat.Domain.Identity.Dto;

public class RoleDto : IRole
{
    public string Name { get; set; } = null!; 
}
