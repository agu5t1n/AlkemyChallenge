using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlkemyChallenge.Models.ViewModels
{
    public class ListMatters
    {
        public int Id { get; set; }
        public string Name_Matter { get; set; }
        public DateTime Schedule { get; set; }
        public int Teacher { get; set; }
        public int Quota { get; set; }

    }
}