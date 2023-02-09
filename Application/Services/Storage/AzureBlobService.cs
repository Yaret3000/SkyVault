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
        #region PrivateVariables

        private BlobContainerClient _containerClient;
        private ClaimsPrincipal _claimsPrincipal;
        private string _userKey;
        private string _userRoute;

        private const string _claimKey = "emails";

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="claimsPrincipal">Current sesion claims</param>
        public AzureBlobService(ClaimsPrincipal claimsPrincipal)
        {
            _claimsPrincipal = claimsPrincipal;
            _userKey = claimsPrincipal.Claims.FirstOrDefault(claim => claim.Type == _claimKey)?.Value;
            _userRoute = string.Concat(_userKey, @"\");
            ConnectContainerClient();
        }

        /// <summary>
        /// Init Azure blob connection
        /// </summary>
        public void ConnectContainerClient() 
        {
            AzureBlobCredentialsDto appSettingDto = JsonHelper.ReadJsonFile<AzureBlobCredentialsDto>(FilesConstants.JsonConfig);
            _containerClient = new BlobContainerClient(appSettingDto.AzureBlobConf.ConnectionString, appSettingDto.AzureBlobConf.ContainerName);
        }

        /// <inheritdoc />
        public  void DeleteFile(string fileName)
        {
             _containerClient.DeleteBlob(string.Concat(_userRoute, fileName));
        }

        /// <inheritdoc />
        public Stream DownloadFile(string fileName)
        {
            Response<BlobDownloadResult> downloadResult = _containerClient.GetBlobClient(string.Concat(_userRoute, fileName)).DownloadContent();
            return downloadResult.Value.Content.ToStream();
        }

        /// <inheritdoc />
        public void UploadFile(string fileName, Stream streamFile)
        {
            _containerClient.UploadBlob(string.Concat(_userRoute, fileName), streamFile);
        }

        /// <summary>
        /// Check if a blob exists
        /// </summary>
        /// <param name="fileName">Blob to check</param>
        public bool ExistsFile(string fileName) 
        {
            return _containerClient.GetBlobClient(string.Concat(_userRoute, fileName)).Exists();
        }
    }
}
