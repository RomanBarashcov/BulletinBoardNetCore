using System;
using System.Collections.Generic;
using System.Text;

namespace AppleUsed.DAL.Entities
{
    public class Characteristics
    {
        public string CharacteristicsId { get; set; }
        public ProductTypes ProductTypes { get; set; }
        public ProductModels ProductModels { get; set; }
        public ProductMemories ProductMemories { get; set; }
        public ProductColors ProductColors { get; set; }
        public ProductStates ProductStates { get; set; }

        public Ad Ad { get; set; }
    }
}
