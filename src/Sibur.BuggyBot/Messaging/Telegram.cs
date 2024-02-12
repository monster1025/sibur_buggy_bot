using NLog;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Sibur.BuggyBot.Messaging
{
    public sealed  class Telegram: ITelegram
    {
        private readonly TelegramBotClient _telegram;
        private readonly ILogger _logger;
        public Func<Message, Task>? MessageHook { get; set; } = null;
        public string? NickName { get; set; } = null;

        public Telegram(TelegramBotClient telegram, ILogger logger)
        {
            _telegram = telegram;
            _logger = logger;
        }

        public async Task Start()
        {
            var cts = new CancellationTokenSource();
            _telegram.StartReceiving(updateHandler: HandleUpdateAsync,
                pollingErrorHandler: PollingErrorHandler,
                receiverOptions: new ReceiverOptions(),
                cancellationToken: cts.Token);

            NickName = (await _telegram.GetMeAsync(cancellationToken: cts.Token)).Username;
        }


        private void PollingErrorHandler(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {

        }

        private void HandleUpdateAsync(ITelegramBotClient arg1, Update update, CancellationToken arg3)
        {
            if (update.Message != null)
            {
                _logger.Info($"{update.Message.Chat.Id}: {update.Message.Text}");
                MessageHook?.Invoke(update.Message);
            }
        }

        public Task SendMessageAsync(ChatId chatId, string text, int? messageThreadId = default, ParseMode? parseMode = default,
            IEnumerable<MessageEntity>? entities = default, bool? disableWebPagePreview = default, bool? disableNotification = default,
            bool? protectContent = default, int? replyToMessageId = default, bool? allowSendingWithoutReply = default,
            IReplyMarkup? replyMarkup = default, CancellationToken cancellationToken = default)
        {
            return _telegram.SendTextMessageAsync(chatId, text, messageThreadId, parseMode, entities, disableWebPagePreview,
                disableNotification, protectContent, replyToMessageId, allowSendingWithoutReply, replyMarkup, cancellationToken);
        }
    }
}
