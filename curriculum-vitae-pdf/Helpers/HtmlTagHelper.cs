using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace CurriculumVitae.Helpers
{
	public class HtmlTagHelper
	{
		public static async Task<IHtmlContentBuilder> BuildHtml(
			string tagName,
			Task<IHtmlContentBuilder> content)
		{
			IHtmlContentBuilder body = new HtmlContentBuilder();

			body.AppendHtml(BuildTag(tagName, TagRenderMode.StartTag));

			body.AppendHtml(await content);

			body.AppendHtml(BuildTag(tagName, TagRenderMode.EndTag));

			return body;
		}

		public async Task<IHtmlContentBuilder> BuildHtml(
			IList<string> tagNames,
			Task<IHtmlContentBuilder> content)
		{
			IHtmlContentBuilder body = new HtmlContentBuilder();

			for (int i = default; i < tagNames.Count; i++)
			{
				body.AppendHtml(BuildTag(tagNames[i], TagRenderMode.StartTag));
			}

			body.AppendHtml(await content);

			for (int i = tagNames.Count - 1; i >= default(int); i--)
			{
				body.AppendHtml(BuildTag(tagNames[i], TagRenderMode.EndTag));
			}

			return body;
		}

		public static TagBuilder BuildTag(
			string tag,
			Dictionary<string, string> attributes = default)
			=> BuildTag(tag, TagRenderMode.Normal, attributes);

		public static TagBuilder BuildTag(
			string tag,
			string text,
			Dictionary<string, string> attributes = default)
		{
			var _tag = BuildTag(tag, TagRenderMode.Normal, attributes);

			_tag.InnerHtml.SetContent(text);

			return _tag;
		}

		public static TagBuilder BuildTag(
			string tag,
			TagRenderMode tagRenderMode,
			Dictionary<string, string> attributes = default)
		{
			var _tag = BuildTag(tag, tagRenderMode);

			if (!(attributes is null))
			{
				_tag.MergeAttributes(attributes);
			}

			return _tag;
		}

		public static TagBuilder BuildTag(
			string tag,
			TagRenderMode tagRenderMode)
		{
			var result = CreateNewTag(tag);
			result.TagRenderMode = tagRenderMode;

			return result;
		}

		public static IHtmlContentBuilder BuildTagsOnOneLevel(
			string tag,
			IDictionary<string, string> dataList,
			Dictionary<string, string> attributes = default)
		{
			IHtmlContentBuilder content = new HtmlContentBuilder();

			if (!(dataList is null))
			{
				foreach (var data in dataList)
				{
					string text = null;

					if (!string.IsNullOrEmpty(data.Key)
						&& !string.IsNullOrEmpty(data.Value))
					{
						text = $"{data.Key}: {data.Value}";
					}
					else if (string.IsNullOrEmpty(data.Key)
						&& !string.IsNullOrEmpty(data.Value))
					{
						text = data.Value;
					}

					if (!string.IsNullOrEmpty(text))
					{
						content.AppendHtml(BuildTag(tag, text, attributes));
					}
				}
			}

			return content;
		}

		public static string ConvertToString(IHtmlContent content)
		{
			var writer = new StringWriter();
			content.WriteTo(writer, HtmlEncoder.Default);

			return writer.ToString();
		}

		public static TagBuilder CreateNewTag(string tag)
			=> new TagBuilder(tag);

		public static IHtmlContent BuildSection(IList<IHtmlContent> tags)
		{
			IHtmlContent section = null;

			if(tags.Count > 1)
			{
				for (int i = default; i < tags.Count - 1; i++)
				{
					TagBuilder temp = tags[i + 1] as TagBuilder;

					if (!(section is null))
					{
						temp.InnerHtml.AppendHtml(section);
					}
					else
					{
						section = new HtmlString(string.Empty);
						temp.InnerHtml.AppendHtml(tags[i]);
					}

					section = temp;
				}
			}
			else
			{
				section = tags[default];
			}

			return section;
		}
	}
}
