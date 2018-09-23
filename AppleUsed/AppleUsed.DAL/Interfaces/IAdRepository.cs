using AppleUsed.DAL.Entities;
using AppleUsed.DAL.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.DAL.Interfaces
{
    public interface IAdRepository : IDisposable
    {
        IQueryable<Ad> GetAdQuery(
            Expression<Func<Ad, bool>> adExpression,
            Expression<Func<ProductTypes, bool>> ptExpression,
            Expression<Func<ApplicationUser, bool>> auExpression,
            Expression<Func<Purchase, bool>> pExpression);

        Task<Ad> FindAdByIdAsync(int id);

        IQueryable<Ad> FindAdsByProductTypeId(int productTypeId);

        Task<IQueryable<Ad>> GetAdsByUserName(string userName);

        IQueryable<Ad> FindActiveAdsByUserId(string userId);

        IQueryable<Ad> FindAdsByUserId(string userId);

        Task<int> AddAd(Ad ad);

        Task<int> UpdateAd(Ad ad);


    }
}
