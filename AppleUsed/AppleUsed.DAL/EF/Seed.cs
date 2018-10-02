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

            InitAdStatuses();
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

            if (!appDbContext.CityAreas.Any())
            {
                AddCityAreas();
                AddCitiesToVinitsyaArea();
                AddCitiesToVolinskayaArea();
                AddCitiesToDnepropetrovskayaArea();
                AddCitiesToDonetskayaArea();
                AddCitiesToZhitomirskayaArea();
                AddCitiesToZakarpatskayaArea();
                AddCitiesToZaporozhskayaArea();
                AddCitiesToIvanoFrankovskayaArea();
                AddCitiesToKievskayaArea();
                AddCitiesToKirovogradskayaArea();
                AddCitiesToLuganskayaArea();
                AddCitiesToNikolaevskayaArea();
                AddCitiesToOdesskayaArea();
                AddCitiesToPoltavskayaArea();
                AddCitiesToRovnenskayaArea();
                AddCitiesToSumyskayaArea();
                AddCitiesToTernopolskayaArea();
                AddCitiesToKharkovskayaArea();
                AddCitiesToKhersonskayaArea();
                AddCitiesToKhmelnitskayaArea();
                AddCitiesToCherkasskayaArea();
                AddCitiesToChernigovskayaArea();
                AddCitiesToChernovetckayaArea();
            }

            if (!appDbContext.Services.Any())
            {
                InitServices();
            }

    }

        private void InitAdStatuses()
        {
            if (!appDbContext.AdStatuses.Any())
            {
                string[] adStatuses = new string[] {
                    SeedData.AdStatus.Active,
                    SeedData.AdStatus.InProgress,
                    SeedData.AdStatus.Deactivated
                };

                List<Entities.AdStatus> adStatusesList = new List<Entities.AdStatus>();

                for (int i = 0; i <= adStatuses.Length - 1; i++)
                {
                    adStatusesList.Add(new Entities.AdStatus { Name = adStatuses[i] });
                }

                appDbContext.AddRange(adStatusesList);
                appDbContext.SaveChanges();
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
                    productMemories.Add(new ProductMemories { StorageSize = memoryArr[i].ToString() });
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

        public void AddCityAreas()
        {
            string[] cityAreas = new string[]
            {
                CityAreas.Vineckaya,
                CityAreas.Volinskaya,
                CityAreas.Dnepropetrovskaya,
                CityAreas.Donetsk,
                CityAreas.Zhitomirskaya,
                CityAreas.Zakarpatskaya,
                CityAreas.Zaporozhskaya,
                CityAreas.IvanoFrankovskaya,
                CityAreas.Kievskaya,
                CityAreas.Kirovogradskays,
                CityAreas.Luganskaya,
                CityAreas.Lvovskaya,
                CityAreas.Nikolaevskaya,
                CityAreas.Odesskaya,
                CityAreas.Poltavskaya,
                CityAreas.Rovnenskaya,
                CityAreas.Sumskaya,
                CityAreas.Ternopolskaya,
                CityAreas. Kharkovskaya,
                CityAreas.Khersonskaya,
                CityAreas.Khmelnickaya,
                CityAreas.Cherkaskaya,
                CityAreas.Chernigovskaya,
                CityAreas.Chernoveckaya
             };

            List<CityArea> cityAreasList = new List<CityArea>();

            for (int i = 0; i <= cityAreas.Length - 1; i++)
            {
                cityAreasList.Add(
                new CityArea
                {
                    Name = cityAreas[i]
                });
            }

            appDbContext.AddRange(cityAreasList);
            appDbContext.SaveChanges();
        }

        public void AddCitiesToVinitsyaArea()
        {
            string[] cities = new string[]
            {
                VineckayaCities.Bar,
                VineckayaCities.Berdash ,
                VineckayaCities.Vinniciya,
                VineckayaCities.Gaisin,
                VineckayaCities.Gnivan ,
                VineckayaCities.Gmerinka ,
                VineckayaCities.Ilyicin ,
                VineckayaCities. Kazatin,
                VineckayaCities.Kalinovka,
                VineckayaCities.Krijopol,
                VineckayaCities.Ladijin ,
                VineckayaCities.Lipovec ,
                VineckayaCities.MaghilevoPodolsk,
                VineckayaCities.Nemirov,
                VineckayaCities.Pesochin ,
                VineckayaCities.Pogrebishe,
                VineckayaCities.Strijovka,
                VineckayaCities.Tulchin ,
                VineckayaCities.Khmelnik ,
                VineckayaCities.Chechelnik,
                VineckayaCities.Shargorod,
                VineckayaCities.Yampol
            };

            CityArea cityArea = appDbContext.CityAreas.Where(x => x.Name == CityAreas.Vineckaya).FirstOrDefault();
            List<City> citiesList = new List<City>();

            for (int i = 0; i <= cities.Length - 1; i++)
            {
                citiesList.Add(
                new City
                {
                    Name = cities[i],
                    CityArea = cityArea
                });
            }

            appDbContext.AddRange(citiesList);
            appDbContext.SaveChanges();
        }

        public void AddCitiesToVolinskayaArea()
        {
            string[] cities = new string[]
            {
                 VolinskayaCities.Berestechko,
                 VolinskayaCities.VladimirVolinsky,
                 VolinskayaCities.Gorohov,
                 VolinskayaCities.Ivanovich,
                 VolinskayaCities.KamenKashirskiy,
                 VolinskayaCities.Kiverciy,
                 VolinskayaCities.Kovel,
                 VolinskayaCities.Luck,
                 VolinskayaCities.Lubeshov,
                 VolinskayaCities.Lubomol,
                 VolinskayaCities.Mavich,
                 VolinskayaCities.Novovolinsk,
                 VolinskayaCities.Ratno,
                 VolinskayaCities.Rojishe,
                 VolinskayaCities. StarayaVijevka,
                 VolinskayaCities.Turiysk,
                 VolinskayaCities.Ystylyg,
                 VolinskayaCities.Cuman,
                 VolinskayaCities.Shacak,
            };

            CityArea cityArea = appDbContext.CityAreas.Where(x => x.Name == CityAreas.Volinskaya).FirstOrDefault();
            List<City> citiesList = new List<City>();

            for (int i = 0; i <= cities.Length - 1; i++)
            {
                citiesList.Add(
                new City
                {
                    Name = cities[i],
                    CityArea = cityArea
                });
            }

            appDbContext.AddRange(citiesList);
            appDbContext.SaveChanges();
        }

        public void AddCitiesToDnepropetrovskayaArea()
        {
            string[] cities = new string[]
            {
                 DnepropetrovskCities.Apostolovo,
                 DnepropetrovskCities.Verhnedneprovsk,
                 DnepropetrovskCities.Volnyogorsk,
                 DnepropetrovskCities.Dnepr,
                 DnepropetrovskCities.JeltieVodi,
                 DnepropetrovskCities.Kamenskoe,
                 DnepropetrovskCities.KrivoyRog,
                 DnepropetrovskCities.Marganec,
                 DnepropetrovskCities.Nikopol,
                 DnepropetrovskCities.Novomoskovsk,
                 DnepropetrovskCities.Orjenihize,
                 DnepropetrovskCities.Pavlograd,
                 DnepropetrovskCities.Pereshepino,
                 DnepropetrovskCities.Pershotravensk,
                 DnepropetrovskCities.Podgorodnoe,
                 DnepropetrovskCities.Patihatki,
                 DnepropetrovskCities.Sinelnikovo,
                 DnepropetrovskCities. Ternovka,
                 DnepropetrovskCities.Chaplinka
            };

            CityArea cityArea = appDbContext.CityAreas.Where(x => x.Name == CityAreas.Dnepropetrovskaya).FirstOrDefault();
            List<City> citiesList = new List<City>();

            for (int i = 0; i <= cities.Length - 1; i++)
            {
                citiesList.Add(
                new City
                {
                    Name = cities[i],
                    CityArea = cityArea
                });
            }

            appDbContext.AddRange(citiesList);
            appDbContext.SaveChanges();
        }

        public void AddCitiesToDonetskayaArea()
        {
            string[] cities = new string[]
            {
                DonetskCities.Avdeevka,
                DonetskCities.Alexandrovka,
                DonetskCities.Amvroisievka,
                DonetskCities.Artemovsk,
                DonetskCities.Volnovaha,
                DonetskCities.Gorlovka,
                DonetskCities.Debalcevo,
                DonetskCities.Djerzinsk,
                DonetskCities.Demitrov,
                DonetskCities.Dobropolie,
                DonetskCities.Dokcuchaevsk,
                DonetskCities.Doneck,
                DonetskCities.Drujovka,
                DonetskCities.Enakievo,
                DonetskCities.Jdanovka,
                DonetskCities.Zugres,
                DonetskCities.Kirovskoe,
                DonetskCities.Konstantinovka,
                DonetskCities.Kramatorsk,
                DonetskCities.Krasnoarmeysk,
                DonetskCities.KrasniyLiman,
                DonetskCities.Mayorsk,
                DonetskCities.Makeevka,
                DonetskCities.Mariupol,
                DonetskCities.Marinka,
                DonetskCities.Novoazonsk,
                DonetskCities.Novogorodovka,
                DonetskCities.Selidovo,
                DonetskCities.Slavansk,
                DonetskCities.Snejnoye,
                DonetskCities.Soledar,
                DonetskCities.Starobeshovo,
                DonetskCities.Torez,
                DonetskCities.Ugleder,
                DonetskCities.Kharzisk,
                DonetskCities.Shahtersk,
                DonetskCities.Yasinovataya
            };

            CityArea cityArea = appDbContext.CityAreas.Where(x => x.Name == CityAreas.Donetsk).FirstOrDefault();
            List<City> citiesList = new List<City>();

            for (int i = 0; i <= cities.Length - 1; i++)
            {
                citiesList.Add(
                new City
                {
                    Name = cities[i],
                    CityArea = cityArea
                });
            }

            appDbContext.AddRange(citiesList);
            appDbContext.SaveChanges();
        }

        public void AddCitiesToZhitomirskayaArea()
        {
            string[] cities = new string[]
            {
                ZhitomirskCities.Andrushevka,
                ZhitomirskCities.Baranovka,
                ZhitomirskCities.Berdichev,
                ZhitomirskCities.VolodarskVolinsk,
                ZhitomirskCities.Emilchino,
                ZhitomirskCities.Djitomir,
                ZhitomirskCities.Irshanskiy,
                ZhitomirskCities.Korosten,
                ZhitomirskCities.Korostishev,
                ZhitomirskCities.Malin,
                ZhitomirskCities.NovogradVolinsk,
                ZhitomirskCities.Ovrush,
                ZhitomirskCities.Olevsk,
                ZhitomirskCities.Popelnya,
                ZhitomirskCities.Rodomishl,
                ZhitomirskCities.Romanov,
                ZhitomirskCities.Chernyahov
            };

            CityArea cityArea = appDbContext.CityAreas.Where(x => x.Name == CityAreas.Zhitomirskaya).FirstOrDefault();
            List<City> citiesList = new List<City>();

            for (int i = 0; i <= cities.Length - 1; i++)
            {
                citiesList.Add(
                new City
                {
                    Name = cities[i],
                    CityArea = cityArea
                });
            }

            appDbContext.AddRange(citiesList);
            appDbContext.SaveChanges();
        }

        public void AddCitiesToZakarpatskayaArea()
        {
            string[] cities = new string[]
            {
               ZakarpatskCities.Beregovo,
               ZakarpatskCities.Bushtinova,
               ZakarpatskCities.VelikiyBichkov,
               ZakarpatskCities.Vinogradov,
               ZakarpatskCities.Vishkovo,
               ZakarpatskCities.Dybovoe,
               ZakarpatskCities.Irshava,
               ZakarpatskCities.Korolevo,
               ZakarpatskCities.Mejgorie,
               ZakarpatskCities.Mukachevo,
              ZakarpatskCities.Perechin,
               ZakarpatskCities.Rahov,
               ZakarpatskCities.Svalyava,
               ZakarpatskCities.Solotvina,
               ZakarpatskCities.Tyachev,
               ZakarpatskCities.Ujgorod,
               ZakarpatskCities.Hust,
               ZakarpatskCities.Chop
            };

            CityArea cityArea = appDbContext.CityAreas.Where(x => x.Name == CityAreas.Zakarpatskaya).FirstOrDefault();
            List<City> citiesList = new List<City>();

            for (int i = 0; i <= cities.Length - 1; i++)
            {
                citiesList.Add(
                new City
                {
                    Name = cities[i],
                    CityArea = cityArea
                });
            }

            appDbContext.AddRange(citiesList);
            appDbContext.SaveChanges();
        }

        public void AddCitiesToZaporozhskayaArea()
        {
            string[] cities = new string[]
            {
                 ZaporozhieCities.Akimovka,
                 ZaporozhieCities.Belyaevka,
                 ZaporozhieCities.Berdansk,
                 ZaporozhieCities.Vasilievka,
                 ZaporozhieCities.Vesoloe,
                 ZaporozhieCities.Volnansk,
                 ZaporozhieCities.GulayPole,
                 ZaporozhieCities.Dneprorudnoe,
                 ZaporozhieCities.Zaporojie,
                 ZaporozhieCities.KamenkaDneprovskaya,
                 ZaporozhieCities.Kuibishevo,
                 ZaporozhieCities.Kushugum,
                 ZaporozhieCities.Melitopol,
                 ZaporozhieCities.Mihailovka,
                 ZaporozhieCities.Molochansk,
                 ZaporozhieCities.Orehov,
                 ZaporozhieCities.Pologin,
                 ZaporozhieCities.Primorsk,
                 ZaporozhieCities.Rozovka,
                 ZaporozhieCities.Tokmak,
                 ZaporozhieCities.Energodar
            };

            CityArea cityArea = appDbContext.CityAreas.Where(x => x.Name == CityAreas.Zaporozhskaya).FirstOrDefault();
            List<City> citiesList = new List<City>();

            for (int i = 0; i <= cities.Length - 1; i++)
            {
                citiesList.Add(
                new City
                {
                    Name = cities[i],
                    CityArea = cityArea
                });
            }

            appDbContext.AddRange(citiesList);
            appDbContext.SaveChanges();
        }

        public void AddCitiesToIvanoFrankovskayaArea()
        {
            string[] cities = new string[]
            {
               IvanoFrankovskCities.Bogorchani,
               IvanoFrankovskCities.Bolehov,
               IvanoFrankovskCities.Burshtin,
               IvanoFrankovskCities.Galich,
               IvanoFrankovskCities.Gorodenka,
               IvanoFrankovskCities.Delyatin,
               IvanoFrankovskCities.Dolina,
               IvanoFrankovskCities.IvanoFrankovsk,
               IvanoFrankovskCities.Kalush,
               IvanoFrankovskCities.Kolomiya,
               IvanoFrankovskCities.Kosov,
               IvanoFrankovskCities.Lanchin,
               IvanoFrankovskCities.Nadvornaya,
               IvanoFrankovskCities.Pereginskoye,
               IvanoFrankovskCities.Rogatin,
               IvanoFrankovskCities.Snyatin,
               IvanoFrankovskCities.Tlymach,
               IvanoFrankovskCities.Tismenica,
               IvanoFrankovskCities.Yaremche
            };

            CityArea cityArea = appDbContext.CityAreas.Where(x => x.Name == CityAreas.IvanoFrankovskaya).FirstOrDefault();
            List<City> citiesList = new List<City>();

            for (int i = 0; i <= cities.Length - 1; i++)
            {
                citiesList.Add(
                new City
                {
                    Name = cities[i],
                    CityArea = cityArea
                });
            }

            appDbContext.AddRange(citiesList);
            appDbContext.SaveChanges();
        }

        public void AddCitiesToKievskayaArea()
        {
            string[] cities = new string[]
            {
                KievCities.Barishevka,
                KievCities.BelayaCerkov,
                KievCities.Berezan,
                KievCities.Boguslav,
                KievCities.Borispol,
                KievCities.Borodyanka,
                KievCities.Boyarka,
                KievCities.Brovari,
                KievCities.Bucha,
                KievCities.Vasilkov,
                KievCities.Vishnevoe,
                KievCities.Volodarka,
                KievCities.Vishgorod,
                KievCities.Glevaha,
                KievCities.Gostomel,
                KievCities.Ivankov,
                KievCities.Irpen,
                KievCities.Kagarlik,
                KievCities.Kiyev,
                KievCities.Kocybinskoe,
                KievCities.Makarov,
                KievCities.Mironovka,
                KievCities.Obyhov,
                KievCities.PereyaslovHmelnitskiy,
                KievCities.Pripyat,
                KievCities.Rjishev,
                KievCities.Rokitnoe,
                KievCities.Skvira,
                KievCities.Slavytich,
                KievCities.Tarasha,
                KievCities.Teterev,
                KievCities.Uzin,
                KievCities.Ukrainka,
                KievCities.Fastov,
                KievCities.Chernobyil,
                KievCities.Yagotin
        };

            CityArea cityArea = appDbContext.CityAreas.Where(x => x.Name == CityAreas.Kievskaya).FirstOrDefault();
            List<City> citiesList = new List<City>();

            for (int i = 0; i <= cities.Length - 1; i++)
            {
                citiesList.Add(
                new City
                {
                    Name = cities[i],
                    CityArea = cityArea
                });
            }

            appDbContext.AddRange(citiesList);
            appDbContext.SaveChanges();
        }

        public void AddCitiesToKirovogradskayaArea()
        {
            string[] cities = new string[]
            {
                KirovogradCities.Alexandiya,
                KirovogradCities.Bobrinec,
                KirovogradCities.Vlasovka,
                KirovogradCities.Gayvoron,
                KirovogradCities.Dolinskaya,
                KirovogradCities.Znamenka,
                KirovogradCities.Kropevnickiy,
                KirovogradCities.MalayaViska,
                KirovogradCities.NovayaPraga,
                KirovogradCities.Novoarhangelsk,
                KirovogradCities.Novoe,
                KirovogradCities.Novomirgorod,
                KirovogradCities.Novoukrainka,
                KirovogradCities.Pervomaysk,
                KirovogradCities.Petrovo,
                KirovogradCities.Pomoshnaya,
                KirovogradCities.Svetlovodsk,
                KirovogradCities.Smolino
            };

            CityArea cityArea = appDbContext.CityAreas.Where(x => x.Name == CityAreas.Kievskaya).FirstOrDefault();
            List<City> citiesList = new List<City>();

            for (int i = 0; i <= cities.Length - 1; i++)
            {
                citiesList.Add(
                new City
                {
                    Name = cities[i],
                    CityArea = cityArea
                });
            }

            appDbContext.AddRange(citiesList);
            appDbContext.SaveChanges();
        }

        public void AddCitiesToLuganskayaArea()
        {
            string[] cities = new string[]
            {
              LuganskCities.Alexandrovsk,
              LuganskCities.Almaznaya,
              LuganskCities.Alchevsk,
              LuganskCities.Antracit,
              LuganskCities.Artemovsk,
              LuganskCities.Bryanke,
              LuganskCities.Vahryshevo,
              LuganskCities.Gornoye,
              LuganskCities.Gorskoye,
              LuganskCities.Zimgorie,
              LuganskCities.Zolotoe,
              LuganskCities.Zorinsk,
              LuganskCities.Irnimo,
              LuganskCities.Kirovsk,
              LuganskCities.Krasnodon,
              LuganskCities.Krasnopartizansk,
              LuganskCities.KrasniyLuch,
              LuganskCities.Kremennaya,
              LuganskCities.Lisichansk,
              LuganskCities.Lugansk,
              LuganskCities.Lytugino,
              LuganskCities.Miysinsk,
              LuganskCities.Molodogvardeysk,
              LuganskCities.Novodrujesk,
              LuganskCities.Novopskov,
              LuganskCities.Pervomaysk,
              LuganskCities.Pervalsk,
              LuganskCities.Petrovskoe,
              LuganskCities.Popasnaya,
              LuganskCities.Privolye,
              LuganskCities.Rovenki,
              LuganskCities.Rybejnoye,
              LuganskCities.Svatovo,
              LuganskCities.Sverdlovsk,
              LuganskCities.Severodonetsk,
              LuganskCities.StanicaLuganskaya,
              LuganskCities.Starobelsk,
              LuganskCities.Stahanov,
              LuganskCities.Suhodolsk,
              LuganskCities.Schastie,
              LuganskCities.Chervonopartizansk
            };

            CityArea cityArea = appDbContext.CityAreas.Where(x => x.Name == CityAreas.Luganskaya).FirstOrDefault();
            List<City> citiesList = new List<City>();

            for (int i = 0; i <= cities.Length - 1; i++)
            {
                citiesList.Add(
                new City
                {
                    Name = cities[i],
                    CityArea = cityArea
                });
            }

            appDbContext.AddRange(citiesList);
            appDbContext.SaveChanges();
        }

        public void AddCitiesToNikolaevskayaArea()
        {
            string[] cities = new string[]
            {
                NikolaevskCities.Alexandrovka,
                NikolaevskCities.Arbyzinka,
                NikolaevskCities.Bashtanka,
                NikolaevskCities.Bereznegovatoe,
                NikolaevskCities.Bratskoe,
                NikolaevskCities.Veselinovo,
                NikolaevskCities.Voznesensk,
                NikolaevskCities.Vradievka,
                NikolaevskCities.Domanevka,
                NikolaevskCities.Elenec,
                NikolaevskCities.Kazanka,
                NikolaevskCities.KrivoeOzero,
                NikolaevskCities.Nikolayev,
                NikolaevskCities.NovayaOdessa,
                NikolaevskCities.NoviyBug,
                NikolaevskCities.Ochakov,
                NikolaevskCities.Pervomaysk,
                NikolaevskCities.Snigerovka,
                NikolaevskCities.Yjnoukrinsk
            };

            CityArea cityArea = appDbContext.CityAreas.Where(x => x.Name == CityAreas.Nikolaevskaya).FirstOrDefault();
            List<City> citiesList = new List<City>();

            for (int i = 0; i <= cities.Length - 1; i++)
            {
                citiesList.Add(
                new City
                {
                    Name = cities[i],
                    CityArea = cityArea
                });
            }

            appDbContext.AddRange(citiesList);
            appDbContext.SaveChanges();
        }

        public void AddCitiesToOdesskayaArea()
        {
            string[] cities = new string[]
            {
                  OdessaCities.Ananyev,
                  OdessaCities.Arciz,
                  OdessaCities.Balta,
                  OdessaCities.BelgorodDnestrovsky,
                  OdessaCities.Belyaevka,
                  OdessaCities.Berezovka,
                  OdessaCities.Bilgrad,
                  OdessaCities.Velikodolinskoe,
                  OdessaCities.Izmail,
                  OdessaCities.Ilyechevsk,
                  OdessaCities.Kiliya,
                  OdessaCities.Kodima,
                  OdessaCities.Kotovsk,
                  OdessaCities.Lybashevka,
                  OdessaCities.Ovidopol,
                  OdessaCities.Odessa,
                  OdessaCities.Razdelnaya,
                  OdessaCities.Reni,
                  OdessaCities.Tatarbynary,
                  OdessaCities.Teplodar,
                  OdessaCities.Shiryaevo,
                  OdessaCities.Youjnoe
            };

            CityArea cityArea = appDbContext.CityAreas.Where(x => x.Name == CityAreas.Odesskaya).FirstOrDefault();
            List<City> citiesList = new List<City>();

            for (int i = 0; i <= cities.Length - 1; i++)
            {
                citiesList.Add(
                new City
                {
                    Name = cities[i],
                    CityArea = cityArea
                });
            }

            appDbContext.AddRange(citiesList);
            appDbContext.SaveChanges();
        }

        public void AddCitiesToPoltavskayaArea()
        {
            string[] cities = new string[]
            {
                PoltavaCities.Gadyach,
                PoltavaCities.Globino,
                PoltavaCities.GorishniePlavny,
                PoltavaCities.Garazhansk,
                PoltavaCities.Grebenka,
                PoltavaCities.Dergachi,
                PoltavaCities.Dikanka,
                PoltavaCities.Zenkov,
                PoltavaCities.Karlovaka,
                PoltavaCities.Kobelky,
                PoltavaCities.Kotelva,
                PoltavaCities.Kremenchyg,
                PoltavaCities.Lohvica,
                PoltavaCities.Lubny,
                PoltavaCities.Mirgorod,
                PoltavaCities.NoviyeSanjary,
                PoltavaCities.Piryatin,
                PoltavaCities.Poltava,
                PoltavaCities.Reshetilovka,
                PoltavaCities.Horol,
                PoltavaCities.Chervonozavodskoe,
                PoltavaCities.Chytovo
            };

            CityArea cityArea = appDbContext.CityAreas.Where(x => x.Name == CityAreas.Poltavskaya).FirstOrDefault();
            List<City> citiesList = new List<City>();

            for (int i = 0; i <= cities.Length - 1; i++)
            {
                citiesList.Add(
                new City
                {
                    Name = cities[i],
                    CityArea = cityArea
                });
            }

            appDbContext.AddRange(citiesList);
            appDbContext.SaveChanges();
        }

        public void AddCitiesToRovnenskayaArea()
        {
            string[] cities = new string[]
            {
               RovnenskCities.Berezne,
               RovnenskCities.Varah,
               RovnenskCities.Vladimirets,
               RovnenskCities.Dubno,
               RovnenskCities.Dubrovitsa,
               RovnenskCities.Zarechnoye,
               RovnenskCities.Zdolbunov,
               RovnenskCities.Kvasils,
               RovnenskCities.Cleaver,
               RovnenskCities.Korets,
               RovnenskCities.Kostopol,
               RovnenskCities.Mlinov,
               RovnenskCities.Prison,
               RovnenskCities.Radivilov,
               RovnenskCities.Smooth,
               RovnenskCities.Rokytnoye,
               RovnenskCities.Sarny
            };

            CityArea cityArea = appDbContext.CityAreas.Where(x => x.Name == CityAreas.Rovnenskaya).FirstOrDefault();
            List<City> citiesList = new List<City>();

            for (int i = 0; i <= cities.Length - 1; i++)
            {
                citiesList.Add(
                new City
                {
                    Name = cities[i],
                    CityArea = cityArea
                });
            }

            appDbContext.AddRange(citiesList);
            appDbContext.SaveChanges();
        }

        public void AddCitiesToSumyskayaArea()
        {
            string[] cities = new string[]
            {
                SumyCities.Akhtyrka,
                SumyCities.Belopolye,
                SumyCities.Buryn,
                SumyCities.Divination,
                SumyCities.Voronezh,
                SumyCities.Gluhov,
                SumyCities.Drujba,
                SumyCities.Konotop,
                SumyCities.Krasnopolye,
                SumyCities.Krolevets,
                SumyCities.Lebedin,
                SumyCities.Putivl,
                SumyCities.Romny,
                SumyCities.Svesa,
                SumyCities.SeredinaByda,
                SumyCities.Sumy,
                SumyCities.Trostyanets,
                SumyCities.Shostka
            };

            CityArea cityArea = appDbContext.CityAreas.Where(x => x.Name == CityAreas.Sumskaya).FirstOrDefault();
            List<City> citiesList = new List<City>();

            for (int i = 0; i <= cities.Length - 1; i++)
            {
                citiesList.Add(
                new City
                {
                    Name = cities[i],
                    CityArea = cityArea
                });
            }

            appDbContext.AddRange(citiesList);
            appDbContext.SaveChanges();
        }

        public void AddCitiesToTernopolskayaArea()
        {
            string[] cities = new string[]
            {
                TernopolCities.Berezhany,
                TernopolCities.Borshchev,
                TernopolCities.Buchach,
                TernopolCities.GreatBerezovitsa,
                TernopolCities.Gusyatin,
                TernopolCities.Zaleshchiki,
                TernopolCities.Zbarazh,
                TernopolCities.Zborov,
                TernopolCities.Kozova,
                TernopolCities.Kopychyntsi,
                TernopolCities.Kremenets,
                TernopolCities.Lanovtsy,
                TernopolCities.Monasteries,
                TernopolCities.Podvolochisk,
                TernopolCities.Podgayitsy,
                TernopolCities.Pochaev,
                TernopolCities.Scalat,
                TernopolCities.Terebovlya,
                TernopolCities.Ternopil,
                TernopolCities.Khostykov,
                TernopolCities.Chertkov,
                TernopolCities.Shu
            };

            CityArea cityArea = appDbContext.CityAreas.Where(x => x.Name == CityAreas.Ternopolskaya).FirstOrDefault();
            List<City> citiesList = new List<City>();

            for (int i = 0; i <= cities.Length - 1; i++)
            {
                citiesList.Add(
                new City
                {
                    Name = cities[i],
                    CityArea = cityArea
                });
            }

            appDbContext.AddRange(citiesList);
            appDbContext.SaveChanges();
        }

        public void AddCitiesToKharkovskayaArea()
        {
            string[] cities = new string[]
            {
                KharkovCities.Balakleya,
                KharkovCities.Barvenkovo,
                KharkovCities.Bogodukhov,
                KharkovCities.Valiki,
                KharkovCities.VelikiyByrluk,
                KharkovCities.Volchansk,
                KharkovCities.Visokiy,
                KharkovCities.Dergachi,
                KharkovCities.Zmiev,
                KharkovCities.Izum,
                KharkovCities.Komsomolskoye,
                KharkovCities.Krasnohrad,
                KharkovCities.Kupyansk,
                KharkovCities.Lozovaya,
                KharkovCities.Lyubotin,
                KharkovCities.Merefa,
                KharkovCities.NewVodoglaga,
                KharkovCities.Pervomaisky,
                KharkovCities.Solonicevka,
                KharkovCities.Kharkiv,
                KharkovCities.Chuguyev
            };

            CityArea cityArea = appDbContext.CityAreas.Where(x => x.Name == CityAreas.Kharkovskaya).FirstOrDefault();
            List<City> citiesList = new List<City>();

            for (int i = 0; i <= cities.Length - 1; i++)
            {
                citiesList.Add(
                new City
                {
                    Name = cities[i],
                    CityArea = cityArea
                });
            }

            appDbContext.AddRange(citiesList);
            appDbContext.SaveChanges();
        }

        public void AddCitiesToKhersonskayaArea()
        {
            string[] cities = new string[]
            {
                KhersonskCities.Antonovka,
                KhersonskCities.Belozirka,
                KhersonskCities.Berislav,
                KhersonskCities.VeliloyaAlexandrovka,
                KhersonskCities.VeliloyaLepetiha,
                KhersonskCities.Genichesk,
                KhersonskCities.GolayaPristan,
                KhersonskCities.Calanchak,
                KhersonskCities.Kamishani,
                KhersonskCities.Kakhovka,
                KhersonskCities.NovayaKahovka,
                KhersonskCities.NovayaMayachke,
                KhersonskCities.Novoalekseevka,
                KhersonskCities.Novotroitskoye,
                KhersonskCities.Poyma,
                KhersonskCities.Skadovsk,
                KhersonskCities.Tavriysk,
                KhersonskCities.Kherson
            };

            CityArea cityArea = appDbContext.CityAreas.Where(x => x.Name == CityAreas.Khersonskaya).FirstOrDefault();
            List<City> citiesList = new List<City>();

            for (int i = 0; i <= cities.Length - 1; i++)
            {
                citiesList.Add(
                new City
                {
                    Name = cities[i],
                    CityArea = cityArea
                });
            }

            appDbContext.AddRange(citiesList);
            appDbContext.SaveChanges();
        }

        public void AddCitiesToKhmelnitskayaArea()
        {
            string[] cities = new string[]
            {
                KhmelnickCities.Vinkovci,
                KhmelnickCities.Volochisk,
                KhmelnickCities.Gotodok,
                KhmelnickCities.Derazhnia,
                KhmelnickCities.Dunayevites,
                KhmelnickCities.Izyaslav,
                KhmelnickCities.KamianetsPodilskyi,
                KhmelnickCities.Krasilov,
                KhmelnickCities.Letychiv,
                KhmelnickCities.Neteshin,
                KhmelnickCities.Polonoe,
                KhmelnickCities.Poninka,
                KhmelnickCities.Slavuta,
                KhmelnickCities.Starokonstantinov,
                KhmelnickCities.Theophipole,
                KhmelnickCities.Khmelnytskyi,
                KhmelnickCities.Shepetivka
            };

            CityArea cityArea = appDbContext.CityAreas.Where(x => x.Name == CityAreas.Khmelnickaya).FirstOrDefault();
            List<City> citiesList = new List<City>();

            for (int i = 0; i <= cities.Length - 1; i++)
            {
                citiesList.Add(
                new City
                {
                    Name = cities[i],
                    CityArea = cityArea
                });
            }

            appDbContext.AddRange(citiesList);
            appDbContext.SaveChanges();
        }

        public void AddCitiesToCherkasskayaArea()
        {
            string[] cities = new string[]
            {
                 CherkassyCities.Vatutino,
                 CherkassyCities.Gorodishe,
                 CherkassyCities.Drabov,
                 CherkassyCities.Zhashkiv,
                 CherkassyCities.Zvenigorodka,
                 CherkassyCities.Zolotonosha,
                 CherkassyCities.Kamenka,
                 CherkassyCities.Kanev,
                 CherkassyCities.KorsunShevchenkovskiy,
                 CherkassyCities.Lysyanka,
                 CherkassyCities.Mankovka,
                 CherkassyCities.Monastery,
                 CherkassyCities.Smila,
                 CherkassyCities.Talnoe,
                 CherkassyCities.Uman,
                 CherkassyCities.Khristinovka,
                 CherkassyCities.Cherkassy,
                 CherkassyCities.Chernobay,
                 CherkassyCities.Chigirin,
                 CherkassyCities.Sphola
            };

            CityArea cityArea = appDbContext.CityAreas.Where(x => x.Name == CityAreas.Cherkaskaya).FirstOrDefault();
            List<City> citiesList = new List<City>();

            for (int i = 0; i <= cities.Length - 1; i++)
            {
                citiesList.Add(
                new City
                {
                    Name = cities[i],
                    CityArea = cityArea
                });
            }

            appDbContext.AddRange(citiesList);
            appDbContext.SaveChanges();
        }

        public void AddCitiesToChernigovskayaArea()
        {
            string[] cities = new string[]
            {
                ChernigovCities.Bakhmach,
                ChernigovCities.Bobrovica,
                ChernigovCities.Borzna,
                ChernigovCities.Gorodnya,
                ChernigovCities.Desna,
                ChernigovCities.Ichnya,
                ChernigovCities.Kozelets,
                ChernigovCities.Koryukovka,
                ChernigovCities.Mena,
                ChernigovCities.Nizhyn,
                ChernigovCities.NovgorodSeversky,
                ChernigovCities.Nosovka,
                ChernigovCities.Priluki,
                ChernigovCities.Sednev,
                ChernigovCities.Semenovka,
                ChernigovCities.Chernigov,
                ChernigovCities.Shchors
            };

            CityArea cityArea = appDbContext.CityAreas.Where(x => x.Name == CityAreas.Chernigovskaya).FirstOrDefault();
            List<City> citiesList = new List<City>();

            for (int i = 0; i <= cities.Length - 1; i++)
            {
                citiesList.Add(
                new City
                {
                    Name = cities[i],
                    CityArea = cityArea
                });
            }

            appDbContext.AddRange(citiesList);
            appDbContext.SaveChanges();
        }

        public void AddCitiesToChernovetckayaArea()
        {
            string[] cities = new string[]
            {
                ChernoveckCities.Beregomet,
                ChernoveckCities.Vashkovtsy,
                ChernoveckCities.Vizhnitsa,
                ChernoveckCities.Hertz,
                ChernoveckCities.Glibokaya,
                ChernoveckCities.Zastavna,
                ChernoveckCities.Kelmians,
                ChernoveckCities.Kitsman,
                ChernoveckCities.Krasnojilsk,
                ChernoveckCities.Novodnestrovsk,
                ChernoveckCities.Novoselitsa,
                ChernoveckCities.Putila,
                ChernoveckCities.Sokyrany,
                ChernoveckCities.Storozhinets,
                ChernoveckCities.Hotin,
                ChernoveckCities.Chernivtsi
            };

            CityArea cityArea = appDbContext.CityAreas.Where(x => x.Name == CityAreas.Chernoveckaya).FirstOrDefault();
            List<City> citiesList = new List<City>();

            for (int i = 0; i <= cities.Length - 1; i++)
            {
                citiesList.Add(
                new City
                {
                    Name = cities[i],
                    CityArea = cityArea
                });
            }

            appDbContext.AddRange(citiesList);
            appDbContext.SaveChanges();
        }

        public void InitServices()
        {
            var pickUpToList = new Services { Name = "Поднять вверх списка", Description = "Объявление единоразово поднимается в верх списка обычных объявлений рубрики, в которой опубликовано, дата меняется на дату предоставления услуги, при этом период жизни объявления остается прежний - 30 дней. После чего объявление снова начинает опускаться вниз, когда новые объявления поступают в рубрику." };
            var highlightAd = new Services { Name = "Выделить объявление", Description = "Объявление визуально выделится в списке рубрики ярким цветом фона на все время размещения объявления." };
            var upToTopAds = new Services { Name = "Поднять в топ объявлений", Description = "Объявления находятся вверху страницы рубрики над списком Обычные объявления. Топ-объявление может быть заказано на 7 дней или 14 дней или 30 дней. В списке Топ-объявления одновременно доступно не более 5-ти оплаченных объявлений, которые отображаются в случайном порядке." };
            var upToVipAds = new Services { Name = "Поднять" , Description = "VIP-объявления находятся на главной странице OLX, которую просматривают 1 500 000 раз в день. Это лучший способ привлечь максимальное количество откликов и продаж - в 20 раз больше по сравнению с обычными объявлениями. VIP-объявление можно заказать на 7 дней или 14 дней." };

            appDbContext.Services.Add(pickUpToList);
            appDbContext.SaveChanges();

            List<ServiceActiveTime> serviceActiveTimesPickUpToList = new List<ServiceActiveTime>
            {
                new ServiceActiveTime
                {
                    Cost = 7,
                    DaysOfActiveService = 0,
                    ServiceId = pickUpToList.ServicesId
                }
            };

            appDbContext.ServiceActiveTimes.AddRange(serviceActiveTimesPickUpToList);
            appDbContext.SaveChanges();

            appDbContext.Services.Add(highlightAd);
            appDbContext.SaveChanges();

            List<ServiceActiveTime> serviceActiveTimeshighlightAd = new List<ServiceActiveTime>
            {
                new ServiceActiveTime
                {
                    Cost = 9,
                    DaysOfActiveService = 7,
                    ServiceId = highlightAd.ServicesId
                },
                new ServiceActiveTime
                {
                    Cost = 15,
                    DaysOfActiveService = 14,
                    ServiceId = highlightAd.ServicesId
                },

                new ServiceActiveTime
                {
                    Cost = 24,
                    DaysOfActiveService = 30,
                    ServiceId = highlightAd.ServicesId
                },
            };

            appDbContext.ServiceActiveTimes.AddRange(serviceActiveTimeshighlightAd);
            appDbContext.SaveChanges();

            appDbContext.Services.Add(upToTopAds);
            appDbContext.SaveChanges();

            List<ServiceActiveTime> serviceActiveTimesUpToTopAds = new List<ServiceActiveTime>
            {
                new ServiceActiveTime
                {
                    Cost = 24,
                    DaysOfActiveService = 7,
                    ServiceId = upToTopAds.ServicesId
                },
                new ServiceActiveTime
                {
                    Cost = 46,
                    DaysOfActiveService = 14,
                    ServiceId = upToTopAds.ServicesId
                },

                new ServiceActiveTime
                {
                    Cost = 78,
                    DaysOfActiveService = 30,
                    ServiceId = upToTopAds.ServicesId
                },
            };

            appDbContext.ServiceActiveTimes.AddRange(serviceActiveTimesUpToTopAds);
            appDbContext.SaveChanges();

            appDbContext.Services.Add(upToVipAds);
            appDbContext.SaveChanges();

            List<ServiceActiveTime> serviceActiveTimesUpToVipAds = new List<ServiceActiveTime>
            {
                new ServiceActiveTime
                {
                    Cost = 44,
                    DaysOfActiveService = 7,
                    ServiceId = upToVipAds.ServicesId
                },
                new ServiceActiveTime
                {
                    Cost = 67,
                    DaysOfActiveService = 14,
                    ServiceId = upToVipAds.ServicesId
                }
            };

            appDbContext.ServiceActiveTimes.AddRange(serviceActiveTimesUpToVipAds);
            appDbContext.SaveChanges();
        }
    }
}
