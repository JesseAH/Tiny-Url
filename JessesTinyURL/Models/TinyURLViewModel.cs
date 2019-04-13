using JessesTinyURL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JessesTinyURL.Models
{
    public class TinyURLViewModel
    {
        public IList<TinyURLs> MyURLs { get; set; }
        public TinyURLs input { get; set; }
    }
}
