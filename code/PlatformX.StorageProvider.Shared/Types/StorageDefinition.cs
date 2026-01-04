using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformX.StorageProvider.Shared.Types
{
    public class StorageDefinition
    {
        public string? ContainerName { get; set; }
        public string? FilePath { get; set; }
        public string? ContentType { get; set; }
    }
}
