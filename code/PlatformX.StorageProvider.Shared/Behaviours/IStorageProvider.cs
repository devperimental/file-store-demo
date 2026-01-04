using PlatformX.StorageProvider.Shared.Types;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PlatformX.StorageProvider.Shared.Behaviours
{
    public interface IStorageProvider
    {
        Task DeleteFile(StorageDefinition storageDefinition);
        Task<string> LoadFile(StorageDefinition storageDefinition);
        Task<T> LoadFile<T>(StorageDefinition storageDefinition);
        Task<Stream> LoadFileAsStream(StorageDefinition storageDefinition);
        Task<string> LoadFileAsBase64(StorageDefinition storageDefinition);
        Task<bool> MoveFile(StorageDefinition storageDefinitionSource, StorageDefinition storageDefinitionTarget);
        Task SaveFile(StorageDefinition storageDefinition, Stream stream);
        Task<List<string>> GetFiles(StorageDefinition storageDefinition, int? segmentSize);
        Task<bool> CopyFile(StorageDefinition storageDefinitionSource, StorageDefinition storageDefinitionTarget);
        Task AppendToFile(StorageDefinition storageDefinition, Stream stream, bool create);
        Task<string> GetPreSignedUrlAsync(StorageDefinition storageDefinition, int expirationMinutes);
    }
}
