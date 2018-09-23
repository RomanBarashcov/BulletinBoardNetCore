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
        public string PhotoHashSmall { get; set; }
        public string PhotoHashAvg { get; set; }
        public string PhotoHashBig { get; set; }

        [ForeignKey("AdId")]
        public int AdId { get; set; }
    }
}
