using PlatformX.FileStore.Shared.Behaviours;
using PlatformX.StorageProvider.Shared.Behaviours;

namespace PlatformX.FileStore
{
    public class AWSFileStore : DefaultFileStore, IAWSFileStore
    {
        public AWSFileStore(IStorageProvider storageProvider) :base (storageProvider:storageProvider){
            
        }
    }
}
