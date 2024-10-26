using Sibentek.Application.Service;
using Sibentek.Core.Interface;
using Sibentek.Core.Model.DTO;

namespace sibentek_restful
{
    using Telegram.Bot;
    using Telegram.Bot.Polling;
    using Telegram.Bot.Types;
    using Telegram.Bot.Types.Enums;

    public class TelegramBot
    {
        private ITelegramBotClient botClient;
        
        private readonly ITelegramBotService _telegramBotService = new TelegramBotService();
        

        public TelegramBot(string token)
        {
            botClient = new TelegramBotClient(token);

            // Получаем информацию о боте
            var me = botClient.GetMeAsync().Result;
            Console.WriteLine($"Bot {me.FirstName} is running...");

            // Настройка приемника сообщений
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };

            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: default
            );

            Console.WriteLine("Bot started receiving messages...");
        }

        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message is not { } message)
                return;

            if (message.Text is not { } messageText)
                return;
            
            if (messageText.Equals("/start", StringComparison.OrdinalIgnoreCase))
            {
                await botClient.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: $"Добро пожаловать, {message.Chat.FirstName}! Я ваш консультант. Чем могу помочь?",
                    cancellationToken: cancellationToken
                );
            } 
            else
            {
                Console.WriteLine(message.Chat?.FirstName ?? "DefaultFirstName");
                Console.WriteLine(messageText);
                UserMessageRequestDTO userMessageRequestDto = new UserMessageRequestDTO(
                    message.Chat?.FirstName ?? "DefaultFirstName",
                    message.Chat?.Username ?? "DefaultUsername",
                    messageText,
                    message.Date
                );
                MessageResponseDTO messageResponseDto = _telegramBotService.CreateMessageResult(userMessageRequestDto);
                
                await botClient.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: $"Вы написали: {messageText}",
                    cancellationToken: cancellationToken
                    
                );
            }
        }

        private Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Error: {exception.Message}");
            return Task.CompletedTask;
        }
    }



}
