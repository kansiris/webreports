using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using report_ankapur.Models;
using System.Web.Routing;
using System.Web.Caching;
using System.Globalization;
using System.IO;
using Newtonsoft.Json;
using System.Web.Security;
//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using iTextSharp.tool.xml;
//using iTextSharp.text.html.simpleparser;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Spreadsheet;
using ClosedXML;
using ClosedXML.Excel;
using System.Reflection;

namespace report_ankapur.Controllers
{
    public class dailyReportsController : Controller
    {
        string restcode1;
        decimal TotalPrice;
        decimal Deliverycharges;
        decimal sgstcharges ;
        decimal cgstcharges ;
        decimal revenue ;
        decimal PriceSum;
        decimal tips;
        decimal discounts;
        // GET: Reports
        public ActionResult Index()
        {
            //try
            //{
            //Ankapurservices objCrd = new Ankapurservices();
            //ViewBag.modelCust = objCrd.Getdailyreports("01/09/2018");
            return View();
            //}
            //        catch (Exception)
            //        {
            //        return Content("<script language='javascript' type='text/javascript'>alert('Restaurant is closed at the moment');location.href='" + @Url.Action("Index", "dailyReports") + "'</script>");
            //    }
        }


        public JsonResult ajaxcall()
        {
            return Json("success");
        }
       
        public ActionResult Dailyreport(DateTime DeliverTime, string restcode)
        {
          
            if (DeliverTime != null)
            {
                try
                {
                    string DeliverTime1 = Convert.ToDateTime(DeliverTime).ToString("dd/MM/yyyy");


                Ankapurservices objCrd = new Ankapurservices();
                    var modelCust = objCrd.Getdailyreports(DeliverTime, restcode);
                ViewBag.modelCust = modelCust.Count;
                foreach (var reports in modelCust) 
                {
                    string amount = reports.amountPaid;

                    string total = reports.TotalPrice;
                    if (total == null || total == "")
                    {
                        total = "0";
                    }
                        string tip = reports.Tip;
                        string disco = reports.Discount;
                        string cgst = reports.cgstcharges;
                    string sgst = reports.sgstcharges;
                    string deliverycharges = reports.Deliverycharges;
                        string status = reports.status;

                        if (status != "8")
                        {


                            TotalPrice = TotalPrice + decimal.Parse(total);
                            Deliverycharges = Deliverycharges + decimal.Parse(deliverycharges);
                            sgstcharges = sgstcharges + decimal.Parse(sgst);
                            cgstcharges = cgstcharges + decimal.Parse(cgst);
                            tips = tips + decimal.Parse(tip);
                            discounts = discounts + decimal.Parse(disco);

                            revenue = TotalPrice + Deliverycharges + sgstcharges + cgstcharges + tips - discounts;
                            PriceSum = PriceSum + decimal.Parse(amount);
                        }
                }
                    var r2 = String.Format(new CultureInfo("en-IN", false), "{0:n}", Convert.ToDouble(revenue));
                    var p2 = String.Format(new CultureInfo("en-IN", false), "{0:n}", Convert.ToDouble(PriceSum));
                    ViewBag.revenue = r2;
                ViewBag.Pricesum = p2;
                ViewBag.selectdate = DeliverTime1;
                    ViewBag.selectdate1 = DeliverTime.ToString("yyyy/MM/dd").Replace('-', '/');
                    ViewBag.restcode = restcode;

                    if (restcode == "HN")
                    {
                        restcode1 = "Himayath Nagar";
                    }
                    else if (restcode == "KP")
                    {
                        restcode1 = "Kukatpally";
                    }
                    else if (restcode == "AN")
                    {
                        restcode1 = "A.S.Rao Nagar";
                    }
                    ViewBag.restcode1 = restcode1;
                    return PartialView("dailyreport", modelCust);
                }
                catch (Exception)
                {
                    return Content("<script language='javascript' type='text/javascript'>alert('Restaurant is closed at the moment');location.href='" + @Url.Action("Index", "dailyReports") + "'</script>");

                }
            }
            return PartialView("dailyreport", null);
        }

       // //excel download
        //[HttpPost]
        //[ValidateInput(false)]
        //public FileResult Export(string GridHtml)
        //{

        //    if (GridHtml != null)
        //    {
        //        using (MemoryStream stream = new System.IO.MemoryStream())
        //        {
        //            StringReader sr = new StringReader(GridHtml);
        //            //Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
        //            Document pdfDoc = new Document(PageSize.A2, 15, 15, 35, 25);



        //            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
        //            pdfDoc.Open();
        //            XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
        //            pdfDoc.Close();
        //            return File(stream.ToArray(), "application/pdf", "Ankapurchicken.pdf");
        //        }
        //    }
        //    return null;

        //}




        //public FileResult downloaddaily(string DeliverTime)
        //{

        //    if (DeliverTime != null)
        //    {
        //        using (XLWorkbook wb = new XLWorkbook())
        //        {
        //            Ankapurservices objCrd = new Ankapurservices();
        //            var modelCust = objCrd.Getdailyreports(DeliverTime);

        //            ListtoDataTable lsttodt = new ListtoDataTable();
        //            DataTable dt = lsttodt.ToDataTable(modelCust);
        //            wb.Worksheets.Add(dt);
        //            using (MemoryStream stream = new MemoryStream())
        //            {
        //                wb.SaveAs(stream);
        //                return File(stream.ToArray(), "application/.sheet", "Grid.xlsx");
        //            }
        //        }

        //    }

        //    return null;

        //}

        //excel download
        public ActionResult downloaddaily(DateTime DeliverTime ,string restcode)
        {
            if (DeliverTime != null)
            {
                //ViewBag.Message = "Your application description page.";
                Ankapurservices objCrd = new Ankapurservices();
                var modelCust1 = objCrd.Getdailyreports(DeliverTime, restcode);
                var gv = new GridView();
                gv.DataSource = modelCust1;
                gv.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=ankapurchickenDaily.xls");
                Response.ContentType = "application/ms-excel";
                Response.Charset = "";
                StringWriter objStringWriter = new StringWriter();
                HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);

                gv.RenderControl(objHtmlTextWriter);
                Response.Output.Write(objStringWriter.ToString());
                Response.Flush();
                Response.End();
                return RedirectToAction("Index", "dailyReport");
            }
        

            return Content ("<script> alert('please select date')</script>");

            }

    //public class ListtoDataTable
    //    {
    //        public DataTable ToDataTable<T>(List<T> items)
    //        {
    //            DataTable dataTable = new DataTable(typeof(T).Name);
    //            //Get all the properties by using reflection   
    //            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
    //            foreach (PropertyInfo prop in Props)
    //            {
    //                //Setting column names as Property names  
    //                dataTable.Columns.Add(prop.Name);
    //            }
    //            foreach (T item in items)
    //            {
    //                var values = new object[Props.Length];
    //                for (int i = 0; i < Props.Length; i++)
    //                {

    //                    values[i] = Props[i].GetValue(item, null);
    //                }
    //                dataTable.Rows.Add(values);
    //            }

    //            return dataTable;
    //        }
    //    }
    }

}
     

           