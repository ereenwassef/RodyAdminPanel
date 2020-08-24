using rody_sys.Areas.adminPanel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace rody_sys.Areas.adminPanel.Controllers
{
    public class purchasesController : generalController
    {
        
        public ActionResult getAllPurchases()
        {
            return View();
        }
//=================================================
        [HttpGet]
        public JsonResult get_all_purchases()
        {
            return Json(new { allPurchases = DB.purchases.Select(p=>new {p.id,p.date,supId=p.supplierId,supName=p.supplier.name,shId=p.shareha.id ,shName=p.shareha.name,p.quantity,p.price,p.total,p.discount,p.total_after,p.clientId,cName=p.client.name}).ToList()}, JsonRequestBehavior.AllowGet);
        }
//================================================
        //public ActionResult getAllPurchasesOfSupplier()
        //{
        //    return View();
        //}
//================================================
        //[HttpGet]
        //public JsonResult get_all_purchases_of_supplier(int supId)
        //{
        //    return Json(new { allPurchases = DB.purchases.Where(p=>p.supplierId==supId).Select(p => new { p.id, p.date, supId = p.supplier.id, supName = p.supplier.name, shId = p.total_charge.id, shName = p.total_charge.charge_type, p.quantity, p.price, p.total, p.discount, p.total_after }).ToList() }, JsonRequestBehavior.AllowGet);
        //}
//================================================
        [HttpGet]
        public ActionResult addPurchaseOperation()
        {
            return View();
        }
//================================================

        public int add_purchase_operation(string date, int supplierId, int clientId, int shareehaId, float quantity, float price, float discount)
        {
            purchases p = new purchases();

            if (date != null&& date!="")
            {
                p.date = date;
                p.date = DateTime.Parse(date).ToString("dd/MM/yyyy");
            }
            else
            {
                p.date = DateTime.Now.ToString("dd/MM/yyyy");
            }

            if(clientId==0)
            {
                p.supplierId = supplierId;
            }
            else
            {
                p.clientId = clientId;
            }
            p.shareehaId = shareehaId;
            p.quantity = Math.Round(quantity, 5);
            p.price = Math.Round(price, 5);
            p.total =Math.Round( quantity * price,5);
            p.discount = Math.Round(discount, 5);
            p.total_after = Math.Round(((quantity * price) - discount),5);

            DB.purchases.Add(p);
            DB.SaveChanges();

            return p.id;
        }
//===============================================
        public void increase_supplier_charge(int supplierId,float value)
        {

            supplier sup = DB.supplier.Where(s => s.id == supplierId).Single();

            sup.charge += value;
            DB.Entry(sup).State = EntityState.Modified;
            DB.SaveChanges();
        }
//===============================================
        public void increase_shareha_charge(int shareehaId, float value)
        {

            shareha sh = DB.shareha.Where(s => s.id == shareehaId).Single();
            sh.value += value;            
            DB.Entry(sh).State = EntityState.Modified;
            DB.SaveChanges();
        }
//===============================================
        public void add_supplier_operation(string date,int supplierId,float creditorValue)
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
            op.opType = "مشتريات";
            op.supId = supplierId;
            op.creditor = creditorValue;
            op.charg = DB.supplier.Where(s => s.id == supplierId).Single().charge;
            DB.operations_supplier.Add(op);
            DB.SaveChanges();

        }
//================================================
        enum businessTypeEnum { اجل = 1, نقدي = 2 };
        enum sellerTypeEnum {supplier=1, client=2 };

        [HttpPost]
        public JsonResult add_purchase(int? supplierId, int? clientId, int shareehaId, float quantity, float price, float discount, int storeId, float? payValue, int sellerType, int businessType, string date="")
        {
            int purchaseOperationId = 0;

            // client | supplier (نقدي|اجل)
            using (var purchaseTransaction = DB.Database.BeginTransaction())
            {
                try
                {
                    treasuriesController t = new treasuriesController();//to can use some methods

                    float purchaseValue = (float)Math.Round(((quantity * price) - discount), 5);

                    if (sellerType == (int)sellerTypeEnum.client)
                    {

                      purchaseOperationId = add_purchase_operation(date, supplierId ?? 0, clientId ?? 0, shareehaId, quantity, price, discount);

                        increase_shareha_charge(shareehaId, quantity);

                        t.decrease_charge_of_client(clientId ?? 0, purchaseValue);

                        t.add_operation_client_get(clientId ?? 0, purchaseValue, "", date);

                        t.omolat_distribution(clientId ?? 0, purchaseValue, date);
                    }
                    else if (sellerType == (int)sellerTypeEnum.supplier)
                    {

                        purchaseOperationId = add_purchase_operation(date, supplierId ?? 0, clientId ?? 0, shareehaId, quantity, price, discount);

                        increase_shareha_charge(shareehaId, quantity);

                        increase_supplier_charge(supplierId ?? 0, purchaseValue);

                        add_supplier_operation(date, supplierId ?? 0, purchaseValue);

                        if (businessType == (int)businessTypeEnum.نقدي)
                        {
                            if (payValue > t.get_treasury_value(storeId))
                            {
                                return Json(new { msg = "قيمه الخزنه لا تكفي لاتمام عمليه الشراء..حاول مره اخري" });
                            }
                            else
                            {
                                //t.add_new_give_supplier(supplierId ?? 0, storeId, payValue ?? 0, date);
                                //stored procedure ...
                                 if (date != null && date!="")
                                 {
                                     string dt = DateTime.Parse(date).ToString("MM/dd/yyyy");
                                     DB.call_add_new_give_supplier(supplierId ?? 0, storeId, payValue ?? 0, dt);



                                 }else
                                 {
                                     DB.call_add_new_give_supplier(supplierId ?? 0, storeId, payValue ?? 0, date);
                                 }            
                               
                            }

                        }
                    }
                    purchaseTransaction.Commit();

                    addAdminOperation("w", purchaseOperationId , "purchases", "اضافه حركه مشتريات");

                    return Json(new { msg = "تمت اضافه عمليه الشراء بنجاح" });
                }
                catch
                {
                    purchaseTransaction.Rollback();
                    return Json(new { msg = "لم تتم عمليه الاضافه .. حاول مره اخري" });
                }
               
            }
        }
//==============================================================================================

	}
}