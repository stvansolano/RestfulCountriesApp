namespace RestfulCountries
{
	using System.Collections.ObjectModel;
	using System.ComponentModel;
	using System.Linq;

	public partial class SearchPage : INotifyPropertyChanged
    {
		public NavigationService NavigationService { get; set; }

        public SearchPage()
        {
     		NavigationService = new NavigationService(Navigation);
			Results = new ObservableCollection<CountryViewModel>();

            InitializeComponent();
        }

		private string _searchText;
		public string SearchText
		{
			get { return _searchText; }
			set 
			{ 
				_searchText = value;
				OnPropertyChanged(nameof(SearchText));
				Filter();
			}
		}

		public ObservableCollection<CountryViewModel> Results { get; private set; }
		
		private ObservableCollection<CountryViewModel> _countries;
		public ObservableCollection<CountryViewModel> Countries
		{
			get { return _countries; }
			set
			{
				_countries = value;
				OnPropertyChanged(nameof(Countries));

				SearchText = string.Empty;
			}
		}

		private void Filter()
		{
			Results.Clear();

			var query = string.IsNullOrEmpty(SearchText) ? 
			                  Countries.ToArray() 
			                  : Countries.Where(item => item.Matches(SearchText)).ToArray();

			foreach (var item in query)
			{
				Results.Add(item);
			}
		}
    }
}