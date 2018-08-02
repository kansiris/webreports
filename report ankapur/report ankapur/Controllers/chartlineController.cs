using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Services;
using System.Web.Services;
using report_ankapur.Models;
using System.Web.Routing;
using System.Web.SessionState;
using System.Web.Helpers;


namespace report_ankapur.Controllers
{
    public class chartslineController : Controller
    {

        // GET: charts
        public ActionResult Index()
        {
            return View();
        }

        //[ChildActionOnly]
        public ActionResult clpview(string DeliverTime)
        {
            //string DeliverTime;

            // DeliverTime = DateTime.Today.ToString();
            // Ankapurservices chartl = new Ankapurservices();
            // var modelCust = chartl.chartline(DeliverTime);
            // //ViewBag.time = modelCust.Select(m => Convert.ToDateTime(m.OrderDate).ToString("yyyy-MM-dd")).ToList();
            //var data = String.Join(",", modelCust.Select(m => Convert.ToDateTime(m.OrderDate).ToString("dd-MM-yyyy")).ToList());
            // //ViewBag.time = String.Join(",", modelCust.Select(m => Convert.ToDateTime(m.OrderDate).ToString("MMM dd")).ToList());
            // //var date1 = data.Split(',');
            // //var result = string.Join(",", data);

            // ViewBag.time = data;
            // ViewBag.orders = String.Join(",", modelCust.Select(m => m.Orders).ToList());

            // //var modelCust1 = chartl.chartpie(DeliverTime);

            ////chartline objchartline = new chartline();

            //ViewBag.ordertype = String.Join(",", modelCust1.Select(m => m.Ordertype).ToList());

            //ViewBag.orders1 = String.Join(",", modelCust1.Select(m => m.Orders).ToList());

            //ViewBag.time2 = data;
            //ViewBag.orders2 = String.Join(",", modelCust.Select(m => m.Orders).ToList());

            var modelCust = '0';
            return PartialView("clpview", modelCust);
        }
        //[ChildActionOnly]
        //public ActionResult chpie2view(string DeliverTime)
        //{
        //    DeliverTime = DateTime.Today.ToString();
        //    Ankapurservices chartl = new Ankapurservices();
        //    var modelCust = chartl.chartpie(DeliverTime);

        //    //chartline objchartline = new chartline();

        //    ViewBag.ordertype = String.Join(",", modelCust.Select(m => m.Ordertype).ToList());

        //    ViewBag.orders = String.Join(",", modelCust.Select(m => m.Orders).ToList());


        //    return PartialView("chpie2view", modelCust);
        //}

        //[ChildActionOnly]
        //public ActionResult cbpview(string DeliverTime)
        //{
        //    //string DeliverTime;

        //    DeliverTime = DateTime.Today.ToString();
        //    Ankapurservices chartl = new Ankapurservices();
        //    var modelCust = chartl.chartline(DeliverTime);
        //    //ViewBag.time = modelCust.Select(m => Convert.ToDateTime(m.OrderDate).ToString("yyyy-MM-dd")).ToList();
        //    var data = String.Join(",", modelCust.Select(m => Convert.ToDateTime(m.OrderDate).ToString("yyyy-MM-dd")).ToList());
        //    //ViewBag.time = String.Join(",", modelCust.Select(m => Convert.ToDateTime(m.OrderDate).ToString("MMM dd")).ToList());
        //    //var date1 = data.Split(',');
        //    var result = string.Join(",", data);

        //    ViewBag.time = result;
        //    ViewBag.orders = String.Join(",", modelCust.Select(m => m.Orders).ToList());


        //    return PartialView("cbpview", modelCust);
        //}



    }
}