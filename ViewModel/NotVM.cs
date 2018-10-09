using OgrenciIsleri.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OgrenciIsleri.ViewModel
{
    public class NotVM
    {
        OIDbEntities db = new OIDbEntities();
        public IEnumerable<Dersler> Ders { get; set; }
        public IEnumerable<Ogrenciler> Ogrenciler { get; set; }
        public Notlar Not { get; set; }


    }
}