using Rental.WEB.Models.View_Models.Shared;
using System.Text;
using System.Web.Mvc;

namespace Rental.WEB.Helpers
{
    /// <summary>
    /// Halper for paging
    /// </summary>
    public static class PagingHelper
    {
        /// <summary>
        /// Create page links.
        /// </summary>
        /// <param name="html">Html helper</param>
        /// <param name="pageInfo">Information about page</param>
        /// <param name="form">Form</param>
        /// <param name="name">Button name</param>
        /// <returns></returns>
        public static MvcHtmlString PageLinks(this HtmlHelper html,
             PageInfo pageInfo, string form, string name = "page")
        {
            StringBuilder result = new StringBuilder();
            TagBuilder tag = new TagBuilder("button");
            tag.MergeAttribute("name", name);
            tag.MergeAttribute("form", form);
            TagBuilder span = new TagBuilder("span");
            if (pageInfo.PageNumber > 1)
            {
                tag.MergeAttribute("value",(pageInfo.PageNumber-1).ToString());
                span.AddCssClass("glyphicon glyphicon-circle-arrow-left");
            }

            if (pageInfo.PageNumber < pageInfo.TotalPages)
            {
                tag.MergeAttribute("value", (pageInfo.PageNumber + 1).ToString());
                span.AddCssClass("glyphicon glyphicon-circle-arrow-right");
            }

            tag.InnerHtml = span.ToString();
            tag.AddCssClass("btn btn-default");
            tag.AddCssClass("btn-dark");
            result.Append(tag.ToString());

            return MvcHtmlString.Create(result.ToString());
        }
    }
}