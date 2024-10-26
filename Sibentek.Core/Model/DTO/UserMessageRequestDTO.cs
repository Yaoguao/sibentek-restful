
namespace Sibentek.Core.Model.DTO;

public class UserMessageRequestDTO
{
    public String Name { get; set; }

    public String Username { get; set; }

    public String Message { get; set; }

    public DateTime DateTime {  set; get; }

    public UserMessageRequestDTO()
    {
    }

    public UserMessageRequestDTO(string name, string username, string message, DateTime dateTime)
    {
        Name = name;
        Username = username;
        Message = message;
        DateTime = dateTime;
    }
}