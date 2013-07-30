using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Colegios.Web.Models
{
    public class NurseryBaseInfoModel : BaseInfoModel
    {
        public int Id { get; set; }

        public String Name { get; set; }

        public int EducationType { get; set; }

        public String Address { get; set; }

        public String Postcode { get; set; }

        public String Phone { get; set; }

        public String Fax { get; set; }

        public String Email { get; set; }

        public String Web { get; set; }

        public bool Bilingue { get; set; }

        

    }
}