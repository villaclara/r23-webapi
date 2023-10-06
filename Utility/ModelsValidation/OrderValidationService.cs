using System.Runtime.CompilerServices;

namespace Road23.WebAPI.Utility.ModelsValidation
{
	public class OrderValidationService
	{
		public bool ValidateSum(decimal sum) => sum >= 0;
	}
}
