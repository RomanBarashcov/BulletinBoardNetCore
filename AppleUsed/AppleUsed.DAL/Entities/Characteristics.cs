using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppleUsed.DAL.Entities
{
    public class Characteristics
    {
        public int CharacteristicsId { get; set; }

        public ProductTypes ProductType { get; set; }

        public ProductModels ProductModel { get; set; }

        public ProductMemories ProductMemorie { get; set; }

        public ProductColors ProductColor { get; set; }

        public ProductStates ProductState { get; set; }

        [ForeignKey("AdId")]
        public int AdId { get; set; }
    }
}
