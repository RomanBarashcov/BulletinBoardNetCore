using System;
using System.Collections.Generic;
using System.Text;

namespace AppleUsed.DAL.Entities
{
    public class Characteristics
    {
        public string CharacteristicsId { get; set; }
        public virtual ProductTypes ProductTypes { get; set; }
        public virtual ProductModels ProductModels { get; set; }
        public virtual ProductMemories ProductMemories { get; set; }
        public virtual ProductColors ProductColors { get; set; }
        public virtual ProductStates ProductStates { get; set; }
        public virtual Ad Ad { get; set; }
    }
}
