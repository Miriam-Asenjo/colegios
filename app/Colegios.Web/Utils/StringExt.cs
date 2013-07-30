using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Colegios.Web.Utils
{
    public static class StringExt
    {
        public static string ConvertToUrlString(this string source, int length = 100)
        {
            var slug = "";

            if (!string.IsNullOrEmpty(source))
            {
                slug = source.Trim().ToLower();
                
                slug = slug.Replace('&', ' ');
                slug = slug.Replace(' ', '-');
                slug = slug.Replace('/', '-');
                slug = slug.Replace("---", "-");
                slug = slug.Replace("--", "-");
                if (Expression.ConvertToUrlString != null)
                    slug = Expression.ConvertToUrlString.Replace(slug, "");
                //TODO: Make this regex replace '&' with 'and' befre the ? operator

                if (slug.Length * 2 < source.Length)
                    return "";

                if (slug.Length > length)
                    slug = slug.Substring(0, length);
            }

            return slug.TrimEnd('-');
        }

        public static string StripeAcents(this string source)
        {
            return source.Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u");
        }
    }
}