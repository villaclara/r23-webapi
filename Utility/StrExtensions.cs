namespace Road23.WebAPI.Utility
{
	public static class StrExtensions
	{
		// for comparing
		public static string UnifyToCompare(this string value) => 
			value.Trim().ToLower();
	}
}
