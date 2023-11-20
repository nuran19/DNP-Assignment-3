namespace Domain.DTOs;

public class PostCreationDto
{
    public int UserId { get; }
    public string Title { get; }
    public string Content { get; }

    public PostCreationDto(int userId, string title, string content)
    {
        UserId = userId;
        Title = title;
        Content = content;
    }

    // public PostCreationDto()
            // {
            // }
}