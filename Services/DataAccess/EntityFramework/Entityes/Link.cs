using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DataAccess.EntityFramework.Entityes
{
    public class Link
    {
        public Guid Id { get; set; }
        public string FullLink { get; set; }
        public string CutLink { get; set; }
        public int ViewCount { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
