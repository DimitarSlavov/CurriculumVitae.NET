
using System.Collections.Generic;

namespace CurriculumVitae.Data
{
	public class Location
	{
		public string StreetAddress { get; set; }
		public string City { get; set; }
		public string Country { get; set; }
		public string Postcode { get; set; }

		public IDictionary<string, string> GetProperties()
			=> new Dictionary<string, string>()
				{
					{ nameof(this.StreetAddress), this.StreetAddress },
					{ nameof(this.City), this.City },
					{ nameof(this.Country), this.Country },
					{ nameof(this.Postcode), this.Postcode }
				};
	}
}
