using Sibentek.Core.Model;

namespace Sibentek.DataAccess.repositories;

public class UserMessageRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public UserMessageRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public void SaveMessageUser(UserMessage userMessage)
    {
        _applicationDbContext.Users.Add(userMessage);
        _applicationDbContext.SaveChanges();
    }

    public IEnumerable<UserMessage> GetAllMessages()
    {
        return _applicationDbContext.Users.ToList();
    }

    public UserMessage? GetMessageById(int id)
    {
        return _applicationDbContext.Users.Find(id);
    }

    public void UpdateMessage(UserMessage userMessage)
    {
        _applicationDbContext.Users.Update(userMessage);
        _applicationDbContext.SaveChanges();
    }

    public void DeleteMessageById(int id)
    {
        var userMessage = _applicationDbContext.Users.Find(id);
        if (userMessage != null)
        {
            _applicationDbContext.Users.Remove(userMessage);
            _applicationDbContext.SaveChanges();
        }
    }
}