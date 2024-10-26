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
        private ITelegramBotClient _botClient;
        
        private readonly IServiceProvider _serviceProvider;

        public TelegramBot(ITelegramBotClient botClient, IServiceProvider serviceProvider)
        {
            _botClient = botClient;
            _serviceProvider = serviceProvider;

            InitializeBot();
        }

        private void InitializeBot()
        {
            var me = _botClient.GetMeAsync().Result;
            Console.WriteLine($"Bot {me.FirstName} is running...");

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };

            _botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: default
            );

            Console.WriteLine("Bot started receiving messages...");
        }

        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message is not { } message || message.Text is not { } messageText)
                return;

            using (var scope = _serviceProvider.CreateScope())
            {
                var userMessageService = scope.ServiceProvider.GetRequiredService<IUserMessageService>();

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
                    var userMessageRequestDto = new UserMessageRequestDTO(
                        message.Chat?.FirstName ?? "DefaultFirstName",
                        message.Chat?.Username ?? "DefaultUsername",
                        messageText,
                        message.Date
                    );

                    var messageResponseDto = userMessageService.CreateMessageResult(userMessageRequestDto);

                    
                    await botClient.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: $"Ваше обращение было направлено в раздел: {messageResponseDto.ServiceName}" +
                              $"\nВозможное решение: {messageResponseDto.RecommendedActions}",
                        cancellationToken: cancellationToken
                    );
                    
                    await botClient.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: $"Чем могу еще помочь?",
                        cancellationToken: cancellationToken
                    );
                }
            }
        }

        private Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Error: {exception.Message}");
            return Task.CompletedTask;
        }
    }



}
