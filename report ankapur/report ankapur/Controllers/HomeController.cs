using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using report_ankapur.Models;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using System.Web.UI;
using System.Web.Security;
using System.Data.SqlClient;
using System.Web.Routing;
using System.Web.Caching;
//using Microsoft.Web.Administration;
using System.IO;
using System.Data;
using System.Web.Hosting;
using System.Configuration;
using System.Drawing;
using System.Web.UI.WebControls;

//using Ankapur.Utility;




namespace report_ankapur.Controllers
{
    public class HomeController : Controller
    {
        decimal PriceSum, PriceSum1, PriceSum2;
        decimal TotalPrice, TotalPrice1, TotalPrice2;
        decimal Deliverycharges, Deliverycharges1, Deliverycharges2;
        decimal cgstcharges, cgstcharges1, cgstcharges2;
        decimal sgstcharges, sgstcharges1, sgstcharges2;
        decimal revenue, revenue1, revenue2;
        decimal tips, tips1, tips2;
        decimal discounts, discount1, discount2;
        string restcode1;

        public TimeZoneInfo INDIAN_ZONE { get; private set; }

        public ActionResult Index()
        {
            FormsAuthentication.SignOut();
            return View();
        }

        public JsonResult ajaxcall()
        {
            return Json("success");
        }
        public ActionResult About(string restcode)
        {
            try
            {
                if (restcode == null)
                    { restcode = "HN"; }
                DateTime DeliverTime = DateTime.Today;
                //ViewBag.Message = "Your application description page.";
                Ankapurservices objCrd = new Ankapurservices();
                var modelCust1 = objCrd.Getdailyreports(DeliverTime,restcode);

                foreach (var reports in modelCust1)
                {
                    string amount = reports.amountPaid;
                    string total = reports.TotalPrice;
                    if (total == null || total == "")
                    { total = "0"; }
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
                        revenue2 = TotalPrice + Deliverycharges + sgstcharges + cgstcharges + tips - discounts;
                        PriceSum2 = PriceSum2 + decimal.Parse(amount);
                    }
                }
                var r2 = String.Format(new CultureInfo("en-IN", false), "{0:n}", Convert.ToDouble(revenue2));
                var p2 = String.Format(new CultureInfo("en-IN", false), "{0:n}", Convert.ToDouble(PriceSum2));
                //PriceSum1 += item.amountPaid; // instead of =+
                ViewBag.modelCust = modelCust1.Count;
                ViewBag.Pricesum = p2;
                ViewBag.revenue = r2;

                var modelCust2 = objCrd.Weeklyreports(DeliverTime,restcode);
                //foreach (var item in modelCust2)
                //{
                //    string amount = item.amountPaid;
                //    string total = item.TotalPrice;
                //    if (total == null || total == "")
                //    {
                //        total = "0";
                //    }
                //    string cgst = item.cgstcharges;
                //    string sgst = item.sgstcharges;
                //    string deliverycharges = item.Deliverycharges;
                //    TotalPrice1 = TotalPrice1 + decimal.Parse(total);
                //    Deliverycharges1 = Deliverycharges + decimal.Parse(deliverycharges);
                //    sgstcharges1 = sgstcharges + decimal.Parse(sgst);
                //    cgstcharges1 = cgstcharges + decimal.Parse(cgst);
                //    revenue1 = TotalPrice1 + Deliverycharges1 + sgstcharges1 + cgstcharges1;
                //    PriceSum1 = PriceSum1 + decimal.Parse(amount);
                //    //PriceSum1 += item.amountPaid; // instead of =+
                //}
                var modelCust3 = objCrd.monthlyreports(DeliverTime , restcode);

                //foreach (var item in modelCust3)
                //{
                //    string amount = item.amountPaid;
                //    string total = item.TotalPrice;
                //    if (total == null || total == "")
                //    {
                //        total = "0";
                //    }
                //    string cgst = item.cgstcharges;
                //    string sgst = item.sgstcharges;
                //    string deliverycharges = item.Deliverycharges;
                //    TotalPrice2 = TotalPrice2 + decimal.Parse(total);
                //    Deliverycharges2 = Deliverycharges + decimal.Parse(deliverycharges);
                //    sgstcharges2 = sgstcharges + decimal.Parse(sgst);
                //    cgstcharges2 = cgstcharges + decimal.Parse(cgst);
                //    revenue2 = TotalPrice2 + Deliverycharges2 + sgstcharges2 + cgstcharges2;
                //    PriceSum2 = PriceSum2 + decimal.Parse(item.amountPaid);
                //    var r2 = String.Format(new CultureInfo("en-IN", false), "{0:n}", Convert.ToDouble(revenue2));
                //    var p2 = String.Format(new CultureInfo("en-IN", false), "{0:n}", Convert.ToDouble(PriceSum2));
                //    //PriceSum2 += item.amountPaid; // instead of =+
                //}
                ViewBag.modeldaily = modelCust1;
                ViewBag.modelCust1 = modelCust1.Count;
                ViewBag.modelCust2 = modelCust2.Count;
                ViewBag.modelCust3 = modelCust3.Count;
                //ViewBag.Pricesum = PriceSum;
                //ViewBag.Pricesum1 = PriceSum1;
                //ViewBag.Pricesum2 = PriceSum2;
                //ViewBag.revenue = revenue;
                //ViewBag.revenue1 = revenue1;
                //ViewBag.revenue2 = revenue2;
                ViewBag.selectdate = DeliverTime.ToString("dd/MM/yyyy").Replace('-', '/');
                ViewBag.selectdate1 = DeliverTime.ToString("yyyy/MM/dd").Replace('-', '/');
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
                ViewBag.restcode = restcode;
                return View();
            }

            catch (Exception)
            {
                return Content("<script language='javascript' type='text/javascript'>alert('Restaurant is closed at the moment');location.href='" + @Url.Action("About", "Home") + "'</script>");
            }
        }
        public PartialViewResult dailytable(string restcode)
        {
            if (restcode == null)
            { restcode = "HN"; }
            DateTime DeliverTime = DateTime.Today;
            Ankapurservices objCrd = new Ankapurservices();
            var modelCust = objCrd.Getdailyreports(DeliverTime , restcode);

            foreach (var reports in modelCust)
            {
                string amount = reports.amountPaid;
                string total = reports.TotalPrice;
                if (total == null || total == "")
                {     total = "0";       }
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
                    revenue2 = TotalPrice + Deliverycharges + sgstcharges + cgstcharges + tips - discounts;
                    PriceSum2 = PriceSum2 + decimal.Parse(amount);
                }
            }        
            var r2 = String.Format(new CultureInfo("en-IN", false), "{0:n}", Convert.ToDouble(revenue2));
            var p2 = String.Format(new CultureInfo("en-IN", false), "{0:n}", Convert.ToDouble(PriceSum2));
            //PriceSum1 += item.amountPaid; // instead of =+
            ViewBag.modelCust = modelCust.Count;
            ViewBag.Pricesum = p2;
            ViewBag.revenue = r2;
            ViewBag.selectdate = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            ViewBag.selectdate1 = DateTime.Now.ToString("MM/dd/yyyy").Replace('-', '/');
            return PartialView("dailytable", modelCust);
        }

        public ActionResult Monthlytable(string restcode)
        {
            ViewBag.selectdate = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/'); 
            var today = DateTime.Now;
            var yesterday = today.AddDays(-31);
            ViewBag.selectdate1 = yesterday.ToString("dd/MM/yyyy").Replace('-', '/'); 
            ViewBag.selectdate2 = DateTime.Now.ToString("yyyy/MM/dd").Replace('-', '/');
            ViewBag.restcode = restcode;
            return View();
        }

        public PartialViewResult monthlypview(string restcode)
        {
            if (restcode == null)
            { restcode = "HN"; }
            DateTime DeliverTime = DateTime.Today;
            DateTime dateTime = Convert.ToDateTime(DeliverTime, new CultureInfo("en-GB"));
            string DeliverTime1 = dateTime.AddDays(31).ToString("dd/MM/yyyy");
            Ankapurservices objCrd = new Ankapurservices();
            var modelCust3 = objCrd.monthlyreports(DeliverTime ,restcode);
            foreach (var item in modelCust3)
            {
                string orderid = item.OrderId;
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
                    revenue2 = TotalPrice + Deliverycharges + sgstcharges + cgstcharges + tips - discounts;
                    PriceSum2 = PriceSum2 + decimal.Parse(amount);
                }
                var r2 = String.Format(new CultureInfo("en-IN", false), "{0:n}", Convert.ToDouble(revenue2));
                var p2 = String.Format(new CultureInfo("en-IN", false), "{0:n}", Convert.ToDouble(PriceSum2));
                //PriceSum1 += item.amountPaid; // instead of =+
                ViewBag.modelCust3 = modelCust3.Count;
                ViewBag.Pricesum2 = p2;
                ViewBag.revenue2 = r2;
                ViewBag.selectdate = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                ViewBag.selectdate = DeliverTime1;

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
                ViewBag.restcode = restcode;
               
            }

            return PartialView("monthlypview", modelCust3);
        }

        public ActionResult weeklytable(string restcode)
        {
            ViewBag.selectdate = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            var today = DateTime.Now;
            var yesterday = today.AddDays(-7);
            ViewBag.selectdate1 = yesterday.ToString("dd/MM/yyyy").Replace('-', '/');
            ViewBag.selectdate2 = DateTime.Now.ToString("yyyy/MM/dd").Replace('-', '/');
            // ViewBag.selectdate2 = DateTime.Now.ToString("yyyy/MM/dd").Replace('-', '/');
            ViewBag.restcode = restcode;
            return View();
        }

        public ActionResult weekpview(string restcode)
        {
            if (restcode == null)
            { restcode = "HN"; }
            DateTime DeliverTime = DateTime.Today;
            Ankapurservices objCrd = new Ankapurservices();
            DateTime dateTime = Convert.ToDateTime(DeliverTime, new CultureInfo("en-GB"));
            var modelCust2 = objCrd.Weeklyreports(DeliverTime,restcode);
            string DeliverTime1 = dateTime.AddDays(-7).ToString("dd/MM/yyyy");
            foreach (var reports in modelCust2)
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
                    revenue1 = TotalPrice + Deliverycharges + sgstcharges + cgstcharges + tips - discounts;
                    PriceSum1 = PriceSum1 + decimal.Parse(amount);
                }
            }
            var r1 = String.Format(new CultureInfo("en-IN", false), "{0:n}", Convert.ToDouble(revenue1));
            var p1 = String.Format(new CultureInfo("en-IN", false), "{0:n}", Convert.ToDouble(PriceSum1));
            //PriceSum1 += item.amountPaid; // instead of =+
            ViewBag.modelCust2 = modelCust2.Count;
            ViewBag.Pricesum1 = p1;
            ViewBag.revenue1 = r1;
            ViewBag.selectdate = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            ViewBag.selectdate1 = DeliverTime1;
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
            ViewBag.restcode = restcode;

            return PartialView("weekpview", modelCust2);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        [HttpPost]

        public ActionResult Login(Models.mangement reg1)
        {
            try
            {
                //DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                //int hour = indianTime.Hour;
                //if (hour >= 10 && hour <= 20)
                //{
                if (ModelState.IsValid)
                {
                    // using (AnkapurEntities2 db = new AnkapurEntities2())
                    using (AnkapurEntities db = new AnkapurEntities())
                    {
                        var customerphone = reg1.PhoneNumber;
                        var pw = reg1.Password;
                        if (customerphone == null || pw == null)
                        {
                            return Content("<script language='javascript' type='text/javascript'>alert('Please Enter the valid Details');location.href='" + @Url.Action("Index", "Home") + "'</script>");
                        }
                        else if (customerphone.Length < 10)
                        {
                            //ViewBag.vmobmsg = "Please Enter the valid Phonenumber";
                            return Content("<script language='javascript' type='text/javascript'>alert('Please Enter the valid Phonenumber');location.href='" + @Url.Action("Index", "Home") + "'</script>");
                        }
                        else
                        {
                            var details = (from userlist in db.mangements
                                           where userlist.PhoneNumber == reg1.PhoneNumber && userlist.Password == reg1.Password
                                           select new
                                           {
                                               userlist.PhoneNumber,
                                           }).ToList();
                            if (details.FirstOrDefault() != null)
                            {
                                //if (details.FirstOrDefault().Status == "ACTIVE")
                                //{
                                string userData = JsonConvert.SerializeObject(details.FirstOrDefault());
                                validuser.SetAuthCookie(userData, details.FirstOrDefault().PhoneNumber);
                                Session["CustPhoneNumber"] = details.FirstOrDefault().PhoneNumber;
                                return Content("<script language='javascript' type='text/javascript'>location.href='" + @Url.Action("About", "Home") + "'</script>");
                            }
                            else
                            {
                                return Content("<script language='javascript' type='text/javascript'>alert('Invalid Credentials login failed');location.href='" + @Url.Action("Index", "Home") + "'</script>");
                            }
                        }
                    }
                }
                return Content("<script language='javascript' type='text/javascript'>alert('Invalid Credentials login failed');location.href='" + @Url.Action("Index", "Home") + "'</script>");
            }
            catch (Exception)
            {
                return Content("<script language='javascript' type='text/javascript'>alert('Restaurant is closed at the moment');location.href='" + @Url.Action("Index", "Home") + "'</script>");
            }
        }
     
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult error()
        {
            FormsAuthentication.SignOut();
            return View();
        }
        public ActionResult downloaddaily(string restcode)
        {
            if (restcode == null)
            { restcode = "HN"; }
            DateTime DeliverTime = DateTime.Today;
            //ViewBag.Message = "Your application description page.";
            Ankapurservices objCrd = new Ankapurservices();
            var modelCust1 = objCrd.Getdailyreports(DeliverTime ,restcode);
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
            return View("Index");
        }
        public ActionResult downloadweekly(string restcode)
        {
            
            DateTime DeliverTime = DateTime.Today;
            //ViewBag.Message = "Your application description page.";
            Ankapurservices objCrd = new Ankapurservices();
            var modelCust1 = objCrd.Weeklyreports(DeliverTime,restcode);
            var gv = new GridView();
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
            return View("Index");
        }
        public ActionResult downloadmonthly(string restcode)
        {
            
            DateTime DeliverTime = DateTime.Today;
            //ViewBag.Message = "Your application description page.";
            Ankapurservices objCrd = new Ankapurservices();
            var modelCust1 = objCrd.monthlyreports(DeliverTime,restcode);
            var gv = new GridView();
            gv.DataSource = modelCust1;
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
            return View("Index");
        }
        public ActionResult downloadmonth(DateTime DeliverTime, string restcode)
        {
            if (DeliverTime != null)
            { 
            //ViewBag.Message = "Your application description page.";
            Ankapurservices objCrd = new Ankapurservices();
            var modelCust1 = objCrd.monthlyreports(DeliverTime,restcode);
            var gv = new GridView();
            gv.DataSource = modelCust1;
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
            return View("Index");
        }
         return Content("<script> alert('please select date')</script>");
    }
        public ActionResult previousdaily(DateTime DeliverTime,string restcode)
        {
            DateTime dateTime1 = DeliverTime.AddDays(-1);
            
            Ankapurservices objCrd = new Ankapurservices();
            // return Content("<script language='javascript' type='text/javascript'>alert('"+ dateTime1 + "');</script>");
            var modelCust = objCrd.Getdailyreports(dateTime1,restcode);
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
                    revenue2 = TotalPrice + Deliverycharges + sgstcharges + cgstcharges + tips - discounts;
                    PriceSum2 = PriceSum2 + decimal.Parse(amount);
                }

            }
            var r2 = String.Format(new CultureInfo("en-IN", false), "{0:n}", Convert.ToDouble(revenue2));
            var p2 = String.Format(new CultureInfo("en-IN", false), "{0:n}", Convert.ToDouble(PriceSum2));
            //PriceSum1 += item.amountPaid; // instead of =+
            ViewBag.Pricesum = p2;
            ViewBag.revenue = r2;
            ViewBag.modelCust = modelCust.Count;
            ViewBag.selectdate = dateTime1.ToString("dd/MM/yyyy"); //dd/MM/yyyy
            ViewBag.selectdate1 = dateTime1.ToString("yyyy/MM/dd");
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
            ViewBag.restcode = restcode;
            return View(modelCust);
        }

        public ActionResult nextday(DateTime DeliverTime, string restcode)
        {
            DateTime dateTime = DeliverTime.AddDays(1);
           //string DeliverTime2 = dateTime.AddDays(-1).ToString("dd/MM/yyyy");
            Ankapurservices objCrd = new Ankapurservices();
            var modelCust = objCrd.Getdailyreports(dateTime, restcode);
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
                    revenue2 = TotalPrice + Deliverycharges + sgstcharges + cgstcharges + tips - discounts;
                    PriceSum2 = PriceSum2 + decimal.Parse(amount);
                }
            }
            var r2 = String.Format(new CultureInfo("en-IN", false), "{0:n}", Convert.ToDouble(revenue2));
            var p2 = String.Format(new CultureInfo("en-IN", false), "{0:n}", Convert.ToDouble(PriceSum2));
            //PriceSum1 += item.amountPaid; // instead of =+
            ViewBag.Pricesum = p2;
            ViewBag.revenue = r2;
            ViewBag.modelCust = modelCust.Count;
            ViewBag.selectdate = dateTime.ToString("dd/MM/yyyy");
            ViewBag.selectdate1 = dateTime.ToString("yyyy/MM/dd");
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
            ViewBag.restcode = restcode;
            return View(modelCust);
        }


        public ActionResult previousweek(DateTime DeliverTime,string restcode)
        {        
            DateTime DeliverTime2 = DeliverTime.AddDays(-8);
            DateTime DeliverTime1 = DeliverTime.AddDays(-15);
            Ankapurservices objCrd = new Ankapurservices();
            var modelCust = objCrd.Weeklyreports(DeliverTime2, restcode);
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
                    revenue2 = TotalPrice + Deliverycharges + sgstcharges + cgstcharges + tips - discounts;
                    PriceSum2 = PriceSum2 + decimal.Parse(amount);
                }
            }
            var r2 = String.Format(new CultureInfo("en-IN", false), "{0:n}", Convert.ToDouble(revenue2));
            var p2 = String.Format(new CultureInfo("en-IN", false), "{0:n}", Convert.ToDouble(PriceSum2));
            //PriceSum1 += item.amountPaid; // instead of =+
            ViewBag.Pricesum = p2;
            ViewBag.revenue = r2;
            ViewBag.modelCust = modelCust.Count;
            ViewBag.selectdate1 = DeliverTime1.ToString("dd/MM/yyyy");
            ViewBag.selectdate = DeliverTime2.ToString("dd/MM/yyyy");
            ViewBag.selectdate2 = DeliverTime2.ToString("yyyy/MM/dd");
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
            ViewBag.restcode = restcode;
            return View(modelCust);
      }
        public ActionResult nextweek(DateTime DeliverTime,string restcode)
        {
            DateTime DeliverTime1 = DeliverTime.AddDays(1);
            DateTime DeliverTime2 = DeliverTime.AddDays(8);
            //string DeliverTime1 = dateTime.AddDays(-15).ToString("dd/MM/yyyy");
            Ankapurservices objCrd = new Ankapurservices();
            var modelCust = objCrd.Weeklyreports(DeliverTime2, restcode);
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
                    revenue2 = TotalPrice + Deliverycharges + sgstcharges + cgstcharges + tips - discounts;
                    PriceSum2 = PriceSum2 + decimal.Parse(amount);
                }
            }
            var r2 = String.Format(new CultureInfo("en-IN", false), "{0:n}", Convert.ToDouble(revenue2));
            var p2 = String.Format(new CultureInfo("en-IN", false), "{0:n}", Convert.ToDouble(PriceSum2));
            //PriceSum1 += item.amountPaid; // instead of =+
            ViewBag.Pricesum = p2;
            ViewBag.revenue = r2;
            ViewBag.modelCust = modelCust.Count;
            ViewBag.selectdate1 = DeliverTime1.ToString("dd/MM/yyyy");
            ViewBag.selectdate = DeliverTime2.ToString("dd/MM/yyyy");
            ViewBag.selectdate2 = DeliverTime2.ToString("yyyy/MM/dd");
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
            ViewBag.restcode = restcode;
            return View(modelCust);
        }

        public ActionResult previousmonth(DateTime DeliverTime, string restcode)
        {
            DateTime DeliverTime2 = DeliverTime.AddDays(-32);
            DateTime DeliverTime1 = DeliverTime.AddDays(-63);
            Ankapurservices objCrd = new Ankapurservices();
            var modelCust = objCrd.monthlyreports(DeliverTime2,restcode);
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
                    revenue2 = TotalPrice + Deliverycharges + sgstcharges + cgstcharges + tips - discounts;
                    PriceSum2 = PriceSum2 + decimal.Parse(amount);
                }
            }
            var r2 = String.Format(new CultureInfo("en-IN", false), "{0:n}", Convert.ToDouble(revenue2));
            var p2 = String.Format(new CultureInfo("en-IN", false), "{0:n}", Convert.ToDouble(PriceSum2));
            //PriceSum1 += item.amountPaid; // instead of =+
            ViewBag.Pricesum = p2;
            ViewBag.revenue = r2;
            ViewBag.modelCust = modelCust.Count;
            ViewBag.selectdate1 = DeliverTime1.ToString("dd/MM/yyyy");
            ViewBag.selectdate = DeliverTime2.ToString("dd/MM/yyyy");
            ViewBag.selectdate2 = DeliverTime2.ToString("yyyy/MM/dd").Replace('-', '/');
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
            ViewBag.restcode = restcode;
            return View(modelCust);

        }

        public ActionResult nextmonth(DateTime DeliverTime,string restcode)
        {
            DateTime DeliverTime1 = DeliverTime.AddDays(1);
            DateTime DeliverTime2 = DeliverTime.AddDays(32);
            //string DeliverTime1 = dateTime.AddDays(-15).ToString("dd/MM/yyyy");
            Ankapurservices objCrd = new Ankapurservices();
            var modelCust = objCrd.monthlyreports(DeliverTime2 ,restcode);
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
                    revenue2 = TotalPrice + Deliverycharges + sgstcharges + cgstcharges + tips - discounts;
                    PriceSum2 = PriceSum2 + decimal.Parse(amount);
                }
            }
            var r2 = String.Format(new CultureInfo("en-IN", false), "{0:n}", Convert.ToDouble(revenue2));
            var p2 = String.Format(new CultureInfo("en-IN", false), "{0:n}", Convert.ToDouble(PriceSum2));
            //PriceSum1 += item.amountPaid; // instead of =+
            ViewBag.Pricesum = p2;
            ViewBag.revenue = r2;
            ViewBag.modelCust = modelCust.Count;
            ViewBag.selectdate1 = DeliverTime1.ToString("dd/MM/yyyy");
            ViewBag.selectdate = DeliverTime2.ToString("dd/MM/yyyy");
            ViewBag.selectdate2 = DeliverTime2.ToString("yyyy/MM/dd").Replace('-', '/');
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
            ViewBag.restcode = restcode;
            return View(modelCust);

        }

        public ActionResult OrderDetails(string orderid,string restcode)
        {
            if (orderid != null)
            {
                if (restcode == null | restcode == "")
                { restcode = "HN"; }
                Ankapurservices objCrd = new Ankapurservices();
                var modelCust = objCrd.orderdetails(orderid, restcode);

                //foreach (var item in modelCust)
                //{
                //    string Price = item.Price;
                //    string Quantity = item.Quantity;
                //    string ProductName = item.ProductName;
                //}
                ViewBag.check = "Y";
                ViewBag.data = modelCust.ToList();

                return PartialView("OrderDetails");
            }
            ViewBag.check = "N";
            return PartialView("OrderDetails", null);
        }
    }
}