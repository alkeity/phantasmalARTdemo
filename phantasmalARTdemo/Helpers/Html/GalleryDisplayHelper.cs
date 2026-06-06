using System.Text.Encodings.Web;
using phantasmalARTdemo.Models.DTO;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace phantasmalARTdemo.Helpers.Html
{
    public static class GalleryDisplayHelper
    {
        public static HtmlString ArtGallery(this IHtmlHelper helper, List<ArtDTO> arts)
        {
            TagBuilder divBase = new TagBuilder("div");
            divBase.Attributes.Add("class", "d-flex flex-wrap justify-content-start");

            foreach (ArtDTO art in arts)
            {
                TagBuilder img = new TagBuilder("img");
                //img.Attributes.Add("src", art.FilePath);
                img.Attributes.Add("src", $"{art.FilePath}");
                img.Attributes.Add("alt", art.Title);
                img.Attributes.Add("class", "img-fluid mb-2 me-2 art-thumbnail");

                TagBuilder a = new TagBuilder("a");
                a.Attributes.Add("href", $"/{art.Author}/gallery/{art.ExternalUUID}"); // TODO links
                a.InnerHtml.AppendHtml(img);

                TagBuilder div = new TagBuilder("div");
                div.InnerHtml.AppendHtml(a);

                divBase.InnerHtml.AppendHtml(div);
            }

            using StringWriter sw = new StringWriter();
            divBase.WriteTo(sw, HtmlEncoder.Default);

            return new HtmlString(sw.ToString());
        }
    }
}
