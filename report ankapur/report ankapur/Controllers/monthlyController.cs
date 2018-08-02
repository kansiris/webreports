using System;
using System.Web.Mvc;
using report_ankapur.Models;
using System.Globalization;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace report_ankapur.Controllers
{
    public class monthlyController : Controller
    {
        decimal TotalPrice;
        decimal Deliverycharges;
        decimal sgstcharges;
        decimal cgstcharges;
        decimal revenue;
        decimal PriceSum;
        decimal tips;
        decimal discounts;
        string restcode1;
        // GET: monthly
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult ajaxcall()
        {
            return Json("success");
        }
        public ActionResult monthlyreport(string DeliverTime, string restcode)
        {
            
            if (DeliverTime != null)
            {
                try
                {
                    string DeliverTime1 = Convert.ToDateTime(DeliverTime).ToString("MMMM-yyyy");
                    string[] nameParts = DeliverTime.Split('-');
                    string year = nameParts[0];
                string month = nameParts[1];
                Ankapurservices objCrd = new Ankapurservices();
                var modelCust = objCrd.monthlyreportsk(year, month, restcode);
               
                foreach (var reports in modelCust)
                {
                    string amount = reports.amountPaid;
                    string total = reports.TotalPrice;
                    if (total == null || total == "")
                    {           total = "0";        }
                    string cgst = reports.cgstcharges;
                    string sgst = reports.sgstcharges;
                    string deliverycharges = reports.Deliverycharges;
                        string status = reports.status;
                        string tip = reports.Tip;
                        string disco = reports.Discount;
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

                    //string r2 = String.Format("{0:n0}", Convert.ToDouble(revenue));
                    //string p2 = String.Format("{0:n0}", Convert.ToDouble(PriceSum));
                    ViewBag.revenue = r2;
                ViewBag.Pricesum = p2;
                ViewBag.modelCust = modelCust.Count;
                    ViewBag.month = DeliverTime1;
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
                    return PartialView("monthlyreports", modelCust);
                }
                catch (Exception)
                {
                   return Content("<script language='javascript' type='text/javascript'>alert('Restaurant is closed at the moment');location.href='" + @Url.Action("Index", " monthly") + "'</script>");

                }
            }
            return PartialView("monthlyreports", null);
        }

        //excel download
        public ActionResult downloadmonthly(string DeliverTime, string restcode)
        {
            if (DeliverTime != null)
            {

                var today = Convert.ToDateTime(DeliverTime);
                string DeliverTime2 = Convert.ToDateTime(today).ToString("yyyy-MM");

                string DeliverTime1 = Convert.ToDateTime(DeliverTime).ToString("MMMM-yyyy");
                string[] nameParts = DeliverTime2.Split('-');

                string year = nameParts[0];
                string month = nameParts[1];
                Ankapurservices objCrd = new Ankapurservices();
                var modelCust = objCrd.monthlyreportsk(year, month, restcode);
                var gv = new GridView();
                gv.DataSource = modelCust;
                gv.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=ankapurchickenmonthly.xls");
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


        public ActionResult previousmonth(string DeliverTime,string restcode)
        {
            var today = Convert.ToDateTime(DeliverTime);
            DateTime lastmonth;
            if (today.Month != 1)
            {    lastmonth = new DateTime(today.Year, today.Month - 1, 1);       }
            else
            {   lastmonth = new DateTime(today.Year - 1, today.Month + 11, 1);   }

            string DeliverTime1 = Convert.ToDateTime(lastmonth).ToString("MMMM-yyyy");
            string DeliverTime2 = Convert.ToDateTime(lastmonth).ToString("MM-yyyy");
            string[] nameParts = DeliverTime2.Split('-');

            string month = nameParts[0];
            string year = nameParts[1];
        
            Ankapurservices objCrd = new Ankapurservices();
            var modelCust = objCrd.monthlyreportsk(year, month, restcode);
            foreach (var item in modelCust)
            {
                string amount = item.amountPaid;
                string total = item.TotalPrice;
                if (total == null || total == "")
                {
                    total = "0";
                }
                string cgst = item.cgstcharges;
                string sgst = item.sgstcharges;
                string deliverycharges = item.Deliverycharges;
                string status = item.status;
                string tip = item.Tip;
                string disco = item.Discount;
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
                var r2 = String.Format(new CultureInfo("en-IN", false), "{0:n}", Convert.ToDouble(revenue));
                var p2 = String.Format(new CultureInfo("en-IN", false), "{0:n}", Convert.ToDouble(PriceSum));
                //PriceSum1 += item.amountPaid; // instead of =+
                ViewBag.Pricesum = p2;
                ViewBag.revenue = r2;
            }

            ViewBag.modelCust = modelCust.Count;
            ViewBag.month = DeliverTime1;
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
            return View(modelCust);

        }

        public ActionResult nextmonth(string DeliverTime,string restcode)
        {
            var today = Convert.ToDateTime(DeliverTime);
            DateTime lastmonth;
            if (today.Month != 12)
            {

                lastmonth = new DateTime(today.Year, today.Month + 1, 1);
            }
            else
            {
                lastmonth = new DateTime(today.Year + 1, today.Month - 11, 1);

            }

            string DeliverTime1 = Convert.ToDateTime(lastmonth).ToString("MMMM-yyyy");

            string DeliverTime2 = Convert.ToDateTime(lastmonth).ToString("MM-yyyy");
            string[] nameParts = DeliverTime2.Split('-');

            string month = nameParts[0];
            string year = nameParts[1];

            Ankapurservices objCrd = new Ankapurservices();
            var modelCust = objCrd.monthlyreportsk(year, month,restcode);
            foreach (var item in modelCust)
            {
                string amount = item.amountPaid;
                string total = item.TotalPrice;
                if (total == null || total == "")
                {
                    total = "0";
                }
                string cgst = item.cgstcharges;
                string sgst = item.sgstcharges;
                string deliverycharges = item.Deliverycharges;
                string status = item.status;
                string tip = item.Tip;
                string disco = item.Discount;
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
                var r2 = String.Format(new CultureInfo("en-IN", false), "{0:n}", Convert.ToDouble(revenue));
                var p2 = String.Format(new CultureInfo("en-IN", false), "{0:n}", Convert.ToDouble(PriceSum));
                //PriceSum1 += item.amountPaid; // instead of =+
                ViewBag.Pricesum = p2;
                ViewBag.revenue = r2;
            }

            ViewBag.modelCust = modelCust.Count;
            ViewBag.month = DeliverTime1;
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
            return View(modelCust);

        }

    }
}
  