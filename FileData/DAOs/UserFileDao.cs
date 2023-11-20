using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace FileData.DAOs;

public class UserFileDao : IUserDao
{
    private readonly FileContext context;

    public UserFileDao(FileContext context)
    {
        this.context = context;
    }
    
    public Task<User> CreateAsync(User user)
    {
        int userId = 1;
        if (context.Users.Any())
        {
            userId = context.Users.Max(u => u.Id);
            userId++;
        }

        user.Id = userId;
        
        context.Users.Add(user);
        context.SaveChanges();

        return Task.FromResult(user);
    }

    public Task<User?> GetByUsernameAsync(string userName)
    {
        User? existing = context.Users.FirstOrDefault(u =>
            u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase)
        );
        return Task.FromResult(existing);
    }

    public Task<User?> GetAsync(UserLoginDto loginDto)
    {
        IEnumerable<User> users = context.Users.AsEnumerable();
        User? existingUser = null;
        if (loginDto.UserName != null && loginDto.Password != null) // Log in request!
        {
                 existingUser = context.Users.FirstOrDefault(user => user.UserName.Equals(loginDto.UserName, StringComparison.OrdinalIgnoreCase) && user.Password.Equals(loginDto.Password));
                if (existingUser == null)
                {
                    throw new Exception("User not found");
                }

                if (!existingUser.Password.Equals(loginDto.Password))
                {
                    throw new Exception("Password mismatch");
                }
        }

        return Task.FromResult(existingUser);
    }

    public Task<User?> GetByIdAsync(int id)
    {
        User? existing = context.Users.FirstOrDefault(u => u.Id == id);
        return Task.FromResult(existing);
    }

    public Task<IEnumerable<User>> GetAsync(string userName)
    {
        throw new NotImplementedException();
    }
}