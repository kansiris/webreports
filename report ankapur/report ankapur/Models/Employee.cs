//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace report_ankapur.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Employee
    {
        public string EmpCode { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Mobile1 { get; set; }
        public string Mobile2 { get; set; }
        public Nullable<bool> Active { get; set; }
        public string Password { get; set; }
        public string RestCode { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> UserTypeId { get; set; }
        public string EmailId { get; set; }
        public byte[] PhotoURL { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}
