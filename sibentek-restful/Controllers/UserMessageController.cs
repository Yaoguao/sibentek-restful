using Microsoft.AspNetCore.Mvc;
using Sibentek.Core.Interface;
using Sibentek.Core.Model.DTO;

namespace sibentek_restful.Controllers;

[ApiController]
[Route("[controller]")]
public class UserMessageController : ControllerBase
{

    private readonly IUserMessageService _userMessageService;

    public UserMessageController(IUserMessageService userMessageService)
    {
        _userMessageService = userMessageService;
    }

    [HttpPost]
    public ActionResult<MessageResponseDTO> CreateMessageResult([FromBody] UserMessageRequestDTO messageRequest)
    {

        var messageResponseDto = _userMessageService.CreateMessageResult(messageRequest);
        
        return Ok(messageResponseDto);
    }
}