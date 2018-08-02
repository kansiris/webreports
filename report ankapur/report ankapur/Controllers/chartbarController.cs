using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace report_ankapur.Controllers
{
    public class chartbarController : Controller
    {
        // GET: chartbar
        public ActionResult Index()
        {
            return View();
        }
        [ChildActionOnly]
        public ActionResult cbpview(string DeliverTime)
        {
            //string DeliverTime;

            //DeliverTime = DateTime.Today.ToString();
            //Ankapurservices chartl = new Ankapurservices();
            ////var modelCust = chartl.chartbar(DeliverTime);
            ////ViewBag.time = modelCust.Select(m => Convert.ToDateTime(m.OrderDate).ToString("yyyy-MM-dd")).ToList();
            //var data = String.Join(",", modelCust.Select(m => Convert.ToDateTime(m.month).ToString("MMMM-yyyy")).ToList());
            ////ViewBag.time = String.Join(",", modelCust.Select(m => Convert.ToDateTime(m.OrderDate).ToString("MMM dd")).ToList());
            ////var date1 = data.Split(',');
            //var result = string.Join(",", data);

            //ViewBag.time = result;
            //ViewBag.orders = String.Join(",", modelCust.Select(m => m.count).ToList());

            var modelCust= '0';
            return PartialView("cbpview", modelCust);
        }

    }
}