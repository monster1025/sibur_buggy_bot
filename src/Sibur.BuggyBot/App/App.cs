using Sibur.BuggyBot.Messaging;
using System;
using Telegram.Bot.Types;

namespace Sibur.BuggyBot.App;

public class App : IApp
{
    private readonly ITelegram _telegram;
    private readonly Random _random;
    private readonly string[] _assignee = new[] { "yaprostosplu", "RobinsonBastard" };

    public App(ITelegram telegram)
    {
        _telegram = telegram;
        _telegram.Start().GetAwaiter().GetResult();
        _telegram.MessageHook = OnMessage;
        _random = new Random();
    }

    private async Task OnMessage(Message message)
    {
        if (message.Text?.StartsWith($"@{_telegram.NickName}") == true)
        {
            var assignee = _assignee[_random.Next(0, _assignee.Length)];
            var msg = $"@{assignee}, прошу взять в работу.";

            await _telegram.SendMessageAsync(message.Chat.Id, msg, replyToMessageId: message.MessageId);
        }

        Console.WriteLine($"[{message.From?.Id}] {message.Text}");
    }

    public async Task RunAsync()
    {
        while (true)
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }
}