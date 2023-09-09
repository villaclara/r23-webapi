using Road23.WebAPI.Models;
using Road23.WebAPI.ViewModels;

namespace Road23.WebAPI.Utility
{
	public static class CandleItemExtensions
	{
		public static CandleItemToDisplayVM MakeCandleToDisplay (this CandleItem cItem, CandleCategory? category = default, CandleIngredient? ingredient = default)
		{
			return new CandleItemToDisplayVM
			{
				Name = cItem.Name,
				Description = cItem.Description,
				Category = category?.Name ?? "unknown",
				RealCost = cItem.RealCost,
				SellPrice = cItem.SellPrice,
				BurningTimeMins = cItem.BurningTimeMins,
				WaxNeededGram = ingredient?.WaxNeededGram ?? 0,
				WickDiameterCM = ingredient?.WickForDiameterCD ?? 0
			};
		}


		public static CandleItemBasicVM MakeBasicCandleToDisplay(this CandleItem cItem, CandleCategory? category = default) =>
			new() 
			{
				Name = cItem.Name,
				Desciption = cItem.Description,
				Category = category?.Name ?? "unknown",
				Price = cItem.SellPrice,
				Height = cItem.HeightCM,
				BurningTime = cItem.BurningTimeMins
			};


	}
}
