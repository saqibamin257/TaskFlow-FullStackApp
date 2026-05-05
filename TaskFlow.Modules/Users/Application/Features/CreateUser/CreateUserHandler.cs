using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Modules.Users.Application.Abstractions;
using TaskFlow.Modules.Users.Domain.Entities;

namespace TaskFlow.Modules.Users.Application.Features.CreateUser
{
    public class CreateUserHandler :IRequestHandler<CreateUserQuery,CreateUserResponse>
    {
        private readonly IUserRepository _userRepository;
        public CreateUserHandler(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }
        public async Task<CreateUserResponse> Handle(CreateUserQuery request, CancellationToken cancellationToken) 
        {
            //Temp password hashing (replace later)
            var passwordHash = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(request.Password));

            //Domain Creation
            var user = User.Create(
                request.Name,
                request.Email,
                passwordHash,
                request.Role,
                Guid.NewGuid() // later from teenant Context
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
