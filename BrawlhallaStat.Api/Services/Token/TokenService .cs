using BrawlhallaStat.Domain.Identity.Dto;
using BrawlhallaStat.Domain;

namespace BrawlhallaStat.Api.Services.Token;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public TokenPair GenerateTokenPair(User user)
    {
        // Реализация логики для генерации access и refresh токенов
        // Используйте _configuration для получения секретного ключа и других параметров
        // ...

        // Вернуть сгенерированные токены
        throw new NotImplementedException();

        return new TokenPair
        {
            Access = "generated_access_token",
            Refresh = "generated_refresh_token"
        };
    }

    public TokenPair RefreshAccessToken(string refreshToken)
    {
        // Реализация логики обновления access токена по refresh токену
        // ...

        // Вернуть новые access и refresh токены
        
        throw new NotImplementedException();

        return new TokenPair
        {
            Access = "new_access_token",
            Refresh = "new_refresh_token"
        };
    }

    public void RevokeRefreshToken(string refreshToken)
    {
        // Реализация логики отзыва (инвалидации) refresh токена
        // ...
        throw new NotImplementedException();
    }
}