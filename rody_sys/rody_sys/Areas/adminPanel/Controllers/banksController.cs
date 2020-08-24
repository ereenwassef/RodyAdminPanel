using rody_sys.Areas.adminPanel.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace rody_sys.Areas.adminPanel.Controllers
{
    public class banksController : generalController
    {
       
        public ActionResult getAllBanks()
        {
            return View();
        }
//===============================================
        [HttpGet]
        public JsonResult get_all_banks()
        {
            return Json(new { allBanks = DB.banks.Select(x => new { x.id, x.name, x.address,x.phone1,x.phone2,x.phone3,x.fax,x.responsible,x.notes }).ToList() }, JsonRequestBehavior.AllowGet);
        }
//===============================================
        public JsonResult delete_bank(int id)
        {
            try
            {

                banks bk = DB.banks.Where(p => p.id == id).Single();

                DB.banks.Remove(bk);
                DB.SaveChanges();

                return Json(new { msg = "تم حذف البنك بنجاح" });
            }
            catch
            {
                return Json(new { msg = "لم تتم عمليه الحذف .. حاول مره اخري" });
            }
        }
//===============================================
        public JsonResult add_bank(string name,string address, string phone1,string phone2,string phone3,string fax,string notes,string responsible)
        {
            try
            {
                banks bk = new banks();
                bk.name = name;
                bk.address = address;
                bk.phone1 = phone1;
                bk.phone2 = phone2;
                bk.phone3 = phone3;
                bk.fax = fax;
                bk.responsible = responsible;
                bk.notes = notes;
                DB.banks.Add(bk);
                DB.SaveChanges();

                return Json(new { msg = "تمت اضافه البنك بنجاح" });
            }
            catch
            {
                return Json(new { msg = "لم تتم عمليه الاضافه .. حاول مره اخري" });
            }
        }
//===============================================
        public JsonResult update_bank(int id,string name, string address, string phone1, string phone2, string phone3, string fax, string notes, string responsible)
        {
            try
            {
                banks bk = DB.banks.Where(b => b.id == id).Single();
                bk.name = name;
                bk.address = address;
                bk.phone1 = phone1;
                bk.phone2 = phone2;
                bk.phone3 = phone3;
                bk.fax = fax;
                bk.responsible = responsible;
                bk.notes = notes;

                DB.Entry(bk).State = EntityState.Modified;
                DB.SaveChanges();

                return Json(new { msg = "تمت تعديل بيانات البنك بنجاح" });
            }
            catch
            {
                return Json(new { msg = "لم تتم عمليه التعديل .. حاول مره اخري" });
            }
        }
//=======================================================================



	}
}