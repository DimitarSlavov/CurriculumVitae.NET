using System.Collections.Generic;

namespace CurriculumVitae.Data
{
	public class Contact
	{
		public Contact()
		{
			ContactLocation = new Location();
		}

		public string FirstName { get; set; }
		public string Surname { get; set; }
		public Location ContactLocation { get; set; }
		public string PhoneNumber { get; set; }
		public string EmailAddress { get; set; }

		public IDictionary<string, string> GetProperties()
		{
			var contactProperties = new Dictionary<string, string>() 
			{
				{ nameof(this.FirstName), this.FirstName },
				{ nameof(this.Surname), this.Surname },
				{ nameof(this.PhoneNumber), this.PhoneNumber },
				{ nameof(this.EmailAddress), this.EmailAddress }
			};

			foreach (var detail in this.ContactLocation.GetProperties())
			{
				contactProperties.Add(detail.Key, detail.Value);
			}

			return contactProperties;
		}
	}
}
