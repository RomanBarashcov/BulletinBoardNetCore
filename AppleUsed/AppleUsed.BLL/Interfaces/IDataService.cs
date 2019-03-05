using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Enums;
using AppleUsed.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.BLL.Interfaces
{
    public interface IDataTransformerService : IDisposable
    {
        Ad TransformingAdDTOToAdEntities(
                AdDTO ad
            );

        AdDTO TransformingAdToAdDTO(Ad ad);
       List<AdDTO> TransformingAdListToAdDTOList(List<Ad> adList);
    }
}
