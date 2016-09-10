using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace RestfulCountries
{
    public partial class MainPage
    {
        private PlainRestClient DataService;
        public ObservableCollection<Country> Countries { get; set; }
		public ICommand CallCommand { get; set; }
		public ICommand StatusCommand { get; set; }
		public ICommand TapCommand { get; set; }
		public NavigationService NavigationService { get; set; }

        public MainPage()
        {
            Countries = new ObservableCollection<Country>();
            DataService = new PlainRestClient();
			CallCommand = new Command(obj => CallCountries());
			StatusCommand = new Command(obj => CallPlain());
			TapCommand = new Command<Country>(obj => BrowseCountry(obj));
			NavigationService = new NavigationService(Navigation);

            InitializeComponent();
        }

		protected override void OnAppearing()
		{
			base.OnAppearing();

			if (Countries.Any())
			{
				return;
			}
			CallCountries();
		}

		private async void BrowseCountry(Country obj)
		{
			await NavigationService.PushAsync<CountryPage>(obj);
		}

        private async void CallPlain()
        {
            CallButton.Text = "Calling";
            IsBusy = true;
			List.IsVisible = true;

			Response.Text = string.Empty;
			StatusPanel.IsVisible = false;

			try
            {
                Response.Text = await DataService.GetData();
				List.IsVisible = false;
				StatusPanel.IsVisible = true;
            }
            catch (Exception ex)
            {
                Response.Text = ex.Message;
				StatusPanel.IsVisible = true;
				List.IsVisible = false;
            }
            finally
            {
                IsBusy = false;
                CallButton.Text = "Refresh";
            }
        }

        private async void CallCountries()
        {
            CallButton.Text = "Calling";
            IsBusy = true;
			List.IsVisible = true;
			Response.Text = string.Empty;

            try
            {
                var result = await DataService.GetCountries();

                Countries.Clear();

                foreach (var item in result)
                {
                    Countries.Add(item);
                }
                Response.Text = string.Empty;
				StatusPanel.IsVisible = false;
            }
            catch (Exception ex)
            {
                Response.Text = ex.Message;
				StatusPanel.IsVisible = true;
				List.IsVisible = false;
			}
            finally
            {
                IsBusy = false;
                CallButton.Text = "Refresh";
            }
        }
    }
}