using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Modules.Users.Application.Abstractions;

namespace TaskFlow.Modules.Users.Application.Features.GetUsers
{
    public class GetUsersHandler:IRequestHandler<GetUsersQuery,List<GetUsersResponse>>
    {
        private readonly IUserRepository _userRepository;

        public GetUsersHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<GetUsersResponse>> Handle(
            GetUsersQuery request,
            CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync(cancellationToken);

            return users.Select(u => new GetUsersResponse
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email
            }).ToList();
        }
    }
}
