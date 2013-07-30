using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Collections;
using System.Web.Script.Serialization;

namespace Colegios.Web.Utils
{
    public static class CollectionsExt
    {
        //public static string ToJson(this IDictionary collection)
        //{
        //    var serializer = new JavaScriptSerializer();
        //    return serializer.Serialize(collection);
        //}

        //public static string ToJson(this IList collection)
        //{
        //    var serializer = new JavaScriptSerializer();
        //    return serializer.Serialize(collection);
        //}

        public static string ToJson(this object collection)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(collection);
        }

    }
}