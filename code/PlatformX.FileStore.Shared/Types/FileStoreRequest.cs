namespace PlatformX.FileStore.Shared.Types
{
    public class FileStoreRequest
    {
        public string? ContainerName { get; set; }
        public string? FilePath { get; set; }
        public string? ContentType { get; set; }
    }
}
