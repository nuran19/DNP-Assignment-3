using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class UserLogic : IUserLogic
{
    private readonly IUserDao userDao;

    public UserLogic(IUserDao userDao)
    {
        this.userDao = userDao;
    }
    
   //Register user
    public async Task<User> CreateAsync(UserCreationDto dto)
    {
        User? existing = await userDao.GetByUsernameAsync(dto.UserName);
        if (existing != null)
            throw new Exception("Username already taken!");


        ValidateData(dto);
        User toCreate = new User
        {
            UserName = dto.UserName,
            Password = dto.Password
        };

        User created = await userDao.CreateAsync(toCreate);

        return created;
    }

    public Task<User> GetAsync(UserLoginDto loginDto)
    {
        return userDao.GetAsync(loginDto);
    }
    


    private static void ValidateData(UserCreationDto userToCreate)
    {
        string userName = userToCreate.UserName;

        if (userName.Length < 3)
            throw new Exception("Username must be at least 3 characters!");

        if (userName.Length > 15)
            throw new Exception("Username must be less that 16 characters!");

        string passWord = userToCreate.Password;
        
        if(passWord.Length < 8)
            throw new Exception("Password must be less that 16 characters!");
    }
}