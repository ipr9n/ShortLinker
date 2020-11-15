using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LinkCutter.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Services.DataAccess.EntityFramework;
using Services.DataAccess.EntityFramework.Entityes;
using Services.Interfaces;
using Services.Services;

namespace LinkCutter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private ILinkInterface LinkService { get; set; }

     

        public HomeController(ILogger<HomeController> logger)
        {
            LinkService = new LinkService();
            _logger = logger;
        }


        public IActionResult Index()
        {
            ViewBag.Url = Request.GetEncodedUrl();
            var links = LinkService.GetAllLinks();
            links.Reverse();

            ViewBag.Text = TempData["text"]?.ToString();
            return View(links);
        }

        public IActionResult SaveLink(LinkViewModel model)
        {
            LinkService.UpdateLink(model);

            return RedirectToAction("Index");
        }

        public IActionResult EditLink(string id) => View(LinkService.GetLink(id));

        public IActionResult DeleteLink(string id)
        {
            LinkService.DeleteLink(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult CreateLink(string link)
        {
            if (SiteCheck(link))
            {
                string shortLink = LinkService.GetShortLink(link);
                string siteUrl = Request.GetEncodedUrl();
                siteUrl = siteUrl.Remove(siteUrl.IndexOf("Home"));
                TempData["text"] = $"Короткая ссылка: {siteUrl}{shortLink}";
            }
            else
            {
                TempData["text"] = "Сайт недоступен. Проверьте введенные данные";
            }

            return RedirectToAction("Index");
        }

        public IActionResult Link(string id)
        {
            if (LinkService.IsLinkExist(id))
            {
                LinkService.AddViewCount(id);
                return Redirect(LinkService.GetFullLink(id));
            }
            else return View("Error");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private bool SiteCheck(string url)
        {
            try
            {
                Uri uri = new Uri(LinkService.GetLinkWithProtocol(url));
                HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(uri);
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
