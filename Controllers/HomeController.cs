using DealerTrackSalesFile.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DealerTrackSalesFile.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFile()
        {
            if (Request.Files.Count != 1) {
                return Json(new SalesReportModel());
            }

            var file = new StreamReader(Request.Files[0].InputStream, System.Text.Encoding.Default, true);
            var rawlines = file.ReadToEnd();
         
            return Json(SalesReportModel.CreateFromFile(Request.Files[0].FileName, rawlines));
        }
    }
}