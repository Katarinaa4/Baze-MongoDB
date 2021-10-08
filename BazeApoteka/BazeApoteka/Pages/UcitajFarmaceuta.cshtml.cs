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
    public class UcitajFarmaceutaModel : PageModel
    {
        [BindProperty]
        public Farmaceut Farmaceut { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPostPoImenu()
        {
            String s = Farmaceut.Ime;
            return RedirectToPage("./FarmaceutPoImenu", new { id = s });
        }
        public IActionResult OnPostPoPrezimenu()
        {
            String s = Farmaceut.Prezime;
            return RedirectToPage("./FarmaceutPoPrezimenu", new { id = s });
        }
        public IActionResult OnPostPoPlati()
        {
            String s = Farmaceut.Plata.ToString();
            return RedirectToPage("./FarmaceutPoPlati", new { id = s });
        }
    }
}
