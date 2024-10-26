namespace Sibentek.Core.Model.DTO;

public class MessageResponseDTO
{
    public String Name { get; set; }

    public String ServiceName { get; set; }

    public String RecommendedActions { get; set; }

    public MessageResponseDTO(string name, string serviceName, string recommendedActions)
    {
        Name = name;
        ServiceName = serviceName;
        RecommendedActions = recommendedActions;
    }
}