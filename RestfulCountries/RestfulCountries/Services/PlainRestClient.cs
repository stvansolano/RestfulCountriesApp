using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RestfulCountries
{
    public class PlainRestClient
    {
        private const string BASE_URL = "https://restcountries.eu/rest/v1/all";

        public async Task<string> GetData()
        {
            using (var httpClient = new HttpClient())
            {
                return await httpClient.GetStringAsync(BASE_URL).ConfigureAwait(false);
            }
        }

        public async Task<IEnumerable<Country>> GetCountries()
        {
            var result = Enumerable.Empty<Country>();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await httpClient.GetAsync(BASE_URL).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        result = await Task.Run(() => {
                            return JsonConvert.DeserializeObject<IEnumerable<Country>>(json);
                        }).ConfigureAwait(false);
                    }
                }
            }
            return result;
        }
    }
}
