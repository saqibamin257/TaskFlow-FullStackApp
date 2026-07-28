using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.BuildingBlocks.Localization;
using TaskFlow.BuildingBlocks.Security.Abstraction;
using TaskFlow.Modules.Users.Application.Abstractions;

namespace TaskFlow.Modules.Users.Application.Features.GetCurrentUser
{
    public sealed class GetCurrentUserHandler:IRequestHandler<GetCurrentUserQuery,GetCurrentUserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUser _currentUser;

        public GetCurrentUserHandler(IUserRepository userRepository, ICurrentUser currentUser) 
        {
            _userRepository = userRepository;
            _currentUser = currentUser;
        }

        public async Task<GetCurrentUserResponse> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken) 
        {
            var user = await _userRepository.GetByIdAsync(_currentUser.UserId, cancellationToken);
            
            if (user is null) 
            {
                throw new ValidationException(ErrorKeys.UserNotFound);
            }
            return new GetCurrentUserResponse(user.Id,user.Name,user.Email,user.Role,user.TenantId);
        }
    }
}
