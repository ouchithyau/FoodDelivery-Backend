using Auth.Application.DTOs;
using Auth.Application.Interfaces;
using Auth.Domain.Entities;

namespace Auth.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly ITokenService _tokenService;

        public AuthService(IAuthRepository authRepository, ITokenService tokenService)
        {
            _authRepository = authRepository;
            _tokenService = tokenService;
        }

        public async Task<string> RegisterAsync(RegisterDto registerDto)
        {
            var existingUser = await _authRepository.GetByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                throw new Exception("User with this email already exists!");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            var user = new User
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                PasswordHash = passwordHash
            };

            if (!string.IsNullOrEmpty(registerDto.Street))
            {
                user.Addresses.Add(new Address
                {
                    Street = registerDto.Street,
                    City = registerDto.City,
                    ZipCode = registerDto.ZipCode
                });
            }

            await _authRepository.AddAsync(user);
            await _authRepository.SaveChangesAsync();

            return "User registered successfully!";
        }

        public async Task<string> LoginAsync(LoginDto loginDto)
        {
            var user = await _authRepository.GetByEmailAsync(loginDto.Email);
            if (user == null)
            {
                throw new Exception("Invalid email or password!");
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);
            if (!isPasswordValid)
            {
                throw new Exception("Invalid email or password!");
            }

            // Generate and return JWT Token upon successful login
            var token = _tokenService.GenerateToken(user);
            return token;
        }
    }
}