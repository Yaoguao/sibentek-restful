using Sibentek.Core.Interface;
using Sibentek.Core.Model.DTO;

namespace Sibentek.Application.Service;

public class TelegramBotService : ITelegramBotService
{

    public MessageResponseDTO CreateMessageResult(UserMessageRequestDTO _userMessageRequestDto)
    {
        Console.WriteLine(_userMessageRequestDto.Username + " " + _userMessageRequestDto.Message + " " + _userMessageRequestDto.DateTime);
        return null;
    }
}