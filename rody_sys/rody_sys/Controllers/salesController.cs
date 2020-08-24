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
    public class salesController : generalController
    {

        [HttpGet]
        public ActionResult getAllSales()
        {
            return View();
        }
//========================================
        [HttpGet]
        public JsonResult get_all_sells()
        {
           return Json(new { allSells = DB.sales.Select(s => new { s.id, s.date,sId=s.serviceId, sName = s.services.name, cName = s.client.name,s.clientId, s.phones.number, s.notes, s.quantity,s.price,s.total , s.otherPhoneNum, s.client.areaId}).ToList() }, JsonRequestBehavior.AllowGet);

        }
//========================================
        [HttpGet]
        public ActionResult addSale()
        {

            ViewBag.date = DateTime.Now.Date;

            return View();
        }
//========================================
        [HttpGet]
        public JsonResult get_sale_info(int cid, int? shId)
        {

            DateTime current_date = DateTime.Now;

            DateTime next_of_current_date = DateTime.Now.AddDays(1);//bokra

            double debtor = (double)DB.operations_client.ToList().Where(c => DateTime.Parse(c.get_date, new CultureInfo("ar-AE")) <= next_of_current_date && c.clientId == cid).Sum(x => x.debtor);

            double creditor = (double)DB.operations_client.ToList().Where(c => c.clientId == cid).Sum(x => x.creditor);

            double deserved = Math.Round(debtor - creditor,5);

            Dictionary<string, decimal> latest_charge_ops = new Dictionary<string, decimal>();

            List<int> phone_ids = new List<int>();

            List<phones> phones = new List<phones>();

            if (shId == null)
            {
                 phones = DB.phones.Where(p => p.clientId == cid).ToList();
            }
            else
            {
                phones = DB.phones.Where(p => p.clientId == cid && p.sharehaId == shId).ToList();
            }

            foreach (phones ph in phones)
            {
                phone_ids.Add(ph.id);
            }


            foreach (int id in phone_ids)
            {

                operations_client latest = DB.operations_client.Where(c => c.clientId == cid && c.phone_id == id).ToList().LastOrDefault();
                if (latest != null)
                {
                    latest_charge_ops.Add(latest.phones.number.Replace(" ", ""), (decimal)latest.charge);
                }
            }

            float _charge=(float) DB.client.Where(c => c.id == cid).Single().charge;

            float charge =(float)Math.Round(_charge, 5);

            return Json(new { phones = DB.phones.Where(p => p.clientId == cid).Select(p => new { p.id, p.number, p.sharehaId }).ToList(), current =charge, deserved = deserved, name = DB.client.Where(c => c.id == cid).Single().name.Replace("  ", ""), activeDate = DB.client.Where(c => c.id == cid).Single().activation_date }, JsonRequestBehavior.AllowGet);
        }
//==========================================================
        public void add_operation_client_sale(client clt, float total, int phoneId, string notes, int serviceId, string otherPhoneNum,int saleOpId, string date = "", string time = "")
        {
            operations_client opc = new operations_client();

            opc.debtor = total;
            if (date == "")
            {
                opc.actual_date = DateTime.Now.Date.ToString("dd/MM/yyyy", new CultureInfo("ar-AE"));
                opc.get_date = DateTime.Now.Date.AddDays(10).ToString("dd/MM/yyyy", new CultureInfo("ar-AE"));

                //first operation for this client
                if (clt.activation_date.Contains("لم"))
                {
                    clt.activation_date = DateTime.Now.Date.ToString("dd/MM/yyyy", new CultureInfo("ar-AE"));
                    clt.contract_state = 1;
                    DB.Entry(clt).State = EntityState.Modified;
                    DB.SaveChanges();
                }

            }
            else
            {
                opc.actual_date = (DateTime.Parse(date)).ToString("dd/MM/yyyy", new CultureInfo("ar-AE"));
                opc.get_date = (DateTime.Parse(date).AddDays(10)).ToString("dd/MM/yyyy", new CultureInfo("ar-AE"));

                clt.activation_date = (DateTime.Parse(date)).ToString("dd/MM/yyyy", new CultureInfo("ar-AE"));
                DB.Entry(clt).State = EntityState.Modified;
                DB.SaveChanges();
            }
            if (time == "")
            {
                opc.time = (DateTime.Now).ToString("HH:mm tt", new CultureInfo("ar-AE"));
            }
            else
            {
                opc.time = (DateTime.Parse(time)).ToString("HH:mm tt", new CultureInfo("ar-AE"));
            }


            opc.clientId = clt.id;

            if(otherPhoneNum=="")
            {
                opc.phone_id = phoneId;
            }
            else
            {
                opc.otherPhoneNum = otherPhoneNum;
            }

            opc.charge = clt.charge + total;
            opc.notes = notes;
            opc.serviceId = serviceId;
            opc.declaration = "-";
            //to assign client operaion to the sale operation
            opc.saleOpId = saleOpId;
            DB.operations_client.Add(opc);
            DB.SaveChanges();
        }
//==========================================================

        public void increase_chage_of_client(client clt, float total)
        {
            clt.charge = clt.charge + total;
            DB.Entry(clt).State = EntityState.Modified;
            DB.SaveChanges();
        }
//==========================================================

        public void decrease_shareha_charge(int serviceId, float chargeValue)
        {
            
            int shId=(int)DB.services.Where(s=>s.id==serviceId).Single().sharehaId;

            shareha shr = DB.shareha.Where(sh => sh.id == shId).Single();
            shr.value -= chargeValue;
            DB.Entry(shr).State = EntityState.Modified;
            DB.SaveChanges();

        }
//===========================================================
        public int  add_sale_operation(int cid, int serviceId, int phoneId,float chargeValue, float price, float total, string notes, string date, string time, string otherPhoneNum)
        {
            sales sl = new sales();
            if (date == "")
            {
                sl.date = DateTime.Now.Date.ToString("dd/MM/yyyy", new CultureInfo("ar-AE"));
            }
            else
            {
                sl.date = (DateTime.Parse(date)).ToString("dd/MM/yyyy", new CultureInfo("ar-AE"));
            }
            if (time == "")
            {
                sl.time = (DateTime.Now).ToString("HH:mm tt", new CultureInfo("ar-AE"));
            }
            else
            {
                sl.time = (DateTime.Parse(time)).ToString("HH:mm tt", new CultureInfo("ar-AE"));
            }

            sl.serviceId = serviceId;
            sl.clientId = cid;

            if (otherPhoneNum == "")
            {
                sl.phoneId = phoneId;
            }
            else
            {
                sl.otherPhoneNum = otherPhoneNum;
            }


            sl.quantity = chargeValue;
            sl.price = price;
            sl.total = total;
            
            sl.notes = notes;

            DB.sales.Add(sl);
            DB.SaveChanges();

           return sl.id;

        }
//===========================================================
        [HttpPost]
        public JsonResult add_new_sale(int cid, int serviceId, int? phoneId, float chargeValue, float total, string notes, string time = "", string date = "", string otherPhoneNum="")
        {
            float price =(float) DB.services.Where(s => s.id == serviceId).Single().sellPrice;

            using (var saleTransaction = DB.Database.BeginTransaction())
            {
                try
                {
                    client clt = DB.client.Where(c => c.id == cid).Single();

                    int saleOpId = add_sale_operation(cid, serviceId, phoneId ?? 0, chargeValue, price, total, notes, date, time, otherPhoneNum);

                    add_operation_client_sale(clt, total, phoneId ?? 0, notes, serviceId, otherPhoneNum,saleOpId, date, time);

                    increase_chage_of_client(clt, total);

                    decrease_shareha_charge(serviceId, chargeValue);

                    //commit transaction if all opertaions is ok 
                    saleTransaction.Commit();

                    addAdminOperation("w", saleOpId, "sales", "اضافه حركه مبيعات");

                    return Json(new { msg = "تمت عمليه الاضافه بنجاح" });
                }
                catch
                {
                    //rollback the transaction in case any exception occurs
                    saleTransaction.Rollback();
                    return Json(new { msg = "لم تتم عمليه الاضافه .. حاول مره اخري" });
                }
            }

        }
//============================================================================
        [HttpPost]
        public JsonResult delete_sale_operation(int saleId)
        {
            sales op = DB.sales.Where(s => s.id == saleId).Single();
            int clientId = (int)op.clientId;
            int serviceId =(int)op.serviceId;
            float quantity = (float)op.quantity; //3l4an el shareha
            float total = (float)op.total; //3l4an el client

            using (var removeSaleTransaction = DB.Database.BeginTransaction())
            {
                try
                {
                    remove_sale_operation(saleId); //by cascading will remove client_sale_operation

                    decrease_chage_of_client(clientId,total); //to rollback the increasing

                    increase_shareha_charge(serviceId, quantity); //to rollback the decreasing

                    //commit transaction if all opertaions is ok 
                    removeSaleTransaction.Commit();
                    return Json(new { msg = "تمت عمليه الاضافه بنجاح" });
                }
                catch
                {
                    //rollback the transaction in case any exception occurs
                    removeSaleTransaction.Rollback();
                    return Json(new { msg = "لم تتم عمليه الاضافه .. حاول مره اخري" });
                }
            }

        }
//============================================================================
        public void remove_sale_operation(int saleId)
        {
            sales op = DB.sales.Where(s => s.id == saleId).Single();
            DB.sales.Remove(op);
            DB.SaveChanges();
        }
//=============================================================================
        public void decrease_chage_of_client(int clientId,float total)
        {
            client clt = DB.client.Where(c => c.id == clientId).Single();
            clt.charge -= total;
            DB.Entry(clt).State = EntityState.Modified;
            DB.SaveChanges();
        }
//=============================================================================
        public void increase_shareha_charge(int serviceId, float quantity)
        {
            int shId = (int)DB.services.Where(s => s.id == serviceId).Single().sharehaId;

            shareha shr = DB.shareha.Where(sh => sh.id == shId).Single();
            shr.value += quantity;
            DB.Entry(shr).State = EntityState.Modified;
            DB.SaveChanges();
        }
//============================================================================

        public ActionResult phoneNumHistory()
        {
            return View();
        }
//==================================================================
        [HttpGet]
        public JsonResult getPhoneNumHistories()
        {
            return Json(new { phoneHistory = DB.operations_client.Where(x => x.phone_id != null || x.otherPhoneNum != null).Select(p => new { p.id, p.debtor, DB.phones.Where(c => c.id == p.phone_id).FirstOrDefault().number, p.otherPhoneNum, DB.client.Where(c => c.id == p.clientId).FirstOrDefault().name, p.actual_date, p.time ,p.notes }).ToList() }, JsonRequestBehavior.AllowGet);
        }
//==================================================================


	}
}