using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace Colegios.Web.Utils
{
    public static class Expression
    {
        public static readonly Regex ConvertToUrlString = new Regex(CovertToUrlString, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        public const string CovertToUrlString = @"([^a-z0-9\-]?)";
    }
}