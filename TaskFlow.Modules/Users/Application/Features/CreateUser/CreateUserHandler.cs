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
        private readonly IUsersUnitOfWork _unitOfWork;
        public CreateUserHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IUsersUnitOfWork unitOfWork) 
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;

        }
        public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken) 
        {            
            var passwordHash = _passwordHasher.Hash(request.Password);

            //Domain Creation
            var user = User.Create(
                request.FirstName,
                request.LastName,
                request.Email,
                passwordHash               
                );

            //add
            await _userRepository.AddAsync(user, cancellationToken);

            //save 
            await _unitOfWork.SaveChangesAsync(cancellationToken);


            //Response
            return new CreateUserResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,                
                IsActive = user.IsActive,
                CreatedAtUTC = user.CreatedAtUTC
            };
        }
    }
}
