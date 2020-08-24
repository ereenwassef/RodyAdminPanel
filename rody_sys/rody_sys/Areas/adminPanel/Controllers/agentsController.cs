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
    public class agentsController : generalController
    {

        [HttpGet]
        public ActionResult getAllAgents()
        {

            ViewBag.all_governs = DB.govern.ToList();

            ViewBag.all_areas = DB.area.ToList();

            ViewBag.agent_size = DB.agent.Count();


            return View();
        }
//=========================================
        [HttpGet]
        public JsonResult get_all_agents()
        {

            return Json(new { allAgents = DB.agent.Select(g => new { g.id, g.name, g.address, g.phone, areaName = g.area.name, areaId = g.areaId, govern = g.area.govern.name, governId = g.area.govId }).ToList() }, JsonRequestBehavior.AllowGet);
        }
//=========================================
        [HttpPost]
        public JsonResult add_agent(string name, string phone, string address, int areaId)
        {
            try
            {

                agent g = new agent();
                g.name = name;
                g.phone = phone;
                g.address = address;
                g.area = DB.area.Where(a => a.id == areaId).Single();
                DB.agent.Add(g);
                DB.SaveChanges();

                return Json(new { msg = "تمت اضافه الوكيل بنجاح" });
            }
            catch
            {
                return Json(new { msg = "لم تتم عمليه الاضافه .. حاول مره اخري" });
            }
        }
//=========================================
        [HttpPost]
        public JsonResult update_agent(int id, string name, string phone, string address, int areaId)
        {
            try
            {
                agent g = DB.agent.Where(x => x.id == id).Single();
                g.name = name;
                g.phone = phone;
                g.address = address;
                g.area = DB.area.Where(a => a.id == areaId).Single();

                DB.Entry(g).State = EntityState.Modified;

                DB.SaveChanges();

                return Json(new { msg = "تمت تعديل  بيانات الوكيل بنجاح" });
            }
            catch
            {
                return Json(new { msg = "لم تتم عمليه التعديل .. حاول مره اخري" });
            }
        }
//=========================================
        [HttpPost]
        public JsonResult delete_agent(int id)
        {
            try
            {
                agent g = DB.agent.Where(x => x.id == id).Single();

                DB.agent.Remove(g);
                DB.SaveChanges();

                return Json(new { msg = "تمت حذف الوكيل بنجاح" });
            }
            catch
            {
                return Json(new { msg = "لم تتم عمليه الحذف .. حاول مره اخري" });
            }
        }
//=========================================

	}
}