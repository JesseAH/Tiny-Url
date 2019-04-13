using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JessesTinyURL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using JessesTinyURL.Data;
using Microsoft.AspNetCore.Http;

namespace JessesTinyURL.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _db;
        private readonly HttpContext _context;
        private static Random random = new Random();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userManager"></param>
        public HomeController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _db = db;
            _context = this.HttpContext;
        }

        #region Views

        public IActionResult Index()
        {
            TinyURLViewModel viewModel = new TinyURLViewModel()
            {
                MyURLs = _db.TinyURLs.Where(t => t.AspNetUserId.ToLower() == UserId().ToLower()).ToList(),
                input = new TinyURLs()
            };

            return View(viewModel);
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        #endregion


        #region Actions

        #endregion


        #region Helpers

        [HttpPost]
        public IActionResult PostTinyUrl(TinyURLs input)
        {
            input.AspNetUserId = UserId();

            //Create and Save new Tiny URL Record
            _db.TinyURLs.Add(input);
            _db.SaveChanges();

            //Encode and Save unique Tiny URL Code
            input.TinyURLCode = GetTinyCode(input.Id);
            input.TinyURL = "https://" + GetBaseUrl() + "/" + input.TinyURLCode;
            _db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Return logged in user's id
        /// </summary>
        /// <returns></returns>
        private string UserId()
        {
            var userId = _userManager.GetUserId(User);
            return userId;
        }

        /// <summary>
        /// Generate Tiny URL Code
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetTinyCode(int id)
        {
            return Base32.EncodeAsBase32String(id.ToString(), false);
        }

        public string GetBaseUrl()
        {
            var request = this.Url.ActionContext.HttpContext.Request.Host.Value;

            return request;
        }

        #endregion


    }
}
