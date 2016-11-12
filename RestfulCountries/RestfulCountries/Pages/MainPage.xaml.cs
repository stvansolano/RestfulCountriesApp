using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace RestfulCountries
{
    public partial class MainPage
    {
		protected CountryService DataService { get; set; }

		public ObservableCollection<CountryViewModel> Countries { get; set; }
		public ICommand CallCommand { get; set; }
		public ICommand StatusCommand { get; set; }
		public ICommand SearchCommand { get; set; }
		public NavigationService NavigationService { get; set; }

        public MainPage()
        {
            Countries = new ObservableCollection<CountryViewModel>();
            DataService = new CountryService();
			CallCommand = new Command(obj => CallCountries());
			StatusCommand = new Command(obj => CallPlain());
			SearchCommand = new Command(obj => CallSearch());
			NavigationService = new NavigationService(Navigation);

            InitializeComponent();

			List.ItemTapped += (sender, e) => {
				var viewModel = ((ListView)sender).BindingContext as CountryViewModel;

				if (viewModel != null && viewModel.BrowseCommand != null)
				{
					viewModel.BrowseCommand.Execute(((ListView)sender).BindingContext);
				}
			};
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

		private async void BrowseCountry(CountryViewModel obj)
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
			if (Countries.Any())
			{
				Indicator.IsVisible = false;
			}
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
					Countries.Add(new CountryViewModel(item)
					{
						FlagSource = ImageSource.FromUri(DataService.GetFlagSource(item.Alpha2Code)),
						BrowseCommand = new Command<CountryViewModel>(BrowseCountry)
					});
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

		private async void CallSearch()
		{
			var searchPage = new SearchPage();
			searchPage.Countries = Countries;

			await NavigationService.PushAsync(searchPage);
		}
    }
}