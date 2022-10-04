using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoManager.Common.Entities;
using TodoManager.Common.Helpers;
using TodoManager.Common.Contracts;
using TodoManager.Common.Models.Users;

namespace TodoManager.Core.Services;

public class UserService : IUserService
{
	private readonly IUserRepository _userRepository;
	private readonly IConfiguration _config;

	public UserService(IUserRepository userRepository, IConfiguration config)
	{
		_userRepository = userRepository;
		_config = config;
	}

	public AuthenticateResponse? Authenticate(AuthenticateRequest model)
	{
		var user = _userRepository.GetByUserName(model.UserName);
        if(user is null)
            return null;

		bool passwordMatches = SecretHasher.Verify(model.Password, user.Password);
		if (!passwordMatches)
            return null;

        var token = GenerateToken(user);

        AuthenticateResponse response = new(user, token);

        return response;
	}

    private string GenerateToken(User user)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
            _config["Authentication:SecretKey"]));

        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        List<Claim> claims = new()
        {
            new(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, user.UserName)
        };

        var token = new JwtSecurityToken(
                _config["Authentication:Issuer"],
                _config["Authentication:Audience"],
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

	public void Create(CreateRequest model)
	{
        var user = _userRepository.GetByUserName(model.UserName);
        // TODO: create a way to return a message that username is already taken
        if (user is not null)
            return;
        
        // TODO: add a contraint to the db that disallows storing leading and trailing spaces
        model.UserName = model.UserName.Trim();
        model.Password = SecretHasher.Hash(model.Password);

		_userRepository.Create(model);
	}

    public User? GetById(int id)
    {
        return _userRepository.GetById(id);
    }

    public void Update(int id, UpdateRequest model)
	{
        var user = _userRepository.GetById(id);

        // TODO: create a way to inform that user with given id doesn't exist
        if(user is null)
            return;

        if(!string.IsNullOrEmpty(model.FirstName))
            user.FirstName = model.FirstName;
        if(!string.IsNullOrEmpty(model.LastName))
            user.LastName = model.LastName;
        if(!string.IsNullOrEmpty(model.Password))
            user.Password = SecretHasher.Hash(model.Password);

		_userRepository.Update(user);
	}

    public void Delete(int id)
    {
        _userRepository.Delete(id);
    }
}