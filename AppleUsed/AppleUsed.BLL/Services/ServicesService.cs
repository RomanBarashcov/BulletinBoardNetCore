using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Infrastructure;
using AppleUsed.BLL.Interfaces;
using AppleUsed.DAL.Identity;
using AppleUsed.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AppleUsed.BLL.Services
{
    public class ServicesService : IServicesService
    {
        private IUnityOfWork _uof;

        public ServicesService(IUnityOfWork uof)
        {
            _uof = uof;
        }

        public IQueryable<ServiceDTO> GetAllServices()
        {
            var services = _uof.ServiceRepository.GetAllServices().Select(s => new ServiceDTO
            {
                ServicesId = s.ServicesId,
                Name = s.Name,
                Description = s.Description,
                ServiceActiveTimes = s.ServiceActiveTimes
            });

            return services;
        }

        public async Task<OperationDetails<ServiceDTO>> GetServiceById(int id)
        {
            OperationDetails<ServiceDTO> operationDetails = 
                new OperationDetails<ServiceDTO>(false, "", new ServiceDTO());

            if (id <= 0)
                return operationDetails;

            var service = await _uof.ServiceRepository.FindServiceByIdAsync(id);
            var serviceDTO = new ServiceDTO
                            {
                                ServicesId = service.ServicesId,
                                Name = service.Name,
                                Description = service.Description,
                                ServiceActiveTimes = service.ServiceActiveTimes
                             };

            operationDetails = new OperationDetails<ServiceDTO>(true, "", serviceDTO);

            return operationDetails;
        }

       
        public async Task<OperationDetails<int>> CreateService(ServiceDTO service)
        {
            OperationDetails<int> operationDetails =
                new OperationDetails<int>(false, "", 0);

            if (service == null)
                return operationDetails;

            DAL.Entities.Services newServices = new DAL.Entities.Services
            {
                Name = service.Name,
                Description = service.Description
            };

            try
            {
                service.ServicesId = await _uof.ServiceRepository.AddServiceAsync(newServices);
                newServices.ServiceActiveTimes = service.ServiceActiveTimes;
                await _uof.ServiceActiveTimeRepository.AddServiceActiveTimeRange(newServices.ServiceActiveTimes);

                operationDetails = new OperationDetails<int>(true, "", newServices.ServicesId);
            }
            catch(Exception ex)
            {
                operationDetails = new OperationDetails<int>(false, ex.Message, 0);
            }

            return operationDetails;
        }

        public async Task<OperationDetails<int>> UpdateService(ServiceDTO service)
        {
            OperationDetails<int> operationDetails =
                new OperationDetails<int>(false, "", 0);

            if (service == null)
                return operationDetails;

            var oldServices = await _uof.ServiceRepository.FindServiceByIdAsync(service.ServicesId);
            if (oldServices == null)
                return operationDetails;

         
            oldServices.Name = service.Name;
            oldServices.Description = service.Description;
          
            try
            {
                oldServices.ServicesId = await _uof.ServiceRepository.UpdateService(oldServices);
                await _uof.ServiceActiveTimeRepository.UpdateServiceActiveTimeRange(service.ServiceActiveTimes);
                operationDetails = new OperationDetails<int>(true, "", oldServices.ServicesId);
            }
            catch (Exception ex)
            {
                operationDetails = new OperationDetails<int>(false, ex.Message, 0);
            }

            return operationDetails;
        }

        public async Task<OperationDetails<int>> DeleteService(int id)
        {
            OperationDetails<int> operationDetails =
                new OperationDetails<int>(false, "", 0);

            var oldServices = await _uof.ServiceRepository.FindServiceByIdAsync(id);
            if (oldServices == null)
                return operationDetails;

            try
            {
                await _uof.ServiceRepository.DeleteService(id);
                operationDetails = new OperationDetails<int>(true, "", 0);
            }
            catch(Exception ex)
            {
                operationDetails = new OperationDetails<int>(false, ex.Message, 0);
            }

            return operationDetails;
        }
    }
}
