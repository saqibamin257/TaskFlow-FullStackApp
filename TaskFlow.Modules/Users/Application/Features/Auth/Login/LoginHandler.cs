using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.BuildingBlocks.Models;
using TaskFlow.BuildingBlocks.Security.Abstraction;
using TaskFlow.Modules.Users.Application.Abstractions;

namespace TaskFlow.Modules.Users.Application.Features.Auth.Login
{
    public sealed class LoginHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IUserRepository _userRepository;

        private readonly IPasswordHasher _passwordHasher;

        private readonly ITokenProvider _tokenProvider;
        public LoginHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenProvider tokenProvider)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenProvider = tokenProvider;
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (user is null)
                throw new Exception("Invalid Email or Password");

            var isPasswordValid = _passwordHasher.Verify(request.Password, user.PasswordHash);

            if (!isPasswordValid)
                throw new Exception("Invalid Email or Password");

            var authenticatedUser =
                new AuthenticatedUser
                {
                    UserId = user.Id,
                    Email = user.Email,
                    Role = user.Role,
                    TenantId = user.TenantId
                };
            var accessToken = _tokenProvider.Generate(authenticatedUser);
            return new LoginResponse
            {
                AccessToken = accessToken
            };
        }
    }
}
