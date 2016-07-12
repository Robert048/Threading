namespace PictureSharing
{
    /// <summary>
    /// Model for the foto
    /// </summary>
    class LocalPhoto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long UserId { get; set; }
        public string Path { get; set; }
        public byte[] ImageData { get; set; }
    }
}
