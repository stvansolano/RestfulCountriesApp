using System;
using System.Diagnostics;
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

        public Task PushAsync<T>()
            where T : Page, new()
        {
            Contract.Ensures(Contract.Result<Task>() != null);

            var page = new T();

            return PushAsync(page);
        }

        internal Task PushAsync(Page page)
        {
            return Navigation.PushAsync(page);
        }

        public Task PushAsync<T>(object obj)
            where T : Page, new()
        {
            Contract.Ensures(Contract.Result<Task>() != null);

            try
            {
                var page = new T();
                page.BindingContext = obj;

                return Navigation.PushAsync(page);
            }
            catch (Exception ex)
            {
                Log(ex);
            }
            return Task.Run(() => { });
        }

        public Task PushModalAsync<T>(object obj)
            where T : Page, new()
        {
            Contract.Ensures(Contract.Result<Task>() != null);

            try
            {
                var page = new T();
                page.BindingContext = obj;

                return Navigation.PushModalAsync(new NavigationPage(page));
            }
            catch (Exception ex)
            {
                Log(ex);
            }
            return Task.Run(() => { });
        }

        public Task PopAsync()
        {
            Contract.Ensures(Contract.Result<Task>() != null);

			return Navigation.PopAsync(true);
        }

        private void Log(Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
}