using Rental.WEB.Models.View_Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Rental.WEB.Helpers
{
    public static class PagingHelper
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html,
             PageInfo pageInfo, string form, string name = "page")
        {
            //StringBuilder result = new StringBuilder();
            //for (int i = 1; i <= pageInfo.TotalPages; i++)
            //{
            //    TagBuilder tag = new TagBuilder("button");
            //    tag.MergeAttribute("name", name);
            //    tag.MergeAttribute("form", form);
            //    tag.MergeAttribute("value", i.ToString());
            //    tag.InnerHtml = i.ToString();
            //    if (i == pageInfo.PageNumber)
            //    {
            //        tag.AddCssClass("btn-dark");
            //    }
            //    tag.AddCssClass("btn btn-default");
            //    result.Append(tag.ToString());
            //}
            StringBuilder result = new StringBuilder();
            if (pageInfo.PageNumber > 1)
            {
                TagBuilder tag = new TagBuilder("button");
                tag.MergeAttribute("name", name);
                tag.MergeAttribute("form", form);
                tag.MergeAttribute("value",(pageInfo.PageNumber-1).ToString());
                TagBuilder span = new TagBuilder("span");
                span.AddCssClass("glyphicon glyphicon-circle-arrow-left");
                tag.InnerHtml =span.ToString();
                tag.AddCssClass("btn btn-default");
                tag.AddCssClass("btn-dark");
                result.Append(tag.ToString());
            }
            if (pageInfo.PageNumber < pageInfo.TotalPages)
            {
                TagBuilder tag = new TagBuilder("button");
                tag.MergeAttribute("name", name);
                tag.MergeAttribute("form", form);
                tag.MergeAttribute("value", (pageInfo.PageNumber + 1).ToString());
                TagBuilder span = new TagBuilder("span");
                span.AddCssClass("glyphicon glyphicon-circle-arrow-right");
                tag.InnerHtml = span.ToString();
                tag.AddCssClass("btn btn-default");
                tag.AddCssClass("btn-dark");
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}