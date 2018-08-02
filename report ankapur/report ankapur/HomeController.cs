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

//using Ankapur.Utility;




namespace report_ankapur.Controllers
{
    public class HomeController : Controller
    {
        decimal PriceSum ;
        decimal PriceSum1;
        decimal PriceSum2;
        decimal TotalPrice;
        decimal TotalPrice1;
        decimal TotalPrice2;
        decimal Deliverycharges;
        decimal Deliverycharges1;
        decimal Deliverycharges2;
        decimal cgstcharges;
        decimal cgstcharges1;
        decimal cgstcharges2;
        decimal sgstcharges;
        decimal sgstcharges1;
        decimal sgstcharges2;
        decimal revenue;
        decimal revenue1;
        decimal revenue2;

        public TimeZoneInfo INDIAN_ZONE { get; private set; }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {

            try
            {
                string DeliverTime = DateTime.Today.ToString();
            //ViewBag.Message = "Your application description page.";
            Ankapurservices objCrd = new Ankapurservices();
            var modelCust1 = objCrd.Getdailyreports(DeliverTime);

            foreach (var reports in modelCust1)
            {
                string amount = reports.amountPaid;

                string total = reports.TotalPrice;
                string cgst = reports.cgstcharges;
                string sgst = reports.sgstcharges;
                string deliverycharges = reports.Deliverycharges;
                TotalPrice = TotalPrice + decimal.Parse(total);
                Deliverycharges = Deliverycharges + decimal.Parse(deliverycharges);
                sgstcharges = sgstcharges + decimal.Parse(sgst);
                cgstcharges = cgstcharges + decimal.Parse(cgst);
                revenue = TotalPrice + Deliverycharges + sgstcharges + cgstcharges;
                PriceSum = PriceSum + decimal.Parse(amount);

            }

            var modelCust2 = objCrd.Weeklyreports(DeliverTime);
            foreach (var item in modelCust2)
            {
                string amount = item.amountPaid;

                string total = item.TotalPrice;
                if (total == null)
                { total = "0";
                }
                string cgst = item.cgstcharges;
                string sgst = item.sgstcharges;
                string deliverycharges = item.Deliverycharges;
                TotalPrice1 = TotalPrice1 + decimal.Parse(total);
                Deliverycharges1 = Deliverycharges + decimal.Parse(deliverycharges);
                sgstcharges1 = sgstcharges + decimal.Parse(sgst);
                cgstcharges1 = cgstcharges + decimal.Parse(cgst);
                revenue1 = TotalPrice1 + Deliverycharges1 + sgstcharges1 + cgstcharges1;
                PriceSum1 = PriceSum1 + decimal.Parse(amount);

                //PriceSum1 += item.amountPaid; // instead of =+
            }
            var modelCust3 = objCrd.monthlyreports(DeliverTime);

            foreach (var item in modelCust3)
            {
                string amount = item.amountPaid;
        
                string total = item.TotalPrice;
                if (total == null)
                {
                    total = "0";
                }
                string cgst = item.cgstcharges;
                string sgst = item.sgstcharges;
                string deliverycharges = item.Deliverycharges;
                TotalPrice2 = TotalPrice2 + decimal.Parse(total);
                Deliverycharges2 = Deliverycharges + decimal.Parse(deliverycharges);
                sgstcharges2 = sgstcharges + decimal.Parse(sgst);
                cgstcharges2 = cgstcharges + decimal.Parse(cgst);
                revenue2 = TotalPrice2 + Deliverycharges2 + sgstcharges2 + cgstcharges2;
                PriceSum2 = PriceSum2 + decimal.Parse(item.amountPaid);
                //PriceSum2 += item.amountPaid; // instead of =+
            }

            ViewBag.modelCust1 = modelCust1.Count;
            ViewBag.modelCust2 = modelCust2.Count;
            ViewBag.modelCust3 = modelCust3.Count;
            ViewBag.Pricesum = PriceSum;
            ViewBag.Pricesum1 = PriceSum1;
            ViewBag.Pricesum2 = PriceSum2;
            ViewBag.revenue = revenue;
            ViewBag.revenue1 = revenue1;
            ViewBag.revenue2 = revenue2;
            ViewBag.selectdate = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/'); 

            return View();
            }

            catch (Exception)
            {
                return Content("<script language='javascript' type='text/javascript'>alert('Restaurant is closed at the moment');location.href='" + @Url.Action("Index", "Home") + "'</script>");
            }
        }
        public PartialViewResult dailytable(string DeliverTime)
        {
            DeliverTime = DateTime.Today.ToString();
            Ankapurservices objCrd = new Ankapurservices();
                var modelCust = objCrd.Getdailyreports(DeliverTime);
                return PartialView("dailytable", modelCust);
       
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
                using (AnkapurEntities db = new AnkapurEntities())
                {
                    var customerphone = reg1.PhoneNumber;
                    var pw = reg1.Password;
                    if (customerphone == null || pw == null)
                    {
                        return Content("<script language='javascript' type='text/javascript'>location.href='" + @Url.Action("Index", "Home") + "'</script>");
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
                            return Content("<script language='javascript' type='text/javascript'>location.href='" + @Url.Action("Index", "Home") + "'</script>");
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


        //[HttpPost]
        //public ActionResult Login(Models.TblNewCustomer reg1)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        using (AnkapurEntities1 db = new AnkapurEntities1())
        //        {
        //            var customerphone = reg1.CustPhoneNumber;
        //            var pw = reg1.Password;
        //            if (customerphone == null || pw == null)
        //            {

        //                return Content("<script language='javascript' type='text/javascript'>location.href='" + @Url.Action("Index", "Home") + "'</script>");

        //            }
        //            else if (customerphone.Length < 10)
        //            {
        //                //ViewBag.vmobmsg = "Please Enter the valid Phonenumber";
        //                return Content("<script language='javascript' type='text/javascript'>alert('Please Enter the valid Phonenumber');location.href='" + @Url.Action("Index", "Home") + "'</script>");
        //            }
        //            else
        //            {
        //                var details = (from userlist in db.TblNewCustomers
        //                               where userlist.CustPhoneNumber == reg1.CustPhoneNumber && userlist.Password == reg1.Password
        //                               select new
        //                               {
        //                                   userlist.CustPhoneNumber,
        //                                   userlist.CustomerFName,
        //                                   userlist.Delivery_Addresss,
        //                                   userlist.Email,
        //                                   userlist.Status

        //                               }).ToList();
        //                if (details.FirstOrDefault() != null)
        //                {
        //                    if (details.FirstOrDefault().Status == "ACTIVE")
        //                    {
        //                        string userData = JsonConvert.SerializeObject(details.FirstOrDefault());
        //                        validuser.SetAuthCookie(userData, details.FirstOrDefault().CustPhoneNumber);
        //                        Session["CustPhoneNumber"] = details.FirstOrDefault().CustPhoneNumber;
        //                        Session["CustomerFName"] = details.FirstOrDefault().CustomerFName;
        //                        Session["Delivery_Addresss"] = details.FirstOrDefault().Delivery_Addresss;
        //                        return Content("<script language='javascript' type='text/javascript'>location.href='" + @Url.Action("About", "Home") + "'</script>");

        //                    }
        //                    else
        //                    {

        //                        return Content("<script language='javascript' type='text/javascript'>location.href='" + @Url.Action("Index", "Home") + "'</script>");
        //                    }

        //                }
        //            }
        //            return Content("<script language='javascript' type='text/javascript'>location.href='" + @Url.Action("Index", "Home") + "'</script>");

        //        }

        //    }
        //    return Content("<script language='javascript' type='text/javascript'>location.href='" + @Url.Action("Index", "Home") + "'</script>");
        //}

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
    }
}