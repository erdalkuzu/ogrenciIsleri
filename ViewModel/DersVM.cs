using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OgrenciIsleri.Models;

namespace OgrenciIsleri.ViewModel
{
    public class DersVM
    {
        public Dersler Dersler { get; set; }
        public IEnumerable<Ogretmenler> Ogretmenler { get; set; }
        public IEnumerable<Bolumler> Bolumler { get; set; }

    }
}