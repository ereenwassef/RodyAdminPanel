using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace rody_sys.Areas.adminPanel.Controllers
{
    public class testController : Controller
    {
       
        string num="01523444";

        public string  index()
        {
            return "ayaaaaaaaah";
        }



        public ActionResult select()
        {
            return View();
        }


        public string getSelected(int [] month)
        {
            string m = "";

            for (int i = 0; i < month.Length;i++ )
            {
                m += month[i] +" ";
            }

                return "month=" + m;
        }


        public ActionResult testTree()
        {
            return View();
        }


        public ActionResult scripts()
        {
            return PartialView();

        }


        public ActionResult testPartial()
        {
            return PartialView();           
            
        }

        [HttpGet]
        public ActionResult panel()
        {
            return View();
        }
        [HttpGet]
        public ActionResult page_1()
        {
            return PartialView();
        }
        [HttpGet]
        public ActionResult page_2()
        {
            return View();
        }



	}
}