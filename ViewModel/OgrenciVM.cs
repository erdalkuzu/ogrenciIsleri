using OgrenciIsleri.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OgrenciIsleri.ViewModel
{
    public class OgrenciVM
    {
        public Ogrenciler Ogrenciler { get; set; }
        public IEnumerable<Cinsiyetler> Cinsiyetlers { get; set; }
        public IEnumerable<Sehirler> Sehirlers { get; set; }
        public IEnumerable<Bolumler> Bolumler { get; set; }
        public IEnumerable<Uyruklar> Uyruklars { get; set; }
        public IEnumerable<Sınıflar> Sınıflars { get; set; }
    }
}