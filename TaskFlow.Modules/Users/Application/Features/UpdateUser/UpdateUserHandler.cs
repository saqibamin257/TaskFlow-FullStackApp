using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Modules.Users.Application.Abstractions;

namespace TaskFlow.Modules.Users.Application.Features.UpdateUser
{
    public class UpdateUserHandler: IRequestHandler<UpdateUserCommand,UpdateUserResponse>
    {
        private readonly IUserRepository _userRepository;
        public UpdateUserHandler(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }
        public async Task<UpdateUserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken) 
        {
            //Fetch existing user
            var user = await _userRepository.GetByIdAsync(request.Id,cancellationToken);
            if (user is null)
                throw new Exception($"User with Id {request.Id} not found");

            //Apply domain behaviour
            user.UpdateProfile(request.Name, request.Email);

            //Persist Changes
            await _userRepository.UpdateAsync(user, cancellationToken);

            //Return response
            return new UpdateUserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role
            };
        }
    }
}
