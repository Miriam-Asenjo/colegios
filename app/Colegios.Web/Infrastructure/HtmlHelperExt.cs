using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Linq.Expressions;

namespace Colegios.Web.Infrastructure
{
    public static class HtmlHelperExt
    {
        public static MvcHtmlString DropDownListExtFor<TModel,TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, String optionLabel, Dictionary<string,object> htmlAttributes)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var validationAttributes = htmlHelper.GetUnobtrusiveValidationAttributes(ExpressionHelper.GetExpressionText(expression), metadata);

            if (htmlAttributes != null)
                validationAttributes = validationAttributes.Concat(htmlAttributes).ToDictionary(pair => pair.Key, pair=> pair.Value);

            return htmlHelper.DropDownListFor(expression, selectList, optionLabel, validationAttributes);
        }

        public static MvcHtmlString DropDownListForEnum<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, Type dropDownType) 
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var validationAttributes = htmlHelper.GetUnobtrusiveValidationAttributes(ExpressionHelper.GetExpressionText(expression), metadata);

            var type = typeof(string);

            var values = from Enum e in Enum.GetValues(dropDownType)
                         select new { Id = e, Name = e.ToString() };

            return htmlHelper.DropDownListFor(expression, new SelectList(values, "Id", "Name"), validationAttributes);

        }
    }
}