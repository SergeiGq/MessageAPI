using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DBMessage.Models;
using DBMessage.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Message.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserRepository _repository;

    public AuthController(UserRepository repository)
    {
        _repository = repository;
    }

    [HttpPost("/token")]
    public string Token(string username, string password)
    {
        var identity = GetIdentity(username, password);
        if (identity == null)
        {
            throw new Exception("");
        }
 
        var now = DateTime.UtcNow;
        // создаем JWT-токен
        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            notBefore: now,
            claims: identity.Claims,
            expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
 
        var response = new
        {
            access_token = encodedJwt,
            username = identity.Name
        };
       
        return response.access_token;
    }
    [HttpGet("/GetNicknameAuthUser")]
    public string GetNick()
    {
        return _repository.GetUserAuth(User.GetId());
    }
    private ClaimsIdentity GetIdentity(string username, string password)
    {
        User? user = _repository.Get(username, password);
        if (user != null)
        {
            var claims = new List<Claim>
            {
                new Claim("userId", user.Id.ToString()),
               
            };
            ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
 
        // если пользователя не найдено
        return null;
        
    }
    

    [HttpPost("/registr")]
    public void Registration(string login,string password,string nickname)
    {
        var auth = new User()
        {
            Login = login,
            Password = password,
            Name = nickname

        };
        _repository.Add(auth);
    }
  
}