using TodoManager.Application.Interfaces.Services;
using TodoManager.Application.Interfaces.Authentication;
using TodoManager.Application.Interfaces.Repositories;
using TodoManager.Application.Models.Users;
using TodoManager.Application.Exceptions;
using TodoManager.Application.Models.Authentication;

namespace TodoManager.Application.Services;

internal class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IPasswordHasher _passwordHasher;

    public UserService(IUserRepository userRepository, IJwtGenerator jwtGenerator, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _jwtGenerator = jwtGenerator;
        _passwordHasher = passwordHasher;
    }

    public async Task<AuthenticationResponse?> Authenticate(LoginRequest request)
    {
        var user = await _userRepository.GetByUserName(request.UserName);
        if (user is null)
            return null;

        bool passwordMatches = _passwordHasher.Verify(request.Password, user.PasswordHash);
        if (!passwordMatches)
            return null;

        var token = _jwtGenerator.GenerateToken(user);
        var response = new AuthenticationResponse(user, token);
        return response;
    }

    public async Task<User> Create(RegisterRequest request)
    {
        var matchingUsersCount = await _userRepository.Count(request.UserName, request.EmailAddress);
        if (matchingUsersCount > 0)
            throw new AlreadyExistsException($"Username or email address already in use.");

        var userName = request.UserName.Trim();
        var password = _passwordHasher.Hash(request.Password);

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
        if (!string.IsNullOrEmpty(request.LastName))
            user.LastName = request.LastName;
        if (!string.IsNullOrEmpty(request.Password))
            user.PasswordHash = _passwordHasher.Hash(request.Password);

        await _userRepository.Update(user);
    }

    public async Task Delete(int subjectId, int requesteeId)
    {
        if (subjectId != requesteeId)
            throw new UnauthorizedAccessException("You can only delete your own user account.");

        await _userRepository.Delete(subjectId);
    }
}