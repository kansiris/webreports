using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace report_ankapur.Models
{
    public class orderdetails
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public Byte[] Image { get; set; }
        public int Quantity { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string customerPhone { get; set; }
        public string RestCode { get; set; }
        public string CustomerRequest { get; set; }
        public string Ordertime { get; set; }
        public string DeliverTime { get; set; }
        public int Totalamount { get; set; }
        public string Restcode { get; set; }
        public DateTime Orderdate { get; set; }
        public string Orderid { get; set; }
        public string Orderidforcart { get; set; }
        public int Price1 { get; set; }
        public int Quantity1 { get; set; }
        public List<orderdetails> Orddetails { get; set; }
    }
}