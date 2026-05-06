using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Modules.Users.Application.Abstractions;

namespace TaskFlow.Modules.Users.Application.Features.DeleteUser
{
    public class DeleteUserHandler:IRequestHandler<DeleteUserCommand,bool>
    {
        private readonly IUserRepository _userRepository;
        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken) 
        {
            //Fetch User
            var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
            if (user is null)
                throw new Exception($"User with Id {request.Id} not found");

            //Delete User
            await _userRepository.DeleteAsync(user, cancellationToken);

            return true;
        }
    }
}
