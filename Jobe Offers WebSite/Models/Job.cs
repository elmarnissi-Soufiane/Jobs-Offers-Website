using Jobe_Offers_WebSite.Migrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace Jobe_Offers_WebSite.Models
{
    public class Job
    {
        public int Id { get; set; }

        [Display(Name = "إسم الوظيفة")]
        public string JobTitle { get; set; }


        [Display(Name = "وصف الوظيفة")]
        [AllowHtml]
        public string JobContent { get; set; }

        [Display(Name = "صورة الوظيفة")]
        public string JobImage { get; set; }

        [Display(Name = "نوع الوظيفة")]
        public int CategoryId { get; set; }


        // nacher alwadifa . 
        public string UserID { get; set; }

        public virtual Category Category { get; set; }


        // le nom li kayrbet bin hadi User et UserID khasso ykone bhal bhal . (User)
        public virtual ApplicationUser User { get; set; }

        // public virtual ICollection<ApplyJobs> ApplyJobs { get; set; }

    }
}