using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RestfulCountries
{
	public class NavigationService
	{
		public INavigation Navigation { get; private set; }

		public NavigationService(INavigation navigation)
		{
			Navigation = navigation;
		}

		public Task PushAsync<T>(object obj)
			where T : Page, new()
		{
			Contract.Ensures(Contract.Result<Task>() != null);

			var page = new T();
			page.BindingContext = obj;

			return Navigation.PushAsync(page);
		}

		public Task PushModalAsync<T>(object obj)
			where T : Page, new()
		{
			Contract.Ensures(Contract.Result<Task>() != null);

			var page = new T();
			page.BindingContext = obj;

			return Navigation.PushModalAsync(new NavigationPage(page));
		}

		public Task PopAsync<T>()
			where T : Page, new()
		{
			Contract.Ensures(Contract.Result<Task>() != null);

			var page = new T();

			return Navigation.PopAsync(true);
		}
	}
}