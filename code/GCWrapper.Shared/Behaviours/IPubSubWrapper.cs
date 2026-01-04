using System.Threading.Tasks;

namespace GCWrapper.Shared.Behaviours
{
    public interface IPubSubWrapper
    {
        Task<string> PublishMessageAsync(string topicId, object messageData);
    }
}
