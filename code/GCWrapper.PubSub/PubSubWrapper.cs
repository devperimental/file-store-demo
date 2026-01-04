using GCWrapper.Shared;
using GCWrapper.Shared.Behaviours;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.PubSub.V1;
using Google.Protobuf;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace GCWrapper.PubSub
{
    public class PubSubWrapper : IPubSubWrapper
    {
        private readonly GCSettings _settings;
        private readonly ILogger<PubSubWrapper> _logger;

        /// <summary>
        /// Constructor for the PubSubWrapper.
        /// </summary>
        /// <param name="logger">The injected logger for the wrapper.</param>
        public PubSubWrapper(GCSettings settings, ILogger<PubSubWrapper> logger)
        {
            _settings = settings;
            _logger = logger;
        }

        public async Task<string> PublishMessageAsync(string topicId, object messageData)
        {
            try
            {
                var credential = GoogleCredential.FromJson(_settings.GoogleCloudServiceAccountKey);
                var clientBuilder = new PublisherClientBuilder
                {
                    TopicName = TopicName.FromProjectTopic(_settings.ProjectId, topicId),
                    Credential = credential
                };
                
                // Initialize the Pub/Sub publisher client
                PublisherClient publisher = await clientBuilder.BuildAsync();

                // Convert message data to JSON string and then to bytes
                string messageJson = JsonSerializer.Serialize(messageData);
                ByteString messageBytes = ByteString.CopyFromUtf8(messageJson);

                // Publish the message
                string messageId = await publisher.PublishAsync(messageBytes);

                // Shut down the publisher client
                await publisher.ShutdownAsync(TimeSpan.FromSeconds(15));

                return messageId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
