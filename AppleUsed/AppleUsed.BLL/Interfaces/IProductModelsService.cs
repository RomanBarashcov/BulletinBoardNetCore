﻿using AppleUsed.BLL.Infrastructure;
using AppleUsed.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.BLL.Interfaces
{
    public interface IProductModelsService : IDisposable
    {
        IQueryable<ProductModels> GetProductModels();

        Task<ProductModels> GetProductModelById(int productModelId);

        IQueryable<ProductModels> GetProductModelsByProductTypeId(int productTypeId);
    }
}
