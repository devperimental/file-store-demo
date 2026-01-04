using GCWrapper.Shared;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using PlatformX.StorageProvider.Shared.Behaviours;
using PlatformX.StorageProvider.Shared.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PlatformX.StorageProvider.GoogleCloud
{
    public class GCStorageProvider : IStorageProvider
    {
        private readonly GCSettings _settings;
        public GCStorageProvider(GCSettings settings) {
            _settings = settings;
        }
        public Task AppendToFile(StorageDefinition storageDefinition, Stream stream, bool create)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CopyFile(StorageDefinition storageDefinitionSource, StorageDefinition storageDefinitionTarget)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteFile(StorageDefinition storageDefinition)
        {
            // Initialize Google Cloud Storage client with credentials
            var credential = GoogleCredential.FromJson(_settings.GoogleCloudServiceAccountKey);
            var storageClient = await StorageClient.CreateAsync(credential);

            await storageClient.DeleteObjectAsync(storageDefinition.ContainerName, storageDefinition.FilePath);
        }

        public Task<List<string>> GetFiles(StorageDefinition storageDefinition, int? segmentSize)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPreSignedUrlAsync(StorageDefinition storageDefinition, int expirationMinutes)
        {
            throw new NotImplementedException();
        }

        public Task<string> LoadFile(StorageDefinition storageDefinition)
        {
            throw new NotImplementedException();
        }

        public Task<T> LoadFile<T>(StorageDefinition storageDefinition)
        {
            throw new NotImplementedException();
        }

        public async Task<string> LoadFileAsBase64(StorageDefinition storageDefinition)
        {
            // Initialize Google Cloud Storage client with credentials
            var credential = GoogleCredential.FromJson(_settings.GoogleCloudServiceAccountKey);
            var storageClient = await StorageClient.CreateAsync(credential);

            var base64Content = string.Empty;
            using (var memoryStream = new MemoryStream())
            {
                await storageClient.DownloadObjectAsync(storageDefinition.ContainerName, storageDefinition.FilePath, memoryStream);

                // Convert the downloaded bytes to Base64
                base64Content = Convert.ToBase64String(memoryStream.ToArray());
            }

            return base64Content;
        }

        public Task<Stream> LoadFileAsStream(StorageDefinition storageDefinition)
        {
            throw new NotImplementedException();
        }

        public Task<bool> MoveFile(StorageDefinition storageDefinitionSource, StorageDefinition storageDefinitionTarget)
        {
            throw new NotImplementedException();
        }

        public async Task SaveFile(StorageDefinition storageDefinition, Stream stream)
        {
            // Initialize Google Cloud Storage client with credentials
            var credential = GoogleCredential.FromJson(_settings.GoogleCloudServiceAccountKey);
            var storageClient = await StorageClient.CreateAsync(credential);

            var response = await storageClient.UploadObjectAsync(storageDefinition.ContainerName, 
                storageDefinition.FilePath,
                storageDefinition.ContentType,
                stream);
        }
    }
}
