using Sibentek.Core.Model.DTO;

namespace Sibentek.Core.Interface;

public interface IUserMessageService
{
    MessageResponseDTO CreateMessageResult(UserMessageRequestDTO _userMessageRequestDto);
}