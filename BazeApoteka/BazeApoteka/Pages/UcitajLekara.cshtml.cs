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
    public class UcitajLekaraModel : PageModel
    {
        [BindProperty]
        public Lekar Lekar { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPostPoImenu()
        {
            String s = Lekar.Ime;
            return RedirectToPage("./LekarPoImenu", new { id = s });
        }
        public IActionResult OnPostPoPrezimenu()
        {
            String s = Lekar.Prezime;
            return RedirectToPage("./LekarPoPrezimenu", new { id = s });
        }
        public IActionResult OnPostPoUstanovi()
        {
            String s = Lekar.UstanovaGdeRadi;
            return RedirectToPage("./LekarPoUstanovi", new { id = s });
        }
    }
}
