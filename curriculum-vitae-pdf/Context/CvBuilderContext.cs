using CurriculumVitae.Data;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Threading.Tasks;
using CurriculumVitae.Helpers;
using System.Linq;

namespace CurriculumVitae.Context
{
	public class CvBuilderContext
	{
		#region HTML tags

		#endregion
		private const string DoctypeHtml = "!DOCTYPE html";
		private const string Html = "html";
		private const string Head = "head";
		private const string Body = "body";
		private const string Div = "div";
		private const string Main = "main";
		private const string H5 = "h5";
		private const string SPAN = "span";

		#region HTML attributes

		private const string Class = "class";

		#endregion

		#region CSS properties

		private const string M2 = "m-2";
		private const string PL1 = "pl-1";
		private const string PR1 = "pr-1";
		private const string P1 = "p-1";
		private const string AlignTop = "align-top";
		private const string MB4 = "mb-4";
		private const string W25 = "w-25";
		private const string W50 = "w-50";
		private const string W75 = "w-75";
		private const string W100 = "w-100";
		private const string ContainerFluid = "container-fluid";
		private const string Row = "row";
		private const string Col = "col";
		private const string Col4 = "col-4";
		private const string Col5 = "col-5";
		private const string Col7 = "col-7";
		private const string Col8 = "col-8";
		private const string Col12 = "col-12";
		private const string TextUppercase = "text-uppercase";
		private const string TextRight = "text-right";
		private const string FloatLeft = "float-left";
		private const string FloatRight = "float-right";
		private const string DisplayInlineBlock = "d-inline-block";
		private const string PositionRelative = "position-relative";
		private const string PositionFixed = "position-fixed";
		private const string PositionAbsolute = "position-absolute";
		private const string Offset3 = "offset-3";
		private const string DisplayTable = "d-table";
		private const string DisplayTableCell = "d-table-cell";


		#endregion

		private readonly IStringLocalizer<StringConstants> _stringLocalizer;

		public CvBuilderContext(
			IStringLocalizer<StringConstants> StringLocalizer)
		{
			_stringLocalizer = StringLocalizer;
		}

		public async Task<string> BuildPage(
			PersonDetails model,
			string styleBootstrap,
			string styleSite)
		{

			IHtmlContentBuilder html = new HtmlContentBuilder();

			html.AppendHtml(HtmlTagHelper.BuildTag(DoctypeHtml, TagRenderMode.StartTag));

			html.AppendHtml(HtmlTagHelper.BuildTag(Html, TagRenderMode.StartTag));

			html.AppendHtml(await HtmlTagHelper.BuildHtml(Head, BuildHeadAsync(styleBootstrap,
			styleSite)));

			html.AppendHtml(await HtmlTagHelper.BuildHtml(Body, BuildBodyAsync(model)));

			html.AppendHtml(HtmlTagHelper.BuildTag(Html, TagRenderMode.EndTag));

			var result = HtmlTagHelper.ConvertToString(html);

			return result;
		}

		private static async Task<IHtmlContentBuilder> BuildHeadAsync(string styleBootstrap,
			string styleSite)
		{
			IHtmlContentBuilder head = new HtmlContentBuilder();

			var headTags = new List<TagBuilder>()
			{
				HtmlTagHelper.BuildTag(
					"meta",
					TagRenderMode.SelfClosing,
					new Dictionary<string, string>()
					{ { "charset", "utf-8" } }),
				HtmlTagHelper.BuildTag(
					"meta",
					TagRenderMode.SelfClosing,
					new Dictionary<string, string>()
					{
						{ "name", "viewport" },
						{"content", "width=device-width, initial-scale=1.0" }
					}),
				HtmlTagHelper.BuildTag(
					"base",
					TagRenderMode.SelfClosing,
					new Dictionary<string, string>()
					{
						{ "href", "/" }
					}),
				HtmlTagHelper.BuildTag("style", styleBootstrap),
				HtmlTagHelper.BuildTag("style", styleSite),
				HtmlTagHelper.BuildTag("title", "European Curriculum Vitae")
			};

			await Task.Run(() =>
			{
				foreach (var tag in headTags)
				{
					head.AppendHtml(tag);
				}
			});

			return head;
		}

		private async Task<IHtmlContentBuilder> BuildBodyAsync(PersonDetails model)
		{
			IHtmlContentBuilder body = new HtmlContentBuilder();

			await Task.Run(() =>
			{
				body.AppendHtml(HtmlTagHelper.BuildTag(
				Main,
				TagRenderMode.StartTag));

				var sections = new Dictionary<IList<IHtmlContent>, IList<IHtmlContent>>();

				foreach (var kvp in model.GetProperties())
				{
					var data = new Dictionary<string, string>();

					foreach (var item in kvp.Value)
					{
						data.Add(_stringLocalizer.GetString(item.Key), item.Value);
					}

					if (!(data.Values.ToHashSet().Count == 1
						&& data.Values.ToHashSet().First() is null))
					{
						sections.Add(
							BuildCategoryTags(_stringLocalizer.GetString(kvp.Key)),
							BuildDataTags(data)
						);
					}
				}

				foreach (var kvp in sections)
				{
					var attributes = new Dictionary<string, string>() 
					{
						{ Class, $"{W100} {PositionRelative} {DisplayTable}" }
					};

					if(sections.Last().Key != kvp.Key)
					{
						attributes.Add("style", "border-bottom: 1px solid black;");
					}

					var tag = HtmlTagHelper.BuildTag(
						Div,
						attributes);

					tag.InnerHtml.AppendHtml(HtmlTagHelper.BuildSection(kvp.Key));
					tag.InnerHtml.AppendHtml(HtmlTagHelper.BuildSection(kvp.Value));

					body.AppendHtml(tag);
				}

				body.AppendHtml(HtmlTagHelper.BuildTag(Main, TagRenderMode.EndTag));
			});

			return body;
		}

		private static IList<IHtmlContent> BuildCategoryTags(string categoryName)
			=> new List<IHtmlContent>()
			{
				HtmlTagHelper.BuildTag(
				H5,
				categoryName),
				HtmlTagHelper.BuildTag(
				Div,
				new Dictionary<string, string>()
					{ { Class, $"{W25} {P1} {DisplayTableCell} {TextUppercase} {TextRight}" } })
			};

		private static IList<IHtmlContent> BuildDataTags(
			IDictionary<string, string> dataList)
			=> new List<IHtmlContent>()
			{
				HtmlTagHelper.BuildTagsOnOneLevel(
				Div,
				dataList,
				new Dictionary<string, string>()
					{ { Class, $"{P1}" } }),
				HtmlTagHelper.BuildTag(
				Div,
				new Dictionary<string, string>()
					{ { Class, $"{W75} {DisplayTableCell}" } })
			};
	}
}
