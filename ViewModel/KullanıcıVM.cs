using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OgrenciIsleri.Models;

namespace OgrenciIsleri.ViewModel
{
    public class KullanıcıVM
    {
        public Kullanıcılar Kullanıcılar { get; set; }
        public IEnumerable<Ogretmenler> Ogretmenlers { get; set; }
    }
}