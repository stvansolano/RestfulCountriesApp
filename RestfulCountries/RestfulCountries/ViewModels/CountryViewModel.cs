using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace RestfulCountries
{
	public class CountryViewModel
	{
		public Country Model { get; set; }
		public ImageSource FlagSource { get; set; }
		public ICommand BrowseCommand { get; set; }

		public CountryViewModel(Country model)
		{
			Model = model;
		}

		internal bool Matches(string searchText)
		{
			return Model != null &&
				(Model.Name ?? string.Empty).ToLowerInvariant().Contains(searchText.ToLowerInvariant());
		}
	}
}