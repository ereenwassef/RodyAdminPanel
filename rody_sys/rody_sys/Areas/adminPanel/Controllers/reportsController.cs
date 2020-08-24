using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using LinqToExcel;
using System.Data.SqlClient;
using System.Web.UI;
using System.Configuration;
using Microsoft.Reporting.WebForms;
using System.Web.Helpers;
using System.Globalization;
using rody_sys.Areas.adminPanel.Models;


namespace rody_sys.Areas.adminPanel.Controllers
{
    public class reportsController : generalController
    {

        public FileContentResult genReport_recorded<T>(int id,List<T> list, string report_name, string dataset, string title, bool format)
        {       
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reports/" + report_name + ".rdlc");

//----------------------------------------------------------------------------------
            reportClientTahseel rpt = DB.reportClientTahseel.Where(r => r.id == id).Single();

            ReportParameter[] para = new ReportParameter[7];
            para[0] = new ReportParameter();
            para[0].Name = "cName";
            para[0].Values.Add(rpt.cltName.Replace("   ", ""));

            para[1] = new ReportParameter();
            para[1].Name = "areaName";
            para[1].Values.Add(rpt.cltAreaName.Replace("   ", ""));

            para[2] = new ReportParameter();
            para[2].Name = "address";
            para[2].Values.Add(rpt.cltAddress.Replace("   ", ""));

            para[3] = new ReportParameter();
            para[3].Name = "phone1";
            para[3].Values.Add(rpt.phone1.Replace("   ", ""));

            para[4] = new ReportParameter();
            para[4].Name = "charge";
            para[4].Values.Add(rpt.charge.ToString());

            para[5] = new ReportParameter();
            para[5].Name = "deserved";
            para[5].Values.Add(rpt.diserved.ToString());

            para[6] = new ReportParameter();
            para[6].Name = "printDate";
            para[6].Values.Add(DateTime.Now.ToString("dd/MM/yy - hh:mm:ss"));
 //----------------------------------------------------------------------------------
             localReport.SetParameters(para);

            ReportDataSource reportDataSource = new ReportDataSource(dataset, list);

            Dictionary<string, string> ps = new Dictionary<string, string>();
            ps.Add("date", DateTime.Now.ToString());
            CultureInfo cf = new CultureInfo("ar-KW");

            ReportParameter dt = new ReportParameter("date", String.Format(cf, "{0:g}", DateTime.Now));

            localReport.DataSources.Add(reportDataSource);
            string reportType = format ? "PDF" : "Excel";
            string mimeType;
            string encoding;
            string fileNameExtension;

            string deviceInfo =
            "<DeviceInfo>" +
            "  <OutputFormat>" + (format ? "PDF" : "Excel") + "</OutputFormat>" +
            "  <PageWidth>10.5in</PageWidth>" +
            "  <PageHeight>0.0in</PageHeight>" +
            "  <MarginTop>0.0in</MarginTop>" +
            "  <MarginLeft>0.5in</MarginLeft>" +
            "  <MarginRight>0.5in</MarginRight>" +
            "  <MarginBottom>0.0in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;


            renderedBytes = localReport.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            return File(renderedBytes, mimeType);
        }
//=========================================================    
        [ActionName("اذن استلام-excel")]
        public FileContentResult Export_to_excel_clientTahseel(int rptId2)
        {
            List<agent> agList = DB.agent.ToList();
            return genReport_recorded(rptId2,agList, "clientTahseel", "DataSet1", " ", false);
        }
//=========================================================
         [ActionName("اذن استلام-pdf")]
        public FileContentResult Export_to_pdf_clientTahseel(int rptId)
        {
            var agList = DB.agent.ToList();
            return genReport_recorded(rptId,agList, "clientTahseel", "DataSet1", " ", true);
        }
//=========================================================   
         [HttpGet]
         public JsonResult get_all_permissions()
         {
             //not distributed yet
             return Json(new { allPermissions = DB.reportClientTahseel.Where(y=>y.treasuryId==null).Select(x => new {x.id,x.cltId ,x.count,x.cltName,x.charge,x.diserved}).ToList() }, JsonRequestBehavior.AllowGet);
         }

//=========================================================
         [HttpPost]
         public JsonResult add_permissions(int cltId)
         {
             try
             {

                 reportClientTahseel rt = DB.reportClientTahseel.Where(x => x.cltId == cltId && x.treasuryId == null).SingleOrDefault();
                 if (rt == null)
                 {

                     DateTime current_date = DateTime.Now;
                     DateTime next_of_current_date = DateTime.Now.AddDays(1);//bokra
                     double debtor = (double)DB.operations_client.ToList().Where(c => DateTime.Parse(c.get_date, new CultureInfo("ar-AE")) <= next_of_current_date && c.clientId == cltId).Sum(x => x.debtor);
                     double creditor = (double)DB.operations_client.ToList().Where(c => c.clientId == cltId).Sum(x => x.creditor);
                     double deserved = Math.Round(debtor - creditor, 5);

                     reportClientTahseel rep = new reportClientTahseel();
                     rep.cltId = cltId;
                     client ct = DB.client.Where(c => c.id == cltId).Single();
                     rep.cltName = ct.name;
                     rep.charge = ct.charge;
                     rep.diserved = deserved;
                     rep.cltAddress = ct.stay_place_get;
                     rep.cltAreaName = ct.area.name;

                     List<phones> phList = DB.phones.Where(p => p.clientId == cltId).ToList();
                     if (phList.Count == 3)
                     {
                         rep.phone1 = phList[0].number;
                         rep.phone2 = phList[1].number;
                         rep.phone1 = phList[2].number;
                     }
                     else if (phList.Count == 2)
                     {
                         rep.phone1 = phList[0].number;
                         rep.phone2 = phList[1].number;
                     }
                     else if (phList.Count == 1)
                     {
                         rep.phone1 = phList[0].number;
                     }

                     rep.count = DB.reportClientTahseel.Where(r => r.cltId == cltId).ToList().Count + 1;

                     DB.reportClientTahseel.Add(rep);
                     DB.SaveChanges();

                     return Json(new { msg = "تمت الاضافه بنجاح" });
                 }
                 else
                 {
                     return Json(new { msg = "تمت اضافه هذا العميل من قبل.." });
                 }
             }
             catch
             {
                 return Json(new { msg = "لم تتم عمليه الاضافه..حاول مره اخري" });
             }
         }
//===========================================================
         [HttpPost]
         public JsonResult delete_clt_permission(int id)
         {
             try
             {
              reportClientTahseel rpt = DB.reportClientTahseel.Where(r =>r.id==id).Single();
              DB.reportClientTahseel.Remove(rpt);
              DB.SaveChanges();
              return Json(new { msg = "تمت عمليه الحذف بنجاح" });
             }catch
             {
                 return Json(new { msg = "لم تتم عمليه الحذف .. حاول مره اخري" });
             }
         }
//==========================================================

	}
}