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
    
    public partial class Matters
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Matters()
        {
            this.Inscription = new HashSet<Inscription>();
        }
    
        public int Id { get; set; }
        public string Name_Matter { get; set; }
        public System.DateTime Schedule { get; set; }
        public int Quota { get; set; }
        public int Id_Teacher { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Inscription> Inscription { get; set; }
        public virtual Teachers Teachers { get; set; }
    }
}
