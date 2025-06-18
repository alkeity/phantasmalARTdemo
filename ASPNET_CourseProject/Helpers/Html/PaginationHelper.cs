using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASPNET_CourseProject.Helpers.Html
{
    public static class PaginationHelper
    {
        public static HtmlString PaginationSimple(this IHtmlHelper helper, int curPage, int maxPage)
        {
            TagBuilder ul = new TagBuilder("ul");
            ul.Attributes.Add("class", "pagination position-relative bottom-0");

            // previous page
            TagBuilder liPrev = new TagBuilder("li");
            if (curPage > 0)
            {
                liPrev.Attributes.Add("class", "page-item");
            }
            else
            {
                liPrev.Attributes.Add("class", "page-item disabled");
            }
            TagBuilder aPrev = new TagBuilder("a");
            aPrev.Attributes.Add("href", $"/?page={curPage - 1}");
            aPrev.Attributes.Add("class", "page-link");
            aPrev.InnerHtml.Append("<");
            liPrev.InnerHtml.AppendHtml(aPrev);
            ul.InnerHtml.AppendHtml(liPrev);

            //next page
            TagBuilder liNext = new TagBuilder("li");
            if (curPage < maxPage)
            {
                liNext.Attributes.Add("class", "page-item");
            }
            else
            {
                liNext.Attributes.Add("class", "page-item disabled");
            }
            TagBuilder aNext = new TagBuilder("a");
            aNext.Attributes.Add("href", $"/?page={curPage + 1}");
            aNext.Attributes.Add("class", "page-link");
            aNext.InnerHtml.Append(">");
            liNext.InnerHtml.AppendHtml(aNext);
            ul.InnerHtml.AppendHtml(liNext);

            TagBuilder div = new TagBuilder("div");
            div.Attributes.Add("class", "container-fluid mt-3");
            div.InnerHtml.AppendHtml(ul);

            using StringWriter sw = new StringWriter();
            div.WriteTo(sw, HtmlEncoder.Default);

            return new HtmlString(sw.ToString());
        }

        public static HtmlString PaginationPages(this IHtmlHelper helper, int curPage, int maxPage)
        {
            throw new NotImplementedException();
        }
    }
}
