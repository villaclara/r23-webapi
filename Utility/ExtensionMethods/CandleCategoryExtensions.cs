using Road23.WebAPI.Models;
using Road23.WebAPI.ViewModels;

namespace Road23.WebAPI.Utility.ExtensionMethods
{
    public static class CandleCategoryExtensions
    {
        public static CandleCategoryFullVM ConvertFromDefaulModel_ToFullVM(this CandleCategory candleCategory, IEnumerable<CandleItem>? candleItems)
        {
            var categoryVM = new CandleCategoryFullVM
            {
                Id = candleCategory.Id,
                Name = candleCategory.Name,
                Candles = new List<string>()
            };

            if (candleItems == null)
                return categoryVM;

            foreach (var c in candleItems)
            {
                categoryVM.Candles.Add(c.Name);
            }
            return categoryVM;
        }

        public static CandleCategoryFullVM ConvertFromDefaulModel_ToFullVM(this CandleCategory candleCategory)
        {
            var categoryVM = new CandleCategoryFullVM
            {
                Id = candleCategory.Id,
                Name = candleCategory.Name,
                Candles = new List<string>()
            };

            if (candleCategory.Candles == null)
                return categoryVM;

            foreach (var c in candleCategory.Candles)
            {
                categoryVM.Candles.Add(c.Name);
            }
            return categoryVM;
        }
    }
}
