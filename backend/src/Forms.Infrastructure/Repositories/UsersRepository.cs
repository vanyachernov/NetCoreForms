using CSharpFunctionalExtensions;
using Forms.Application.DTOs;
using Forms.Application.UserDir;
using Forms.Application.UserDir.GetUsers;
using Forms.Domain.Shared;
using Forms.Domain.TemplateManagement.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Forms.Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly TemplateDbContext _templateContext;
        private readonly UserManager<User> _userManager;

        public UsersRepository(
            TemplateDbContext templateContext,
            UserManager<User> userManager)
        {
            _templateContext = templateContext;
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

            return userListResponse.ToList();
        }

        public async Task<Result<string, Error>> GetUserRole(
            Guid userId, 
            CancellationToken cancellationToken = default)
        {
            var role = await _templateContext.UserRoles
                .Where(ur => ur.UserId == userId.ToString())
                .Select(ur => ur.RoleId)
                .FirstOrDefaultAsync(cancellationToken);

            return role == null 
                ? Errors.General.NotFound(userId)
                : role;
        }
    }
}