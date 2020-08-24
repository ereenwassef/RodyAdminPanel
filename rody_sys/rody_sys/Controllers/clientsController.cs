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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace rody_sys.Areas.adminPanel.Controllers
{
    public class clientsController : generalController
    {

        [HttpGet]
        public ActionResult getAllClients()
        {
            return View();
        }
//=========================================
        [HttpGet]
        public JsonResult get_all_clients(int page=0)
        {
            double count=DB.client.ToList().Count / 10.00;
            double pCount=Math.Ceiling(count);

            return Json(new
            {
                allClients = DB.client.Select(d => new
                {
                    d.id,
                    d.charge,
                    d.name,
                    type = d.client_type1.type,
                    d.contract_date,
                    d.activation_date,
                    govern = d.area.govern.name,
                    governId = d.area.govId,
                    areaName = d.area.name,
                    areaId = d.areaId,
                    d.stay_place_get,
                    d.num_followers,
                    d.parentId,
                    pName = DB.client.Where(x => x.id == d.parentId).FirstOrDefault().name ??"",
                    d.jop,
                    d.date_of_birth,
                    d.id_num,
                    d.id_place,
                    d.work_name,
                    d.work_place,
                    d.stay_place,
                    d.stay_place_floor,
                    d.stay_place_flat,
                    d.delegatorId,
                    d.delegator.agentId,
                    d.available,
                    d.notes,
                    d.MsgCharge,
                    d.done,
                    phones = d.phones.Select(p => new { p.number, p.shareha.name }).ToList()
                }).OrderBy(d => d.id).Skip(page * 10).Take(10).ToList(),
                pageCount = pCount
            } , JsonRequestBehavior.AllowGet);
        }
//================================================================================
        [HttpGet]
        public JsonResult get_all_clients2(int id = 0, string cName = "", string phoneNum = "" , string xyz="")
        {

            if (phoneNum != "")
            {
                return Json(new
                {
                    allClients = DB.phones.Where(p => p.number.Contains(phoneNum)).Select(d => new
                    {
                        d.number,
                    d.client.id,
                    d.client.name,
                    type = d.client.client_type1.type,
                    d.client.contract_date,
                    d.client.activation_date,
                    govern = d.client.area.govern.name,
                    governId = d.client.area.govId,
                    areaName = d.client.area.name,
                    areaId = d.client.areaId,
                    d.client.stay_place_get,
                    d.client.num_followers,
                    d.client.parentId,
                    }).ToList(),
                    phId = DB.phones.Where(ph => ph.number.Contains(phoneNum)).SingleOrDefault().id,
                    phSharehaId = DB.phones.Where(ph => ph.number.Contains(phoneNum)).SingleOrDefault().sharehaId,
                    phShareha = DB.phones.Where(ph => ph.number.Contains(phoneNum)).SingleOrDefault().shareha.name
                }, JsonRequestBehavior.AllowGet);

                //return Json(new
                //{
                //    allClients = DB.client.Select(d => new
                //    {
                //        d.id,
                //        d.charge,
                //        d.name,
                //        type = d.client_type1.type,
                //        d.contract_date,
                //        d.activation_date,
                //        govern = d.area.govern.name,
                //        governId = d.area.govId,
                //        areaName = d.area.name,
                //        areaId = d.areaId,
                //        d.stay_place_get,
                //        d.num_followers,
                //        d.parentId,
                //        pName = DB.client.Where(x => x.id == d.parentId).FirstOrDefault().name ?? "",
                //        d.jop,
                //        d.date_of_birth,
                //        d.id_num,
                //        d.id_place,
                //        d.work_name,
                //        d.work_place,
                //        d.stay_place,
                //        d.stay_place_floor,
                //        d.stay_place_flat,
                //        d.delegatorId,
                //        d.delegator.agentId,
                //        d.available,
                //        d.notes,
                //        d.MsgCharge,
                //        d.done,
                //        phones= d.phones.Select(p => new { p.number, p.shareha.name }).ToList()
                //    }).OrderBy(d => d.id).Where(u => u.phones.Where(p=>p.number.Contains(phoneNum))).ToList(),
                //}, JsonRequestBehavior.AllowGet);
            }
            else
            {

                if (id == 0 && (cName == "" || cName == null))
                {
                    return Json(new
                    {
                        allClients = DB.client.Select(d => new
                        {
                            d.id,
                            d.charge,
                            d.name,
                            type = d.client_type1.type,
                            d.contract_date,
                            d.activation_date,
                            govern = d.area.govern.name,
                            governId = d.area.govId,
                            areaName = d.area.name,
                            areaId = d.areaId,
                            d.stay_place_get,
                            d.num_followers,
                            d.parentId,
                            pName = DB.client.Where(x => x.id == d.parentId).FirstOrDefault().name ?? "",
                            d.jop,
                            d.date_of_birth,
                            d.id_num,
                            d.id_place,
                            d.work_name,
                            d.work_place,
                            d.stay_place,
                            d.stay_place_floor,
                            d.stay_place_flat,
                            d.delegatorId,
                            d.delegator.agentId,
                            d.available,
                            d.notes,
                            d.MsgCharge,
                            d.done,
                            phones = d.phones.Select(p => new { p.number, p.shareha.name }).ToList()
                        }).OrderBy(d => d.id).Take(5).ToList(),
                    }, JsonRequestBehavior.AllowGet);
                }
                else if (id != 0 && (cName == "" || cName == null))
                {
                    return Json(new
                    {
                        allClients = DB.client.Select(d => new
                        {
                            d.id,
                            d.charge,
                            d.name,
                            type = d.client_type1.type,
                            d.contract_date,
                            d.activation_date,
                            govern = d.area.govern.name,
                            governId = d.area.govId,
                            areaName = d.area.name,
                            areaId = d.areaId,
                            d.stay_place_get,
                            d.num_followers,
                            d.parentId,
                            pName = DB.client.Where(x => x.id == d.parentId).FirstOrDefault().name ?? "",
                            d.jop,
                            d.date_of_birth,
                            d.id_num,
                            d.id_place,
                            d.work_name,
                            d.work_place,
                            d.stay_place,
                            d.stay_place_floor,
                            d.stay_place_flat,
                            d.delegatorId,
                            d.delegator.agentId,
                            d.available,
                            d.notes,
                            d.MsgCharge,
                            d.done,
                            phones = d.phones.Select(p => new { p.number, p.shareha.name }).ToList()
                        }).OrderBy(d => d.id).Where(u => u.id == id).ToList(),
                    }, JsonRequestBehavior.AllowGet);
                }
                else if (id == 0 && (cName != "" || cName != null))
                {
                    return Json(new
                    {
                        allClients = DB.client.Select(d => new
                        {
                            d.id,
                            d.charge,
                            d.name,
                            type = d.client_type1.type,
                            d.contract_date,
                            d.activation_date,
                            govern = d.area.govern.name,
                            governId = d.area.govId,
                            areaName = d.area.name,
                            areaId = d.areaId,
                            d.stay_place_get,
                            d.num_followers,
                            d.parentId,
                            pName = DB.client.Where(x => x.id == d.parentId).FirstOrDefault().name ?? "",
                            d.jop,
                            d.date_of_birth,
                            d.id_num,
                            d.id_place,
                            d.work_name,
                            d.work_place,
                            d.stay_place,
                            d.stay_place_floor,
                            d.stay_place_flat,
                            d.delegatorId,
                            d.delegator.agentId,
                            d.available,
                            d.notes,
                            d.MsgCharge,
                            d.done,
                            phones = d.phones.Select(p => new { p.number, p.shareha.name }).ToList()
                        }).OrderBy(d => d.id).Where(u => u.name.Contains(cName)).ToList(),
                    }, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    return Json(new
                    {
                        allClients = DB.client.Select(d => new
                        {
                            d.id,
                            d.charge,
                            d.name,
                            type = d.client_type1.type,
                            d.contract_date,
                            d.activation_date,
                            govern = d.area.govern.name,
                            governId = d.area.govId,
                            areaName = d.area.name,
                            areaId = d.areaId,
                            d.stay_place_get,
                            d.num_followers,
                            d.parentId,
                            pName = DB.client.Where(x => x.id == d.parentId).FirstOrDefault().name ?? "",
                            d.jop,
                            d.date_of_birth,
                            d.id_num,
                            d.id_place,
                            d.work_name,
                            d.work_place,
                            d.stay_place,
                            d.stay_place_floor,
                            d.stay_place_flat,
                            d.delegatorId,
                            d.delegator.agentId,
                            d.available,
                            d.notes,
                            d.MsgCharge,
                            d.done,
                            phones = d.phones.Select(p => new { p.number, p.shareha.name }).ToList()
                        }).OrderBy(d => d.id).Where(u => u.id == id && u.name.Contains(cName)).ToList(),
                    }, JsonRequestBehavior.AllowGet);
                }
            }
        }
//================================================================================
        [HttpGet]
        public JsonResult search_clients()
        {
            return Json(new
            {
                allClients = DB.client.Select(d => new
                {
                    d.id,
                    d.charge,
                    d.name,
                    type = d.client_type1.type,
                    d.contract_date,
                    d.activation_date,
                    govern = d.area.govern.name,
                    governId = d.area.govId,
                    areaName = d.area.name,
                    areaId = d.areaId,
                    d.stay_place_get,
                    d.num_followers,
                    d.parentId,
                    pName = DB.client.Where(x => x.id == d.parentId).FirstOrDefault().name ?? "",
                    d.jop,
                    d.date_of_birth,
                    d.id_num,
                    d.id_place,
                    d.work_name,
                    d.work_place,
                    d.stay_place,
                    d.stay_place_floor,
                    d.stay_place_flat,
                    d.delegatorId,
                    d.delegator.agentId,
                    d.available,
                    d.notes,
                    d.MsgCharge,
                    d.done,
                    phones = d.phones.Select(p => new { p.number, p.shareha.name }).ToList()
                }).OrderBy(d => d.id).ToList(),
            }, JsonRequestBehavior.AllowGet);
        }
//=================================================================================
        [HttpPost]
        public JsonResult make_done(int clientId)
        {
            try
            {
                client c = DB.client.Where(x => x.id == clientId).Single();
                c.done = 1;
                DB.Entry(c).State = EntityState.Modified;
                DB.SaveChanges();

                return Json(new { msg = "تمت التعاقد بنجاح" });
            }
            catch
            {

                return Json(new { msg = "لم تتم عمليه التعاقد .. حاول مره اخري" });
            }
        }
//==================================================================================
        [HttpGet]
        public JsonResult get_client_phones(int cId)
        {
            return Json(new { phones = DB.phones.Where(c => c.clientId == cId).Select(p => new {p.number,p.sharehaId }).ToList() }, JsonRequestBehavior.AllowGet);
        }
//==================================================================================
        public treeNode build_tree(int cId)
        {
            client clt = DB.client.Where(c => c.id == cId).Single();

            //root
            treeNode cTree = new treeNode();
            cTree.id = clt.id;
            cTree.name = clt.name;
            cTree.Numfollow = (int)clt.num_followers;

            if (clt.my_childs != null)
            {
                string myChildsIds = clt.my_childs;
                List<string> myChildsIds_List = myChildsIds.Split(',').ToList();

                foreach (string childId in myChildsIds_List)
                {
                    int child_id = Int32.Parse(childId);

                    treeNode node = new treeNode();

                    node.id = child_id;

                    client ct=DB.client.Where(c => c.id == child_id).Single();
                    node.name = ct.name;
                    node.Numfollow = (int)ct.num_followers;

                    cTree.childs.Add(node);

                    complete_tree(node);//to can fill rest of the tree

                }
            }

          return cTree;
        }
//==================================================================================        
        public void complete_tree(treeNode child)
        {

            client clt = DB.client.Where(c => c.id == child.id).Single();

            if (clt.my_childs != null)
            {
                string myChildsIds = clt.my_childs;
                List<string> myChildsIds_List = myChildsIds.Split(',').ToList();

                foreach (string childId in myChildsIds_List)
                {
                    int child_id = Int32.Parse(childId);

                    treeNode node = new treeNode();

                    node.id = child_id;

                    client ct=DB.client.Where(c => c.id == child_id).Single();

                    node.name = ct.name;
                    node.Numfollow = (int)ct.num_followers;

                    child.childs.Add(node); //add child to the current node

                    complete_tree(node); //recursion

                }
            }

        }
//====================================================================================
        [HttpGet]
        public ActionResult clientTree(int cId)
        {
           treeNode clientTree= build_tree(cId);

           return View(clientTree);
        }
//===================================================================================
        [HttpGet]
        public ActionResult clientHistory(int id = 0)
        {

            ViewBag.id = id;
            try
            {
                ViewBag.name = DB.client.Where(c => c.id == id).SingleOrDefault().name;
            }
            catch
            {
                ViewBag.name = "";
            }

            return View();
        }
//=======================================================================================
        [HttpGet]
        public JsonResult get_all_client_operations(int id)
        {
            try
            {
                return Json(new
                {
                    allOperations = DB.operations_client.Where(c => c.clientId == id).Select(d => new { d.id, d.debtor, d.creditor, d.actual_date, d.get_date, d.time, dec = d.declaration, serviceName = d.services.name, type = "", phone = d.phones.number, charge = Math.Round((float)d.charge, 5), d.notes, d.otherPhoneNum }).ToList(),
                    clinetInfo = DB.client.Where(c => c.id == id).Select(d => new
                    {
                        d.id,
                        d.name,
                        d.contract_date,
                        d.activation_date,
                        governId = d.area.govId,
                        areaName = d.area.name,
                        areaId = d.areaId,
                        d.stay_place_get,
                        d.num_followers,
                        d.parentId,
                        pName = DB.client.Where(x => x.id == d.parentId).FirstOrDefault().name,
                        d.jop,
                        d.date_of_birth,
                        d.id_num,
                        d.id_place,
                        d.work_name,
                        d.work_place,
                        d.stay_place,
                        d.stay_place_floor,
                        d.stay_place_flat,
                        d.delegatorId,
                        d.delegator.agentId,
                        d.available,
                        d.notes,
                        d.MsgCharge
                    }).Single()
                }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                return Json(new { }, JsonRequestBehavior.AllowGet);
                
            }
        }
//======================================================================================
        public ActionResult clientsSummarization()
        {
            return View();
        }
//===================================================================================
        public ActionResult areasSummarization()
        {
            return View();
        }
//===================================================================================
        //[HttpGet]
        //public JsonResult get_areas_summarization(string sDate, string eDate)
        //{
        //   string summariseClientList= JsonConvert.SerializeObject(get_clients_summarization(sDate, eDate));

        //   List<JsonResult> summariseAreaList = new List<JsonResult>();

        //   List<area> areaList = DB.area.ToList();

        //   foreach (var obj in summariseClientList)
        //   {
        //       dynamic x = JValue.Parse(summariseClientList[0].ToString());
               
        //   }
            
        //   return Json(new { summariseAreasList = JValue.Parse(summariseClientList[0].ToString())}, JsonRequestBehavior.AllowGet);
        //}
//====================================================================================
        [HttpGet]
        public JsonResult get_clients_summarization(string sDate,string eDate)
        {
            List<client> cList=DB.client.ToList();
            List<JsonResult> summariseList = new List<JsonResult>();

            foreach (client c in cList)
            {
                JsonResult total = get_total_client_operations(c.id, sDate, eDate);
                summariseList.Add(total);
            }

            return Json(new { summariseList = summariseList, areaIds=DB.area.Select(x=>x.id).ToList() }, JsonRequestBehavior.AllowGet);
        }
//======================================================================================
        [HttpGet]
        public JsonResult get_total_client_operations(int cid,string startDate="",string endDate="")
        {
            string cName = DB.client.Where(c => c.id == cid).Single().name;
            int id = DB.client.Where(c => c.id == cid).Single().id;
            int areaId = DB.client.Where(c => c.id == cid).Single().area.id;
            string areaName= DB.client.Where(c => c.id == cid).Single().area.name;

            try
            {
                if ((startDate != "undefined" && startDate != "") && (endDate == "undefined" || endDate == "" || endDate==null))
                {
                    DateTime SDate = DateTime.Parse(startDate, new CultureInfo("ar-AE"));

                    double total_sell = (double)DB.operations_client.ToList().Where(c => DateTime.Parse(c.actual_date, new CultureInfo("ar-AE")) >= SDate && c.clientId == cid && c.declaration.Contains("-")).Sum(x => x.debtor);

                    double badal_tahseel = (double)DB.operations_client.ToList().Where(c => DateTime.Parse(c.actual_date, new CultureInfo("ar-AE")) >= SDate && c.clientId == cid && c.declaration.Contains("بدل تحصيل")).Sum(c => c.debtor);

                    double tahseel = (double)DB.operations_client.ToList().Where(c => DateTime.Parse(c.actual_date, new CultureInfo("ar-AE")) >= SDate && c.clientId == cid && c.declaration.Contains("استلام نقديه")).Sum(c => c.creditor);

                    double amola = (double)DB.operations_client.ToList().Where(c => DateTime.Parse(c.actual_date, new CultureInfo("ar-AE")) >= SDate && c.clientId == cid && c.declaration.Contains("عموله مستحقه")).Sum(c => c.creditor);

                    double actual_charge = (double)DB.operations_client.ToList().Where(c => DateTime.Parse(c.actual_date, new CultureInfo("ar-AE")) >= SDate && c.clientId == cid).LastOrDefault().charge;

                    double actual_charge_start = (double)DB.operations_client.ToList().Where(c => DateTime.Parse(c.actual_date, new CultureInfo("ar-AE")) >= SDate && c.clientId == cid).FirstOrDefault().charge;

                    double deserved = get_deserved(cid, "");

                    return Json(new { id = id, cName = cName, areaId = areaId, areaName = areaName, sales = total_sell, badal_tahseel = badal_tahseel, tahseel = tahseel, amola = Math.Round(amola, 5), actual_charge = Math.Round(actual_charge, 5), deserved = deserved, actual_charge_start = actual_charge_start }, JsonRequestBehavior.AllowGet);

                }
                else if ((startDate == "undefined" || startDate == "") && (endDate != "undefined" && endDate != ""))
                {
                    DateTime EDate = DateTime.Parse(endDate, new CultureInfo("ar-AE"));

                    double total_sell = (double)DB.operations_client.ToList().Where(c => DateTime.Parse(c.actual_date, new CultureInfo("ar-AE")) <= EDate && c.clientId == cid && c.declaration.Contains("-")).Sum(x => x.debtor);

                    double badal_tahseel = (double)DB.operations_client.ToList().Where(c => DateTime.Parse(c.actual_date, new CultureInfo("ar-AE")) <= EDate && c.clientId == cid && c.declaration.Contains("بدل تحصيل")).Sum(c => c.debtor);

                    double tahseel = (double)DB.operations_client.ToList().Where(c => DateTime.Parse(c.actual_date, new CultureInfo("ar-AE")) <= EDate && c.clientId == cid && c.declaration.Contains("استلام نقديه")).Sum(c => c.creditor);

                    double amola = (double)DB.operations_client.ToList().Where(c => DateTime.Parse(c.actual_date, new CultureInfo("ar-AE")) <= EDate && c.clientId == cid && c.declaration.Contains("عموله مستحقه")).Sum(c => c.creditor);

                    double actual_charge = (double)DB.operations_client.ToList().Where(c => DateTime.Parse(c.actual_date, new CultureInfo("ar-AE")) <= EDate && c.clientId == cid).LastOrDefault().charge;

                    double actual_charge_start = (double)DB.operations_client.ToList().Where(c => DateTime.Parse(c.actual_date, new CultureInfo("ar-AE")) <= EDate && c.clientId == cid).FirstOrDefault().charge;

                    double deserved = get_deserved(cid, endDate);

                    return Json(new { id = id, cName = cName, areaId = areaId, areaName = areaName, sales = total_sell, badal_tahseel = badal_tahseel, tahseel = tahseel, amola = Math.Round(amola, 5), actual_charge = Math.Round(actual_charge, 5), deserved = deserved, actual_charge_start = actual_charge_start }, JsonRequestBehavior.AllowGet);

                }
                else if ((startDate != "undefined" && startDate != "") && (endDate != "undefined" && endDate != ""))
                {
                    DateTime SDate = DateTime.Parse(startDate, new CultureInfo("ar-AE"));
                    DateTime EDate = DateTime.Parse(endDate, new CultureInfo("ar-AE"));

                    double total_sell = (double)DB.operations_client.ToList().Where(c => (DateTime.Parse(c.actual_date, new CultureInfo("ar-AE")) >= SDate && DateTime.Parse(c.actual_date, new CultureInfo("ar-AE")) <= EDate) && c.clientId == cid && c.declaration.Contains("-")).Sum(x => x.debtor);

                    double badal_tahseel = (double)DB.operations_client.ToList().Where(c => (DateTime.Parse(c.actual_date, new CultureInfo("ar-AE")) >= SDate && DateTime.Parse(c.actual_date, new CultureInfo("ar-AE")) <= EDate) && c.clientId == cid && c.declaration.Contains("بدل تحصيل")).Sum(c => c.debtor);

                    double tahseel = (double)DB.operations_client.ToList().Where(c => (DateTime.Parse(c.actual_date, new CultureInfo("ar-AE")) >= SDate && DateTime.Parse(c.actual_date, new CultureInfo("ar-AE")) <= EDate) && c.clientId == cid && c.declaration.Contains("استلام نقديه")).ToList().Sum(c => c.creditor);

                    double amola = (double)DB.operations_client.ToList().Where(c => (DateTime.Parse(c.actual_date, new CultureInfo("ar-AE")) >= SDate && DateTime.Parse(c.actual_date, new CultureInfo("ar-AE")) <= EDate) && c.clientId == cid && c.declaration.Contains("عموله مستحقه")).ToList().Sum(c => c.creditor);

                    double actual_charge = (double)DB.operations_client.ToList().Where(c => (DateTime.Parse(c.actual_date, new CultureInfo("ar-AE")) >= SDate && DateTime.Parse(c.actual_date, new CultureInfo("ar-AE")) <= EDate) && c.clientId == cid).LastOrDefault().charge;

                    double actual_charge_start = (double)DB.operations_client.ToList().Where(c => (DateTime.Parse(c.actual_date, new CultureInfo("ar-AE")) >= SDate && DateTime.Parse(c.actual_date, new CultureInfo("ar-AE")) <= EDate) && c.clientId == cid).FirstOrDefault().charge;

                    double deserved = get_deserved(cid, endDate);

                    return Json(new { id = id, cName = cName, areaId = areaId, areaName = areaName, sales = total_sell, badal_tahseel = badal_tahseel, tahseel = tahseel, amola = Math.Round(amola, 5), actual_charge = Math.Round(actual_charge, 5), deserved = deserved, actual_charge_start = actual_charge_start }, JsonRequestBehavior.AllowGet);

                }
                else
                {

                    double total_sell = (double)DB.operations_client.ToList().Where(c => c.clientId == cid && c.declaration.Contains("-")).ToList().Sum(x => x.debtor);

                    double badal_tahseel = (double)DB.operations_client.ToList().Where(c => c.clientId == cid && c.declaration.Contains("بدل تحصيل")).ToList().Sum(c => c.debtor);

                    double tahseel = (double)DB.operations_client.ToList().Where(c => c.clientId == cid && c.declaration.Contains("استلام نقديه")).ToList().Sum(c => c.creditor);

                    double amola = (double)DB.operations_client.ToList().Where(c => c.clientId == cid && c.declaration.Contains("عموله مستحقه")).ToList().Sum(c => c.creditor);

                    double actual_charge = (double)DB.operations_client.ToList().Where(c => c.clientId == cid).LastOrDefault().charge;

                    double actual_charge_start = (double)DB.operations_client.ToList().Where(c => c.clientId == cid).FirstOrDefault().charge;

                    double deserved = get_deserved(cid, "");

                    return Json(new { id = id, cName = cName, areaId = areaId, areaName = areaName, sales = total_sell, badal_tahseel = badal_tahseel, tahseel = tahseel, amola = Math.Round(amola, 5), actual_charge = Math.Round(actual_charge, 5), deserved = deserved, actual_charge_start = actual_charge_start }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(new { id = id, cName = cName, areaId = areaId, areaName = areaName, sales = "0", badal_tahseel = "0", tahseel = "0", amola = "0", actual_charge = "0", deserved = "0", actual_charge_start = "0" }, JsonRequestBehavior.AllowGet);
            }

        }
//==========================================
        [HttpGet]
        public ActionResult getAllClientsCharge()
        {
            return View();
        }
//=========================================
        public double get_deserved(int cid, string date)
        {

            if (date == "")
            {

                DateTime current_date = DateTime.Now;
                DateTime next_of_current_date = DateTime.Now.AddDays(1);//bokra

                double debtor = (double)DB.operations_client.ToList().Where(c => DateTime.Parse(c.get_date, new CultureInfo("ar-AE")) <= next_of_current_date && c.clientId == cid).Sum(x => x.debtor);
                double creditor = (double)DB.operations_client.ToList().Where(c => c.clientId == cid).Sum(x => x.creditor);
                double deserved = debtor - creditor;

                return Math.Round(deserved,5);
            }
            else
            {

                DateTime current_date = DateTime.Parse(date, new CultureInfo("ar-AE"));

                DateTime next_of_current_date = current_date.AddDays(1);//bokra

                double debtor = (double)DB.operations_client.ToList().Where(c => DateTime.Parse(c.get_date, new CultureInfo("ar-AE")) <= next_of_current_date && c.clientId == cid).Sum(x => x.debtor);
                double creditor = (double)DB.operations_client.ToList().Where(c => c.clientId == cid).Sum(x => x.creditor);
                double deserved = debtor - creditor;

                return Math.Round(deserved, 5);
            }
        }
//=========================================
        [HttpGet]
        public JsonResult get_clients_charge(string date)
        {
            List<client> cList = new List<client>();

            foreach (client clt in DB.client)
            {

                double deserved = get_deserved(clt.id, date);
                if (deserved != 0)
                {
                    client c = new client();
                    c.id = clt.id;
                    c.name = clt.name;
                    c.charge =  Math.Round((float)clt.charge,5);
                    c.notes = deserved.ToString(); //but deserved in notes
                    c.areaId = clt.areaId;
                    cList.Add(c);
                }
            }

            return Json(new { allClients = cList.Select(d => new { d.id, d.name,d.areaId, d.charge, d.notes }).ToList() }, JsonRequestBehavior.AllowGet);
        }
//==========================================
        public ActionResult clientTypes()
        {
            return View();
        }   
//==========================================
        public JsonResult get_all_types()
        {
            return Json(new { allTypes = DB.client_type.Select(t => new { t.id,t.type}).ToList() }, JsonRequestBehavior.AllowGet);
        }
//=========================================
        [HttpPost]
        public JsonResult add_type(string type)
        {
            try
            {
                client_type t = new client_type();
                t.type = type;
                DB.client_type.Add(t);
                DB.SaveChanges();
                
                addAdminOperation("w", t.id, "client_type","اضافه تصنيف جديد للعميل");

                return Json(new {msg="تمت عمليه الاضافه بنجاح" });
            }catch
            {
                return Json(new { msg = "لم تتم عمليه الاضافه..حاول مره اخري" });
            }           
        }
//==========================================

        [HttpPost]
        public JsonResult update_type(int id,string type)
        {
            try
            {
                client_type t = DB.client_type.Where(x => x.id == id).Single();
                t.type = type;

                DB.Entry(t).State = EntityState.Modified;
                DB.SaveChanges();

                addAdminOperation("u", t.id, "client_type", "تعديل تصنيف العميل");

                return Json(new { msg = "تمت عمليه التعديل بنجاح" });
            }
            catch
            {

                return Json(new { msg = "لم تتم عمليه التعديل..حاول مره اخري" });
            }
        }
//==========================================
        [HttpPost]
        public JsonResult delete_type(int id)
        {
            try
            {
                client_type t = DB.client_type.Where(x => x.id == id).Single();
                DB.client_type.Remove(t);
                DB.SaveChanges();

                addAdminOperation("d", t.id, "client_type", "حذف تصنيف العميل");

                return Json(new { msg = "تمت عمليه الحذف بنجاح" });
            }
            catch
            {

                return Json(new { msg = "لم تتم عمليه الحذف .. حاول مره اخري" });
            }
        }
//==========================================
        [HttpGet]
        public JsonResult getActivationDateOfClient(int id)
        {
                client t = DB.client.Where(x => x.id == id).Single();

                return Json(new { actDate = t.activation_date }, JsonRequestBehavior.AllowGet);
        }
//===========================================
        public void addPhones(int cId,string phone1, int shreha1, string phone2, int shreha2, string phone3, int shreha3)
        {
            if (phone1 != null)
            {
                phones ph1 = new phones();
                ph1.clientId = cId;
                ph1.number = phone1;
                ph1.sharehaId = shreha1;
                DB.phones.Add(ph1);
                DB.SaveChanges();
            }
            if (phone2 != null)
            {
                phones ph2 = new phones();
                ph2.clientId = cId;
                ph2.number = phone2;
                ph2.sharehaId = shreha2;
                DB.phones.Add(ph2);
                DB.SaveChanges();
            }
            if (phone3 != null)
            {
                phones ph3 = new phones();
                ph3.clientId = cId;
                ph3.number = phone3;
                ph3.sharehaId = shreha3;
                DB.phones.Add(ph3);
                DB.SaveChanges();
            }

        }
//===========================================
        public void updatePhones(int cId, string phone1, int shreha1, string phone2, int shreha2, string phone3, int shreha3)
        {
            bool first=false, second=false, third=false;

            List<phones> phoneList = DB.phones.Where(p => p.clientId == cId).ToList();
            if(phoneList.Count()==3)
            {
                first = true;
                second = true;
                third = true;
            }
            else if (phoneList.Count() == 2)
            {
                first = true;
                second = true;
            }
            else if (phoneList.Count() == 1)
            {
                first = true;
            }


            if (phone1 != "" && first)
            {
                phones ph1 = phoneList[0];
                ph1.number = phone1;
                ph1.sharehaId = shreha1;
                DB.Entry(ph1).State = EntityState.Modified;
                DB.SaveChanges();
            }
            else if (phone1 != "" && !first)
            {
                phones ph1 = new phones();
                ph1.clientId = cId;
                ph1.number = phone1;
                ph1.sharehaId = shreha1;
                DB.phones.Add(ph1);
                DB.SaveChanges();
            }


            if (phone2 != "" && second)
            {
                phones ph2 = phoneList[1];
                ph2.number = phone2;
                ph2.sharehaId = shreha2;
                DB.Entry(ph2).State = EntityState.Modified;
                DB.SaveChanges();
            }
            else if (phone2 != "" && !second)
            {
                phones ph2 = new phones();
                ph2.clientId = cId;
                ph2.number = phone2;
                ph2.sharehaId = shreha2;
                DB.phones.Add(ph2);
                DB.SaveChanges();
            }

            if (phone3 != "" && third)
            {
                phones ph3 = phoneList[2];
                ph3.number = phone3;
                ph3.sharehaId = shreha3;
                DB.Entry(ph3).State = EntityState.Modified;
                DB.SaveChanges();
            }
            else if (phone3 != "" && !third)
            {
                phones ph3 = new phones();
                ph3.clientId = cId;
                ph3.number = phone3;
                ph3.sharehaId = shreha3;
                DB.phones.Add(ph3);
                DB.SaveChanges();
            }      
        }
//===========================================
        [HttpGet]
        public JsonResult increase_num_of_follower(int parentId)
        {
            string parents = DB.client.Where(p=>p.id==parentId).Single().my_parents;
            List<string> pList = parents.Split(',').ToList();

            foreach (string p in pList)
            {
                if (p != "")
                {
                    int idc = Int32.Parse(p);
                    client clt = DB.client.Where(c => c.id == idc).Single();
                    clt.num_followers = clt.num_followers + 1;
                    DB.Entry(clt).State = EntityState.Modified;
                    DB.SaveChanges();
                }
            }

            return Json(new { pList = pList }, JsonRequestBehavior.AllowGet);

        }
//===========================================
        [HttpPost]
        public JsonResult add_client(string name,int gender,string idNum,string DOB,int cltTypeId,string jop,string workName,
        string workPlace,string idPlace,string stayPlace, int? floor , int? flat, string getPlace, int? areaId,int? delegatorId,
        string notes,float? chargeMsg,int parentId,string contractDate,
        string phone1, int shreha1, string phone2, int shreha2, string phone3, int shreha3)
        {

            client clt_found = DB.client.Where(x => x.id_num == idNum && idNum !=null).FirstOrDefault();
            if (clt_found == null)
            {
                using (var addClientTransaction = DB.Database.BeginTransaction())
                {
                    try
                    {
                        client myParent = DB.client.Where(c => c.id == parentId).Single();
                        increase_num_of_follower(myParent.id);

                        client clt = new client();
                        clt.name = name;
                        clt.gender = gender;
                        clt.id_num = idNum;
                        clt.date_of_birth = DOB;
                        clt.client_type = cltTypeId;
                        clt.jop = jop;
                        clt.work_name = workName;
                        clt.work_place = workPlace;
                        clt.id_place = idPlace;
                        clt.stay_place = stayPlace;
                        clt.stay_place_floor = floor;
                        clt.stay_place_flat = flat;
                        clt.stay_place_get = getPlace;
                        clt.areaId = areaId;
                        clt.delegatorId = delegatorId;
                        clt.available = 1;
                        clt.contract_state = 0;
                        clt.notes = notes;
                        clt.charge = 0;//initial charge
                        clt.done = 1; //tam el t3akod
                        clt.MsgCharge = chargeMsg;//charge of call me msg
                        clt.parentId = parentId;
                        clt.activation_date = "لم يتم التفعيل";
                        if (contractDate == null)
                        {
                            clt.contract_date = DateTime.Now.ToString("dd/MM/yyyy");
                        }
                        else
                        {
                            clt.contract_date = contractDate;
                        }
                        clt.num_followers = 0;
                        DB.client.Add(clt);
                        DB.SaveChanges();

                        clt.my_parents = clt.id + "," + myParent.my_parents;
                        DB.Entry(clt).State = EntityState.Modified;
                        DB.SaveChanges();

                        if (myParent.my_childs == null)
                        {
                            //first child
                            myParent.my_childs = clt.id.ToString();
                        }
                        else
                        {
                            myParent.my_childs += "," + clt.id.ToString();
                        }
                        DB.Entry(myParent).State = EntityState.Modified;
                        DB.SaveChanges();

                        addPhones(clt.id, phone1, shreha1, phone2, shreha2, phone3, shreha3);

                        //commit transaction if all opertaions is ok 
                        addClientTransaction.Commit();

                        addAdminOperation("w", clt.id, "client", "اضافه عميل جديد");

                        return Json(new { msg = "تم اضافه العميل بنجاح" });
                    }
                    catch
                    {
                        //rollback the transaction in case any exception occurs
                        addClientTransaction.Rollback();
                        return Json(new { msg = "لم تتم عمليه الاضافه..حاول مره اخري" });
                    }
                }
            }
            else
            {
                return Json(new { msg = "لم تتم الاضافه .. العميل موجود من قبل " });
            }
        }
//=============================================================================================
        [HttpPost]
        public JsonResult update_client(int cId,string name, string idNum, string jop, string workName,
        string workPlace, string idPlace, string stayPlace, int? floor, int? flat, string getPlace, int? areaId, int? delegatorId,
        string notes, float? chargeMsg, int? parentId,int available,
        string phone1, int shreha1, string phone2, int shreha2, string phone3, int shreha3)
        {
            using (var updateClientTransaction = DB.Database.BeginTransaction())
            {
                try
                {
                   // client myParent = DB.client.Where(c => c.id == parentId).Single();

                    client clt = DB.client.Where(c => c.id == cId).Single();
                    clt.name = name;
                    clt.id_num = idNum;
                    clt.jop = jop;
                    clt.work_name = workName;
                    clt.work_place = workPlace;
                    clt.id_place = idPlace;
                    clt.stay_place = stayPlace;
                    clt.stay_place_floor = floor;
                    clt.stay_place_flat = flat;
                    clt.stay_place_get = getPlace;
                    clt.areaId = areaId;
                    clt.delegatorId = delegatorId;
                    clt.available = available;
                    clt.notes = notes;
                    clt.MsgCharge = chargeMsg;//charge of call me msg
                    clt.parentId = parentId;
                   
                    DB.Entry(clt).State = EntityState.Modified;
                    DB.SaveChanges();

                    //clt.my_parents = clt.id + "," + myParent.my_parents;
                    //DB.Entry(clt).State = EntityState.Modified;
                    //DB.SaveChanges();

                    //if (myParent.my_childs == null)
                    //{
                    //    //first child
                    //    myParent.my_childs = clt.id.ToString();
                    //}
                    //else
                    //{
                    //    myParent.my_childs += "," + clt.id.ToString();
                    //}

                    //DB.Entry(myParent).State = EntityState.Modified;
                    //DB.SaveChanges();

                    updatePhones(cId, phone1, shreha1, phone2, shreha2, phone3, shreha3);

                    //commit transaction if all opertaions is ok 
                    updateClientTransaction.Commit();

                    addAdminOperation("u", clt.id, "client", "تعديل عميل");

                    return Json(new { msg = "تم تعديل بيانات العميل بنجاح" });
                }
                catch
                {
                    //rollback the transaction in case any exception occurs
                    updateClientTransaction.Rollback();
                    return Json(new { msg = "لم تتم عمليه التعديل .. حاول مره اخري" });
                }
            }
        }
//=============================================================================================
        [HttpPost]
        public JsonResult delete_client(int cId)
        {
            client ct = DB.client.Where(x => x.id == cId).Single();
            string chd = ct.my_childs;
            int op_count = DB.operations_client.Where(x => x.clientId == cId).ToList().Count();

            if (chd == null && op_count == 0)
            {
                try
                {
                    client clt = DB.client.Where(c => c.id == cId).Single();
                    int cParentId = (int)clt.parentId;
                    DB.client.Remove(clt);
                    DB.SaveChanges();

                    client parent = DB.client.Where(c => c.id == cParentId).Single();

                    string cParent_childs = parent.my_childs;
                    List<string> idList = cParent_childs.Split(',').ToList();
                    foreach (string id in idList.ToList())
                    {
                        if (id.Equals(cId.ToString()))
                        {
                            idList.Remove(id);
                        }
                    }

                    string new_ids = "";
                    foreach (string id in idList.ToList())
                    {
                        if (new_ids.Equals(""))
                        {
                            new_ids += id;
                        }
                        else
                        {
                            new_ids += "," + id;
                        }

                    }
                    if (new_ids.Equals(""))
                    {
                        parent.my_childs = null;
                    }
                    else
                    {
                        parent.my_childs = new_ids;
                    }

                    DB.Entry(parent).State = EntityState.Modified;
                    DB.SaveChanges();

                    List<string> myParents = clt.my_parents.Split(',').ToList();
                    for (int i = 1; i < myParents.Count;i++) //skip me
                    {
                        int id=Int32.Parse(myParents[i]);
                        client cit = DB.client.Where(c => c.id == id).Single();
                        cit.num_followers -= 1; //decrease num of followers

                        DB.Entry(cit).State = EntityState.Modified;
                        DB.SaveChanges();
                    }

                    addAdminOperation("d", clt.id, "client", "حذف عميل");

                    return Json(new { msg = "تم حذف العميل بنجاح" });
                }
               catch
                {
                    return Json(new { msg = "لم تتم عمليه الحذف .. حاول مره اخري" });
                }
            }
            else
            {
                return Json(new { msg = "لا يمكن حذف هذا العميل" });
            }
        }

//==================================================================
        [HttpGet]
        public JsonResult all_client_notes(int cId)
        {
            return Json(new { allNotes = DB.notes.Where(c => c.clientId == cId).Select(x => new { x.id,x.date,x.note}).ToList() }, JsonRequestBehavior.AllowGet);
        }
//==================================================================
        public JsonResult add_note(int cId,string note)
        {
            try
            {
                notes nt = new notes();
                nt.clientId = cId;
                nt.note = note;
                nt.date = DateTime.Now.ToString("dd/MM/yyyy");

                DB.notes.Add(nt);
                DB.SaveChanges();

                addAdminOperation("w", nt.id, "notes", "اضافه ملاحظه");

                return Json(new { msg = "تم اضافه الملاحظه بنجاح" });
            }catch
            {
                return Json(new { msg = "لم تتم الاضافه حاول مره اخري" });
            }
        }
//==================================================================
        public JsonResult delete_note(int noteId)
        {
            try
            {
                notes nt = DB.notes.Where(n => n.id == noteId).Single();
                DB.notes.Remove(nt);
                DB.SaveChanges();

                addAdminOperation("d", nt.id, "notes", "حذف ملاحظه");

                return Json(new { msg = "تم حذف الملاحظه بنجاح" });
            }
            catch
            {
                return Json(new { msg = "لم تتم عمليه الحذف حاول مره اخري" });
            }
        }

//==================================================================
        public ActionResult clientsMostahekElsadad()
        {
            return View();
        }
//=================================================================
        [HttpPost]
        public JsonResult add_phone_to_client(int cId, string phone , int sharehaId)
        {
            try
            {
                phones ph = new phones();
                ph.clientId = cId;
                ph.number = phone;
                ph.sharehaId = sharehaId;

                DB.phones.Add(ph);
                DB.SaveChanges();             
                
                return Json(new { msg = "تمت اضافه رقم التليفون بنجاح" });
            }
            catch
            {
                return Json(new { msg = "لم تتم عمليه الاضافه حاول مره اخري" });
            }
        }
//==================================================================
        [HttpGet]
        public JsonResult load_client_phones(int cId)
        {
            return Json(new { allClientPhones = DB.phones.Where(c => c.clientId == cId).Select(p => new { p.id, p.number, shareha = p.shareha.name, sharehaId=p.sharehaId }).ToList() }, JsonRequestBehavior.AllowGet);
        }
//==================================================================
        [HttpPost]
        public JsonResult update_phone_of_client(int phId, string phone, int sharehaId)
        {
            try
            {
                phones ph = DB.phones.Where(p => p.id == phId).Single();
                ph.number = phone;
                ph.sharehaId = sharehaId;

                DB.Entry(ph).State = EntityState.Modified;
                DB.SaveChanges();

                return Json(new { msg = "تمت تعديل رقم التليفون بنجاح" });
            }
            catch
            {
                return Json(new { msg = "لم تتم عمليه التعديل حاول مره اخري" });
            }
        }
//================================================================================

        public JsonResult delete_phone(int phId)
        {
            try
            {
                phones ph = DB.phones.Where(p => p.id == phId).Single();
                DB.phones.Remove(ph);
                DB.SaveChanges();

                return Json(new { msg = "تم حذف الرقم بنجاح" });
            }
            catch
            {
                return Json(new { msg = "لا يمكن حذف هذا الرقم" });
            }
        }












    }
}