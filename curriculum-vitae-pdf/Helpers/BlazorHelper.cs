namespace CurriculumVitae.Helpers
{
	public class BlazorHelper
	{
		public static string Toggle(bool condition, string item)
			=> condition ? string.Empty : item;
	}
}
