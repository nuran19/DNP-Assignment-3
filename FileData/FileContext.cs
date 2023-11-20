using System.Text.Json;
using Domain.Models;

namespace FileData;

public class FileContext
{
    private const string filePath = "data.json";
    private DataContainer? dataContainer;
    
    public ICollection<Post> Posts
    {
        get
        {
            LoadData();
            return dataContainer!.Posts;
        }
    }

    public ICollection<User> Users
    {
        get
        {
            LoadData();
            return dataContainer!.Users;
        }
    }

    /*private void LazyLoadData()
    {
        if (dataContainer == null)
        {
            LoadData();
        }
    }*/

    private void LoadData()
    {
        if (dataContainer != null) return;

        if (!File.Exists((filePath)))
        {
            dataContainer = new()
            {
                Posts = new List<Post>(),
                Users = new List<User>()
            };
            return;
        }
        string content = File.ReadAllText(filePath);
        Console.WriteLine("Content: "+content);
        dataContainer = JsonSerializer.Deserialize<DataContainer>(content);
    }

    /*public void SaveChanges()
    {
        string serialized = JsonSerializer.Serialize(dataContainer);
        File.WriteAllText(filePath, serialized);
        dataContainer = null;
    }*/
    
    
    public void SaveChanges()
    {
        string serialized = JsonSerializer.Serialize(dataContainer, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        File.WriteAllText(filePath, serialized);
        dataContainer = null;
    }
}