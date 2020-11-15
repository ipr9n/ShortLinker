using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkCutter.Models
{
    public class LinkViewModel
    {
        public string Id { get; set; }
        public string FullLink { get; set; }
        public string CutLink { get; set; }
        public int ViewCount { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
