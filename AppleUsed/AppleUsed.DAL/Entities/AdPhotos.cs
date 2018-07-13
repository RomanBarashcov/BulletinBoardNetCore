using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppleUsed.DAL.Entities
{
    public class AdPhotos
    {
        public int AdPhotosId { get; set; }

        public string AdPhotoName { get; set; }
        public byte[] Photo { get; set; }

        [ForeignKey("AdId")]
        public virtual Ad Ad { get; set; }
    }
}
