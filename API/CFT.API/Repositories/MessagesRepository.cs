using System;
using System.Linq;
using CFT.API.Interfaces;
using CFT.API.Models;

namespace CFT.API.Repositories
{
    public class MessagesRepository : Repository<Messages>, IMessageRepository
    {
        public CFTContext Cft => Context as CFTContext;

        public MessagesRepository(CFTContext context) : base(context)
        {
        }

        public Messages GetCurrentMessage()
        {
            return Cft.Messages.FirstOrDefault(x => x.StartDate <= DateTime.Today && DateTime.Today <= x.EndDate);
        }
    }
}
