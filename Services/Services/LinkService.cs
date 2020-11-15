using System;
using System.Collections.Generic;
using System.Text;

using LinkCutter.Models;
using Services.DataAccess.EntityFramework.Entityes;
using Services.DataAccess.EntityFramework.Interfaces;
using Services.DataAccess.EntityFramework.Repository;
using Services.Interfaces;

namespace Services.Services
{
   public class LinkService : ILinkInterface
    {
        private ILinkRepository Db = new LinkRepository();

        public void DeleteLink(string id)
        {
            Db.RemoveLinkById(id);
        }

        public LinkViewModel GetLink(string id)
        {
            Link tempLink = Db.GetLinkById(id);

            return new LinkViewModel()
            {
                CutLink = tempLink.CutLink,
                CreateTime = tempLink.CreateTime,
                FullLink = tempLink.FullLink,
                Id = tempLink.Id.ToString(),
                ViewCount = tempLink.ViewCount
            };
        }

        public List<LinkViewModel> GetAllLinks()
        {
            var links = Db.GetLinks();

            List<LinkViewModel> outputList = new List<LinkViewModel>();

            links.ForEach(x => outputList.Add(new LinkViewModel()
            {
                CreateTime = x.CreateTime,
                FullLink = x.FullLink,
                CutLink = x.CutLink,
                Id = x.Id.ToString(),
                ViewCount = x.ViewCount
            }));

            return outputList;
        }

        public void AddViewCount(string id)
        {
            Db.AddViewCount(id);
        }

        private string GetShortLink()
        {
            string shortLink = "";
            var r = new Random();
            while (shortLink.Length < 6)
            {
                Char c = (char)r.Next(33, 125);
                if (Char.IsLetterOrDigit(c))
                    shortLink += c;
            }
            return shortLink;
        }

        public string GetShortLink(string fullLink)
        {
            string cutLink;
            if (IsLinkExist(GetLinkWithProtocol(fullLink), out Link outputLink))
                return outputLink.CutLink;

            do
            {
                cutLink = GetShortLink();
            } while (Db.IsShortLinkExist(cutLink));

            var newLink = new Link()
            {
                CreateTime = DateTime.Now,
                FullLink = GetLinkWithProtocol(fullLink),
                CutLink = cutLink,
                ViewCount = 0
            };

            Db.AddLink(newLink);

            return newLink.CutLink;
        }

        public bool IsLinkExist(string fullLink, out Link outputLink)
        {
            var Links = Db.GetLinks();
            outputLink = Links.Find(x => x.FullLink == fullLink);

            if (outputLink != null)
                return true;
            return false;
        }

        public bool IsLinkExist(string shortLink)
        {
            var Links = Db.GetLinks();
            if (Links.Find(x => x.CutLink == shortLink) != null)
                return true;
            return false;
        }

        public string GetFullLink(string id)
        {
            return Db.GetLinkByShortLink(id).FullLink;
           return Db.GetLinkById(id).FullLink;
        }

        public string GetLinkWithProtocol(string link)
        {
            if (link != null)
            {
                if (link.Contains("https://") || link.Contains("http://"))
                {
                    return (link);
                }
                else
                {
                    link = link.Insert(0, "https://");
                    return (link);
                }
            }

            return null;
        }

        public void UpdateLink(LinkViewModel model)
        {
            Link tempLink = new Link()
            {
                Id = Guid.Parse(model.Id),
                CutLink = model.CutLink,
                CreateTime = model.CreateTime,
                ViewCount = model.ViewCount
            };

            Db.UpdateLink(tempLink);
        }
    }
}
