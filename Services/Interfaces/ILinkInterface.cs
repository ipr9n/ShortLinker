using System;
using System.Collections.Generic;
using System.Text;
using LinkCutter.Models;
using Services.DataAccess.EntityFramework.Entityes;

namespace Services.Interfaces
{
    public interface ILinkInterface
    {
        string GetShortLink(string fullLink);
        LinkViewModel GetLink(string id);
        void AddViewCount(string id);
        string GetLinkWithProtocol(string link);
        bool IsLinkExist(string fullLink, out Link outputLink);
        bool IsLinkExist(string shortLink);
        string GetFullLink(string id);
        void DeleteLink(string id);
        void UpdateLink(LinkViewModel model);
        List<LinkViewModel> GetAllLinks();
    }
}
