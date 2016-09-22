using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace RestfulCountries
{
	public class CountryViewModel
	{
		public Country Model { get; set; }
		public ImageSource FlagSource { get; set; }
		public ICommand BrowseCommand { get; set; }
		public ObservableCollection<CountryStat> Details { get; set; }
		public ObservableCollection<ImageDetail> Photos { get; set; }
        public string Title { get; private set; }

        public CountryViewModel(Country model)
		{
            Title = "Country: " + model.Name;
             
			Model = model;

			Details = new ObservableCollection<CountryStat>(new[] { 
				new CountryStat("Region", Model.Region),
				new CountryStat("SubRegion", Model.SubRegion),
				new CountryStat("NativeName", Model.NativeName),
				new CountryStat("Population", Model.Population),
				new CountryStat("Code", Model.Alpha2Code)
			});

			Photos = new ObservableCollection<ImageDetail>(new ImageDetail[] {  
				new ImageDetail { Image = FlagSource},
				new ImageDetail { Image = FlagSource},
				new ImageDetail { Image = FlagSource}
			});
		}

		internal bool Matches(string searchText)
		{
			return Model != null &&
				(Model.Name ?? string.Empty).ToLowerInvariant().Contains(searchText.ToLowerInvariant());
		}
	}
}