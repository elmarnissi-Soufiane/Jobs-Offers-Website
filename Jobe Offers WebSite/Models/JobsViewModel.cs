using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jobe_Offers_WebSite.Models
{
    public class JobsViewModel
    {

        // li takhzine 3amaliyate group . 

        public string JobTitle { get; set; }
        public IEnumerable<ApplyForJob> Items { get; set; }

    }
}