USE AppleUsedDB
GO

CREATE PROCEDURE dbo.GetAllAds 
AS

SELECT a.AdId, a.Title, a.Description, a.Price, a.DateCreated, a.DateUpdated, ca.Name AS CityArea ,c.Name AS CityName, 
av.SumViews, pt.Name AS ProductType, pm.Name AS ProductModel , prm.Name AS ProductMemory,
pc.Name AS ProductColor , prs.Name AS ProductState, u.Id AS UserId , u.Email, u.PhoneNumber
from Ads AS a
Inner join Cities AS c ON a.CityId = c.CityId 
Inner join CityAreas AS ca ON c.CityAreaId = ca.CityAreaId
Inner join AdViews AS av ON a.AdViewsId = av.AdId
Inner join Characteristics AS ch ON a.AdId = ch.AdId 
Inner join ProductModels AS pm ON ch.ProductModelsId = pm.ProductModelsId
Inner join ProductTypes AS pt ON pm.ProductTypesId = pt.ProductTypesId
Inner join ProductMemories as prm ON ch.ProductMemoriesId = prm.ProductMemoriesId
Inner join ProductColors AS pc ON ch.ProductColorsId = pc.ProductColorsId
Inner join ProductStates AS prs ON ch.ProductStatesId = prs.ProductStatesId
Inner join AspNetUsers AS u ON a.ApplicationUserId = u.Id

