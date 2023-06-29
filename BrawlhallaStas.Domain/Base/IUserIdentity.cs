namespace BrawlhallaStat.Domain.Base;

public interface IUserIdentity : IHaveId<string>
{
    public string Login { get; set; }
    public string NickName { get; set; }
}