using TodoManager.Common.Contracts;
using TodoManager.Common.Models.Users;
using TodoManager.Common.Helpers;
using TodoManager.Common.Exceptions;
using TodoManager.Common.Contracts.Services;
using TodoManager.Common.Contracts.Repositories;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace TodoManager.Core.Services;

public class UserService : IUserService
{
	private readonly IUserRepository _userRepository;
    private readonly IJwtUtils _jwtUtils;
    private readonly IHttpContextAccessor _accessor;

    public UserService(IUserRepository userRepository, IJwtUtils jwtUtils, IHttpContextAccessor accessor)
	{
		_userRepository = userRepository;
        _jwtUtils = jwtUtils;
        _accessor = accessor;
    }

	public async Task<AuthenticateResponse?> Authenticate(AuthenticateRequest request)
	{
		var user = await _userRepository.GetByUserName(request.UserName);
        if(user is null)
            return null;

		bool passwordMatches = SecretHasher.Verify(request.Password, user.Password);
		if (!passwordMatches)
            return null;

        var token = _jwtUtils.GenerateToken(user);

        AuthenticateResponse response = new(user, token);

        return response;
	}

	public async Task<User> Create(UserCreateRequest request)
	{
        var existingUser = await _userRepository.Count(request.UserName, request.EmailAddress);
        if (existingUser > 0)
            throw new AlreadyExistsException($"Username or email address already in use.");
        
        request.UserName = request.UserName.Trim();
        request.Password = SecretHasher.Hash(request.Password);

        var newUser = new User(request.UserName, request.FirstName, request.LastName, request.Password, request.EmailAddress); 
        newUser.UserId = await _userRepository.Create(request);

        if(newUser.UserId == 0)
            throw new Exception("Something went wrong. Contact support");

        return newUser;
	}

    public async Task<User?> GetById(int id)
    {
        var user = await _userRepository.GetById(id);

        return user;
    }

    public async Task<User?> GetByUserName(string userName)
    {
        var user = await _userRepository.GetByUserName(userName);

        return user;
    }


    public int GetActiveUserId()
    {
        string userIdText = _accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;

        return int.Parse(userIdText);
    }

    public async Task Update(int id, UserUpdateRequest request)
	{
        var user = await _userRepository.GetById(id);
        if(user is null)
            throw new NotFoundException($"User with given id '{id}' doesn't exist in the database. Please try again.");

        if(!string.IsNullOrEmpty(request.FirstName))
            user.FirstName = request.FirstName;
        if(!string.IsNullOrEmpty(request.LastName))
            user.LastName = request.LastName;
        if(!string.IsNullOrEmpty(request.Password))
            user.Password = SecretHasher.Hash(request.Password);

		await _userRepository.Update(user);
	}

    public async Task Delete(int id)
    {
        await _userRepository.Delete(id);
    }
}