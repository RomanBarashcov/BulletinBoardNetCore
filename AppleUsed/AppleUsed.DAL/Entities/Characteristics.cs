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

        [NotMapped]
        public ProductTypes ProductType { get; set; }

        public int ProductModelsId { get; set; }

        [NotMapped]
        public ProductModels ProductModel { get; set; }

        public int ProductMemoriesId { get; set; }

        [NotMapped]
        public ProductMemories ProductMemorie { get; set; }

        public int ProductColorsId { get; set; }

        [NotMapped]
        public ProductColors PorductColor { get; set; }

        public int ProductStatesId { get; set; }

        [NotMapped]
        public ProductStates ProductState { get; set; }

        [ForeignKey("AdId")]
        public int AdId { get; set; }
    }
}
