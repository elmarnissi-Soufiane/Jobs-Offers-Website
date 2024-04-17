using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace Jobe_Offers_WebSite.Models
{
    public class ApplyForJob // il a une realtion avce Job et AspUsers (Mini To Mini) . (soufiane)
    {
        // nacher Wadefa . lmostafidine ghaytkhezno hena fhade model . 

        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime ApplyDate { get; set; }
        public int JobId { get; set; }
        public string UserId { get; set; }

        public virtual Job job { get; set; }
        public virtual ApplicationUser user { get; set; }

    }
}