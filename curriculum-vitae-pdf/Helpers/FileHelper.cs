using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CurriculumVitae.Helpers
{
	public class FileHelper
	{

		public static async Task<string> GetCssFileAsString(string path)
		{
			try
			{
				return string.Join(
							string.Empty,
							await File.ReadAllLinesAsync(path, Encoding.UTF8));
			}
			catch (IOException)
			{
				return string.Empty;
			}
		}
	}
}
