namespace Model
{
    /// <summary>
    /// This entity is the representation of
    /// stored files metadata
    /// </summary>
    public class FileMetadata
    {
        public Guid Id { get; set; }
        /// <summary>
        /// Physical name
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// Name in store
        /// </summary>
        public string FullNameStorage { get; set; }
        /// <summary>
        /// Content type (Mime)
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// Size en bytes
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// Upload date
        /// </summary>
        public DateTime CreationDate { get; set; }
    }
}
