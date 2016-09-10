namespace RestfulCountries
{
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public class CountryService : PlainRestClient
	{
		public CountryService() : base("https://restcountries.eu/rest/v1/all") {}

        public Task<IEnumerable<Country>> GetCountries()
        {
			return GetAsJson<Country>();
        }
	}
}