
namespace Unohana.Dtos;

public record CookieDtos(
    ClaimsIdentity ClaimsIdentity,
    AuthenticationProperties AuthenticationProperties
);
