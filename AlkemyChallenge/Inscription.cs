//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AlkemyChallenge
{
    using System;
    using System.Collections.Generic;
    
    public partial class Inscription
    {
        public int Id { get; set; }
        public int Id_Student { get; set; }
        public int Id_Matter { get; set; }
    
        public virtual Matters Matters { get; set; }
        public virtual Students Students { get; set; }
    }
}
