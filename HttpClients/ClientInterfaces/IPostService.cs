using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IPostService
{
    Task Create(PostCreationDto dto);
    
    Task<ICollection<Post>> GetAsync(int? postId, int? userId);
}