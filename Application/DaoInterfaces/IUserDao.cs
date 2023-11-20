using Domain.DTOs;
using Domain.Models;

namespace Application.DaoInterfaces;

public interface IUserDao
{
    Task<User> CreateAsync(User user);
    Task<User?> GetByUsernameAsync(string userName);
    Task<User?> GetAsync(UserLoginDto loginDto);
    Task<User?> GetByIdAsync(int id);
    Task<IEnumerable<User>> GetAsync(String userName);

}