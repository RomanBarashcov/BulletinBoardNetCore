using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppleUsed.DAL.Entities
{
    public class AdViews
    {
        public int AdViewsId { get; set; }
        public int SumViews { get; set; }

        [ForeignKey("AdId")]
        public virtual Ad Ads { get; set; }
    }
}
