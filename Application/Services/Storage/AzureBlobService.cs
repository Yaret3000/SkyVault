using Application.Constants;
using Application.Dto;
using Application.Helpers;
using Application.Services.Storage.Interfaces;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.Security.Claims;

namespace Application.Services.Storage
{
    /// <summary>
    /// Azure Blob service for cloud storage
    /// </summary>
    public class AzureBlobService : IStorageService
    {
        #region PublicProps
        public string UserKey { get; set ; }

        #endregion

        #region PrivateVariables

        private readonly BlobServiceClient _blobServiceClient;
        private BlobContainerClient _containerClient;
        
        private string _userRoute => string.Concat(UserKey, @"\");

        public AzureBlobService(BlobServiceClient blobServiceClient)
        {
            AzureBlobCredentialsDto appSettingDto = JsonHelper.ReadJsonFile<AzureBlobCredentialsDto>(FilesConstants.JsonConfig);
            _blobServiceClient = blobServiceClient;
            _containerClient = _blobServiceClient.GetBlobContainerClient(appSettingDto.AzureBlobConf.ContainerName);
        }

        #endregion

        /// <inheritdoc />
        public  void DeleteFile(string fileName)
        {
            CheckUserKey();
             _containerClient.DeleteBlob(string.Concat(_userRoute, fileName));
        }

        /// <inheritdoc />
        public Stream DownloadFile(string fileName)
        {
            CheckUserKey();
            Response<BlobDownloadResult> downloadResult = _containerClient.GetBlobClient(string.Concat(_userRoute, fileName)).DownloadContent();
            return downloadResult.Value.Content.ToStream();
        }

        /// <inheritdoc />
        public void UploadFile(string fileName, Stream streamFile)
        {
            CheckUserKey();
            _containerClient.UploadBlob(string.Concat(_userRoute, fileName), streamFile);
        }

        /// <summary>
        /// Check if a blob exists
        /// </summary>
        /// <param name="fileName">Blob to check</param>
        public bool ExistsFile(string fileName) 
        {
            CheckUserKey();
            return _containerClient.GetBlobClient(string.Concat(_userRoute, fileName)).Exists();
        }

        /// <summary>
        /// Check if the user key is defined
        /// </summary>
        /// <exception cref="Exception">Exception if userkey is not defined</exception>
        public void CheckUserKey() 
        {
            if (string.IsNullOrEmpty(UserKey))
                throw new Exception("UserKey is not defined");
        }
    }
}
