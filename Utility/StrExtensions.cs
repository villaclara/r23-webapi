namespace Road23.WebAPI.Utility
{
	public static class StrExtensions
	{
		public static string Normalize(this string value) => 
			value.Trim().ToLower();
	}
}
