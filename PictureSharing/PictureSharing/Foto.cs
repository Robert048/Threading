using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureSharing
{
    class Foto
    {
        public long fotoID { get; set; }
        public string fotoNaam { get; set; }
        public long gebruikersID { get; set; }
        public string path { get; set; }
    }
}
