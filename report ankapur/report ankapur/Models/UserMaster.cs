using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace report_ankapur.Models
{
    public class UserMaster
    {
        [Key]
        [StringLength(10)]
        public string ID { get; set; }

        [StringLength(10)]
        public string EmailId { get; set; }

        [StringLength(50)]
        public string First_Name { get; set; }

        [StringLength(50)]
        public string Last_Name { get; set; }
        public DateTime? Created_Date { get; set; }
        [StringLength(50)]
        public string Password { get; set; }
        public string Phone { get; set; }
        public string activationcode { get; set; }
        public string Profile_Picture { get; set; }
    }
}