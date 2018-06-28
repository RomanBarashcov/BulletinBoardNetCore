using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AppleUsed.Data.Entities
{
    public class Series : BaseAuditClass
    {
        public string SeriesName { get; set; }
        
        public virtual ICollection<BookSeries> BookSeries { get; set; } = new Collection<BookSeries>();
    }
}