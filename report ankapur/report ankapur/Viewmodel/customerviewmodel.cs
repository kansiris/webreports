using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using report_ankapur.Models;


namespace report_ankapur.Viewmodel
{
    public class customerviewmodel
    {
        public List<reports> orderDetails { get; set; }
        public customer custDetails { get; set; }
    }
}