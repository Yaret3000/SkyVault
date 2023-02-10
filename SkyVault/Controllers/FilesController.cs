using Application.Repositories;
using Application.Services.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.EntityFrameworkCore;
using Model;
using Persistence;
using SkyVault.ViewModels;
using System.Diagnostics;
using System.Security.Claims;

namespace SkyVault.Controllers
{
    /// <summary>
    /// Controller to handle file requests
    /// </summary>
    [Authorize]
    public class FilesController : Controller
    {
        private DataContext _dataContext;
        private FileMetadataRepository _fileMetadataRepository;
        private AzureBlobService _azureBlobService;

        /// <summary>
        /// Unique user key (Can be: username, email, etc...)
        /// </summary>
        private string _userKey;

        public FilesController(DataContext dataContext, IHttpContextAccessor contextAccessor)
        {
            _userKey = contextAccessor.HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "emails").Value;
            _dataContext = dataContext;
            _fileMetadataRepository = new FileMetadataRepository(dataContext);
            _azureBlobService = new AzureBlobService(contextAccessor.HttpContext.User);
        }

        /// <summary>
        /// Return File list view
        /// </summary>
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                return View(_fileMetadataRepository.GetAll()
                .AsNoTracking()
                .Where(file => file.OwnerKey == _userKey));
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        /// <summary>
        /// Upload file to storage service
        /// </summary>
        /// <param name="file">Selected file</param>
        /// <param name="description">Custom description</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Upload(IFormFile file, string description)
        {
            try
            {
                Guid fileGuid = Guid.NewGuid();
                string fullNameStorage = string.Concat(fileGuid, file.FileName);
                FileMetadata fileMetadata = new FileMetadata
                {
                    Id = fileGuid,
                    FileName = file.FileName,
                    FullNameStorage = fullNameStorage,
                    Description = description,
                    ContentType = file.ContentType,
                    Size = file.Length,
                    CreationDate = DateTime.UtcNow,
                    OwnerKey = _userKey
                };

                _azureBlobService.UploadFile(fullNameStorage, file.OpenReadStream());
                _fileMetadataRepository.Add(fileMetadata);
                _fileMetadataRepository.Save();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        /// <summary>
        /// Download file by id
        /// </summary>
        /// <param name="id">File id</param>
        [HttpGet]
        public FileResult Download(Guid id)
        {
            try
            {
                FileMetadata fileMetadata = _fileMetadataRepository.FirstOrDefault(file => file.Id == id);
                if (fileMetadata == null)
                    RedirectToAction(nameof(Index));

                Stream downloadedFile = _azureBlobService.DownloadFile(fileMetadata.FullNameStorage);
                return File(downloadedFile, fileMetadata.ContentType, fileDownloadName: fileMetadata.FileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Delete file by id
        /// </summary>
        /// <param name="id"></param>
        public IActionResult Delete(Guid id)
        {
            try
            {
                FileMetadata fileMetadata = _fileMetadataRepository.FirstOrDefault(file => file.Id == id);
                _azureBlobService.DeleteFile(fileMetadata.FullNameStorage);
                _fileMetadataRepository.Delete(fileMetadata);
                _fileMetadataRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        /// <summary>
        /// Return error view,
        /// its used to handle exceptions
        /// </summary>
        /// <param name="ex">Exception</param>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(Exception ex)
        {
            return View(nameof(Error),new ErrorViewModel { RequestId = ex.Message });
        }
    }
}
