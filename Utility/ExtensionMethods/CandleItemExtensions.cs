using Road23.WebAPI.Models;
using Road23.WebAPI.ViewModels;

namespace Road23.WebAPI.Utility.ExtensionMethods
{
    public static class CandleItemExtensions
    {
        public static CandleItemBasicVM ConvertFromDefaultModel_ToBasicVM(this CandleItem cItem, CandleCategory? category = default) =>
            new()
            {
                Name = cItem.Name,
                Desciption = cItem.Description,
                Category = category?.Name ?? "unknown",
                Price = cItem.SellPrice,
                Height = cItem.HeightCM,
                BurningTime = cItem.BurningTimeMins
            };

        public static CandleItemBasicVM ConvertFromDefaultModel_ToBasicVM(this CandleItem cItem) =>
            new()
            {
                Name = cItem.Name,
                Desciption = cItem.Description,
                Category = cItem.Category.Name,
                Price = cItem.SellPrice,
                Height = cItem.HeightCM,
                BurningTime = cItem.BurningTimeMins
            };

        public static CandleItemFullVM ConvertFromDefaultModel_ToFullVM(this CandleItem cItem) =>
            new()
            {
                Id = cItem.Id,
                Name = cItem.Name,
                Description = cItem.Description,
                Category = cItem.Category.Name,
                RealCost = cItem.RealCost,
                SellPrice = cItem.SellPrice,
                HeightCM = cItem.HeightCM,
                BurningTimeMins = cItem.BurningTimeMins,
                WaxNeededGram = cItem.Ingredient.WaxNeededGram,
                WickDiameterCM = cItem.Ingredient.WickForDiameterCD
            };

        public static CandleItemFullVM ConvertFromDefaultModel_ToFullVM(this CandleItem cItem, CandleCategory? category = default, CandleIngredient? ingredient = default) =>
            new()
            {
                Id = cItem.Id,
                Name = cItem.Name,
                Description = cItem.Description,
                Category = category?.Name ?? "unknown",
                RealCost = cItem.RealCost,
                SellPrice = cItem.SellPrice,
                HeightCM = cItem.HeightCM,
                BurningTimeMins = cItem.BurningTimeMins,
                WaxNeededGram = ingredient?.WaxNeededGram ?? 0,
                WickDiameterCM = ingredient?.WickForDiameterCD ?? 0
            };

        public static CandleItem ConvertFromFullVM_ToDefaultModel(this CandleItemFullVM cFullItem, CandleCategory ctgr, int? ingredientId = default) =>
            new()
            {
                // Id = cFullItem.Id,	-  not needed for CreateCandle
                Name = cFullItem.Name,
                Description = cFullItem.Description,
                RealCost = cFullItem.RealCost,
                SellPrice = cFullItem.SellPrice,
                HeightCM = cFullItem.HeightCM,
                BurningTimeMins = cFullItem.BurningTimeMins,
                PhotoLink = cFullItem.PhotoLink,
                Category = ctgr,
                Ingredient = new CandleIngredient
                {
                    //Id = ingredientId ?? cFullItem.Id,
                    CandleId = cFullItem.Id,
                    WaxNeededGram = cFullItem.WaxNeededGram,
                    WickForDiameterCD = cFullItem.WickDiameterCM
                }
            };

    }
}
