using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace report_ankapur.Models
{
    public class Login
    {
        
        public string CustPhoneNumber { get; set; }
    
        public string CustomerFName { get; set; }

        public string CustomerLName { get; set; }

        public string Billing_Address { get; set; }

        public string Delivery_Addresss { get; set; }

        public string Land_Mark { get; set; }
        public string Mobile1 { get; set; }
        public string Mobile2 { get; set; }
        public int CustomerTypeId { get; set; }
        public Nullable<double> DeliveryLocationLatitude { get; set; }
        public Nullable<double> DeliveryLocationLongitude { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        //[Required]
        //[StringLength(20, ErrorMessage = "Must be between 5 and 20 characters", MinimumLength = 5)]
        //[DataType(DataType.Password)]
        public string Password { get; set; }

        public string Email { get; set; }
    }
}