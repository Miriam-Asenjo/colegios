using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.DomainModel;

namespace Colegios.Command.Core.Domain
{
    [Serializable]
    public class Nursery: Entity
    {
        public virtual EducationType Type { get; set; }
        public virtual String Name { get; set; }
        public virtual String Address { get; set; }
        public virtual int PostCode { get; set; }
        public virtual String Phone { get; set; }
        public virtual String Fax { get; set; }
        public virtual String Email { get; set; }
        public virtual String Web { get; set; }
        public virtual Locality Locality { get; set; }
        public virtual decimal Latitude { get; set; }
        public virtual decimal Longitude { get; set; }
        public virtual string OpenDay { get; set; }
        public virtual bool IsBilingue { get; set; }
    }
}
