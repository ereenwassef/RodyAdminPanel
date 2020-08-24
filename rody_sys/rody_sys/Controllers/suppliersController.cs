using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using rody_sys.Areas.adminPanel.Models;
using System.Data.Entity;

namespace rody_sys.Areas.adminPanel.Controllers
{
    public class suppliersController : generalController
    {

        public ActionResult mainCategories()
        {
            return View();
        }
//===================================================

        public JsonResult get_all_main_categories()
        {

            return Json(new { mainCategories = DB.main_category_supplier.Select(x => new { x.id,x.tasneef_name}).ToList() }, JsonRequestBehavior.AllowGet);
        }
//====================================================
        public JsonResult add_main_category(string name)
        {
            try
            {
                main_category_supplier ms = new main_category_supplier();
                ms.tasneef_name = name;

                DB.main_category_supplier.Add(ms);
                DB.SaveChanges();

                addAdminOperation("w", ms.id, "main_category_supplier", "اضافه تصنيف رئيسي للموردين");

                return Json(new {msg="تم اضافه التصنيف بنجاح" });
            }
            catch
            {
                return Json(new { msg = "لم تتم عمليه الاضافه .. حاول مره اخري" });
            }
        }
//==================================================
        public JsonResult update_main_category(int id,string name)
        {
            try
            {
                main_category_supplier ms = DB.main_category_supplier.Where(m => m.id == id).Single();
                ms.tasneef_name = name;

                DB.Entry(ms).State = EntityState.Modified;

                DB.SaveChanges();

                addAdminOperation("u", ms.id, "main_category_supplier", "تعديل تصنيف رئيسي للموردين");

                return Json(new { msg = "تمت عمليه التعديل بنجاح" });
            }
            catch
            {
                return Json(new { msg = "لم تتم عمليه التعديل .. حاول مره اخري" });
            }
        }
//===================================================
        public JsonResult delete_main_category(int id)
        {
            try
            {
                main_category_supplier ms = DB.main_category_supplier.Where(m => m.id == id).Single();

                DB.main_category_supplier.Remove(ms);

                DB.SaveChanges();

                addAdminOperation("d", ms.id, "main_category_supplier", "حذف تصنيف رئيسي للموردين");

                return Json(new { msg = "تمت حذف التصنيف بنجاح" });
            }
            catch
            {
                return Json(new { msg = "لم تتم عمليه الحذف .. حاول مره اخري" });
            }
        }
//===================================================
        public ActionResult subCategories()
        {
            return View();
        }
//===================================================
        [HttpGet]
        public JsonResult get_all_sub_categories()
        {
            return Json(new { subCategories = DB.sub_category_supplier.Select(x => new { x.id, x.tasneef_name ,mainName=x.main_category_supplier.tasneef_name,mainId=x.main_category_supplier.id}).ToList() }, JsonRequestBehavior.AllowGet);
        }
//====================================================
         [HttpPost]
        public JsonResult add_sub_category(string name, int mainId)
        {
            try
            {
                sub_category_supplier ms = new sub_category_supplier();
                ms.tasneef_name = name;
                ms.mainId = mainId;
                DB.sub_category_supplier.Add(ms);
                DB.SaveChanges();

                addAdminOperation("w", ms.id, "sub_category_supplier", "اضافه تصنيف فرعي للموردين");

                return Json(new { msg = "تم اضافه التصنيف بنجاح" });
            }
            catch
            {
                return Json(new { msg = "لم تتم عمليه الاضافه .. حاول مره اخري" });
            }
        }
//==================================================
         [HttpPost]
        public JsonResult update_sub_category(int id, string name, int mainId)
        {
            try
            {
                sub_category_supplier ms = DB.sub_category_supplier.Where(m => m.id == id).Single();
                ms.tasneef_name = name;
                ms.mainId = mainId;

                DB.Entry(ms).State = EntityState.Modified;

                DB.SaveChanges();

                addAdminOperation("u", ms.id, "sub_category_supplier", "تعديل تصنيف فرعي للموردين");

                return Json(new { msg = "تمت عمليه التعديل بنجاح" });
            }
            catch
            {
                return Json(new { msg = "لم تتم عمليه التعديل .. حاول مره اخري" });
            }
        }
//===================================================
        [HttpPost]
        public JsonResult delete_sub_category(int id)
        {
            try
            {
                sub_category_supplier ms = DB.sub_category_supplier.Where(m => m.id == id).Single();

                DB.sub_category_supplier.Remove(ms);

                DB.SaveChanges();

                addAdminOperation("d", ms.id, "sub_category_supplier", "حذف تصنيف فرعي للموردين");

                return Json(new { msg = "تمت حذف التصنيف بنجاح" });
            }
            catch
            {
                return Json(new { msg = "لم تتم عمليه الحذف .. حاول مره اخري" });
            }
        }
//===================================================
         [HttpGet]
        public ActionResult getAllSuppliers()
        {
            return View();
        }
//===================================================
        [HttpGet]
        public JsonResult get_all_suppliers()
        {
            return Json(new { allSuppliers = DB.supplier.Select(x => new { x.id,x.charge,subc=x.sub_category_supplier.id, x.name, x.address,  x.responsible, x.phone1,x.phone2,x.phone3,x.fax,x.notes,x.type,x.business_type }).ToList() }, JsonRequestBehavior.AllowGet);
        }
//====================================================
         [HttpPost]
        public JsonResult add_supplier(string name, string address, string responsible, string phone1, string phone2, string phone3, string fax, int tasneefId, int type, string notes)
        {
            try
            {
                supplier s = new supplier();
                s.name = name;
                s.address = address;
                s.responsible = responsible;
                s.phone1 = phone1;
                s.phone2 = phone2;
                s.phone3 = phone3;
                s.fax = fax;
                s.notes = notes;
                s.tasneefId = tasneefId;
                s.type = type;
                s.charge = 0; //starting charge

                DB.supplier.Add(s);
                DB.SaveChanges();

                addAdminOperation("w", s.id, "supplier", "اضافه مورد");

                return Json(new { msg = "تمت اضافه المورد بنجاح" });
            }
            catch
            {
                return Json(new { msg = "لم تتم عمليه الاضافه .. حاول مره اخري" });
            }
        }
//====================================================
         [HttpPost]
        public JsonResult update_supplier(int id,string name, string address, string responsible, string phone1, string phone2, string phone3, string fax, string notes, int tasneefId, int type)
        {
            try
            {
                supplier s = DB.supplier.Where(x => x.id == id).Single();
                s.name = name;
                s.address = address;
                s.responsible = responsible;
                s.phone1 = phone1;
                s.phone2 = phone2;
                s.phone3 = phone3;
                s.fax = fax;
                s.notes = notes;
                s.tasneefId = tasneefId;
                s.type = type;

                DB.Entry(s).State = EntityState.Modified;
                DB.SaveChanges();

                addAdminOperation("u", s.id, "supplier", "تعديل مورد");

                return Json(new { msg = "تمت تعديل بيانات المورد بنجاح" });
            }
            catch
            {
                return Json(new { msg = "لم تتم عمليه التعديل .. حاول مره اخري" });
            }
        }
//====================================================
        [HttpPost]
        public JsonResult delete_supplier(int id)
        {
            try
            {
                supplier s = DB.supplier.Where(x => x.id == id).Single();
                DB.supplier.Remove(s);
                DB.SaveChanges();

                addAdminOperation("d", s.id, "supplier", "حذف مورد");

                return Json(new { msg = "تمت حذف المورد بنجاح" });
            }
            catch
            {
                return Json(new { msg = "لم تتم عمليه الحذف .. حاول مره اخري" });
            }
        }
//====================================================


        public ActionResult supplierHistory()
        {
            return View();
        }
//====================================================

        public JsonResult get_supplier_history(int id)
        {
            if (id != 0)
            {
                return Json(new { supplierHistory = DB.operations_supplier.Where(s => s.supId == id).Select(x => new { x.id,x.supplier.name, x.debtor, x.creditor, x.charg, x.opType, x.date }).ToList() }, JsonRequestBehavior.AllowGet);
            }else
            {
                return Json(new { supplierHistory = DB.operations_supplier.Select(x => new { x.id, x.supplier.name, x.debtor, x.creditor, x.charg, x.opType, x.date }).ToList() }, JsonRequestBehavior.AllowGet);
            }
        }
//=================================================
        


	}
}