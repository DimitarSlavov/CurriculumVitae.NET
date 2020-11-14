using System;
using System.Collections.Generic;

namespace CurriculumVitae.Data
{
	public class Experience
	{
		public Experience()
		{
			WorkLocation = new Location();
		}

		public string JobTitle { get; set; }
		public string Employer { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public bool? WorkingHereNow { get; set; }
		public Location WorkLocation { get; set; }

		public IDictionary<string, string> GetProperties()
		{
			var experienceProperties = new Dictionary<string, string>()
			{
				{ nameof(this.JobTitle), this.JobTitle },
				{ nameof(this.Employer), this.Employer },
				{ nameof(this.StartDate), this.StartDate != default(DateTime) ? this.StartDate.ToString("dd/MM/yyyy") : null },
				{ nameof(this.EndDate),  GetEndDate() },
				{ nameof(this.WorkingHereNow), this.GetWorkingHereNow() }
			};

			foreach (var detail in this.WorkLocation.GetProperties())
			{
				experienceProperties.Add(detail.Key, detail.Value);
			}

			return experienceProperties;
		}

		private string GetWorkingHereNow()
		{
			if (this.WorkingHereNow == true)
			{
				return "Yes";
			}
			else if (this.WorkingHereNow == false)
			{
				return "No";
			}

			return null;
		}

		private string GetEndDate()
		{
			if(this.WorkingHereNow != true &&
				this.EndDate != default(DateTime))
			{
				return this.EndDate.ToString("dd/MM/yyyy");
			}

			return null;
		}
	}
}
