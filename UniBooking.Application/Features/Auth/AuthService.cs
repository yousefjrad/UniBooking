using UniBooking.Application.Common.Interfaces;
using UniBooking.Application.Features.Auth.Dtos;
using UniBooking.Domain.Entities;

namespace UniBooking.Application.Features.Auth;

public class AuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;

    public AuthService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        ITokenService tokenService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request)
    {
        if (await _userRepository.ExistsByEmailAsync(request.Email))
        {
            throw new InvalidOperationException("Email Already Exist)");
        }
        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PasswordHash = _passwordHasher.Hash(request.Password),
            TenantId = request.TenantId,
            Role = request.Role
        };

        await _userRepository.AddAsync(user);
        var token = _tokenService.GenerateToken(user);
        return new AuthResponseDto(token, $"{user.FirstName} {user.LastName}", user.Email, user.Role.ToString());

    }

    public async Task <AuthResponseDto> LoginAsync(LoginRequestDto request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user is null || !_passwordHasher.Verify(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Email Or Paswword Was Wrong");
        }
        var token = _tokenService.GenerateToken(user);

        return new AuthResponseDto(token, $"{user.FirstName} {user.LastName}", user.Email, user.Role.ToString());
    }
}