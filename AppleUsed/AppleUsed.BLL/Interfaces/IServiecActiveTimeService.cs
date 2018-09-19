using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.BLL.Interfaces
{
    public interface IServiceActiveTimeService
    {

        OperationDetails<IQueryable<ServiceActiveTimesDTO>> GetAllActiveServiceTimesByServiceId(int sericeId);

        Task<OperationDetails<ServiceActiveTimesDTO>> GetServiceActiveTimesById(int serviceActiveId);

        Task<OperationDetails<int>> CreateServiceActiveTime(ServiceDTO service);

        Task<OperationDetails<int>> UpdateServiceActiveTime(ServiceDTO service);

        Task<OperationDetails<int>> DeleteServiceActiveTime(int id);
    }
}
