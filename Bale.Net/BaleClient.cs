using Bale.Net.Implementations;
using Bale.Net.Interfaces;

namespace Bale.Net;

public class BaleClient
{
    internal readonly string Token;
    public IAttachments Attachments { get; }
    public IChats Chats { get; }
    public IMessages Messages { get; }
    public IPayments Payments { get; }
    public IUpdates Updates { get; }
    public IUsers Users { get; }
    public BaleClient(string token)
    {
        Token = token;
        Attachments = new Attachments();
        Chats = new Chats();
        Messages = new Messages();
        Payments = new Payments();
        Updates = new Updates();
        Users = new Users();
    }
}