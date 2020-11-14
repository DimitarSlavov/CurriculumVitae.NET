using iText.Html2pdf;
using System.IO;

namespace CurriculumVitae.Helpers
{
	public class PdfHelper
	{
		public static void ConvertHtmlToPdf(
			string htmlSource, 
			string fullPath)
		{
			try
			{
				using (FileStream pdfDest = File.Open(fullPath, FileMode.OpenOrCreate))
				{
					ConverterProperties converterProperties = new ConverterProperties();

					HtmlConverter.ConvertToPdf(htmlSource, pdfDest);
				}
			}
			catch (IOException)
			{ }
		}
	}
}
