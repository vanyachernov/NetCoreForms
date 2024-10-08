using CSharpFunctionalExtensions;
using Forms.Application.DTOs;
using Forms.Application.UserDir;
using Forms.Application.UserDir.GetUsers;
using Forms.Domain.TemplateManagement.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Forms.Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly UserManager<User> _userManager;

        public UsersRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<IEnumerable<GetUsersResponse>>> GetUsers(
            CancellationToken cancellationToken = default)
        {
            var users = await _userManager
                .Users
                .ToListAsync(cancellationToken);

            var userListResponse = users.Select(user => new GetUsersResponse
            {
                Id = Guid.Parse(user.Id),
                Email = new EmailDto(user.Email!),
                FullName = new FullNameDto(
                    user.FullName.FirstName, 
                    user.FullName.LastName)
            }).ToList();

            return Result.Success<IEnumerable<GetUsersResponse>>(userListResponse);
        }
    }
}