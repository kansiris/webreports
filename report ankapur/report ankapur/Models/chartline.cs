using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace report_ankapur.Models
{
    public class chartline
    {
        public DateTime OrderDate { get; set; }
        public string Orders { get; set; }
        public string Ordertype { get; set; }

        public chartData Chart { get; set; }

    }

    public class chartData
    {
        public string OrderDate { get; set; }
        public string Orders { get; set; }
    }
}