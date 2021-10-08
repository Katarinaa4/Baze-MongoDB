using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;
using MongoDB.Bson;
using BazeApoteka.Entiteti;
using MongoDB.Driver.Linq;

namespace BazeApoteka.Pages
{
    public class UcitajKorisnikaModel : PageModel
    {
        [BindProperty]
        public Korisnik Korisnik { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPostPoBrKnjizice()
        {
            String s = Korisnik.BrojZdravstveneKnjizice.ToString();
            return RedirectToPage("./KorisnikBrKnjizice", new { id = s });
        }
    }
}
