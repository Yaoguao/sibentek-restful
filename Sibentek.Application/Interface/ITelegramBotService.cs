using Sibentek.Core.Model.DTO;

namespace Sibentek.Core.Interface;

public interface ITelegramBotService
{
    MessageResponseDTO CreateMessageResult(UserMessageRequestDTO _userMessageRequestDto);
}