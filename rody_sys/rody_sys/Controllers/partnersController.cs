using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using rody_sys.Areas.adminPanel.Models;
using System.Web.Mvc;
using System.Data.Entity;

namespace rody_sys.Areas.adminPanel.Controllers
{
    public class partnersController : generalController
    {


        public ActionResult getAllPartners()
        {
            return View();
        }
//===============================================
        [HttpGet]
        public JsonResult get_all_partners()
        {
            return Json(new { allPartners = DB.partner.Select(x => new { x.id, x.name, x.address, x.phone, x.mobile, x.money }).ToList() }, JsonRequestBehavior.AllowGet);
        }
//===============================================
        public JsonResult add_partner(string name, string address, string phone, string mobile, double? money)
        {
            try
            {
                partner pt = new partner();
                pt.name = name;
                pt.address = address;
                pt.phone=phone;
                pt.mobile = mobile;

                if (money == null)
                {
                    pt.money = 0;
                }
                else
                {
                    pt.money = money;
                }

                DB.partner.Add(pt);
                DB.SaveChanges();

                addAdminOperation("w", pt.id, "partner", "اضافه شريك");
                return Json(new { msg="تم اضافه الشريك بنجاح"});
            }
            catch
            {
                return Json(new { msg = "لم تتم الاضافه .. حاول مره اخري" });
            }
        }
//===============================================
        public JsonResult delete_partner(int id)
        {
            try
            {

                partner pt = DB.partner.Where(p => p.id == id).Single();

                DB.partner.Remove(pt);
                DB.SaveChanges();

                addAdminOperation("d", pt.id, "partner", "حذف شريك");
                return Json(new { msg = "تم حذف الشريك بنجاح" });
            }
            catch
            {
                return Json(new { msg = "لم تتم عمليه الحذف .. حاول مره اخري" });
            }
        }
//===============================================
        public JsonResult update_partner(int id,string name, string address, string phone, string mobile, double? money)
        {
            try
            {
                partner pt = DB.partner.Where(p => p.id == id).Single();
                pt.name = name;
                pt.address = address;
                pt.phone = phone;
                pt.mobile = mobile;
                if (money == null)
                {
                    pt.money = 0;
                }
                else
                {
                    pt.money = money;
                }

                DB.Entry(pt).State = EntityState.Modified;
                DB.SaveChanges();

                addAdminOperation("u", pt.id, "partner", "تعديل شريك");
                return Json(new { msg = "تم تعديل بيانات الشريك بنجاح" });
            }
            catch
            {
                return Json(new { msg = "لم تتم عمليه التعديل .. حاول مره اخري" });
            }
        }
//===============================================

	}
}