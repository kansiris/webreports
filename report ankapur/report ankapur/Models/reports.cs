using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace report_ankapur.Models
{
    public class reports
    {
        public string OrderId { get; set; }
        public string OrderDate { get; set; }
     //   public string OrderTime { get; set; }
        public string restcode { get; set; }
     //   public string Totalamount { get; set; }
        public string Orderstatus { get; set; }
        public string status { get; set; }
        public string customerphone { get; set; }
        public string statusdiscription { get; set; }
        public string customerfname { get; set; }
        public string customerlname { get; set; }
        public string Billing_Address { get; set; }
        public string Delivery_Addresss { get; set; }
        public string empcode { get; set; }
    
        public string Quantity { get; set; }
        public string TotalPrice { get; set; }
        public string Deliverycharges { get; set; }
        public string cgstcharges { get; set; }
        public string sgstcharges { get; set; }
        public string Discount { get; set; }
        public string Tip { get; set; }
        public string amountPaid { get; set; }
        public string Remarks { get; set; }
        public string OrderType { get; set; }
        public string TransactionId { get; set; }

        public string TransactionStatus { get; set; }

        public List<reports> Orderinfo { get; set; }

    }
}