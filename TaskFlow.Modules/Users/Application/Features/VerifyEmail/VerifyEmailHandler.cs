using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.BuildingBlocks.Localization;
using TaskFlow.Modules.Users.Application.Abstractions;

namespace TaskFlow.Modules.Users.Application.Features.VerifyEmail
{
    public sealed class VerifyEmailHandler : IRequestHandler<VerifyEmailCommand, VerifyEmailResponse>
    {
        private readonly IEmailVerificationTokenGenerator _tokenGenerator;
        private readonly IEmailVerificationTokenRepository _emailVerificationTokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUsersUnitOfWork _unitOfWork;
        public VerifyEmailHandler(IEmailVerificationTokenGenerator tokenGenerator, IEmailVerificationTokenRepository emailVerificationTokenRepository, IUserRepository userRepository, IUsersUnitOfWork unitOfWork)
        {
            _tokenGenerator = tokenGenerator;
            _emailVerificationTokenRepository = emailVerificationTokenRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<VerifyEmailResponse> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            var tokenHash = _tokenGenerator.Hash(request.Token);
            
            var verificationToken = await _emailVerificationTokenRepository.GetByTokenHashAsync(tokenHash, cancellationToken);

            if (verificationToken is null)
            {
                throw new ValidationException(ErrorKeys.InvalidEmailVerificationToken);
            }

            if (verificationToken.IsUsed())
            {                
                throw new ValidationException(
                                                new[]
                                                {
                                                    new ValidationFailure(nameof(request.Token),ErrorKeys.EmailVerificationTokenAlreadyUsed)
                                                });
            }

            if (verificationToken.IsExpired())
            {
                throw new ValidationException(
                                                new[]
                                                {
                                                    new ValidationFailure(
                                                        nameof(request.Token),
                                                        ErrorKeys.EmailVerificationTokenExpired)
                                                });               
            }

            var user = await _userRepository.GetByIdAsync(verificationToken.UserId, cancellationToken);

            if (user is null)
            {
                throw new ValidationException(
                                                new[]
                                                {
                                                    new ValidationFailure(
                                                        nameof(request.Token),
                                                        ErrorKeys.UserNotFound)
                                                });               
            }

            user.VerifyEmail();

            verificationToken.MarkAsUsed();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new VerifyEmailResponse("Email verified successfully.");
        }
    }
}

