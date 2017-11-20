using Scrummage.Core.Domain;

namespace Scrummage.Core.Services
{
    public interface IEventService : IService
    {
        Event Create(Event newEvent);
    }
}
