using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppleUsed.DAL.Entities
{
    public class AdUp
    {
        public int AdUpId { get; set; }
        public int LimitUp { get; set; }
        public int CurrentRaisedUpCount { get; set; }
        public DateTime StartDateAction { get; set; }
        public DateTime EndDateAction { get; set; }
        public DateTime LastUp { get; set; }

        [ForeignKey("AdId")]
        public virtual Ad Ad { get; set; }
    }
}
