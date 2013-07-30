using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Colegios.Web.Models;
using Microsoft.Web.Helpers;
using System.Configuration;
using Colegios.Query.Core.QueryInterfaces;
using SharpArch.NHibernate.Web.Mvc;

namespace Colegios.Web.Controllers
{
    public class OpinionController : Controller
    {
        private readonly IOpinionQuery opinionQuery;

        public OpinionController(IOpinionQuery opinionQuery)
        {
            this.opinionQuery = opinionQuery;
        }

        public ActionResult Index()
        {
            return PartialView(new OpinionModel());
        }
        //
        // GET: /Opinion/
        [Transaction]
        [HttpPost]
        public JsonResult RegisterOpinion(OpinionModel model)
        {
            if (ReCaptcha.Validate(ConfigurationManager.AppSettings["ReCaptcha.PrivateKey"]))
            {
                if (ModelState.IsValid)
                {
                    this.opinionQuery.saveOpinion(model.Name, model.Email, model.Opinion);

                    return Json(new { Message = "" });
                }
                return Json(new { Message = "La información proporcionada es incorrecta. Revise los campos" });
            }
            else
            {
                return Json(new { Message = "Error secuencia captcha" });

                    //register opinion database

                    //send us an email
            }


        }

    }
}
