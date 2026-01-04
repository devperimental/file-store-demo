using System;

namespace GCWrapper.Shared
{
    public class GCSettings
    {
        public string? GoogleCloudServiceAccountKey { get; set; } = default!;
        public string? ProjectId { get; set; }
        public string? GenerationTopicId { get; set; }
    }
}
