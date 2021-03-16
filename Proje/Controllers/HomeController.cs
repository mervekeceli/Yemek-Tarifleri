using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Proje.Data;
using Proje.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Proje.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IStringLocalizer<HomeController> _localizer;

        public HomeController(ApplicationDbContext context, IStringLocalizer<HomeController> localizer)
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


            var anaSayfa = yemekList.OrderBy(x=>x.YemekId).Take(6).ToList();

            return View(anaSayfa);

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
