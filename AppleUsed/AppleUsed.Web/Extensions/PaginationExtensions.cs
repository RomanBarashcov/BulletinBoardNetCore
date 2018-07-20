using AppleUsed.BLL.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppleUsed.Web.Extensions
{
    public class PaginationExtensions<T>
    {
        public virtual async Task<List<AdDTO>> PaginationAsync(IQueryable<AdDTO> query , int pageSize, int pageNumber, string fieldName, string sort)
        {
            try
            {
                if (sort == "DESC")
                {
                    return await query.OrderByDescending(x => EF.Property<object>(x, fieldName)).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToListAsync();
                }
                else
                {
                    return await query.OrderBy((x) => EF.Property<object>(x, fieldName)).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToListAsync();
                }
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }
    }
}
