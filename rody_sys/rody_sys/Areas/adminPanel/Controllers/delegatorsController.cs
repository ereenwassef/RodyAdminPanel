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
    public class delegatorsController : generalController
    {

        [HttpGet]
        public ActionResult getAllDelegators()
        {
            return View();
        }
//=========================================
        [HttpGet]
        public JsonResult get_all_delegators()
        {
            return Json(new { allDelegators = DB.delegator.Select(d => new { d.active, d.id, d.name, d.address, d.phone, agentId = (d.agent.id==null ? 0: d.agent.id), agentName = d.agent.name, areaName = d.agent.area.name, govern = d.agent.area.govern.name }).ToList() }, JsonRequestBehavior.AllowGet);
        }
//=========================================
        [HttpPost]
        public JsonResult delete(int id)
        {
            try
            {
                delegator dele = DB.delegator.Where(x => x.id == id).Single();

                DB.delegator.Remove(dele);
                DB.SaveChanges();

                return Json(new { msg = "تمت حذف المندوب بنجاح" });
            }
            catch
            {
                return Json(new { msg = "لم تتم عمليه الحذف .. حاول مره اخري" });
            }
        }
//========================================= 
        [HttpPost]
        public JsonResult add_delegator(string name, string phone, string address, int agentId)
        {
            try
            {

                delegator g = new delegator();
                g.name = name;
                g.phone = phone;
                g.address = address;
                g.agent = DB.agent.Where(a => a.id == agentId).Single();
                g.active = 1;
                DB.delegator.Add(g);
                DB.SaveChanges();

                return Json(new { msg = "تمت اضافه المندوب بنجاح" });
            }
            catch
            {
                return Json(new { msg = "لم تتم عمليه الاضافه .. حاول مره اخري" });
            }
        }
//=========================================

        [HttpPost]
        public JsonResult update_delegator(int id,string name, string phone, string address, int agentId, int active)
        {
            try
            {

                delegator g = DB.delegator.Where(x => x.id == id).Single();
                g.name = name;
                g.phone = phone;
                g.address = address;
                g.agent = DB.agent.Where(a => a.id == agentId).Single();
                g.active = active;

                DB.Entry(g).State = EntityState.Modified;
                DB.SaveChanges();

                return Json(new { msg = "تمت تعديل  بيانات المندوب بنجاح" });
            }
            catch
            {
                return Json(new { msg = "لم تتم عمليه التعديل .. حاول مره اخري" });
            }
        }

//=========================================


	}
}