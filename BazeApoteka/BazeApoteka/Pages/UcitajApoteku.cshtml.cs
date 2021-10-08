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
    public class UcitajApotekuModel : PageModel
    {

        [BindProperty]
        public Apoteka Apoteka { get; set; }
        public IMongoCollection<Apoteka> collection { get; set; }
        public List<Apoteka> apoteke { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPostPoNazivu()
        {
            String tt = Apoteka.Naziv;
            return RedirectToPage("./ApotekaPoNazivu", new { id = tt });
        }

        public IActionResult OnPostPoAdresi()
        {
            String tt = Apoteka.Adresa;
            return RedirectToPage("./ApotekaPoAdresi", new { id = tt });
        }

        public IActionResult OnPostPoRegistarskomBroju()
        {
            String tt = Apoteka.RegistarskiBroj;
            return RedirectToPage("./ApotekaPoRegistarskomBroju", new { id = tt });
        }
    }
}
