using Microsoft.EntityFrameworkCore;
using UniTask_backend.DTO;
using UniTask_backend.Entities;
using UniTask_backend.Interfaces;
using UniTask_backend.Persistence;

namespace UniTask_backend.Services
{
    public class GroupService : IGroupService
    {
        private readonly AppDbContext _context;

        public GroupService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(bool Success, string? ErrorMessage, Guid? GroupId)> CreateGroup(string name, Guid ownerId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    return (false, "Group name cannot be empty.", null);

                var newGroup = new Group(name, ownerId);
                _context.Groups.Add(newGroup);

                var membership = new GroupUser(ownerId, newGroup.Id);
                _context.GroupUsers.Add(membership);

                await _context.SaveChangesAsync();
                return (true, null, newGroup.Id);
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException?.Message ?? ex.Message;
                return (false, "Unable to create group: " + innerMessage, null);
            }
        }

        public async Task<(bool Success, string? ErrorMessage)> AddMemberToGroup(Guid userId, Guid groupId)
        {
            try
            {
                var groupExists = await _context.Groups.AnyAsync(g => g.Id == groupId);
                if (!groupExists)
                    return (false, "Group not found.");

                var userExists = await _context.Users.AnyAsync(u => u.Id == userId);
                if (!userExists)
                    return (false, "User not found.");

                var membershipExists = await _context.GroupUsers.AnyAsync(gu => gu.UserId == userId && gu.GroupId == groupId);
                if (membershipExists)
                    return (false, "User is already a member of the group.");

                var membership = new GroupUser(userId, groupId);
                _context.GroupUsers.Add(membership);

                await _context.SaveChangesAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException?.Message ?? ex.Message;
                return (false, "Unable to add member: " + innerMessage);
            }
        }

        public async Task<(bool Success, string? ErrorMessage, List<GroupDTO> Groups)> GetGroupsByUserId(Guid userId)
        {
            try
            {
                var groups = await _context.Groups
                    .Where(g => g.Members.Any(m => m.UserId == userId))
                    .Select(g => new GroupDTO
                    {
                        Id = g.Id,
                        Name = g.Name
                    })
                    .ToListAsync();

                return (true, null, groups);
            }
            catch (Exception ex)
            {
                return (false, "Unable to retrieve groups: " + ex.Message, new List<GroupDTO>());
            }
        }
    }
}
