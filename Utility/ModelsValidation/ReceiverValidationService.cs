using System.Text.RegularExpressions;

namespace Road23.WebAPI.Utility.ModelsValidation
{
	public class ReceiverValidationService
	{
		private const int PHONE_NUMBER_LENGTH = 10;
		public bool ValidatePhoneNumber(string phoneNumber, bool isRequired = true)
		{
			var phone = phoneNumber.Trim();
			var resultPhone = Regex.Replace(phone, @"[^0-9]+", "");
			
			if (string.IsNullOrEmpty(resultPhone) && isRequired)
			{
				return false;
			}
			
			if(resultPhone.Length !=  PHONE_NUMBER_LENGTH && isRequired)
			{
				return false;
			}

			return true;
			
		}
	}
}
