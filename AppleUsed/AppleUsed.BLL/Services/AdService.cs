using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Infrastructure;
using AppleUsed.BLL.Interfaces;
using AppleUsed.DAL.Entities;
using AppleUsed.DAL.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.BLL.Services
{
    public class AdService : IAdService
    {
        private AppDbContext _db;
        private IDataService _dataService;

        public AdService(AppDbContext context, IDataService dataService)
        {
            _db = context;
            _dataService = dataService;
        }

        public OperationDetails<List<AdDTO>> GetAds()
        {
            ProcedureService procedureService = new ProcedureService();
            var result = procedureService.GetResultFromStoredProcedure("dbo.GetAllAds");
            var res = new OperationDetails<List<AdDTO>>(true, "", new List<AdDTO>());
            return res;
        }

        public Task<OperationDetails<int>> AddImageToAd(int id, string imageName, byte[] imageData)
        {
            throw new NotImplementedException();
        }

        public async Task<AdDTO> GetDataForCreatingAd()
        {

            AdDTO adDto = new AdDTO();
            adDto.CityAreasList = await _db.CityAreas.ToListAsync();
            adDto.ProductTypesList = await (from t in _db.ProductTypes
                                         select new ProductTypes
                                         {
                                             ProductTypesId = t.ProductTypesId,
                                             Name = t.Name

                                         }).ToListAsync();

            adDto.ProductModelsList = await _db.ProductModels.ToListAsync();
            adDto.ProductMemoriesList = await _db.ProductMemories.ToListAsync();
            adDto.ProductColorsList = await _db.ProductColors.ToListAsync();
            adDto.ProductStatesList = await _db.ProductStates.ToListAsync();

            return adDto;
        }

        public async Task<OperationDetails<int>> CreateAdAsync(string userId, AdDTO ad)
        {
            OperationDetails<int> operationDetails = new OperationDetails<int>(false, "", 0);
            ApplicationUser user = new ApplicationUser();

            if(ad == null)
                return new OperationDetails<int>(false, "new Ad can't be null or empty", 0);

            if (!String.IsNullOrEmpty(userId))
            {
                user = await _db.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();

                if (user != null)
                {
                    var newAd = _dataService.TransformingAdDTOToAdEntities(ad);
                    newAd.ApplicationUser = user;

                    try
                    {
                        var addResult = await _db.Ads.AddAsync(newAd);
                    }
                    catch (Exception ex)
                    {
                        operationDetails = new OperationDetails<int>(false, ex.Message.FirstOrDefault().ToString(), 0);
                    }

                    try
                    {
                        var saveChangesResult = await _db.SaveChangesAsync();
                        operationDetails = new OperationDetails<int>(true, "", newAd.AdId);
                    }
                    catch (Exception ex)
                    {
                        operationDetails = new OperationDetails<int>(false, ex.Message.FirstOrDefault().ToString(), 0);
                    }
                }
            }

            return operationDetails;
        }

        public Task<OperationDetails<int>> DeleteAd(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationDetails<Ad>> GetAdById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationDetails<int>> UpdateAd(int id, Ad ad)
        {
            throw new NotImplementedException();
        }

    }
}
