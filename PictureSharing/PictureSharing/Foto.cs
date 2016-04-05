using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureSharing
{
    class Foto
    {
        private long fotoID;
        private string fotoNaam;
        private long gebruikersID;
        private string path;

        public Foto(long fotoID, string fotoNaam, long gebruikersID, string path)
        {
            this.fotoID = fotoID;
            this.fotoNaam = fotoNaam;
            this.gebruikersID = gebruikersID;
            this.path = path;
        }
    }
}
