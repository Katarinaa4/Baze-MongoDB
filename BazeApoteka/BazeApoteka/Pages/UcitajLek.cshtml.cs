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
    public class UcitajLekModel : PageModel
    {
        [BindProperty]
        public Lek Lek { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPostPoGenNazivu()
        {
            String s = Lek.GenerickiNaziv;
            return RedirectToPage("./LekGenNaziv", new { id = s });
        }
        public IActionResult OnPostPoKomNazivu()
        {
            String s = Lek.KomercijaniNaziv;
            return RedirectToPage("./LekKomNaziv", new { id = s });
        }
    }
}
