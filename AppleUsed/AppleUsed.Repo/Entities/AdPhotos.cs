using System;
using System.Collections.Generic;
using System.Text;

namespace AppleUsed.DAL.Entities
{
    public class AdPhotos
    {
        public string AdPhotosId { get; set; }
        public string AdPhotoName { get; set; }
        public byte[] Photo { get; set; }

        public string AdId { get; set; }
        public Ad Ad { get; set; }
    }
}
