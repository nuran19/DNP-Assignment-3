using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace FileData.DAOs;

public class PostFileDao : IPostDao
{
    
    private readonly FileContext context;

    public PostFileDao(FileContext context)
    {
        this.context = context;
    }

    public Task<Post> CreateAsync(Post post)
    {
        int id = 1;
        if (context.Posts.Any())
        {
            id = context.Posts.Max(t => t.Id);
            id++;
        }

        post.Id = id;
        
        context.Posts.Add(post);
        context.SaveChanges();

        return Task.FromResult(post);
    }
    
    public Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParams)
    {
        IEnumerable<Post> result = context.Posts.AsEnumerable();
        Console.WriteLine("Post Title "+ context.Posts.First().Title); 
        if (!string.IsNullOrEmpty(searchParams.Username))
        {
            // we know username is unique, so just fetch the first
            result = context.Posts.Where(post =>
                post.Owner.UserName.Equals(searchParams.Username, StringComparison.OrdinalIgnoreCase));
        }

        if (searchParams.UserId != null)
        {
            result = result.Where(p => p.Owner.Id == searchParams.UserId);
        }

        if (searchParams.PostId != null)
        { 
            result = result.Where(p => p.Id == searchParams.PostId);
        }
          

        return Task.FromResult(result);
    }
    
    
}