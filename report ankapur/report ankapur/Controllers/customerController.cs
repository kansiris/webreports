using report_ankapur.Models;
using report_ankapur.Viewmodel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace report_ankapur.Controllers
{
    public class customerController : Controller
    {
        decimal PriceSum;
      
        // GET: customer
        public ActionResult Index()
        {
            try
            {

                return View();
            }
            catch (Exception)
            {
                return Content("<script language='javascript' type='text/javascript'>alert('Restaurant is closed at the moment');location.href='" + @Url.Action("Index", "customer") + "'</script>");
            }
        }

        public JsonResult ajaxcall()
        {
            return Json("success");
        }
        //public PartialViewResult customerpview(string DeliverTime)
        //{
        //    if (DeliverTime != null)
        //    {
        //        List<customerviewmodel> allOrder = new List<customerviewmodel>();
        //        Ankapurservices objCrd = new Ankapurservices();
        //        var modelCust = objCrd.customerdetails(DeliverTime);
        //        //ViewBag.orderhistory = Odtails();
        //        foreach (var i in modelCust)
        //        {
        //            string phoneno = i.phoneNo;
        //            var od = objCrd.custreports(phoneno).ToList();;
        //            allOrder.Add(new customerviewmodel { custDetails = i, orderDetails = od });
        //            ViewBag.custdetails = modelCust;
        //            ViewBag.orderdetails = od;
        //        }
        //           return PartialView("customerpview", allOrder);
        //        //  return PartialView("customerpview");
        //    }
        //    return PartialView("customerpview", null);
        //}


        public PartialViewResult customerpview(string DeliverTime,string restcode)
        {
            try
            {
            if (DeliverTime != null)
            {
                List<customerviewmodel> allOrder = new List<customerviewmodel>();
                Ankapurservices objCrd = new Ankapurservices();
                var modelCust = objCrd.customerdetails(DeliverTime,restcode);
                    //ViewBag.orderhistory = Odtails();
                    ViewBag.restcode = restcode;
                return PartialView("customerpview", modelCust);
                //  return PartialView("customerpview");
            }
            return PartialView("customerpview", null);
                 }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult customerpview1(string phoneno,string restcode)
        {
            if (phoneno != null)
            {
                var p2 = "0" ;
                Ankapurservices objCrd = new Ankapurservices();
                var od = objCrd.custreports(phoneno,restcode);
                ViewBag.orders = od.Count;
                ViewBag.phoneno = phoneno;
                foreach (var item in od)
                {
                    string amount = item.amountPaid;
                    if (amount == null || amount == "")
                    {
                        amount = "0";
                    }
                    PriceSum = PriceSum + decimal.Parse(amount);
                    p2 = String.Format(new CultureInfo("en-IN", false), "{0:n}", Convert.ToDouble(PriceSum));
                }
                ViewBag.Totalamount = p2;
                var od1 = objCrd.custdiscounts(phoneno,restcode).FirstOrDefault();
                var disc = (od1 != null) ? od1.Discount : "0";
                ViewBag.discount = disc;

                return PartialView("customerpview1");
                //return View();
                //return Content("<script> alert(phoneno); </script>");
            }
            return PartialView("customerpview1",null);
        }
    }
}