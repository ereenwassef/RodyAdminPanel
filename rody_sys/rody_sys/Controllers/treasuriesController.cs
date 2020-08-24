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
using System.Threading.Tasks;

namespace rody_sys.Areas.adminPanel.Controllers
{
    public class treasuriesController : generalController
    {

        public ActionResult addTahseel()
        {
            return View();
        }
//======================================================
        List<string> omolatList = new List<string>();
        
        [HttpGet] //calculate el omolat
        public JsonResult get_my_parents(int cid, float get_value)
        {
            
            float omola = (float)((get_value * 2.7) / 100);

            float updated_omola = omola;

            float rest_omola = omola;

            string myParents = DB.client.Where(c => c.id == cid).Single().my_parents;

            List<string> pList = myParents.Split(',').ToList();

            List<string> pList_active = new List<string>();

            foreach (string p in pList)
            {
                int idc = Int32.Parse(p);

               if(DB.client.Where(c=>c.id==idc).Single().available==1)
               {
                   pList_active.Add(idc.ToString());
               }

            }

            List<string> pList_omola = new List<string>();


            foreach (string p in pList_active)
            {
                if (p.Equals(pList_active.Last()))
                {
                    int i = Int32.Parse(p);

                    string s = i + "-" + DB.client.Where(c => c.id == i).Single().name.Replace("     ", "") + "-" + rest_omola;
                    pList_omola.Add(s);

                }
                else
                {
                    // string s = p + "-" + updated_omola/2;
                    int i = Int32.Parse(p);

                    string s = i + "-" + DB.client.Where(c => c.id == i).Single().name.Replace("     ", "") + "-" + updated_omola / 2;

                    pList_omola.Add(s);

                    updated_omola /= 2;
                    rest_omola -= updated_omola;
                }
            }

            omolatList = pList_omola;

            return Json(new { myParents = pList_omola, total_omola = omola }, JsonRequestBehavior.AllowGet);
        }

//======================================================

        public void decrease_charge_of_client(int cid, float getValue)
        {
            client clt = DB.client.Where(c => c.id == cid).Single();
            clt.charge -= getValue;
            DB.Entry(clt).State = EntityState.Modified;
            DB.SaveChanges();
        }
//======================================================
        public void increase_charge_of_store(int storeId, float getValue)
        {
            stores str = DB.stores.Where(s => s.id == storeId).Single();
            str.value = str.value + (decimal)getValue;
            DB.Entry(str).State = EntityState.Modified;
            DB.SaveChanges();
        }
//======================================================
        public int add_store_operation_get(int cid, int storeId, float getValue,string date="")
        {

            stores st = DB.stores.Where(s => s.id == storeId).SingleOrDefault();
            
            operations_store ops = new operations_store();
            ops.clientId = cid;
            ops.storeId = storeId;
            ops.get_value = getValue;
            ops.charge =(double)st.value;
            if (date == "")
            {
                ops.date = DateTime.Now.Date.ToString("dd/MM/yyyy", new CultureInfo("ar-AE"));
            }
            else
            {
                ops.date = (DateTime.Parse(date)).ToString("dd/MM/yyyy", new CultureInfo("ar-AE"));
            }
            DB.operations_store.Add(ops);
            DB.SaveChanges();

            return ops.id;
        }
//=====================================================
        public int  add_store_operation_get_store(int storeId1, int storeId2, float getValue, string date = "")
        {

            stores st = DB.stores.Where(s => s.id == storeId1).SingleOrDefault();

            operations_store ops = new operations_store();
            ops.storeId = storeId1;
            ops.fromToStoreId = storeId2;
            ops.get_value = getValue;
            ops.charge = (double)st.value;
            if (date == "")
            {
                ops.date = DateTime.Now.Date.ToString("dd/MM/yyyy", new CultureInfo("ar-AE"));
            }
            else
            {
                ops.date = (DateTime.Parse(date)).ToString("dd/MM/yyyy", new CultureInfo("ar-AE"));
            }
            DB.operations_store.Add(ops);
            DB.SaveChanges();

            return ops.id;
        }
//=====================================================
        public int add_store_operation_give_store(int storeId2, int storeId1, float getValue, string date = "")
        {

            stores st = DB.stores.Where(s => s.id == storeId2).SingleOrDefault();

            operations_store ops = new operations_store();
            ops.storeId = storeId2;
            ops.fromToStoreId = storeId1;
            ops.give_value = getValue;
            ops.charge = (double)st.value;
            if (date == "")
            {
                ops.date = DateTime.Now.Date.ToString("dd/MM/yyyy", new CultureInfo("ar-AE"));
            }
            else
            {
                ops.date = (DateTime.Parse(date)).ToString("dd/MM/yyyy", new CultureInfo("ar-AE"));
            }
            DB.operations_store.Add(ops);
            DB.SaveChanges();

            return ops.id;
        }
//=====================================================
        public void add_operation_client_get(int cid, float getValue,string notes,string date="")
        {
            operations_client op_get = new operations_client();
            op_get.clientId = cid;
            if (date == "")
            {
                op_get.actual_date = DateTime.Now.ToString("dd/MM/yyyy");
                op_get.get_date = DateTime.Now.ToString("dd/MM/yyyy");
            }
            else
            {
                op_get.actual_date = DateTime.Parse(date).ToString("dd/MM/yyyy");
                op_get.get_date = DateTime.Parse(date).ToString("dd/MM/yyyy");
            }
            op_get.creditor = getValue;
            op_get.notes = notes;
            op_get.declaration = "دفع نقديه";
            op_get.charge = DB.client.Where(c=>c.id==cid).Single().charge;

            DB.operations_client.Add(op_get);
            DB.SaveChanges();
        }
//======================================================

        public void omolat_distribution(int cid,float getValue,string date="")
        {
            get_my_parents(cid, getValue);

            foreach (string ml in omolatList)
            {
                string[] sl = ml.Split('-');


                operations_client opc = new operations_client();
                opc.clientId = Int32.Parse(sl[0]);
                opc.creditor = double.Parse(sl[2]);

                if (date == "")
                {
                    opc.actual_date = DateTime.Now.ToString("dd/MM/yyyy");
                    opc.get_date = DateTime.Now.ToString("dd/MM/yyyy");
                }
                else
                {
                    opc.actual_date = (DateTime.Parse(date)).ToString("dd/MM/yyyy");
                    opc.get_date = (DateTime.Parse(date)).ToString("dd/MM/yyyy");
                }

                opc.declaration = "عموله مستحقه";

                int id = Int32.Parse(sl[0]);

                client cl = DB.client.Where(c => c.id == id).Single();
                cl.charge = cl.charge - double.Parse(sl[2]);
                DB.Entry(cl).State = EntityState.Modified;
                DB.SaveChanges();

                opc.charge = cl.charge;
                opc.notes = DB.client.Where(x => x.id == cid).Single().name;


                DB.operations_client.Add(opc);
                DB.SaveChanges();

            }
        }
//=====================================================
        public void add_badal_tahseel(int storeId,float getValue,int cid,string date="")
        {
            stores main = DB.stores.FirstOrDefault();// main store

            if (storeId != main.id)
            {

                bool date_expired = false;

                string active_date = DB.client.Where(c => c.id == cid).Single().activation_date;

                if (date == "")
                {
                    if (DateTime.Now.Date >= DateTime.Parse(active_date, new CultureInfo("en-GB")).AddDays(101))
                    {
                        date_expired = true;
                    }
                }
                else
                {
                    if (DateTime.Parse(date) >= DateTime.Parse(active_date, new CultureInfo("en-GB")).AddDays(101))
                    {
                        date_expired = true;
                    }
                }

                if (date_expired || getValue == 0)
                {
                    operations_client badal_tahseel = new operations_client();
                    badal_tahseel.debtor = 5;
                    if (date == "")
                    {
                        badal_tahseel.actual_date = DateTime.Now.Date.ToString("dd/MM/yyyy", new CultureInfo("ar-AE"));
                        badal_tahseel.get_date = DateTime.Now.Date.ToString("dd/MM/yyyy", new CultureInfo("ar-AE"));
                    }
                    else
                    {
                        badal_tahseel.actual_date = (DateTime.Parse(date)).ToString("dd/MM/yyyy", new CultureInfo("ar-AE"));
                        badal_tahseel.get_date = (DateTime.Parse(date)).ToString("dd/MM/yyyy", new CultureInfo("ar-AE"));
                    }

                    badal_tahseel.clientId = cid;
                    badal_tahseel.declaration = "بدل تحصيل";

                    client ant = DB.client.Where(c => c.id == cid).Single();
                    ant.charge = ant.charge + 5;
                    DB.Entry(ant).State = EntityState.Modified;
                    DB.SaveChanges();

                    badal_tahseel.charge = DB.client.Where(c => c.id == cid).Single().charge;
                    DB.operations_client.Add(badal_tahseel);
                    DB.SaveChanges();
                }
            }
        }
//======================================================
        [HttpPost]
        public JsonResult wared_client(int cid, int storeId, float getValue, float storeValue, string notes, string date = "")
        {
            int stOpId = 0;

           using(var treasuryTransaction=DB.Database.BeginTransaction())
           {
            try
            {
                decrease_charge_of_client(cid, getValue);

                increase_charge_of_store(storeId, getValue);

                stOpId=add_store_operation_get(cid, storeId, getValue, date);

                add_operation_client_get(cid, getValue, notes, date);

                omolat_distribution(cid, getValue, date);

                add_badal_tahseel(storeId, getValue, cid, date);

                //commit transaction if all opertaions is ok 
                treasuryTransaction.Commit();

                addAdminOperation("w", stOpId, "operations_store", "اضافه حركه خزنه ");

                return Json(new { msg = "تم اضافه حركه الخزنه بنجاح" });
            }
            catch
            {
                //rollback the transaction in case any exception occurs
                treasuryTransaction.Rollback();

                return Json(new { msg = "لم تتم الاضافه .. حاول مره اخري" });
            }
           }
        }
//============================================================================
        [HttpPost]
        public JsonResult monsaref_client(int cid, int storeId, float getValue, string notes, string date = "")
        {
            using (var treasuryTransaction = DB.Database.BeginTransaction())
            {
                try
                {
                    int stOpId = 0;

                    increase_charge_of_client(cid, getValue);

                    decrease_charge_of_store(storeId, getValue);

                    stOpId = add_store_operation_give(cid, storeId, getValue, date);

                    add_operation_client_give(cid, getValue, notes, date);
                    
                    //commit transaction if all opertaions is ok 
                    treasuryTransaction.Commit();

                    addAdminOperation("w", stOpId, "operations_store", "اضافه حركه خزنه ");

                    return Json(new { msg = "تم اضافه حركه الخزنه بنجاح" });
                }
                catch
                {
                    //rollback the transaction in case any exception occurs
                    treasuryTransaction.Rollback();

                    return Json(new { msg = "لم تتم الاضافه .. حاول مره اخري" });
                }
            }
        }
//===========================================================================
        [HttpPost]
        public JsonResult wared_store( int storeId1,int storeId2 , float getValue, string notes, string date = "")
        {
            using (var treasuryTransaction = DB.Database.BeginTransaction())
            {
                try
                {
                    int stOpId = 0;

                    increase_charge_of_store(storeId1, getValue);

                    decrease_charge_of_store(storeId2, getValue);

                    stOpId = add_store_operation_get_store(storeId1, storeId2, getValue, date);

                    add_store_operation_give_store(storeId2, storeId1, getValue, date);

                    //commit transaction if all opertaions is ok 
                    treasuryTransaction.Commit();

                    addAdminOperation("w", stOpId, "operations_store", "اضافه حركه خزنه ");

                    return Json(new { msg = "تم اضافه حركه الخزنه بنجاح" });
                }
                catch
                {
                    //rollback the transaction in case any exception occurs
                    treasuryTransaction.Rollback();

                    return Json(new { msg = "لم تتم الاضافه .. حاول مره اخري" });
                }
            }
        }
//============================================================================

        [HttpPost]
        public JsonResult monsaref_store(int storeId1, int storeId2, float getValue, string notes, string date = "")
        {
            using (var treasuryTransaction = DB.Database.BeginTransaction())
            {
                try
                {
                    int stOpId = 0;

                    decrease_charge_of_store(storeId1, getValue);

                    increase_charge_of_store(storeId2, getValue);

                    stOpId=add_store_operation_give_store(storeId1, storeId2, getValue, date);

                    add_store_operation_get_store(storeId2, storeId1, getValue, date);

                    //commit transaction if all opertaions is ok 
                    treasuryTransaction.Commit();

                    addAdminOperation("w", stOpId, "operations_store", "اضافه حركه خزنه ");

                    return Json(new { msg = "تم اضافه حركه الخزنه بنجاح" });
                }
                catch
                {
                    //rollback the transaction in case any exception occurs
                    treasuryTransaction.Rollback();

                    return Json(new { msg = "لم تتم الاضافه .. حاول مره اخري" });
                }
            }
        }
//============================================================================
        //store give client a value
        public void add_operation_client_give(int cid, float value, string notes, string date = "")
        {
            operations_client op_get = new operations_client();
            op_get.clientId = cid;
            if (date == "")
            {
                op_get.actual_date = DateTime.Now.ToString("dd/MM/yyyy");
                op_get.get_date = DateTime.Now.ToString("dd/MM/yyyy");
            }
            else
            {
                op_get.actual_date = DateTime.Parse(date).ToString("dd/MM/yyyy");
                op_get.get_date = DateTime.Parse(date).ToString("dd/MM/yyyy");
            }
            op_get.debtor = value;
            op_get.notes = notes;
            op_get.declaration = "صرف نقديه";
            op_get.charge = DB.client.Where(c => c.id == cid).Single().charge;

            DB.operations_client.Add(op_get);
            DB.SaveChanges();
        }
//===========================================================================

        public int add_store_operation_give(int cid, int storeId, float value, string date = "")
        {

            stores st = DB.stores.Where(s => s.id == storeId).SingleOrDefault();

            operations_store ops = new operations_store();
            ops.clientId = cid;
            ops.storeId = storeId;
            ops.give_value = value;
            ops.charge = (double)st.value;
            if (date == "")
            {
                ops.date = DateTime.Now.Date.ToString("dd/MM/yyyy", new CultureInfo("ar-AE"));
            }
            else
            {
                ops.date = (DateTime.Parse(date)).ToString("dd/MM/yyyy", new CultureInfo("ar-AE"));
            }
            DB.operations_store.Add(ops);
            DB.SaveChanges();

            return ops.id;
        }
//===========================================================================
        public void increase_charge_of_client(int cid, float getValue)
        {
            client clt = DB.client.Where(c => c.id == cid).Single();
            clt.charge += getValue;
            DB.Entry(clt).State = EntityState.Modified;
            DB.SaveChanges();
        }
//===========================================================================
     
        public void decrease_charge_of_supplier(int sid, float getValue)
        {

            supplier sup = DB.supplier.Where(s => s.id == sid).Single();
            sup.charge -= getValue;
            DB.Entry(sup).State = EntityState.Modified;
            DB.SaveChanges();
        }
//========================================================================
        public void decrease_charge_of_store(int storeId, float getValue)
        {

            stores st = DB.stores.Where(s => s.id == storeId).Single();
            st.value -= (decimal)getValue;
            DB.Entry(st).State = EntityState.Modified;
            DB.SaveChanges();
        }
//===========================================================================

        public void add_store_operation_sup(int sid, int storeId, float getValue, string date)
        {
            operations_store ops = new operations_store();

            if (date != null && date!="")
            {
                ops.date = date;
            }
            else
            {
                ops.date = DateTime.Now.ToString("dd/MM/yyyy");
            }

            ops.supplierId = sid;
            ops.storeId = storeId;
            ops.give_value= getValue;

            DB.operations_store.Add(ops);
            DB.SaveChanges();

        }
//===========================================================================
        public void add_operation_supplier_give(int sid, float getValue, string date)
        {
            operations_supplier op = new operations_supplier();
            if (date != null && date!="")
            {
                op.date = date;
                op.date = DateTime.Parse(date).ToString("dd/MM/yyyy");
            }
            else
            {
                op.date = DateTime.Now.ToString("dd/MM/yyyy");
            }
            op.opType = "سداد";
            op.supId = sid;
            op.debtor = getValue;
            op.charg = DB.supplier.Where(s => s.id == sid).Single().charge;
            DB.operations_supplier.Add(op);
            DB.SaveChanges();
            
        }

//===========================================================================
        public  void add_new_give_supplier(int sid, int storeId, float getValue, string date = "")
        {

            //Parallel.Invoke(() => decrease_charge_of_supplier(sid, getValue), 
            //   () => decrease_charge_of_store(storeId, getValue),
            //   () => add_store_operation_sup(sid, storeId, getValue, date),
            //   () => add_operation_supplier_give(sid, getValue, date)
            //   );

            decrease_charge_of_supplier(sid, getValue);
            decrease_charge_of_store(storeId, getValue);
            add_store_operation_sup(sid, storeId, getValue, date);
            add_operation_supplier_give(sid, getValue, date);        

        }
//============================================================================
        [HttpGet]
        public JsonResult get_all_treasuries()
        {
            return Json(new { allTreasuries = DB.stores.Select(s => new { s.id, s.name, s.value }).ToList() }, JsonRequestBehavior.AllowGet);
        }
//============================================================================
        [HttpGet]
        public float get_treasury_value(int tId)
        {
            //return Json(new { value = DB.stores.Where(s=>s.id==tId).Single().value }, JsonRequestBehavior.AllowGet);
           
            return (float)DB.stores.Where(s => s.id == tId).Single().value;
        }
//============================================================================
        [HttpGet]
        public ActionResult getAllTreasuries()
        {
            return View();
        }
//===========================================================================
        [HttpPost]
        public JsonResult add_Treasury(string name,decimal value)
        {
            try
            {
                stores st = new stores();
                st.name = name;
                st.value = value;

                DB.stores.Add(st);
                DB.SaveChanges();

                addAdminOperation("w", st.id, "stores", "اضافه خزنه");

                return Json(new { msg = "تمت اضافه الخزنه بنجاح" });
            }
            catch
            {
                return Json(new { msg = "لم تتم عمليه الاضافه .. حاول مره اخري" });
            }
        }
//===========================================================================
        private void make_store_operation(int storeId, decimal value, int opType)
        {
            operations_store ops = new operations_store();
            ops.storeId = storeId;

            if(opType==1)
            {
                ops.get_value = (float)value;
            }
            else if(opType==2)
            {
                ops.give_value = (float)value;
            }
            float charge = (float) DB.stores.Where(s => s.id == storeId).Single().value;
            ops.charge = charge;
            ops.date = DateTime.Now.ToString("dd/MM/yyyy");
            DB.operations_store.Add(ops);
            DB.SaveChanges();
        }
//===========================================================================
        [HttpPost]
        public JsonResult update_Treasury(int id,string name, decimal value , int opType )
        {
            try
            {
                stores st = DB.stores.Where(s => s.id == id).Single();
                st.name = name;
                if (opType == 1)
                {
                    st.value += value;
                }
                else if(opType==2) 
                {
                    st.value -= value;
                }
                DB.Entry(st).State = EntityState.Modified;
                DB.SaveChanges();

                make_store_operation(id, value, opType);

                addAdminOperation("u", st.id, "stores", "تعديل خزنه");

                return Json(new { msg = "تم تعديل بيانات الخزنه بنجاح" });
            }
            catch
            {
                return Json(new { msg = "لم تتم عمليه التعديل .. حاول مره اخري" });
            }
        }
//===========================================================================
        [HttpPost]
        public JsonResult delete_Treasury(int id)
        {
            try
            {
                stores st = DB.stores.Where(s => s.id == id).Single();
               
                DB.stores.Remove(st);
                DB.SaveChanges();

                addAdminOperation("d", st.id, "stores", "حذف خزنه");

                return Json(new { msg = "تم حذف الخزنه بنجاح" });
            }
            catch
            {
                return Json(new { msg = "لم تتم عمليه الحذف .. حاول مره اخري" });
            }
        }
//===========================================================================
        [HttpGet]
        public JsonResult treasuries_operations()
        {
            return Json(new { allOperations = DB.operations_store.Select(s => new { s.id,s.storeId,sName=s.stores.name , s.get_value , s.clientId ,cName= s.client.name , s.date, s.supplierId ,supName= s.supplier.name ,s.give_value , s.charge ,s.fromToStoreId , fromToStore=s.stores1.name  }).ToList() }, JsonRequestBehavior.AllowGet);
        }
//===========================================================================
        public ActionResult treasuriesOperations()
        {
            return View();
        }
//===========================================================================
        
	}
}