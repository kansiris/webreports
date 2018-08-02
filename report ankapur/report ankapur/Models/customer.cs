using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace report_ankapur.Models
{
    public class customer
    {
        public string phoneNo { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string daddress { get; set; }
        public string laddress { get; set; }
        public string mobileno1 { get; set; }
        public string mobileno2 { get; set; }
        public string Deliverylocationlatitude { get; set; }
        public string Deliverylocationlongitude { get; set; }
        public string ModifiedDate { get; set; }
        public string CreatedDate { get; set; }
        public string custTypeId { get; set; }

       
    }
}