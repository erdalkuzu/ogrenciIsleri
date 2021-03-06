//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OgrenciIsleri.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Dersler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Dersler()
        {
            this.Notlars = new HashSet<Notlar>();
        }
    
        public int ID { get; set; }
        [Required(ErrorMessage ="Ders Adı boş geçilemez")]
        public string DersAdı { get; set; }
        [Required(ErrorMessage = "{0} boş geçilemez")]
        public Nullable<int> OgretmenID { get; set; }
        [Required(ErrorMessage = "{0} boş geçilemez")]
        public Nullable<int> BolumID { get; set; }
    
        public virtual Bolumler Bolumler { get; set; }
        public virtual Ogretmenler Ogretmenler { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Notlar> Notlars { get; set; }
    }
}
