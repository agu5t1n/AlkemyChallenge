using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlkemyChallenge.Models.ViewModels
{
    public class DataTeacher
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Nombre")]
        public string First_Name { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Apellido")]
        public string Last_Name { get; set; }
        [Required]
        [Display(Name = "DNI")]
        public int DNI { get; set; }
        [Required]
        [StringLength(20)]
        [Display(Name = "Legajo")]
        public string File_Teacher { get; set; }
    }
}