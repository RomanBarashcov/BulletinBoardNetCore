using AppleUsed.DAL.Entities;
using AppleUsed.DAL.Identity;
using AppleUsed.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.DAL.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private AppDbContext _db;

        public ServiceRepository(AppDbContext db)
        {
            _db = db;
        }

        private IQueryable<Services> ServiceQuery(Expression<Func<Services, bool>> whereExpression)
        {
            var servicesQuery = whereExpression == null ? _db.Services : _db.Services.Where(whereExpression);
            var services = (from s in servicesQuery
                            join sa in _db.ServiceActiveTimes on s.ServicesId equals sa.ServiceId into result
                            select new Services
                            {
                                ServicesId = s.ServicesId,
                                Name = s.Name,
                                Description = s.Description,
                                ServiceActiveTimes = result.ToList()
                            });

            return services;
        }

        public IQueryable<Services> GetAllServices()
        {
            var services = ServiceQuery(null);
            return services;
        }

        public async Task<Services> FindServiceByIdAsync(int serviceId)
        {
            var service = await ServiceQuery(x => x.ServicesId == serviceId).FirstOrDefaultAsync();
            return service;
        }

        public async Task<int> AddServiceAsync(Services services)
        {
            await _db.AddAsync(services);
            await _db.SaveChangesAsync();
            return services.ServicesId;
        }

        public async Task<int> UpdateService(Services services)
        {
            _db.Services.Update(services);
            await _db.SaveChangesAsync();
            return services.ServicesId;
        }

        public async Task DeleteService(int id)
        {
            var oldService = await FindServiceByIdAsync(id);
            _db.Services.Remove(oldService);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Purchase> GetAllUserPurchaseByAds(IQueryable<Ad> adQuery)
        {
            var purchases = (from ads in adQuery
                             join p in _db.Purchases on ads.AdId equals p.AdId
                             select new Purchase
                             {
                                 PurchaseId = p.PurchaseId,
                                 TotalCost = p.TotalCost,
                                 DateOfPayment = p.DateOfPayment,
                                 StartDateService = p.StartDateService,
                                 EndDateService = p.EndDateService,
                                 IsPayed = p.IsPayed,
                                 ServicesId = p.ServicesId,
                                 ServiceActiveTimeId = p.ServiceActiveTimeId,
                                 AdId = p.AdId
                             });

            return purchases;
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db = null;
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
