using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class UserEfcDao : IUserDao
{
    private readonly PostContext context;

    public UserEfcDao(PostContext context)
    {
        this.context = context;
    }

    public async Task<User> CreateAsync(User user)
    {
        EntityEntry<User> newUser = await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return newUser.Entity;
    }

    public async Task<User?> GetByUsernameAsync(string userName)
    {
        User? existing = context.Users.FirstOrDefault(u =>
            u.UserName.ToLower().Equals(userName.ToLower())
            );
        return existing;
    }

    public Task<User?> GetAsync(UserLoginDto loginDto)
    {
        IQueryable<User> usersQuery = context.Users.AsQueryable();
        
        User? existingUser = null;
        if (loginDto.UserName != null && loginDto.Password != null) // Log in request!
        {
            existingUser = usersQuery.FirstOrDefault(user => user.UserName.Equals(loginDto.UserName) && user.Password.Equals(loginDto.Password));
            if (existingUser == null)
            {
                throw new Exception("User not found");
            }
        }

        return Task.FromResult(existingUser);
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        User? user = await context.Users.FindAsync(id);
        return user;
        
    }

    public async Task<IEnumerable<User>> GetAsync(string userName)
    {
        IQueryable<User> usersQuery = context.Users.AsQueryable();
        if (userName != null)
        {
            usersQuery = usersQuery.Where(u => u.UserName.ToLower().Contains(userName.ToLower()));
        }

        IEnumerable<User> result = await usersQuery.ToListAsync();
        return result;
    }
}