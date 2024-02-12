using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Sibur.BuggyBot.Messaging;

public interface ITelegram
{
    Task Start();
    public Func<Message, Task>? MessageHook { get; set; }
    public string? NickName { get; set; }
    Task SendMessageAsync(
        ChatId chatId,
        string text,
        int? messageThreadId = default,
        ParseMode? parseMode = default,
        IEnumerable<MessageEntity>? entities = default,
        bool? disableWebPagePreview = default,
        bool? disableNotification = default,
        bool? protectContent = default,
        int? replyToMessageId = default,
        bool? allowSendingWithoutReply = default,
        IReplyMarkup? replyMarkup = default,
        CancellationToken cancellationToken = default);
}