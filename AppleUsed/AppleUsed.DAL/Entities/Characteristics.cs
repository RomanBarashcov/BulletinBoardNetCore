using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppleUsed.DAL.Entities
{
    public class Characteristics
    {
        public int CharacteristicsId { get; set; }

        public int ProductTypesId { get; set; }
        public int ProductModelsId { get; set; }
        public int ProductMemoriesId { get; set; }
        public int ProductColorsId { get; set; }
        public int ProductStatesId { get; set; }

        [ForeignKey("AdId")]
        public int AdId { get; set; }
    }
}
