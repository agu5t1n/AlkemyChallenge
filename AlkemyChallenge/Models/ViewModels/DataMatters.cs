using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlkemyChallenge.Models.ViewModels
{
    public class DataMatters
    {
        public int Id { get; set; }
        [Required]
        [StringLength(60)]
        [Display(Name = "Nombre de la Materia")]
        public string Name_Matter { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Inicio")]
        public DateTime Schedule { get; set; }
        [Required]
        [Display(Name = "ID_Teacher")]
        public int Id_Teacher { get; set; }
        [Required]
        [Display(Name = "Cupo")]
        public int Quota { get; set; }
    }
}