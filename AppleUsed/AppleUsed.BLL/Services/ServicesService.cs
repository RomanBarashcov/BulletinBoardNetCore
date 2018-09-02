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
        private readonly IServiecActiveTimeService _serviecActiveTimeService;

        public ServicesService(AppDbContext db, IServiecActiveTimeService serviecActiveTimeService)
        {
            _db = db;
            _serviecActiveTimeService = serviecActiveTimeService;
        }

        public IQueryable<ServiceDTO> GetAllServices()
        {
            var services = (from s in _db.Services
                                join sa in _db.ServiceActiveTimes on s.ServicesId equals sa.ServiceId into result
                                select new ServiceDTO
                                {
                                    ServicesId = s.ServicesId,
                                    Name = s.Name,
                                    Description = s.Description,
                                    ServiceActiveTimes = result.ToList()
                                }); 

            return services;
        }

        public async Task<OperationDetails<ServiceDTO>> GetServiceById(int id)
        {
            OperationDetails<ServiceDTO> operationDetails = 
                new OperationDetails<ServiceDTO>(false, "", new ServiceDTO());

            if (id <= 0)
                return operationDetails;

            var service = await (from s in _db.Services.Where(x => x.ServicesId == id)
                            join sa in _db.ServiceActiveTimes on s.ServicesId equals sa.ServiceId into result
                            select new ServiceDTO
                            {
                                ServicesId = s.ServicesId,
                                Name = s.Name,
                                Description = s.Description,
                                ServiceActiveTimes = result.ToList()
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
                Description = service.Description
            };

            try
            {
                await _db.AddAsync(newServices);
                await _db.SaveChangesAsync();
                operationDetails = new OperationDetails<int>(true, "", newServices.ServicesId);
                service.ServicesId = newServices.ServicesId;
            }
            catch(Exception ex)
            {
                operationDetails = new OperationDetails<int>(false, ex.Message, 0);
            }

            var createServiceActiveTimeResult = await _serviecActiveTimeService.CreateServiceActiveTime(service);
            if (!createServiceActiveTimeResult.Succedeed)
                return createServiceActiveTimeResult;

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

            var updateServiceActiveTimeResult = await _serviecActiveTimeService.UpdateServiceActiveTime(service);
            if (!updateServiceActiveTimeResult.Succedeed)
                return updateServiceActiveTimeResult;

            oldServices.Name = service.Name;
            oldServices.Description = service.Description;
          
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
                await _db.SaveChangesAsync();
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
