using OgrenciIsleri.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OgrenciIsleri.ViewModel
{
    public class OgretmenVM
    {
        public Ogretmenler Ogretmenlers { get; set; }
        public IEnumerable<Bolumler> Bolumlers { get; set; }
        public IEnumerable<Cinsiyetler> Cinsiyetlers { get; set; }
        public IEnumerable<Sehirler> Sehirlers { get; set; }
    }
}