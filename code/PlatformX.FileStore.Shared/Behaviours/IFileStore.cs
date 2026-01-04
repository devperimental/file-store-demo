using PlatformX.FileStore.Shared.Types;
using System.IO;
using System.Threading.Tasks;

namespace PlatformX.FileStore.Shared.Behaviours
{
    public interface IFileStore
    {
        TResponse LoadFile<TResponse>(FileStoreRequest fileStoreRequest) where TResponse : new();
        Task<TResponse> LoadFileAsync<TResponse>(FileStoreRequest fileStoreRequest) where TResponse : new();
        string LoadFileAsString(FileStoreRequest fileStoreRequest);
        Task<string> LoadFileAsStringAsync(FileStoreRequest fileStoreRequest);
        Task<Stream> LoadFileAsStream(FileStoreRequest fileStoreRequest);
        Task<string> LoadFileAsBase64(FileStoreRequest fileStoreRequest);
        void SaveFile<TRequest>(TRequest data, FileStoreRequest fileStoreRequest);
        Task SaveFileAsync<TRequest>(TRequest data, FileStoreRequest fileStoreRequest);
        void SaveFile(FileStoreRequest fileStoreRequest, Stream stream, string contentType);
        Task SaveFileAsync(FileStoreRequest fileStoreRequest, Stream stream, string contentType);
        Task<string> GetPreSignedUrlAsync(FileStoreRequest fileStoreRequest, int expirationMinutes);
    }
}
