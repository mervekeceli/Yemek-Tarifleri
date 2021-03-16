using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Proje.Data;
using Proje.Models;
using Microsoft.Extensions.Localization;

namespace Proje.Controllers
{
    public class AnaYemeklerController : Controller
    {
        private ApplicationDbContext _context;

        private readonly IStringLocalizer<AnaYemeklerController> _localizer;

        public AnaYemeklerController(ApplicationDbContext context, IStringLocalizer<AnaYemeklerController> localizer)
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

            var anaYemekler = yemekList.Where(x => x.KategoriAdi == "AnaYemekler").ToList();

            return View(anaYemekler);
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
                                 KacKisilik = y.KacKisilik,
                                 Resim = y.Resim,


                                 KategoriAdi = k.KategoriAdi


                             })
                             .ToList();




            foreach (var item in yemekList)
            {
                item.Malzemeler = GetirMalzemeListesi(item.YemekId);

            }

            var anaYemek = yemekList.Where(x => x.YemekId == id).ToList();

            return View(anaYemek);

        }

            

        

    }

    
}


