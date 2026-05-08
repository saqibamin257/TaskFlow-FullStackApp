using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.BuildingBlocks.Security.Abstraction;
using TaskFlow.Modules.Users.Application.Abstractions;
using TaskFlow.Modules.Users.Domain.Entities;

namespace TaskFlow.Modules.Users.Application.Features.CreateUser
{
    public class CreateUserHandler :IRequestHandler<CreateUserCommand,CreateUserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        public CreateUserHandler(IUserRepository userRepository, IPasswordHasher passwordHasher) 
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }
        public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken) 
        {            
            var passwordHash = _passwordHasher.Hash(request.Password);

            //Domain Creation
            var user = User.Create(
                request.Name,
                request.Email,
                passwordHash,
                request.Role,
                request.TenantId
                );

            //save
            await _userRepository.AddAsync(user, cancellationToken);

            //Response
            return new CreateUserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                IsActive = user.IsActive,
                CreatedAtUTC = user.CreatedAtUTC
            };
        }
    }
}
