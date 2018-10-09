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

    public partial class Notlar
    {
        public int ID { get; set; }
        public Nullable<int> OgretmenID { get; set; }
        public Nullable<int> BolumID { get; set; }
        public Nullable<int> OgrencıID { get; set; }
        public Nullable<int> DersID { get; set; }
        [Required(ErrorMessage ="Not alanı boş geçilemez.")]
        [Range(1,100,ErrorMessage ="Bu alan 1 ile 100 arasın da olmalıdır.")]
        public Nullable<int> Notu { get; set; }
    
        public virtual Bolumler Bolumler { get; set; }
        public virtual Dersler Dersler { get; set; }
        public virtual Ogrenciler Ogrenciler { get; set; }
        public virtual Ogretmenler Ogretmenler { get; set; }
    }
}