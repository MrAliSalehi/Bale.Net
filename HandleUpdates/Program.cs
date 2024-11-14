using Bale.Net;
using Bale.Net.NUnit;
using Bale.Net.Types;
using Bale.Net.Updates;

var client = new BaleClient(Helpers.GetTestToken());


client.Updates.ReceiveUpdates<MessageUpdateHandler, Message>();

Console.ReadKey();
Console.WriteLine("closing...");
await Task.Delay(TimeSpan.FromSeconds(10));
client.Updates.StopReceiving();
Console.WriteLine("stopped");
await Task.Delay(TimeSpan.FromDays(10));


class MessageUpdateHandler : IUpdateHandler<Message>
{
    public ValueTask ReceiveUpdateAsync(Message update, CancellationToken ct = default)
    {
        Console.WriteLine(update.Text); return ValueTask.CompletedTask;
    }
    public ValueTask HandleErrorsAsync(Exception exception, CancellationToken ct = default)
    {
        Console.WriteLine(exception.Message); return ValueTask.CompletedTask;
    }
}

/*class UpdateHandler : IUpdateHandler
{
    public ValueTask ReceiveUpdateAsync(Update update, CancellationToken ct = default)
    {
        Console.WriteLine(update.UpdateId);
        return ValueTask.CompletedTask;
    }
    public ValueTask HandleErrorsAsync(Exception exception, CancellationToken ct = default)
    {
        Console.WriteLine(exception.Message);
        return ValueTask.CompletedTask;
    }
}*/