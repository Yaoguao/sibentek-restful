namespace sibentek_restful
{
    using Telegram.Bot;
    using Telegram.Bot.Polling;
    using Telegram.Bot.Types;
    using Telegram.Bot.Types.Enums;

    public class TelegramBot
    {
        private ITelegramBotClient botClient;

        public TelegramBot(string token)
        {
            botClient = new TelegramBotClient(token);

            // Получаем информацию о боте
            var me = botClient.GetMeAsync().Result;
            Console.WriteLine($"Bot {me.FirstName} is running...");

            // Настройка приемника сообщений
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>() // Получать все типы обновлений
            };

            // Запуск асинхронной обработки сообщений без errorHandler
            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: default
            );

            Console.WriteLine("Bot started receiving messages...");
        }

        // Асинхронная обработка входящих сообщений
        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message is not { } message)
                return;

            if (message.Text is not { } messageText)
                return;

            // Ответ на сообщение
            await botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: $"Привет, {message.Chat.FirstName}!\nТы написал: {messageText}",
                cancellationToken: cancellationToken
            );
        }

        // Обработка ошибок polling
        private Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Error: {exception.Message}");
            return Task.CompletedTask;
        }
    }



}
