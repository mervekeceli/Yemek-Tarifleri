using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Proje.Data;
using Proje.Models;

namespace Proje.Controllers
{
    public class SalatalarController : Controller
    {
        private ApplicationDbContext _context;
        private readonly IStringLocalizer<SalatalarController> _localizer;

        public SalatalarController(ApplicationDbContext context, IStringLocalizer<SalatalarController> localizer)
        {
            _context = context;
            _localizer = localizer;
        }
        public IActionResult Index()
        {
            var yemekList = (from k in _context.Kategori
                             join y in _context.Yemek on k.KategoriId equals y.KategoriId

                             select new YemekDTO
                             {
                                 YemekId = y.Id,
                                 YemekBaslıgı = y.Adi,
                                 Tarifi = y.Tarif,
                                 YuklemeTarihi = y.YuklemeTarihi,
                                 KategoriId = k.KategoriId,
                                 HazirlikSuresi = y.HazirlikSuresi,
                                 PisirmeSuresi = y.PisirmeSuresi,
                                 Resim = y.Resim,
                                 KacKisilik = y.KacKisilik,
                                 KategoriAdi = k.KategoriAdi


                             })
                             .ToList();

            foreach (var item in yemekList)
            {
                item.Malzemeler = GetirMalzemeListesi(item.YemekId);
            }

            var salatalar = yemekList.Where(x => x.KategoriAdi == "Salatalar").ToList();

            return View(salatalar);
        }


        public string GetirMalzemeListesi(int FKYemekId)
        {
            string MalzemeMiktar = "";
            var malzemeList = (from a in _context.YemekMalzeme
                               .Where(x => x.YemekId == FKYemekId)
                               select new
                               {
                                   MalzemeId = a.MalzemeId,
                                   Miktari = a.MalzemeMiktari,
                                   Malzemeler = a.Malzeme.MalzemeAdi
                               })
                               .ToList();

            foreach (var item in malzemeList)
            {
                MalzemeMiktar += "<li>" + item.Miktari + " " + item.Malzemeler + "</li>";
            }

            return MalzemeMiktar;
        }

        public IActionResult Detay(int id)
        {
            var yemekList = (from k in _context.Kategori
                             join y in _context.Yemek on k.KategoriId equals y.KategoriId

                             select new YemekDTO
                             {
                                 YemekId = y.Id,
                                 YemekBaslıgı = y.Adi,
                                 Tarifi = y.Tarif,
                                 YuklemeTarihi = y.YuklemeTarihi,
                                 KategoriId = k.KategoriId,
                                 HazirlikSuresi = y.HazirlikSuresi,
                                 PisirmeSuresi = y.PisirmeSuresi,
                                 Resim = y.Resim,
                                 KacKisilik = y.KacKisilik,


                                 KategoriAdi = k.KategoriAdi


                             })
                             .ToList();




            foreach (var item in yemekList)
            {
                item.Malzemeler = GetirMalzemeListesi(item.YemekId);

            }

            var salata = yemekList.Where(x => x.YemekId == id).ToList();

            return View(salata);
        }
    }
}
