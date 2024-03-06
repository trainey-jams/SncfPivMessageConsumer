using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SncfPivMessageConsumer.Models
{
    public interface IServiceContext
    {
        string ConversationId { get; }
    }

    public class ServiceContext : IServiceContext
    {
        public string ConversationId => $"{Guid.NewGuid()}";
    }
}
