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
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                return Errors.General.NotFound(userId);
            }

            var roles = await _userManager.GetRolesAsync(user);

            var role = roles.FirstOrDefault();
            
            return role == null 
                ? Errors.General.NotFound(userId)
                : role;
        }

        public async Task<Result<Guid, Error>> Register(
            User user, 
            string password, 
            CancellationToken cancellationToken = default)
        {
            var result = await _userManager.CreateAsync(
                user, 
                password);

            if (!result.Succeeded)
            {
                return Errors.General.ValueIsInvalid("User register");
            }
        
            if (!Guid.TryParse(
                    user.Id, 
                    out Guid userId))
            {
                return Errors.General.ValueIsInvalid("UserId");
            }
            
            await _userManager.AddToRoleAsync(user, "User");

            return userId;
        }
    }
}