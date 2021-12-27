using System.Threading.Tasks;

namespace ProjectService.EventProcessing
{
    public interface IEventProcessor
    {
        Task ProcessEvent(string message);
    }
}