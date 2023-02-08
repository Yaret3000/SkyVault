using Application.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Application.Repositories
{
    /// <summary>
    /// Repository of <see cref="FileMetadata"/> model
    /// </summary>
    public class FileMetadataRepository : BaseRepository<FileMetadata>
    {
        public FileMetadataRepository(DbContext context) : base(context)
        {
        }
    }
}
