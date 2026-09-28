using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.BuildingBlocks.Localization;
using TaskFlow.Modules.Users.Application.Abstractions;

namespace TaskFlow.Modules.Users.Application.Features.DeleteUser
{
    public class DeleteUserHandler:IRequestHandler<DeleteUserCommand,bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUsersUnitOfWork _unitOfWork;
        public DeleteUserHandler(IUserRepository userRepository, IUsersUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken) 
        {
            //Fetch User
            var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
            if (user is null)
                throw new ValidationException(
                                                new[]
                                                {
                                                    new ValidationFailure("Delete User",ErrorKeys.UserNotFound)
                                                });
            //Delete User
            await _userRepository.DeleteAsync(user, cancellationToken);

            //save 
            await _unitOfWork.SaveChangesAsync(cancellationToken);


            return true;
        }
    }
}
