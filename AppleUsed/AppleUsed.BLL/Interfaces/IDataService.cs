using AppleUsed.BLL.DTO;
using AppleUsed.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppleUsed.BLL.Interfaces
{
    public interface IDataTransformerService
    {
        Ad TransformingAdDTOToAdEntities(AdDTO ad);
        AdDTO TransformingAdToAdDTO(Ad ad);
        IQueryable<AdDTO> TransformingAdQueryToAdDTO(IQueryable<Ad> adQuery);
    }
}
