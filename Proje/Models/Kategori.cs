using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Proje.Models
{
    public class Kategori
    {
        [Display(Name = "KategoriId")]
        public int KategoriId { get; set; }

        [Display(Name = "KategoriAdi")]
        public string KategoriAdi { get; set; }

        //public ICollection<Yemek> Yemek{ get; set; }
    }
}
