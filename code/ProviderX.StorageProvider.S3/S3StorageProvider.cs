using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Newtonsoft.Json;
using PlatformX.StorageProvider.Shared.Behaviours;
using PlatformX.StorageProvider.Shared.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ProviderX.StorageProvider.S3
{
    public class S3StorageProvider : IStorageProvider
    {
        private AmazonS3Client _amazonS3Client;
        public S3StorageProvider(AmazonS3Client amazonS3Client) { 
            _amazonS3Client= amazonS3Client;
        }
        public Task AppendToFile(StorageDefinition storageDefinition, Stream stream, bool create)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CopyFile(StorageDefinition storageDefinitionSource, StorageDefinition storageDefinitionTarget)
        {
            throw new NotImplementedException();
        }

        public Task DeleteFile(StorageDefinition storageDefinition)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> GetFiles(StorageDefinition storageDefinition, int? segmentSize)
        {
            throw new NotImplementedException();
        }

        public async Task<string> LoadFile(StorageDefinition storageDefinition)
        {
            var request = new GetObjectRequest
            {
                BucketName = storageDefinition.ContainerName,
                Key = storageDefinition.FilePath
            };

            using var objectResponse = await _amazonS3Client.GetObjectAsync(request);
            using var responseStream = objectResponse.ResponseStream;
            using var sr = new StreamReader(responseStream);
            var output = await sr.ReadToEndAsync();

            return output;
        }

        public async Task<T> LoadFile<T>(StorageDefinition storageDefinition)
        {
            var request = new GetObjectRequest
            {
                BucketName = storageDefinition.ContainerName,
                Key = storageDefinition.FilePath
            };

            using var objectResponse = await _amazonS3Client.GetObjectAsync(request);
            using var responseStream = objectResponse.ResponseStream;
            using var sr = new StreamReader(responseStream);
            var output = await sr.ReadToEndAsync();

            var response = JsonConvert.DeserializeObject<T>(output);

            return response ?? default!;
        }

        public async Task<Stream> LoadFileAsStream(StorageDefinition storageDefinition)
        {
            var request = new GetObjectRequest
            {
                BucketName = storageDefinition.ContainerName,
                Key = storageDefinition.FilePath
            };

            // Create the streams.
            MemoryStream destination = new MemoryStream();
            using (var response = await _amazonS3Client.GetObjectAsync(request))
            {
                var responseStream = response.ResponseStream;
                await responseStream.CopyToAsync(destination);
                destination.Position = 0;
            }
            return destination;
        }

        public async Task<string> GetPreSignedUrlAsync(StorageDefinition storageDefinition, int expirationMinutes)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = storageDefinition.ContainerName,
                Key = storageDefinition.FilePath,
                Expires = DateTime.UtcNow.Add(TimeSpan.FromMinutes(expirationMinutes)), // Set expiration time
                Verb = HttpVerb.GET // For downloading
            };

            return await _amazonS3Client.GetPreSignedURLAsync(request);
        }

        public async Task<string> LoadFileAsBase64(StorageDefinition storageDefinition)
        {
            var request = new GetObjectRequest
            {
                BucketName = storageDefinition.ContainerName,
                Key = storageDefinition.FilePath
            };

            var base64 = string.Empty;
            
            using (GetObjectResponse response = await _amazonS3Client.GetObjectAsync(request))
            using (MemoryStream destination = new MemoryStream())
            {
                var responseStream = response.ResponseStream;
                await responseStream.CopyToAsync(destination);
                destination.Position = 0;
                base64 = Convert.ToBase64String(destination.ToArray());
            }
            
            return base64;
        }

        public Task<bool> MoveFile(StorageDefinition storageDefinitionSource, StorageDefinition storageDefinitionTarget)
        {
            throw new NotImplementedException();
        }

        public async Task SaveFile(StorageDefinition storageDefinition, Stream stream)
        {
            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = stream,
                Key = storageDefinition.FilePath,
                BucketName = storageDefinition.ContainerName,
                ContentType = storageDefinition.ContentType
            };

            uploadRequest.UploadProgressEvent += UploadRequest_UploadProgressEvent;

            var fileTransferUtility = new TransferUtility(_amazonS3Client);

            await fileTransferUtility.UploadAsync(uploadRequest);
        }

        private void UploadRequest_UploadProgressEvent(object sender, UploadProgressArgs e)
        {
            

        }
    }
}
