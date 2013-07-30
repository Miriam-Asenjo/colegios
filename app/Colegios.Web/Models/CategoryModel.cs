using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Colegios.Web.Models
{
    public class CategoryModel
    {
        public string Name { get; set; }

        public IList<CategoryField> Fields { get; set; }


    }

    public class CategoryField
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Mandatory { get; set; }
    }
}