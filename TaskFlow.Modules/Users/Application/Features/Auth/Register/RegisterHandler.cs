using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.BuildingBlocks.Localization;
using TaskFlow.BuildingBlocks.Security.Abstraction;
using TaskFlow.Modules.Users.Application.Abstractions;
using TaskFlow.Modules.Users.Application.Features.CreateUser;
using TaskFlow.Modules.Users.Domain.Entities;
using TaskFlow.Modules.Users.Infrastructure.Repositories;

namespace TaskFlow.Modules.Users.Application.Features.Auth.Register
{
    public sealed class RegisterHandler:IRequestHandler<RegisterCommand,RegisterResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IEmailVerificationTokenGenerator _tokenGenerator;
        private readonly IEmailVerificationTokenRepository _emailVerificationTokenRepository;
        private readonly IUsersUnitOfWork _unitOfWork;

        public RegisterHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IEmailVerificationTokenGenerator tokenGenerator, IEmailVerificationTokenRepository emailVerificationTokenRepository, IUsersUnitOfWork unitOfWork) 
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
            _emailVerificationTokenRepository = emailVerificationTokenRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken) 
        {
            var email = request.Email.Trim().ToLowerInvariant();
            var existingUser = await _userRepository.GetByEmailAsync(email);
            if (existingUser is not null) 
            {
                throw new ValidationException(ValidationKeys.EmailAlreadyExists);
            }
            var passwordHash = _passwordHasher.Hash(request.Password);


            var user = User.Create(request.FirstName, request.LastName, request.Email, passwordHash);

            await _userRepository.AddAsync(user,cancellationToken);
            var generatedToken = _tokenGenerator.Generate();

            var verificationToken = EmailVerificationToken.Create(user.Id, generatedToken.TokenHash, generatedToken.ExpiresAtUTC);

            await _emailVerificationTokenRepository.AddAsync(verificationToken,cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new RegisterResponse(user.Id, user.FirstName, user.LastName, user.Email);
        }
    }
}
