using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Proje.Data;
using Proje.Models;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Html;
namespace Proje.Controllers
{
    public class TatlilarController : Controller
    {
        //controllerın sonuna controller yazısını eklemen gerekir
        //TatlilarController olmalı tammdır gözden kaçırmışım o kısmını deneyelim bi
        private ApplicationDbContext _context;
        private readonly IStringLocalizer<TatlilarController> _localizer;

        public TatlilarController(ApplicationDbContext context, IStringLocalizer<TatlilarController> localizer)
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

            var tatlilar = yemekList.Where(x => x.KategoriAdi == "Tatlilar").ToList();

            return View(tatlilar);
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
                MalzemeMiktar += "<li>" + item.Miktari + " " + item.Malzemeler +"</li>";
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

            var tatli = yemekList.Where(x => x.YemekId == id).ToList();

            return View(tatli);
        }




    }

   
}


