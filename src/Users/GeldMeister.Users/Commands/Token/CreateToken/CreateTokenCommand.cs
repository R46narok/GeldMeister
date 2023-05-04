using System.IdentityModel.Tokens.Jwt;
using System.Text;
using ErrorOr;
using GeldMeister.Users.Data.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GeldMeister.Users.Commands.Token.CreateToken;

public class CreateTokenCommandResponse
{
    public CreateTokenCommandResponse(string token, DateTime validTo)
    {
        Token = token;
        ValidTo = validTo;
    }

    public string Token { get; set; }
    public DateTime ValidTo { get; set; }
}

public class CreateTokenCommand : IRequest<ErrorOr<CreateTokenCommandResponse>>
{
    public string UserName { get; set; } 
    public string Password { get; set; }
    
    public CreateTokenCommand(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }
}

public class CreateTokenCommandHandler : IRequestHandler<CreateTokenCommand, ErrorOr<CreateTokenCommandResponse>>
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;

    private const int ExpirationInMinutes = 30;

    public CreateTokenCommandHandler(IConfiguration configuration, UserManager<User> userManager)
    {
        _configuration = configuration;
        _userManager = userManager;
    }

    public async Task<ErrorOr<CreateTokenCommandResponse>> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
    {
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var user = await _userManager.FindByNameAsync(request.UserName);

        var claims = await _userManager.GetClaimsAsync(user);

        var expires = DateTime.UtcNow.AddMinutes(ExpirationInMinutes);
        
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            signingCredentials: credentials,
            expires: expires,
            claims: claims);

        var tokenHandler = new JwtSecurityTokenHandler();
        var stringToken = tokenHandler.WriteToken(token);

        return new CreateTokenCommandResponse(stringToken, expires);
    }
}