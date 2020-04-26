using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecycleHelperApplication.Controllers
{
    public class BahanController : BaseController
    {
        // GET: Bahan
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllBahan()
        {

        }
    }
}