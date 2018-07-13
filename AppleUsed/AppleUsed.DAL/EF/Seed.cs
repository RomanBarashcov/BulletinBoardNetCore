using AppleUsed.DAL.Entities;
using AppleUsed.DAL.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static AppleUsed.DAL.EF.SeedData;

namespace AppleUsed.DAL.EF
{
    public class Seed
    {
        private AppDbContext appDbContext;

        public Seed(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;

            ProductTypeInit();
            ProductColorsInit();
            ProductStatesInit();
            ProductMemoriesInit();

            if (!appDbContext.ProductModels.Any())
            {
                ProductIphoneModelsInit();
                ProductMacModelsInit();
                ProductIpadModelsInit();
                ProductIpodModelsInit();
                ProductAppleWathcModelsInit();
                ProductAppleTVModelsInit();
            }
        }

        private void ProductTypeInit()
        {
            if (!appDbContext.ProductTypes.Any())
            {
                string[] productArr = new string[] {
                    ProductType.iPhone,
                    ProductType.iPad,
                    ProductType.iMacAndMacbook,
                    ProductType.iPod,
                    ProductType.AppleWatch,
                    ProductType.AppleTV,
                    ProductType.Accessories,
                    ProductType.Other
                };

                List<ProductTypes> productTypes = new List<ProductTypes>();

                for (int i = 0; i <= productArr.Length - 1; i++)
                {
                    productTypes.Add(new ProductTypes { Name = productArr[i] });
                }

                appDbContext.AddRange(productTypes);
                appDbContext.SaveChanges();
            }
        }


        private void ProductColorsInit()
        {
            if (!appDbContext.ProductColors.Any())
            {
                string[] colorArr = new string[] {
                    Colors.SpaceGrey,
                    Colors.Silver,
                    Colors.Gold,
                    Colors.RoseGold,
                    Colors.White,
                    Colors.Black,
                    Colors.JetBlack
                };

                List<ProductColors> productColors = new List<ProductColors>();

                for (int i = 0; i <= colorArr.Length - 1; i++)
                {
                    productColors.Add(new ProductColors { Name = colorArr[i] });
                }

                appDbContext.AddRange(productColors);
                appDbContext.SaveChanges();
            }
        }

        private void ProductStatesInit()
        {
            if (!appDbContext.ProductStates.Any())
            {
                string[] stateArr = new string[] {
                    States.New,
                    States.LikeNew,
                    States.SecondHand,
                    States.ForParts
            };

                List<ProductStates> productStates = new List<ProductStates>();

                for (int i = 0; i <= stateArr.Length - 1; i++)
                {
                    productStates.Add(new ProductStates { Name = stateArr[i] });
                }

                appDbContext.AddRange(productStates);
                appDbContext.SaveChanges();
            }
        }

        private void ProductMemoriesInit()
        {
            if (!appDbContext.ProductMemories.Any())
            {
                int[] memoryArr = new int[] {
                    Memmories._4GB,
                    Memmories._8GB,
                    Memmories._16GB,
                    Memmories._32GB,
                    Memmories._64GB,
                    Memmories._128GB,
                    Memmories._256GB,
                    Memmories._512GB,
                    Memmories._1TB,
                    Memmories._2TB
                };

                List<ProductMemories> productMemories = new List<ProductMemories>();

                for (int i = 0; i <= memoryArr.Length - 1; i++)
                {
                    productMemories.Add( new ProductMemories { Name = memoryArr[i].ToString() });
                }

                appDbContext.AddRange(productMemories);
                appDbContext.SaveChanges();
            }
        }

        private void ProductIphoneModelsInit()
        {
                string[] iPhonesArr = new string[] {
                    iPhones.iPhone2G,
                    iPhones.iPhone3G,
                    iPhones.iPhone3GS,
                    iPhones.iPhone4,
                    iPhones.iPhone4S,
                    iPhones.iPhone5,
                    iPhones.iPhone5C,
                    iPhones.iPhone5S,
                    iPhones.iPhoneSE,
                    iPhones.iPhone6,
                    iPhones.iPhone6Plus,
                    iPhones.iPhone6S,
                    iPhones.iPhone6SPlus,
                    iPhones.iPhone7,
                    iPhones.iPhone7Plus,
                    iPhones.iPhone8,
                    iPhones.iPhone8Plus,
                    iPhones.iPhoneX
                };

                ProductTypes iPhoneProductTypeModel = appDbContext.ProductTypes.Where(x => x.Name == ProductType.iPhone).FirstOrDefault();
                List<ProductModels> productModels = new List<ProductModels>();

                for (int i = 0; i <= iPhonesArr.Length - 1; i++)
                {
                    productModels.Add(new ProductModels { Name = iPhonesArr[i], ProductTypes = iPhoneProductTypeModel });
                }

                appDbContext.AddRange(productModels);
                appDbContext.SaveChanges();
        }

        public void ProductMacModelsInit()
        {
            string[] MacsArr = new string[] {
                Macs.MacBook,
                Macs.MacBookPro,
                Macs.MacBookAir,
                Macs.iMacMini,
                Macs.iMac,
                Macs.MacPro
            };

            ProductTypes MacProductTypeModel = appDbContext.ProductTypes.Where(x => x.Name == ProductType.iMacAndMacbook).FirstOrDefault();
            List<ProductModels> productModels = new List<ProductModels>();

            for (int i = 0; i <= MacsArr.Length - 1; i++)
            {
                productModels.Add(
                    new ProductModels
                    {
                        Name = MacsArr[i],
                        ProductTypes = MacProductTypeModel
                    });
            }

            appDbContext.AddRange(productModels);
            appDbContext.SaveChanges();
        }

        public void ProductIpadModelsInit()
        {
            string[] iPadArr = new string[] {
                iPads.iPad,
                iPads.iPad2,
                iPads.iPad3,
                iPads.iPad4,
                iPads.iPadAir,
                iPads.iPadAir2,
                iPads.iPadMini,
                iPads.iPadMini2,
                iPads.iPadMini3,
                iPads.iPadMini4,
                iPads.iPadPro10_5,
                iPads.iPadPro9_7,
                iPads.iPad2017,
                iPads.iPad2018
            };

            ProductTypes iPadProductTypeModel = appDbContext.ProductTypes.Where(x => x.Name == ProductType.iPad).FirstOrDefault();
            List<ProductModels> productModels = new List<ProductModels>();

            for (int i = 0; i <= iPadArr.Length - 1; i++)
            {
                productModels.Add(
                    new ProductModels
                    {
                        Name = iPadArr[i],
                        ProductTypes = iPadProductTypeModel
                    });
            }

            appDbContext.AddRange(productModels);
            appDbContext.SaveChanges();
        }

        public void ProductIpodModelsInit()
        {
            string[] iPodArr = new string[] {
                iPods.iPodShuffle,
                iPods.iPodNano,
                iPods.iPodClassic,
                iPods.iPodTouch
            };

            ProductTypes iPodProductTypeModel = appDbContext.ProductTypes.Where(x => x.Name == ProductType.iPod).FirstOrDefault();
            List<ProductModels> productModels = new List<ProductModels>();

            for (int i = 0; i <= iPodArr.Length - 1; i++)
            {
                productModels.Add(
                    new ProductModels
                    {
                        Name = iPodArr[i],
                        ProductTypes = iPodProductTypeModel
                    });
            }

            appDbContext.AddRange(productModels);
            appDbContext.SaveChanges();
        }

        public void ProductAppleWathcModelsInit()
        {
            string[] appleWathcArr = new string[] {
                AppleWatches.AppleWatch,
                AppleWatches.AppleWatchEdition,
                AppleWatches.AppleWatchSport,
                AppleWatches.Series1,
                AppleWatches.Series1Sport,
                AppleWatches.Series2,
                AppleWatches.Series2Sport,
                AppleWatches.Series3,
                AppleWatches.Series3Sport
            };

            ProductTypes appleWathcProductTypeModel = appDbContext.ProductTypes.Where(x => x.Name == ProductType.AppleWatch).FirstOrDefault();
            List<ProductModels> productModels = new List<ProductModels>();

            for (int i = 0; i <= appleWathcArr.Length - 1; i++)
            {
                productModels.Add(
                    new ProductModels
                    {
                        Name = appleWathcArr[i],
                        ProductTypes = appleWathcProductTypeModel
                    });

            }

            appDbContext.AddRange(productModels);
            appDbContext.SaveChanges();
        }

        public void ProductAppleTVModelsInit()
        {
            string[] appleTVArr = new string[] {
                AppleTVs.AppleTV,
                AppleTVs.AppleTV2,
                AppleTVs.AppleTV3,
                AppleTVs.AppleTV4,
                AppleTVs.AppleTV4K
            };

            ProductTypes appleTVProductTypeModel = appDbContext.ProductTypes.Where(x => x.Name == ProductType.AppleTV).FirstOrDefault();
            List<ProductModels> productModels = new List<ProductModels>();

            for (int i = 0; i <= appleTVArr.Length - 1; i++)
            {
                productModels.Add(
                    new ProductModels
                    {
                        Name = appleTVArr[i],
                        ProductTypes = appleTVProductTypeModel
                    });
            }

            appDbContext.AddRange(productModels);
            appDbContext.SaveChanges();
        }
    }
}
