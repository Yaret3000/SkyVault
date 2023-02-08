namespace Application.Services.Storage.Interfaces
{
    /// <summary>
    /// Contract for storage services
    /// </summary>
    public interface IStorageService
    {
        /// <summary>
        /// Method for uploading files
        /// </summary>
        /// <param name="fileName">Physical file name</param>
        /// <param name="memoryStream">Memory stream file</param>
        /// <param name="description">Optional description</param>
        void UploadFile(string fileName, MemoryStream memoryStream, string description = default);
        
        /// <summary>
        /// Method to download files
        /// </summary>
        /// <param name="fileName">Physical file name</param>
        /// <returns>Stream file</returns>
        Task<Stream> DownloadFile(string fileName);

        /// <summary>
        /// Method to delete files
        /// </summary>
        /// <param name="fileName">Filename to delete</param>
        void DeleteFile(string fileName);
    }
}
