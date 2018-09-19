using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Infrastructure;
using AppleUsed.BLL.Interfaces;
using AppleUsed.DAL.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.BLL.Services
{
    public class ServiceActiveTimeService : IServiceActiveTimeService
    {
        private readonly AppDbContext _db;

        public ServiceActiveTimeService(AppDbContext db)
        {
            _db = db;
        }

        public OperationDetails<IQueryable<ServiceActiveTimesDTO>> GetAllActiveServiceTimesByServiceId(int sericeId)
        {
            OperationDetails<IQueryable<ServiceActiveTimesDTO>> operationDetails =
             new OperationDetails<IQueryable<ServiceActiveTimesDTO>>(false, "", null);

            if (sericeId <= 0)
                return operationDetails;

            var serviceActiveTimes = _db.ServiceActiveTimes.Where(sa => sa.ServiceId == sericeId)
                .Select(x => new ServiceActiveTimesDTO
                {
                    ServiceActiveTimeId = x.ServiceActiveTimeId,
                    Cost = x.Cost,
                    DaysOfActiveService = x.DaysOfActiveService,
                    ServiceId = x.DaysOfActiveService
                });

            return new OperationDetails<IQueryable<ServiceActiveTimesDTO>>(false, "", serviceActiveTimes);
        }

        public async Task<OperationDetails<ServiceActiveTimesDTO>> GetServiceActiveTimesById(int serviceActiveId)
        {
            OperationDetails<ServiceActiveTimesDTO> operationDetails =
               new OperationDetails<ServiceActiveTimesDTO>(false, "", new ServiceActiveTimesDTO());

            if (serviceActiveId <= 0)
                return operationDetails;

            var serviceActiveResult = await _db.ServiceActiveTimes.FindAsync(serviceActiveId);
            ServiceActiveTimesDTO serviceActiveTime = new ServiceActiveTimesDTO
            {
                ServiceActiveTimeId = serviceActiveResult.ServiceActiveTimeId,
                Cost = serviceActiveResult.Cost,
                DaysOfActiveService = serviceActiveResult.DaysOfActiveService,
                ServiceId = serviceActiveResult.DaysOfActiveService
            };

            return new OperationDetails<ServiceActiveTimesDTO>(true, "", serviceActiveTime);
        }

        public async Task<OperationDetails<int>> CreateServiceActiveTime(ServiceDTO service)
        {
            OperationDetails<int> operationDetails =
                new OperationDetails<int>(false, "", 0);

            if (service.ServiceActiveTimes == null)
                return operationDetails;

            for (int i = 0; i <= service.ServiceActiveTimes.Count - 1; i++)
            {
                service.ServiceActiveTimes[i].ServiceId = service.ServicesId;
            }

            try
            {
                await _db.AddRangeAsync(service.ServiceActiveTimes);
                await _db.SaveChangesAsync();
                operationDetails = new OperationDetails<int>(true, "", 0);
            }
            catch (Exception ex)
            {
                operationDetails = new OperationDetails<int>(false, ex.Message, 0);
            }

            return operationDetails;
        }

        public async Task<OperationDetails<int>> UpdateServiceActiveTime(ServiceDTO service)
        {
            OperationDetails<int> operationDetails =
                new OperationDetails<int>(false, "", 0);

            if (service.ServiceActiveTimes == null)
                return operationDetails;

            var oldServiceActiveTimes = await _db.ServiceActiveTimes.Where(x => x.ServiceId == service.ServicesId).ToListAsync();

            for (int i = 0; i <= oldServiceActiveTimes.Count() - 1; i++)
            {
                service.ServiceActiveTimes[i].Cost = service.ServiceActiveTimes[i].Cost;
                service.ServiceActiveTimes[i].DaysOfActiveService = service.ServiceActiveTimes[i].DaysOfActiveService;
            }

            try
            {
                await _db.AddRangeAsync(service.ServiceActiveTimes);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                operationDetails = new OperationDetails<int>(false, ex.Message, 0);
            }

            return operationDetails;
        }

        public async Task<OperationDetails<int>> DeleteServiceActiveTime(int id)
        {
            OperationDetails<int> operationDetails =
               new OperationDetails<int>(false, "", 0);

            if (id <= 0)
                return operationDetails;

            var oldServiceActiveTime = await _db.ServiceActiveTimes.Where(x=>x.ServiceActiveTimeId == id).FirstOrDefaultAsync();
            if(oldServiceActiveTime == null)
                return operationDetails;

            try
            {
                _db.Remove(oldServiceActiveTime);
                await _db.SaveChangesAsync();
                operationDetails = new OperationDetails<int>(true, "", 0);
            }
            catch (Exception ex)
            {
                operationDetails = new OperationDetails<int>(false, ex.Message, 0);
            }

            return operationDetails;
        }
    }
}
