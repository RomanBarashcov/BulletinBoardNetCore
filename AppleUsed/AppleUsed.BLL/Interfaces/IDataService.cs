using AppleUsed.BLL.DTO;
using AppleUsed.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppleUsed.BLL.Interfaces
{
    public interface IDataService
    {
        Ad TransformingAdDTOToAdEntities(AdDTO ad);
    }
}
