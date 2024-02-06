namespace BrawlhallaStat.Api.Authentication.Services.Hashing;

public class IdentityHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        return password;
    }

    public bool Compare(string hash, string password)
    {
        return password == hash;
    }
}