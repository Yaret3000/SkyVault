namespace Application.Dto
{
    /// <summary>
    /// Dto used to deserialize BLOB credentials
    /// </summary>
    public class AzureBlobCredentialsDto
    {
        public BlobCredentialsDetails AzureBlobConf { get; set; }
    }

    public class BlobCredentialsDetails 
    {
        public string ConnectionString { get; set; }
        public string ContainerName { get; set; }
    }
}
