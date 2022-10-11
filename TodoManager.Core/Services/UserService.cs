using TodoManager.Common.Entities;
using TodoManager.Common.Contracts;
using TodoManager.Common.Models.Users;
using TodoManager.Common.Helpers;

namespace TodoManager.Core.Services;

public class UserService : IUserService
{
	private readonly IUserRepository _userRepository;
    private readonly IJwtUtils _jwtUtils;

    public UserService(IUserRepository userRepository, IJwtUtils jwtUtils)
	{
		_userRepository = userRepository;
        _jwtUtils = jwtUtils;
    }

	public async Task<AuthenticateResponse?> Authenticate(AuthenticateRequest model)
	{
		var user = await _userRepository.GetByUserName(model.UserName);
        if(user is null)
            return null;

		bool passwordMatches = SecretHasher.Verify(model.Password, user.Password);
		if (!passwordMatches)
            return null;

        var token = _jwtUtils.GenerateToken(user);

        AuthenticateResponse response = new(user, token);

        return response;
	}

	public async Task Create(CreateRequest model)
	{
        var user = await _userRepository.GetByUserName(model.UserName);
        // TODO: create a way to return a message that username is already taken
        if (user is not null)
            return;
        
        // TODO: add a contraint to the db that disallows storing leading and trailing spaces
        model.UserName = model.UserName.Trim();
        model.Password = SecretHasher.Hash(model.Password);

		await _userRepository.Create(model);
	}

    public async Task<User?> GetById(int id)
    {
        return await _userRepository.GetById(id);
    }

    public async Task Update(int id, UpdateRequest model)
	{
        var user = await _userRepository.GetById(id);

        // TODO: create a way to inform that user with given id doesn't exist
        if(user is null)
            return;

        if(!string.IsNullOrEmpty(model.FirstName))
            user.FirstName = model.FirstName;
        if(!string.IsNullOrEmpty(model.LastName))
            user.LastName = model.LastName;
        if(!string.IsNullOrEmpty(model.Password))
            user.Password = SecretHasher.Hash(model.Password);

		await _userRepository.Update(user);
	}

    public async Task Delete(int id)
    {
        await _userRepository.Delete(id);
    }
}