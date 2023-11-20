namespace Domain.DTOs;

public class SearchPostParametersDto
{
    public int? PostId { get; }
    public int? UserId { get; }
    public string? Username { get; }


    public SearchPostParametersDto(int? postId, int? userId, string? username)
    {
        PostId = postId;
        UserId = userId;
        Username = username;
    } 
}