using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using rody_sys.Areas.adminPanel.Models;
using rody_sys.Areas.adminPanel.Controllers;
using System.Data.Entity;
using System.Threading;
using System.Globalization;
using System.Data;

namespace rody_sys.Areas.adminPanel.Controllers
{
    public class governsController : generalController
    {

        [HttpGet]
        public ActionResult getAllGoverns()
        {
            ViewBag.govern_size = DB.govern.Count();

            return View();
        }
//=========================================
        [HttpGet]
        public JsonResult get_all_govern()
        {
            return Json(new { allGovern = DB.govern.Select(g => new { g.id, g.name }).ToList() }, JsonRequestBehavior.AllowGet);
        }
//=========================================
        [HttpPost]
        public JsonResult add_govern(string govern_name)
        {
            try
            {

                govern g = new govern();
                g.name = govern_name;

                DB.govern.Add(g);
                DB.SaveChanges();

                return Json(new { msg = "تمت اضافه المحافظه بنجاح" });
            }
            catch
            {
                return Json(new { msg = "لم تتم عمليه الاضافه .. حاول مره اخري" });
            }
        }

//=========================================
        [HttpPost]
        public JsonResult delete_govern(int id)
        {
            try
            {
                govern g = DB.govern.Where(x => x.id == id).Single();

                DB.govern.Remove(g);
                DB.SaveChanges();

                return Json(new { msg = "تمت حذف المحافظه بنجاح" });
            }
            catch
            {
                return Json(new { msg = "لم تتم عمليه الحذف .. حاول مره اخري" });
            }
        }
//=========================================
        [HttpPost]
        public JsonResult update_govern(int id, string name)
        {
            try
            {
                govern g = DB.govern.Where(x => x.id == id).Single();

                g.name = name;

                DB.Entry(g).State = EntityState.Modified;

                DB.SaveChanges();

                return Json(new { msg = "تمت تعديل المحافظه بنجاح" });
            }
            catch
            {
                return Json(new { msg = "لم تتم عمليه التعديل .. حاول مره اخري" });
            }
        }
//=========================================
       


	}
}