using Bale.Net.Interfaces;
using Bale.Net.Types;

namespace Bale.Net.Implementations;

public class Payments : IPayments
{
    private readonly BaleClient _client;
    public Payments(BaleClient client)
    {
        _client = client;
    }
    public async ValueTask<Message> SendInvoiceAsync(InvoiceRequest request) => 
        await _client.PostAsync<InvoiceRequest, Message>(Endpoint.SendInvoice, request);
}