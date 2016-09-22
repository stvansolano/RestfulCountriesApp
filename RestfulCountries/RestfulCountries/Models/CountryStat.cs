using Xamarin.Forms;

namespace RestfulCountries
{
	public class CountryStat
	{
		public CountryStat(string name, object value)
		{
			Name = name;
			Value = value;
		}

		public string Name { get; set; }
		public object Value { get; set; }
		public ImageSource Image { get; set; }
	}
}