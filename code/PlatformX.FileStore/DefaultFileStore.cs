using Newtonsoft.Json;
using PlatformX.FileStore.Shared.Behaviours;
using PlatformX.FileStore.Shared.Types;
using PlatformX.StorageProvider.Shared.Behaviours;
using PlatformX.StorageProvider.Shared.Types;
using System.IO;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace PlatformX.FileStore
{
    public class DefaultFileStore : IFileStore
    {
        private readonly IStorageProvider _storageProvider;

        public DefaultFileStore(IStorageProvider storageProvider) {
            _storageProvider = storageProvider;
        }

        public TResponse LoadFile<TResponse>(FileStoreRequest fileStoreRequest) where TResponse : new()
        {
            return LoadFileAsync<TResponse>(fileStoreRequest).Result;
        }

        public async Task<TResponse> LoadFileAsync<TResponse>(FileStoreRequest fileStoreRequest) where TResponse : new()
        {
            var storageDefinition = new StorageDefinition
            {
                ContainerName = fileStoreRequest.ContainerName,
                FilePath = fileStoreRequest.FilePath
            };

            return await _storageProvider.LoadFile<TResponse>(storageDefinition);
        }

        public string LoadFileAsString(FileStoreRequest fileStoreRequest)
        {
            return LoadFileAsStringAsync(fileStoreRequest).Result;
        }

        public async Task<string> LoadFileAsStringAsync(FileStoreRequest fileStoreRequest)
        {
            var storageDefinition = new StorageDefinition
            {
                ContainerName = fileStoreRequest.ContainerName,
                FilePath = fileStoreRequest.FilePath
            };

            return await _storageProvider.LoadFile(storageDefinition);
        }

        public async Task<Stream> LoadFileAsStream(FileStoreRequest fileStoreRequest)
        {
            var storageDefinition = new StorageDefinition
            {
                ContainerName = fileStoreRequest.ContainerName,
                FilePath = fileStoreRequest.FilePath
            };

            return await _storageProvider.LoadFileAsStream(storageDefinition);
        }

        public async Task<string> LoadFileAsBase64(FileStoreRequest fileStoreRequest)
        {
            var storageDefinition = new StorageDefinition
            {
                ContainerName = fileStoreRequest.ContainerName,
                FilePath = fileStoreRequest.FilePath
            };

            return await _storageProvider.LoadFileAsBase64(storageDefinition);
        }

        public void SaveFile<TRequest>(TRequest data, FileStoreRequest fileStoreRequest)
        {
            SaveFileAsync(data, fileStoreRequest).Wait();
        }

        public async Task SaveFileAsync<TRequest>(TRequest data, FileStoreRequest fileStoreRequest)
        {
            var storageDefinition = new StorageDefinition
            {
                ContainerName = fileStoreRequest.ContainerName,
                FilePath = fileStoreRequest.FilePath,
                ContentType = "application/json"
            };

            var jsonData = JsonConvert.SerializeObject(data);
            var bytes = Encoding.UTF8.GetBytes(jsonData);

            using var memoryStream = new MemoryStream();
            memoryStream.Write(bytes, 0, bytes.Length);
            await _storageProvider.SaveFile(storageDefinition, memoryStream);
        }

        public void SaveFile(FileStoreRequest fileStoreRequest, Stream stream, string contentType)
        {
            SaveFileAsync(fileStoreRequest, stream, contentType).Wait();
        }

        public async Task SaveFileAsync(FileStoreRequest fileStoreRequest, Stream stream, string contentType)
        {
            var storageDefinition = new StorageDefinition
            {
                ContainerName = fileStoreRequest.ContainerName,
                FilePath = fileStoreRequest.FilePath,
                ContentType = contentType
            };

            await _storageProvider.SaveFile(storageDefinition, stream);
        }

        public async Task<string> GetPreSignedUrlAsync(FileStoreRequest fileStoreRequest, int expirationMinutes)
        {
            var storageDefinition = new StorageDefinition
            {
                ContainerName = fileStoreRequest.ContainerName,
                FilePath = fileStoreRequest.FilePath,
            };

            return await _storageProvider.GetPreSignedUrlAsync(storageDefinition, expirationMinutes);
        }
    }
}
