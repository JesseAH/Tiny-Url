using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JessesTinyURL.Data
{
    public class TinyURLs
    {
        public int Id { get; set; }

        public string AspNetUserId { get; set; }

        [Url]
        [Display(Name = "Original URL")]
        public string URL { get; set; }

        [Url]
        [Display(Name = "Tiny URL")]
        public string TinyURL { get; set; }

        public string TinyURLCode { get; set; }
        
        [Display(Name = "Hit Count")]
        public int Hits { get; set; }
    }
}
