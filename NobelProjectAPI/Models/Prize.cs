using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NobelProjectAPI.Models
{
    public class Prize
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string Category { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
    }
}