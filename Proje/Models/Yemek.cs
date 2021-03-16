using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Proje.Models
{
    public class Yemek
    {
        public int Id { get; set; }

        public string Adi { get; set; }

        public string Tarif { get; set; }

        public string Resim { get; set; }

        public int? KacKisilik { get; set; }

        public int? HazirlikSuresi { get; set; }

        public int? PisirmeSuresi { get; set; }

        [DataType(DataType.Date)]
        public DateTime YuklemeTarihi { get; set; }

        public int KategoriId { get; set; }
        public Kategori Kategori { get; set; }
    }

    public class YemekDTO
    {
        [Display(Name = "YemekId")]
        public int YemekId { get; internal set; }

        [Display(Name = "YemekBaslıgı")]
        public string YemekBaslıgı { get; internal set; }

        [Display(Name = "Tarifi")]
        public string Tarifi { get; internal set; }

        [DataType(DataType.Date)]
        public DateTime YuklemeTarihi { get; internal set; }

        [Display(Name = "KacKisilik")]
        public int? KacKisilik { get; internal set; }

        [Display(Name = "KategoriId")]
        public int KategoriId { get; internal set; }

        [Display(Name = "HazirlikSuresi")]
        public int? HazirlikSuresi { get; internal set; }

        [Display(Name = "PisirmeSuresi")]
        public int? PisirmeSuresi { get; internal set; }

        [Display(Name = "Resim")]
        public string Resim { get; internal set; }

        [Display(Name = "Malzemeler")]
        public string Malzemeler { get; internal set; }

        [Display(Name = "Miktari")]
        public string Miktari { get; internal set; }

        [Display(Name = "MalzemeId")]
        public int MalzemeId { get; internal set; }

        [Display(Name = "KategoriAdi")]
        public string KategoriAdi { get; internal set; }

        public string KategoriContreller { get; internal set; }
    }
}
