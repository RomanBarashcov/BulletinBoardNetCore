using AppleUsed.BLL.Infrastructure;
using AppleUsed.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.BLL.Interfaces
{
    public interface IServicesService
    {
        Task<OperationDetails<List<DAL.Entities.Services>>> GetAllServices();
        Task<OperationDetails<DAL.Entities.Services>> GetServiceById(int id);
        Task<OperationDetails<int>> CreateService(DAL.Entities.Services service);
        Task<OperationDetails<int>> UpdateService(int id, DAL.Entities.Services service);
        Task<OperationDetails<int>> DeleteService(int id);
    }
}
