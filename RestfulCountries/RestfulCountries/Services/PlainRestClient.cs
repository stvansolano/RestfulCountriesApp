using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RestfulCountries
{
	public abstract class PlainRestClient
	{
		protected string BaseUrl { get; set; }

		protected PlainRestClient(string baseUrl)
		{
			BaseUrl = baseUrl;
		}

        public async Task<string> GetData()
        {
            using (var httpClient = new HttpClient())
            {
                return await httpClient.GetStringAsync(BaseUrl).ConfigureAwait(false);
            }
        }

		protected async Task<IEnumerable<T>> GetAsJson<T>()
			where T : class
		{
			var result = Enumerable.Empty<T>();

			using (var httpClient = new HttpClient())
			{
				httpClient.DefaultRequestHeaders.Accept.Clear();
				httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = await httpClient.GetAsync(BaseUrl).ConfigureAwait(false);

				if (response.IsSuccessStatusCode)
				{
					var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

					if (!string.IsNullOrWhiteSpace(json))
					{
						result = await Task.Run(() =>
						{
							return JsonConvert.DeserializeObject<IEnumerable<T>>(json);
						}).ConfigureAwait(false);
					}
				}
			}
			return result;
		}

		protected string FromUrl(string baseUrl, string resource)
		{
			return string.Join("/", baseUrl, resource);
		}
    }
}
