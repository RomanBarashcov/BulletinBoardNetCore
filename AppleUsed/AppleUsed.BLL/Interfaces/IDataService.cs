using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Enums;
using AppleUsed.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppleUsed.BLL.Interfaces
{
    public interface IDataTransformerService : IDisposable
    {
        Ad TransformingAdDTOToAdEntities(
                AdDTO ad, 
                Dictionary<SelectListProps, string> selectedValuesDictionary
            );

        AdDTO TransformingAdToAdDTO(Ad ad);
        IQueryable<AdDTO> TransformingAdQueryToAdDTO(IQueryable<Ad> adQuery);
    }
}
