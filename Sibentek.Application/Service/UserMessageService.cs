using ModelTextForApi;
using Sibentek.Core.Interface;
using Sibentek.Core.Model;
using Sibentek.Core.Model.DTO;
using Sibentek.DataAccess.repositories;

namespace Sibentek.Application.Service;

public class UserMessageService : IUserMessageService
{

    public readonly UserMessageRepository _UserMessageRepository;

    public UserMessageService(UserMessageRepository userMessageRepository)
    {
        _UserMessageRepository = userMessageRepository;
    }

    public MessageResponseDTO CreateMessageResult(UserMessageRequestDTO _userMessageRequestDto)
    {
        Console.WriteLine(_userMessageRequestDto.Username + " " + _userMessageRequestDto.Message + " " + _userMessageRequestDto.DateTime);
        UserMessage userMessage = new UserMessage();
        userMessage.Name = _userMessageRequestDto.Name;
        userMessage.Message = _userMessageRequestDto.Message;
        
        _UserMessageRepository.SaveMessageUser(userMessage);

        var name = _userMessageRequestDto.Name;
        
        var sampleData = new MLModel1.ModelInput()
        {
            Topic = @_userMessageRequestDto.Message,
        };
        var res = MLModel1.Predict(sampleData);
        Console.WriteLine(res.Score.Max() * 100 + " " + res.PredictedLabel);

        if (res.Score.Max() * 100 < 40) res.PredictedLabel = "Не определено";


        var sampleDataSol = new ModelSolution.ModelInput()
        {
            Col0 = @_userMessageRequestDto.Message,
        };
        var resSol = ModelSolution.Predict(sampleDataSol);
        
        if (resSol.Score.Max() * 100 < 4) resSol.PredictedLabel = "Не определено";
        
        
        
        MessageResponseDTO messageResponseDto = 
            new MessageResponseDTO(name, res.PredictedLabel, resSol.PredictedLabel);
        return messageResponseDto;
    }
}