using System;
using System.Web.Mvc;
using report_ankapur.Models;
using System.Globalization;
using System.IO;
using System.Web.UI;

namespace report_ankapur.Controllers
{

    public class weekreportsController : Controller
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
        //GET: weekreports
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ajaxcall()
        {
            return Json("success");
        }

        public ActionResult weeklyreport(DateTime DeliverTime, string restcode)
        {
           
            if (DeliverTime != null)
            {
                try
                {
                    DateTime yesterday1 = DeliverTime.AddDays(-7);
                ViewBag.selectdate1 = Convert.ToDateTime(yesterday1).ToString("dd/MM/yyyy");


                Ankapurservices objCrd = new Ankapurservices();
                var modelCust = objCrd.Weeklyreports(DeliverTime, restcode);

                ViewBag.modelCust = modelCust.Count;
                foreach (var reports in modelCust)
                {
                    string amount = reports.amountPaid;

                    string total = reports.TotalPrice;
                    if (total == null || total == "")
                    {
                        total = "0";
                    }
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
                if (r2 == null || r2 == "")
                {
                    r2 = "0";
                }
                if (p2 == null || p2 == "")
                {
                    p2 = "0";
                }
                ViewBag.revenue = r2;
                ViewBag.Pricesum = p2;
                ViewBag.selectdate = Convert.ToDateTime(DeliverTime).ToString("dd/MM/yyyy");
                ViewBag.selectdate1 = Convert.ToDateTime(yesterday1).ToString("dd/MM/yyyy");

                ViewBag.selectdate2 = Convert.ToDateTime(DeliverTime).ToString("yyyy/MM/dd");
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
                return PartialView("weeklyreport", modelCust);
            }
                catch (Exception)
            {
                    return Content("<script language='javascript' type='text/javascript'>alert('Restaurant is closed at the moment');location.href='" + @Url.Action("Index", "weekreports") + "'</script>");

                }
            }
            return PartialView("weeklyreport", null);
    }
    public ActionResult downloadweekly(DateTime DeliverTime, string restcode)
    {
        if (DeliverTime != null)
        {
            ViewBag.Message = "Your application description page.";
            Ankapurservices objCrd = new Ankapurservices();
            var modelCust1 = objCrd.Weeklyreports(DeliverTime, restcode);
            var gv = new System.Web.UI.WebControls.GridView();
            gv.DataSource = modelCust1;
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=ankapurchickenweekly.xls");
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