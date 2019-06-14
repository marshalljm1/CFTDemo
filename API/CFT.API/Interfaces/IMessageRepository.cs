using CFT.API.Models;

namespace CFT.API.Interfaces
{
    public interface IMessageRepository : IRepository<Messages>
    {
        Messages GetCurrentMessage();
    }
}
