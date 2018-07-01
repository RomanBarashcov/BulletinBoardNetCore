using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppleUsed.DAL.Entities
{
    public class Characteristics
    {
        public int CharacteristicsId { get; set; }

        [ForeignKey("ProductTypesId")]
        public virtual ProductTypes ProductTypes { get; set; }

        [ForeignKey("ProductModelsId")]
        public virtual ProductModels ProductModels { get; set; }

        [ForeignKey("ProductMemoriesId")]
        public virtual ProductMemories ProductMemories { get; set; }

        [ForeignKey("ProductColorsId")]
        public virtual ProductColors ProductColors { get; set; }

        [ForeignKey("ProductStatesId")]
        public virtual ProductStates ProductStates { get; set; }

        [ForeignKey("AdId")]
        public virtual Ad Ads { get; set; }
    }
}
