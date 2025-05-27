using System.Text.RegularExpressions;
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

        public (bool Success, string? ErrorMessage, Guid? GroupId) CreateGroup(string name, Guid ownerId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    return (false, "Group name cannot be empty.", null);

                // Sukuriame naują grupę
                var newGroup = new Entities.Group(name, ownerId);

                // Pirmiausia įrašome grupę, kad Id būtų patvirtintas
                _context.Groups.Add(newGroup);

                var membership = new GroupUser(ownerId, newGroup.Id);
                _context.GroupUsers.Add(membership);
                _context.SaveChanges();
                return (true, null, newGroup.Id);

           
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException?.Message ?? ex.Message;
                return (false, "Unable to create group: " + innerMessage, null);
            }
        }

        public (bool Success, string? ErrorMessage) AddMemberToGroup(Guid userId, Guid groupId)
        {
            try
            {
                // Patikrinam, ar grupė egzistuoja
                var groupExists = _context.Groups.Any(g => g.Id == groupId);
                if (!groupExists)
                    return (false, "Group not found.");

                // Patikrinam, ar vartotojas egzistuoja
                var userExists = _context.Users.Any(u => u.Id == userId);
                if (!userExists)
                    return (false, "User not found.");

                // Patikrinam, ar vartotojas jau yra grupės narys
                var membershipExists = _context.GroupUsers.Any(gu => gu.UserId == userId && gu.GroupId == groupId);
                if (membershipExists)
                    return (false, "User is already a member of the group.");

                var membership = new GroupUser(userId, groupId);
                _context.GroupUsers.Add(membership);

                // Išsaugome narystės įrašą
                _context.SaveChanges();

                return (true, null);
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException?.Message ?? ex.Message;
                return (false, "Unable to add member: " + innerMessage);
            }
        

        }

        public (bool Success, string? ErrorMessage, List<GroupDTO> Groups) GetGroupsByUserId(Guid userId)
        {
            try
            {
                var groups = _context.Groups
                    .Where(g => g.Members.Any(m => m.UserId == userId))
                    .Select(g => new GroupDTO
                    {
                        Id = g.Id,
                        Name = g.Name
                    })
                    .ToList();

                if (groups is null)
                {
                    return (true, null, new List<GroupDTO>());
                }

                return (true, null, groups);
            }
            catch (Exception ex)
            {
                // Optionally log ex.Message
                return (false, "Unable to retrieve groups.", new List<GroupDTO>());
            }
        }

    }
}
