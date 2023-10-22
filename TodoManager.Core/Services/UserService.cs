using TodoManager.Common.Contracts;
using TodoManager.Common.Models.Users;
using TodoManager.Common.Helpers;
using TodoManager.Common.Exceptions;
using TodoManager.Common.Contracts.Services;
using TodoManager.Common.Contracts.Repositories;

namespace TodoManager.Core.Services;

public class UserService : IUserService
{
	private readonly IUserRepository _userRepository;
    private readonly IJwtGenerator _jwtGenerator;

    public UserService(IUserRepository userRepository, IJwtGenerator jwtGenerator)
	{
		_userRepository = userRepository;
        _jwtGenerator = jwtGenerator;
    }

	public async Task<AuthenticateResponse?> Authenticate(AuthenticateRequest request)
	{
		var user = await _userRepository.GetByUserName(request.UserName);
        if(user is null)
            return null;

		bool passwordMatches = SecretHasher.Verify(request.Password, user.PasswordHash);
		if (!passwordMatches)
            return null;

        var token = _jwtGenerator.GenerateToken(user);
        var response = new AuthenticateResponse(user, token);
        return response;
	}

	public async Task<User> Create(UserCreateRequest request)
	{
        var matchingUsersCount = await _userRepository.Count(request.UserName, request.EmailAddress);
        if (matchingUsersCount > 0)
            throw new AlreadyExistsException($"Username or email address already in use.");
        
        var userName = request.UserName.Trim();
        var password = SecretHasher.Hash(request.Password);

        var newUser = new User(userName, request.FirstName, request.LastName, password, request.EmailAddress); 
        newUser.UserId = await _userRepository.Create(request);

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
    
    public async Task Update(int subjectId, UserUpdateRequest request, int requesteeId)
	{
        if (subjectId != requesteeId)
            throw new UnauthorizedAccessException("You can only update your own user account.");

        var user = await _userRepository.GetById(subjectId) 
            ?? throw new NotFoundException($"User doesn't exist in the database. Please try again.");
        
        if (!string.IsNullOrEmpty(request.FirstName))
            user.FirstName = request.FirstName;
        if(!string.IsNullOrEmpty(request.LastName))
            user.LastName = request.LastName;
        if(!string.IsNullOrEmpty(request.Password))
            user.PasswordHash = SecretHasher.Hash(request.Password);

		await _userRepository.Update(user);
	}

    public async Task Delete(int subjectId, int requesteeId)
    {
        if (subjectId != requesteeId)
            throw new UnauthorizedAccessException("You can only delete your own user account.");

        await _userRepository.Delete(subjectId);
    }
}