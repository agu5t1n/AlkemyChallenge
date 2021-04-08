using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlkemyChallenge.Models.ViewModels
{
    public class DataStudent
    {
        public int Id { get; set; }

        public string First_Name { get; set; }

        public string Last_Name { get; set; }

        public int DNI { get; set; }

        public string File_Student { get; set; }
    }
}