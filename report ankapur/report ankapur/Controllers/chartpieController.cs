using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace report_ankapur.Controllers
{
    public class chartpieController : Controller
    {
        // GET: chartpie
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult chpie2view(string DeliverTime)
        {
            //DeliverTime = DateTime.Today.ToString();
            //Ankapurservices chartl = new Ankapurservices();
            //var modelCust = chartl.chartpie(DeliverTime);

            ////chartline objchartline = new chartline();

            //ViewBag.ordertype = String.Join(",", modelCust.Select(m => m.Ordertype).ToList());

            //ViewBag.orders = String.Join(",", modelCust.Select(m => m.Orders).ToList());
            var modelCust = '0';

            return PartialView("chpie2view", modelCust);
        }
    }
}