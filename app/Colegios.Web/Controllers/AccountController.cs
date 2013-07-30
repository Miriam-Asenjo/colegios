using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolBlog.Controllers;
using Colegios.Query.Core.QueryInterfaces;

namespace Colegios.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserQuery userQuery;
        public AccountController(IUserQuery userQuery)
        {
            this.userQuery = userQuery;
        }

        public ActionResult IsUserNameAvailable([Bind(Prefix = "Registration.UserName")]string userName, [Bind(Prefix = "Registration.UserId")] int? userId)
        {
            var isUserAvailable = userQuery.IsUserNameAvailable(userName); 
            return Json(isUserAvailable, JsonRequestBehavior.AllowGet);
        }

        public ActionResult IsEmailAvailable([Bind(Prefix = "Registration.Email")]string email)
        {
            var isEmailAvailable = userQuery.IsUserEmailAvailable(email);
            return Json(isEmailAvailable, JsonRequestBehavior.AllowGet);
        }

    }
}
