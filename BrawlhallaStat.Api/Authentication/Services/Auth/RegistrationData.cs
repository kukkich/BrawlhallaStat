namespace BrawlhallaStat.Api.Authentication.Services.Auth;

public record RegistrationData(
    string Login,
    string NickName,
    string Password,
    string Email
);