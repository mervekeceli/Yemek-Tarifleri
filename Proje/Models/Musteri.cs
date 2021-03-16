using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proje.Models
{
    public class Musteri : IdentityUser
    {
        public string Ad { get; set; }

        public string Soyad { get; set; }


        public DateTime DogumTarihi { get; set; }

       



    }
}
