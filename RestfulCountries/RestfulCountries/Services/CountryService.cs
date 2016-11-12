namespace RestfulCountries
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public class CountryService : PlainRestClient
	{
		protected const string FLAG_SERVICE = "http://www.geognos.com/api/en/countries/flag";

		public CountryService() : base("https://restcountries.eu/rest/v1/all") {}

		public Task<IEnumerable<Country>> GetCountries()
        {
			return GetAsJson<Country>();
        }

		public Uri GetFlagSource(string alpha2Code)
		{
			return new Uri(FromUrl(FLAG_SERVICE, alpha2Code + ".png"));
		}
	}
}