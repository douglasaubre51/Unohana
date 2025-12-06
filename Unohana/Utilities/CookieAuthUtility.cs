namespace Unohana.Utilities;

public static class CookieAuthUtility
{
    public static CookieDtos InitTutorCookie(Tutor tutor)
    {
        var claims = new List<Claim>
            {
                new (ClaimTypes.Name, tutor.Email),
                new ("Id", tutor.Id.ToString()),
                new ("UserName", tutor.FirstName + " " + tutor.LastName),
                new (ClaimTypes.Role, Roles.TUTOR.ToString())
            };
        var claimsIdentity = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme
        );
        var authProperties = new AuthenticationProperties
        {
            AllowRefresh = true,
            IsPersistent = true,
            IssuedUtc = DateTime.UtcNow.ToLocalTime()
        };

        return new CookieDtos(claimsIdentity, authProperties);
    }

    public static CookieDtos InitStudentCookie(Student student)
    {
        var claims = new List<Claim>
            {
                new (ClaimTypes.Name, student.Email),
                new ("Id", student.Id.ToString()),
                new ("UserName", student.FirstName + " " + student.LastName),
                new (ClaimTypes.Role, Roles.STUDENT.ToString())
            };
        var claimsIdentity = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme
        );
        var authProperties = new AuthenticationProperties
        {
            AllowRefresh = true,
            IsPersistent = true,
            IssuedUtc = DateTime.UtcNow.ToLocalTime()
        };

        return new CookieDtos(claimsIdentity, authProperties);
    }
}
