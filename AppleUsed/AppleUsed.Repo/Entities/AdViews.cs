using System;
using System.Collections.Generic;
using System.Text;

namespace AppleUsed.DAL.Entities
{
    public class AdViews
    {
        public string AdViewsId { get; set; }
        public int SumViews { get; set; }

        public virtual Ad Ad { get; set; }
    }
}
