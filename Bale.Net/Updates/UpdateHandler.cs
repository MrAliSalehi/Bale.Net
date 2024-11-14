using Bale.Net.Interfaces;
using Bale.Net.Types;

namespace Bale.Net.Updates;

public static class UpdateHandler
{
    private static readonly Update[] EmptyUpdates = Array.Empty<Update>();
    private static readonly List<long> CacheUpdateId = new();

    private static readonly CancellationTokenSource Cts = new();
    public static void ReceiveUpdates<THandler>(this IUpdates updateInterface, TimeSpan delay = default) where THandler : IUpdateHandler, new()
    {
#pragma warning disable CS4014
        Task.Run(() => RunAsync<THandler>(updateInterface, delay, Cts.Token), Cts.Token);
#pragma warning restore CS4014
    }

    public static void ReceiveUpdates<THandler, TUpdateType>(this IUpdates updateInterface, TimeSpan delay = default)
        where THandler : IUpdateHandler<TUpdateType>, new()
        where TUpdateType : IUpdateType, new()
    {
#pragma warning disable CS4014
        Task.Run(() => RunAsync<THandler, TUpdateType>(updateInterface, delay, Cts.Token), Cts.Token);
#pragma warning restore CS4014
    }

    private static async ValueTask RunAsync<THandler>(this IUpdates updateInterface, TimeSpan delay = default, CancellationToken ct = default) where THandler : IUpdateHandler, new()
    {
        var handler = new THandler();
        long offset = 0;
        var empty = EmptyUpdates;

        while (!ct.IsCancellationRequested)
        {
            if (delay != default)
                await Task.Delay(delay, ct);

            var updates = empty;
            try
            {
                updates = await updateInterface.GetUpdatesAsync(offset, 100);
            }
            catch (Exception e)
            {
                await Helpers.ExecIgnoreExceptionsAsync(() => handler.HandleErrorsAsync(e, ct));
            }

            foreach (var update in updates)
            {
                if (CacheUpdateId.Contains(update.UpdateId))
                {
                    CacheUpdateId.Remove(update.UpdateId);
                    continue;
                }

                CacheUpdateId.Add(update.UpdateId);

                try
                {
                    await handler.ReceiveUpdateAsync(update, ct);
                }
                catch (Exception e)
                {
                    await Helpers.ExecIgnoreExceptionsAsync(() => handler.HandleErrorsAsync(e, ct));
                }

                offset = update.UpdateId + 1;
            }

            if (CacheUpdateId.Count > 10)
                CacheUpdateId.RemoveRange(0, 3);
        }
    }

    public static void StopReceiving(this IUpdates _)
    {
        Cts.Cancel();
    }

    private static async ValueTask RunAsync<THandler, TUpdateType>(this IUpdates updateInterface, TimeSpan delay = default, CancellationToken ct = default)
        where THandler : IUpdateHandler<TUpdateType>, new()
        where TUpdateType : IUpdateType, new()
    {
        var handler = new THandler();

        long offset = 0;
        var empty = EmptyUpdates;
        var updateType = typeof(TUpdateType);

        while (!ct.IsCancellationRequested)
        {
            if (delay != default)
                await Task.Delay(delay, ct);

            var updates = empty;
            try
            {
                updates = await updateInterface.GetUpdatesAsync(offset, 100);
            }
            catch (Exception e)
            {
                await Helpers.ExecIgnoreExceptionsAsync(() => handler.HandleErrorsAsync(e, ct));
            }

            foreach (var update in updates)
            {
                if (CacheUpdateId.Contains(update.UpdateId))
                {
                    CacheUpdateId.Remove(update.UpdateId);
                    continue;
                }

                CacheUpdateId.Add(update.UpdateId);

                try
                {
                    if (updateType == typeof(Message))
                    {
                        if (update.Message is not null)
                            await handler.ReceiveUpdateAsync((TUpdateType)(IUpdateType)update.Message!, ct);
                        else if (update.ChannelPost is not null)
                            await handler.ReceiveUpdateAsync((TUpdateType)(IUpdateType)update.ChannelPost!, ct);
                        else if (update.EditedMessage is not null)
                            await handler.ReceiveUpdateAsync((TUpdateType)(IUpdateType)update.EditedMessage!, ct);
                        else if (update.EditedChannelPost is not null)
                            await handler.ReceiveUpdateAsync((TUpdateType)(IUpdateType)update.EditedChannelPost!, ct);
                    }
                    else if (updateType == typeof(CallbackQuery))
                    {
                        await handler.ReceiveUpdateAsync((TUpdateType)(IUpdateType)update.CallbackQuery!, ct);
                    }
                    else if (updateType == typeof(ShippingQuery))
                    {
                        await handler.ReceiveUpdateAsync((TUpdateType)(IUpdateType)update.ShippingQuery!, ct);
                    }
                    else if (updateType == typeof(PreCheckoutQuery))
                    {
                        await handler.ReceiveUpdateAsync((TUpdateType)(IUpdateType)update.PreCheckoutQuery!, ct);
                    }

                    //await handler.ReceiveUpdateAsync(update, ct);
                }
                catch (Exception e)
                {
                    await Helpers.ExecIgnoreExceptionsAsync(() => handler.HandleErrorsAsync(e, ct));
                }

                offset = update.UpdateId + 1;
            }

            if (CacheUpdateId.Count > 10)
                CacheUpdateId.RemoveRange(0, 3);
        }
    }
}