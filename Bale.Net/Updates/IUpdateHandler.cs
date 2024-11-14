using Bale.Net.Types;

namespace Bale.Net.Updates;

public interface IUpdateHandler
{
    ValueTask ReceiveUpdateAsync(Update update, CancellationToken ct = default);
    ValueTask HandleErrorsAsync(Exception exception, CancellationToken ct = default);
}


public interface IUpdateHandler<in TUpdate> where TUpdate : IUpdateType
{
    ValueTask ReceiveUpdateAsync(TUpdate update, CancellationToken ct = default);
    ValueTask HandleErrorsAsync(Exception exception, CancellationToken ct = default);
}