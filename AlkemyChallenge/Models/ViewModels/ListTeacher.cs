using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlkemyChallenge.Models.ViewModels
{
    public class ListTeacher
    {
        public int Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public int DNI { get; set; }
        public string File_Teacher { get; set; }
        public bool Active { get; set; }
    }
}