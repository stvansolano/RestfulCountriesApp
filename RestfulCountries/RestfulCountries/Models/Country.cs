using System.Collections.Generic;

namespace RestfulCountries
{
    public class Country
    {
        public string Name { get; set; }
        public string Region { get; set; }
		public string SubRegion { get; set; }
		public int Population { get; set; }
		public string NativeName { get; set; }
		public string Alpha2Code { get; set; }
		public string Demonym { get; set; }

		/*
        public List<string> topLevelDomain { get; set; }
        public string alpha3Code { get; set; }
        public List<string> currencies { get; set; }
        public List<string> callingCodes { get; set; }
        public string capital { get; set; }
        public List<string> altSpellings { get; set; }
        public string relevance { get; set; }
		public List<string> languages { get; set; }
        public Translations translations { get; set; }

        public List<double> latlng { get; set; }

        public double? area { get; set; }
        public double? gini { get; set; }
        public List<string> timezones { get; set; }
        public List<object> borders { get; set; }*/
    }
}