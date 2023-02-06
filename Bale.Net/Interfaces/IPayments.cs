using Bale.Net.Types;

namespace Bale.Net.Interfaces;

public interface IPayments
{
    ValueTask<Message> SendInvoiceAsync(InvoiceRequest request);
}