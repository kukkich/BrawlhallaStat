namespace BrawlhallaStat.Api.Authentication.Services.Hashing;

public interface IPasswordHasher
{
    public string Hash(string password);
    public bool Compare(string hash, string password);
}