using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace AppleUsed.BLL.Interfaces
{
    public interface IServicesService
    {
        IQueryable<ServiceDTO> GetAllServices();
        Task<OperationDetails<ServiceDTO>> GetServiceById(int id);
        Task<OperationDetails<int>> CreateService(ServiceDTO service);
        Task<OperationDetails<int>> UpdateService(ServiceDTO service);
        Task<OperationDetails<int>> DeleteService(int id);
    }
}
