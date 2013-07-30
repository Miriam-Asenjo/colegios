using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Colegios.Web.Models;
using Colegios.Command.Core.Domain;
using Colegios.Command.Core.Domain.Lookups;

namespace Colegios.Web.Mappers
{
    public static class NurseryMapper
    {
        public static NurseryModified ToNurseryModifiedDto(this NurserySignUpModel nurserySignUpModel)
        {
            return new NurseryModified()
            {
                User = new User(nurserySignUpModel.Registration.UserName, nurserySignUpModel.Registration.Email, nurserySignUpModel.Registration.Password, UserType.Owner),

                Name = nurserySignUpModel.Institution.Name,
                Address = nurserySignUpModel.Institution.Address,
                Type = (EducationTypeNursery)nurserySignUpModel.Institution.EducationType,
                PostCode = nurserySignUpModel.Institution.Postcode,
                Phone = nurserySignUpModel.Institution.FirstPhone,
                Fax = nurserySignUpModel.Institution.Fax,
                Email = nurserySignUpModel.Institution.Email,
                Web = nurserySignUpModel.Institution.WebSite

            };
        }
    }
}