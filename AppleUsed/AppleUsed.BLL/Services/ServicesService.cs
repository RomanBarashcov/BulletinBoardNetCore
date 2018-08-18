using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Infrastructure;
using AppleUsed.BLL.Interfaces;
using AppleUsed.DAL.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AppleUsed.BLL.Services
{
    public class ServicesService : IServicesService
    {
        private readonly AppDbContext _db;

        public ServicesService(AppDbContext db)
        {
            _db = db;
        }

        public IQueryable<ServiceDTO> GetAllServices()
        {
            var services = _db.Services.Select(x=> 
            new ServiceDTO
            {
                ServicesId = x.ServicesId,
                Name = x.Name,
                Description = x.Description,
                Cost = x.Cost,
                DaysOfActiveService = x.DaysOfActiveService
            });

            return services;
        }

        public async Task<OperationDetails<ServiceDTO>> GetServiceById(int id)
        {
            OperationDetails<ServiceDTO> operationDetails = 
                new OperationDetails<ServiceDTO>(false, "", new ServiceDTO());

            if (id <= 0)
                return operationDetails;

            var service = await _db.Services.Where(s=>s.ServicesId == id).Select(x =>
            new ServiceDTO
            {
                ServicesId = x.ServicesId,
                Name = x.Name,
                Description = x.Description,
                Cost = x.Cost,
                DaysOfActiveService = x.DaysOfActiveService

            }).FirstOrDefaultAsync();

            operationDetails = new OperationDetails<ServiceDTO>(true, "", service);

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
                Description = service.Description,
                Cost = service.Cost,
                DaysOfActiveService = service.DaysOfActiveService
            };

            try
            {
                await _db.AddAsync(newServices);
                await _db.SaveChangesAsync();
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

            var oldServices = await _db.Services.FindAsync(service.ServicesId);
            if (oldServices == null)
                return operationDetails;

            oldServices.Name = service.Name;
            oldServices.Description = service.Description;
            oldServices.Cost = service.Cost;
            oldServices.DaysOfActiveService = service.DaysOfActiveService;
          
            try
            {
                _db.Update(oldServices);
                await _db.SaveChangesAsync();
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

            var oldServices = await _db.Services.FindAsync(id);
            if (oldServices == null)
                return operationDetails;

            try
            {
                _db.Remove(oldServices);
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
