using System.Collections.Generic;

namespace CurriculumVitae.Data
{
	public class PersonDetails
	{
		public PersonDetails()
		{
			ContactDetails = new Contact();
			Experience = new List<Experience>() { new Experience() };
			Education = new Education();
		}
		public Contact ContactDetails { get; set; }
		public List<Experience> Experience { get; set; }
		public Education Education { get; set; }
		public string Skills { get; set; }
		public string ProfessionalSummary { get; set; }

		public Dictionary<string, IDictionary<string, string>> GetProperties()
		{
			var result = new Dictionary<string, IDictionary<string, string>>();

			result.Add(
				nameof(this.ContactDetails), 
				this.ContactDetails.GetProperties());

			this.Experience
				.ForEach(e => {
					if (!result.ContainsKey(nameof(this.Experience)))
					{
						result.Add(nameof(this.Experience), e.GetProperties());
					}
					else
					{
						result.Add(string.Empty, e.GetProperties());
					}
				});

			result.Add(
				nameof(this.Education), 
				this.Education.GetProperties());
			result.Add(
				nameof(this.Skills), 
				new Dictionary<string, string>()
					{ {string.Empty, this.Skills } });
			result.Add(
				nameof(this.ProfessionalSummary), 
				new Dictionary<string, string>() { {string.Empty, this.ProfessionalSummary } });

			return result;
		}
	}
}
