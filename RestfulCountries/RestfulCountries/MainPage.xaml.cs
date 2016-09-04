using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace RestfulCountries
{
    public partial class MainPage : ContentPage
    {
        private PlainRestClient _client;
        public ObservableCollection<Country> Countries { get; set; }

        public MainPage()
        {
            Countries = new ObservableCollection<Country>();
            _client = new PlainRestClient();

            InitializeComponent();

            CallButton.Clicked += OnClicked;
        }

        private void OnClicked(object sender, EventArgs e)
        {
            CallCountries();
        }

        private async void CallPlain()
        {
            CallButton.Text = "Calling";
            CallButton.IsEnabled = false;
            IsBusy = true;

            try
            {
                Response.Text = await _client.GetData();
            }
            catch (Exception ex)
            {
                Response.Text = ex.Message;
            }
            finally
            {
                IsBusy = false;
                CallButton.Text = "Call";
                CallButton.IsEnabled = true;
            }
        }

        private async void CallCountries()
        {
            CallButton.Text = "Calling";
            CallButton.IsEnabled = false;
            IsBusy = true;

            try
            {
                var result = await _client.GetCountries();

                Countries.Clear();

                foreach (var item in result)
                {
                    Countries.Add(item);
                }
                Response.IsVisible = false;
            }
            catch (Exception ex)
            {
                Response.Text = ex.Message;
            }
            finally
            {
                IsBusy = false;
                CallButton.Text = "Call";
                CallButton.IsEnabled = true;
            }
        }
    }
}
