using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.text.html.simpleparser;
using System.IO;
using System.Globalization;
using System.Web.UI;

namespace report_ankapur.Controllers
{
    public class rangeController : Controller
    {
        decimal TotalPrice, Deliverycharges , sgstcharges,cgstcharges,revenue,PriceSum; string restcode1; decimal tips; decimal discounts;
        // GET: range
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ajaxcall()
        {
            return Json("success");
        }

        public ActionResult rangereport(string DeliverTime, string DeliverTime1 , string restcode)
        {
            
            if (DeliverTime != null && DeliverTime1 != null)
            {
                try
                {
                    string[] formats = {"dd/MM/yyyy", "dd-MMM-yyyy", "yyyy-MM-dd",
                   "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy"};
                //            DateTime DelTime = Convert.ToDateTime(DeliverTime,
                //System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);

                //            DateTime DelTime1 = Convert.ToDateTime(DeliverTime1,
                //System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);

                //    string DeliverT = DateTime.ParseExact(DeliverTime, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("dd/MM/yyyy");
                //   string DeliverT1 = DateTime.ParseExact(DeliverTime1, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("dd/MM/yyyy");

                string DeliverT = Convert.ToDateTime(DeliverTime).ToString("dd/MM/yyyy");
                string DeliverT1 = Convert.ToDateTime(DeliverTime1).ToString("dd/MM/yyyy");
                Ankapurservices objCrd = new Ankapurservices();
                    var modelCust = objCrd.rangereports(DeliverTime,DeliverTime1,restcode);

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
                        TotalPrice = TotalPrice + decimal.Parse(total);
                        Deliverycharges = Deliverycharges + decimal.Parse(deliverycharges);
                        sgstcharges = sgstcharges + decimal.Parse(sgst);
                        cgstcharges = cgstcharges + decimal.Parse(cgst);
                    tips = tips + decimal.Parse(tip);
                    discounts = discounts + decimal.Parse(disco);
                    revenue = TotalPrice + Deliverycharges + sgstcharges + cgstcharges + tips - discounts;
                        PriceSum = PriceSum + decimal.Parse(amount);

                    }
                var r2 = String.Format(new CultureInfo("en-IN", false), "{0:n}", Convert.ToDouble(revenue));
                var p2 = String.Format(new CultureInfo("en-IN", false), "{0:n}", Convert.ToDouble(PriceSum));
                ViewBag.revenue = r2;
                    ViewBag.Pricesum = p2;
                    ViewBag.selectdate = DeliverT1;
                ViewBag.selectdate1 = DeliverT; ViewBag.restcode = restcode;

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
                return PartialView("rangereport", modelCust);
                }
                
                catch (Exception)
                {
                    return Content("<script language='javascript' type='text/javascript'>alert('Restaurant is closed at the moment');location.href='" + @Url.Action("Index", "range") + "'</script>");

                }
               

            }

            return PartialView("rangereport", null);
            
        }
        public ActionResult downloadrange(string DeliverTime, string DeliverTime1, string restcode)
        {
            if (DeliverTime == null)
            {
                return Content("<script language='javascript' type='text/javascript'>alert('Select date');location.href='" + @Url.Action("Index", "range") + "'</script>");

            }
            if (DeliverTime1 == null)
            {
                return Content("<script language='javascript' type='text/javascript'>alert('Select date');location.href='" + @Url.Action("Index", "range") + "'</script>");

            }

            if (restcode == null)
            {
                return Content("<script language='javascript' type='text/javascript'>alert('Select restaurant');location.href='" + @Url.Action("Index", "range") + "'</script>");

            }
            if (DeliverTime != null && DeliverTime1 != null && restcode != null)
            {
                //ViewBag.Message = "Your application description page.";
                Ankapurservices objCrd = new Ankapurservices();
                var modelCust = objCrd.rangereports(DeliverTime, DeliverTime1 , restcode);
                var gv = new System.Web.UI.WebControls.GridView();
                gv.DataSource = modelCust;
                gv.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=ankapurchickenrange.xls");
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


            return Content("<script> alert('please select date')</script>");

        }


    }
}