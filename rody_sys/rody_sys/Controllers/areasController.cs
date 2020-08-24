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

namespace rody_sys.Areas.adminPanel.Controllers
{
    public class areasController : generalController
    {
        [HttpGet]
        public ActionResult getAllAreas()
        {
            ViewBag.area_size = DB.area.Count();

            ViewBag.all_govern = DB.govern.ToList();

            return View();
        }
//=========================================
        [HttpGet]
        public JsonResult get_all_areas()
        {
            return Json(new { allArea = DB.area.Select(a => new { a.id, a.name, gname = a.govern.name, gId = (a.govern.id == null ? 0 : a.govern.id) }).ToList() }, JsonRequestBehavior.AllowGet);
        }
//=========================================
        [HttpPost]
        public JsonResult delete_area(int id)
        {
            try
            {
                area a = DB.area.Where(x => x.id == id).Single();

                DB.area.Remove(a);
                DB.SaveChanges();

                addAdminOperation("d", a.id, "area", "حذف منطقه");
                return Json(new { msg = "تمت حذف المنطقه بنجاح" });
            }
            catch
            {
                return Json(new { msg = "لم تتم عمليه الحذف .. حاول مره اخري" });
            }
        }
//=========================================
        [HttpPost]
        public JsonResult update_area(int id, string name, int governId)
        {
            try
            {
                area a = DB.area.Where(x => x.id == id).Single();

                a.name = name;
                a.govern = DB.govern.Where(g => g.id == governId).Single();

                DB.Entry(a).State = EntityState.Modified;

                DB.SaveChanges();

                addAdminOperation("u", a.id, "area", "تعديل منطقه");
                return Json(new { msg = "تمت تعديل المنطقه بنجاح" });
            }
            catch
            {
                return Json(new { msg = "لم تتم عمليه التعديل .. حاول مره اخري" });
            }
        }
//========================================
        [HttpPost]
        public JsonResult add_area(string name, int governId)
        {
            try
            {
                area a = new area();

                a.name = name;
                a.govern = DB.govern.Where(g => g.id == governId).Single();

                DB.area.Add(a);
                DB.SaveChanges();

                addAdminOperation("w", a.id, "area", "اضافه منطقه");
                return Json(new { msg = "تمت اضافه المنطقه بنجاح" });
            }
            catch
            {
                return Json(new { msg = "لم تتم عمليه الاضافه .. حاول مره اخري" });
            }
        }
//=========================================

	}
}