using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    public Task<User> CreateAsync(UserCreationDto userToCreate);
    
    public Task<User>GetAsync(UserLoginDto loginDto);
    
    

}