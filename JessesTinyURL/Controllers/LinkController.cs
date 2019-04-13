using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JessesTinyURL.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JessesTinyURL.Controllers
{
    public class LinkController : Controller
    {
        private readonly ApplicationDbContext _db;

        public LinkController(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Redirect tiny url to original url
        /// </summary>
        /// <returns></returns>
        public IActionResult ReDirect(string tinyhash)
        {
            var url = _db.TinyURLs.FirstOrDefault(t => t.TinyURLCode.ToLower() == tinyhash.ToLower());

            //Increase hit count and save
            url.Hits++;
            _db.SaveChanges();

            if (url != null)
                return Redirect(url.URL);
            else
                return RedirectToAction("Index", "Home");
        }
    }
}