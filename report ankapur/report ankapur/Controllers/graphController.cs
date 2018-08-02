using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace report_ankapur.Controllers
{
    public class graphController : Controller
    {
        string DeliverTime; string restcode1;
        // GET: graph
        public ActionResult Index(string restcode)
        {
            if (restcode == null)
            { restcode = "HN"; }

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


        public ActionResult clpview(string restcode)
        {
            try
            {
                //string DeliverTime;
                if (restcode == null)
                { restcode = "HN"; }
                DeliverTime = DateTime.Today.ToString();
                Ankapurservices chartl = new Ankapurservices();
                var modelCust = chartl.chartline(DeliverTime, restcode);
                var data = String.Join(",", modelCust.OrderByDescending(m=>m.OrderDate).Select(m => Convert.ToDateTime(m.OrderDate).ToString("dd-MM-yyyy")).ToList());
                ViewBag.time = data;
                ViewBag.orders = String.Join(",", modelCust.OrderByDescending(m => m.OrderDate).Select(m => m.Orders).ToList());
                return PartialView("clpview", modelCust);
            }
            catch (Exception)
            {
                return Content("<script language='javascript' type='text/javascript'>alert('Restaurant is closed at the moment');location.href='" + @Url.Action("Index", "graph") + "'</script>");
            }
        }

        [ChildActionOnly]
        public ActionResult chpie2view(string restcode)
        {
            try
            {
                if (restcode == null)
            { restcode = "HN"; }
            DeliverTime = DateTime.Today.ToString();
            Ankapurservices chartl = new Ankapurservices();
            var modelCust1 = chartl.chartpie(DeliverTime, restcode);
            ViewBag.ordertype = String.Join(",", modelCust1.Select(m => m.Ordertype).ToList());
            ViewBag.orders1 = String.Join(",", modelCust1.Select(m => m.Orders).ToList());
            return PartialView("chpie2view", modelCust1);
            }
            catch (Exception)
            {
                return Content("<script language='javascript' type='text/javascript'>alert('Restaurant is closed at the moment');location.href='" + @Url.Action("Index", "graph") + "'</script>");
            }
        }

        [ChildActionOnly]
        public ActionResult cbpview(string restcode)
        {
            try
            {
                //string DeliverTime;

                if (restcode == null)
            { restcode = "HN"; }
            DeliverTime = DateTime.Today.ToString();
            Ankapurservices chartl = new Ankapurservices();
            var modelCust2 = chartl.chartbar(DeliverTime, restcode);
            //ViewBag.time = modelCust.Select(m => Convert.ToDateTime(m.OrderDate).ToString("yyyy-MM-dd")).ToList();
            var data = String.Join(",", modelCust2.Select(m => Convert.ToDateTime(m.month).ToString("MMMM-yyyy")).ToList());
            //ViewBag.time = String.Join(",", modelCust.Select(m => Convert.ToDateTime(m.OrderDate).ToString("MMM dd")).ToList());
            //var date1 = data.Split(',');
            var result = string.Join(",", data);

            ViewBag.time2 = result;
            ViewBag.orders2 = String.Join(",", modelCust2.Select(m => m.count).ToList());
            return PartialView("cbpview", modelCust2);
            }
            catch (Exception)
            {
                return Content("<script language='javascript' type='text/javascript'>alert('Restaurant is closed at the moment');location.href='" + @Url.Action("Index", "graph") + "'</script>");
            }
        }

    }
}