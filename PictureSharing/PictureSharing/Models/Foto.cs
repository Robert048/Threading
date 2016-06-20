namespace PictureSharing
{
    /// <summary>
    /// Model for the foto
    /// </summary>
    class Foto
    {
        public long fotoID { get; set; }
        public string fotoNaam { get; set; }
        public long gebruikersID { get; set; }
        public string path { get; set; }
    }
}
