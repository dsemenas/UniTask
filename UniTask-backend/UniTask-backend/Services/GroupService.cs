using Microsoft.EntityFrameworkCore;
using UniTask_backend.DTO;
using UniTask_backend.Entities;
using UniTask_backend.Interfaces;
using UniTask_backend.Persistence;

namespace UniTask_backend.Services;
public class GroupService : IGroupService
{
    private readonly AppDbContext _context;

    public GroupService(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateGroup(GroupDTO group)
    {
        var owner = await _context.Users.FindAsync(group.OwnerId);
        if (owner == null) throw new Exception("Owner not found");

        var newGroup = new Group
        {
            GroupName = group.GroupName,
            Owner = owner
        };

        _context.Groups.Add(newGroup);
        await _context.SaveChangesAsync();
    }
    public async Task AddUserToGroup(GroupUser groupUser)
    {
        var group = await _context.Groups.FindAsync(groupUser.GroupId);
        if (group == null) throw new Exception("Group not found");

        var user = await _context.Users.FindAsync(groupUser.UserId);
        if (user == null) throw new Exception("User not found");

        var exists = await _context.GroupUsers
            .AnyAsync(gu => gu.GroupId == groupUser.GroupId && gu.UserId == groupUser.UserId);

        if (exists) throw new Exception("User is already a member of the group");

        _context.GroupUsers.Add(groupUser);
        await _context.SaveChangesAsync();
    }
}
