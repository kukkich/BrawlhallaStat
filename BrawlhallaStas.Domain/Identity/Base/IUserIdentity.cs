using BrawlhallaStat.Domain.Base;

namespace BrawlhallaStat.Domain.Identity.Base;

public interface IUserIdentity : IHaveId<string>
{
    public string Login { get; }
    public string NickName { get; }
    public string Email { get; }

    public IEnumerable<IRole> Roles { get; }
    public IEnumerable<IClaim> Claims { get; }
}