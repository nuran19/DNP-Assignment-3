using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class PostEfcDao : IPostDao
{
    private readonly PostContext context;

    public PostEfcDao(PostContext context)
    {
        this.context = context;
    }

    public async Task<Post> CreateAsync(Post post)
    {
        EntityEntry<Post> added = await context.Posts.AddAsync(post);
        await context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters)
    {
        IQueryable<Post> query = context.Posts.Include(post => post.Owner).AsQueryable();
    
        if (!string.IsNullOrEmpty(searchParameters.Username))
        {
            // we know username is unique, so just fetch the first
            query = query.Where(post =>
                post.Owner.UserName.ToLower().Equals(searchParameters.Username.ToLower()));
        }
    
        if (searchParameters.UserId != null)
        {
            query = query.Where(p => p.Owner.Id == searchParameters.UserId);
        }
        List<Post> result = await query.ToListAsync();
        return result;
    }
}