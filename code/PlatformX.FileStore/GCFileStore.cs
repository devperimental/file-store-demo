using PlatformX.FileStore.Shared.Behaviours;
using PlatformX.StorageProvider.Shared.Behaviours;

namespace PlatformX.FileStore
{
    public class GCFileStore : DefaultFileStore, IGCFileStore
    {
        public GCFileStore(IStorageProvider storageProvider) :base (storageProvider:storageProvider){
            
        }
    }
}
