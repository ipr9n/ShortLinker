using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Services.DataAccess.EntityFramework.Entityes;
using Services.DataAccess.EntityFramework.Interfaces;

namespace Services.DataAccess.EntityFramework.Repository
{
    public class LinkRepository : ILinkRepository
    {
        private ApplicationContext dbContext = new ApplicationContext();

        public void AddLink(Link link)
        {
            dbContext.Links.Add(link);
            dbContext.SaveChanges();
        }

        public Link GetLinkById(string id)
        {
            return dbContext.Links.FirstOrDefault(x => x.Id.ToString() == id);
        }

        public void AddViewCount(string id)
        {
            dbContext.Links.FirstOrDefault(x => x.CutLink == id).ViewCount++;
            dbContext.SaveChanges();
        }

        public Link GetLinkByShortLink(string shortLink)
        {
            return dbContext.Links.FirstOrDefault(x => x.CutLink == shortLink);
        }

       public bool IsShortLinkExist(string link)
        {
            if (dbContext.Links.Count(x => x.CutLink == link) > 0)
                return true;
            return false;
        }

        public List<Link> GetLinks()
        {
            return dbContext.Links.ToList();
        }

        public void RemoveLinkById(string id)
        {
            dbContext.Links.Remove(dbContext.Links.FirstOrDefault(x=>x.Id.ToString() == id));
            dbContext.SaveChanges();
        }

        public void UpdateLink(Link link)
        {
           var dbLink= dbContext.Links.FirstOrDefault(x => x.Id == link.Id);
           dbLink.CreateTime = link.CreateTime;
           dbLink.CutLink = link.CutLink;
           dbLink.ViewCount = link.ViewCount;
           dbContext.SaveChanges();
        }
    }
}
