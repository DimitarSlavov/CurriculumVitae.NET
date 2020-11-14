using System;
using System.Collections.Generic;

namespace CurriculumVitae.Data
{
	public class Education
	{
		public Education()
		{
			EducationLocation = new Location();
		}

		public string FieldOfStudy { get; set; }
		public string Degree { get; set; }
		public string SchoolName { get; set; }
		public DateTime Graduation { get; set; }
		public Location EducationLocation { get; set; }

		public IDictionary<string, string> GetProperties()
		{
			var educationProperties = new Dictionary<string, string>()
			{
				{ nameof(this.FieldOfStudy), this.FieldOfStudy },
				{ nameof(this.Degree), this.Degree },
				{ nameof(this.SchoolName), this.SchoolName },
				{ nameof(this.Graduation), this.Graduation != default(DateTime) ? this.Graduation.ToString("dd/MM/yyyy") : null }
			};

			foreach (var detail in this.EducationLocation.GetProperties())
			{
				educationProperties.Add(detail.Key, detail.Value);
			}

			return educationProperties;
		}
	}
}
