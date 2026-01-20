using System;
using Mentorship.Api.Data;
using Mentorship.Api.Dtos.Users;
using Mentorship.Api.Entities;
using Mentorship.Api.Enums;
using Microsoft.EntityFrameworkCore;

namespace Mentorship.Api.Repository;

public class UserRepository (AppDbContext context)
{
 private readonly AppDbContext _context = context;

    public async Task<List<User>> GetAll()
    {
        return await _context.Users.ToListAsync();
    }
      public async Task<User> CreateUser(CreateUserDto userDto)
    {
        var user = new User
        {
          FullName = userDto.FullName,
          PhoneNumber = userDto.PhoneNumber,
          Email = userDto.Email, 
          Password = userDto.Password
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }
    public async Task<User?> GetSingle(int id)
    {
        return await _context.Users.FirstOrDefaultAsync(p => p.UserId == id);
    }
    public async Task<User?> UpdateUser(int id, UpdateUserDto userDto)
    {
    var user = await _context.Users.FindAsync(id);
    if (user == null) return null;

    if (userDto.FullName != null) user.FullName = userDto.FullName;
    if (userDto.PhoneNumber != null) user.PhoneNumber = userDto.PhoneNumber;
    if (userDto.Bio != null) user.Bio = userDto.Bio;
    if (userDto.Email != null) user.Email = userDto.Email;
    if (userDto.Password != null) user.Password = userDto.Password;
    if (userDto.Role != null) user.Role = (RoleType)userDto.Role;
    
    await _context.SaveChangesAsync();
    return user;
    }
    
    public async Task<User?> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return null;
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return user;
    }
}
