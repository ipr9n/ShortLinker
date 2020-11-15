using System;
using System.Collections.Generic;
using System.Text;
using Services.DataAccess.EntityFramework.Entityes;

namespace Services.DataAccess.EntityFramework.Interfaces
{
    public interface ILinkRepository
    {
        Link GetLinkById(string id);
        void AddViewCount(string id);
        bool IsShortLinkExist(string link);
        Link GetLinkByShortLink(string shortLink);
        List<Link> GetLinks();
        void AddLink(Link link);
        void RemoveLinkById(string id);
        void UpdateLink(Link link);
    }
}
