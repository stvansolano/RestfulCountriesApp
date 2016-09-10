using System;

namespace RestfulCountries
{
	public class CountryFlagService : PlainRestClient
	{
		public CountryFlagService() : base("http://www.geognos.com/api/en/countries/flag") { }

		internal Uri GetFlagSource(string alpha2Code)
		{
			return new Uri(FromUrl(BaseUrl, alpha2Code + ".png"));
		}
	}
}