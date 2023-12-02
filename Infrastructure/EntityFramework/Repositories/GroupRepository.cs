using Infrastructure.EntityFramework.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Repositories;

public class GroupRepository:IGroupRepository
{
    private readonly MoodContext _context;

    public GroupRepository(MoodContext context)
    {
        _context = context;
    }

    public DbGroup Create(DbGroup group, IEnumerable<string> userIds)
    {
        _context.Groups.Add(group);
        _context.SaveChanges();
        foreach (var userId in userIds)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                throw new KeyNotFoundException("userIdNotFound");
                //TODO unit of work
            }
            
            var usrgrp= new DbUserGroup
            {
                UserId = userId,
                GroupId = group.Id
            };
            _context.UserGroups.Add(usrgrp);
            _context.SaveChanges();
        }
        return group;
    }
}