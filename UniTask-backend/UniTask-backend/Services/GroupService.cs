using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Npgsql.Replication.PgOutput.Messages;
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

        public async Task<(bool Success, string? ErrorMessage, Guid? GroupId)> CreateGroup(string name, string ownerName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    return (false, "Group name cannot be empty.", null);

                // Sukuriame naują grupę
                var owner = await _context.Users.FirstOrDefaultAsync(u => u.Username == ownerName);
                var newGroup = new Entities.Group(name, owner.Id);


                var membership = new GroupUser(owner.Id, newGroup.Id);
                _context.GroupUsers.Add(membership);
                
                _context.Groups.Add(newGroup);
                await _context.SaveChangesAsync();

                return (true, null, newGroup.Id);
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException?.Message ?? ex.Message;
                return (false, "Unable to create group: " + innerMessage, null);
            }
        }

        public async Task<(bool Success, string? ErrorMessage)> AddMemberToGroup(string username, Guid groupId)
        {
            try
            {
                // Check if the group exists
                var groupExists = await _context.Groups.AnyAsync(g => g.Id == groupId);
                if (!groupExists)
                    return (false, "Group not found.");

                // Find user by username
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
                if (user == null)
                    return (false, "User not found.");

                var userId = user.Id;

                // Check if the user is already a member of the group
                var membershipExists = await _context.GroupUsers.AnyAsync(gu => gu.UserId == userId && gu.GroupId == groupId);
                if (membershipExists)
                    return (false, "User is already a member of the group.");

                var membership = new GroupUser(userId, groupId);
                _context.GroupUsers.Add(membership);

                // Save the membership record
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
                // Optionally log ex.Message
                return (false, "Unable to retrieve groups.", new List<GroupDTO>());
            }
        }

        public async Task<(bool Success, string? ErrorMessage, List<GetUsersDTO> users)> GetMembers(Guid groupId)
        {
            try
            {
                var users = await _context.GroupUsers
                    .Include(gu => gu.User)  // load User navigation property
                    .Where(gu => gu.GroupId == groupId)
                    .Select(gu => new GetUsersDTO 
                    { 
                        Username = gu.User.Username 
                    })
                    .ToListAsync();

                return (true, null, users);
            }
            catch (Exception ex)
            {
                // Optionally log ex.Message
                return (false, "Unable to retrieve members.", new List<GetUsersDTO>());
            }
        }

        public async Task<(bool Success, string? ErrorMessage)> DeleteGroup(Guid groupId)
        {
            try
            {
                var group = await _context.Groups.FindAsync(groupId);
                if (group == null)
                    return (false, "Group not found.");

                _context.Groups.Remove(group);
                await _context.SaveChangesAsync();

                return (true, null);
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException?.Message ?? ex.Message;
                return (false, "Unable to delete task: " + innerMessage);
            }
        }

    }
}
