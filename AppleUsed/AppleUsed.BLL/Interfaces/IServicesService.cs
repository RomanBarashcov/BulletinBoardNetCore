using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace AppleUsed.BLL.Interfaces
{
    public interface IServicesService
    {
        IQueryable<ServicesDTO> GetAllServices();
        Task<OperationDetails<ServicesDTO>> GetServiceById(int id);
        Task<OperationDetails<int>> CreateService(ServicesDTO service);
        Task<OperationDetails<int>> UpdateService(ServicesDTO service);
        Task<OperationDetails<int>> DeleteService(int id);
    }
}
