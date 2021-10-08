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
using System.Security.Policy;
using MongoDB.Thin;
using MongoDB.Driver.Builders;

namespace BazeApoteka.Pages
{
    public class DodajLekFModel : PageModel
    {
        [BindProperty]
        public Lek Lek { get; set; }
        [BindProperty]
        public String Prosledjeno { get; set; }

        public IMongoCollection<Lek> collection { get; set; }
        public IActionResult OnGet([FromRoute] String id)
        {
            Prosledjeno = id;
            return Page();
        }
        public IActionResult OnPost([FromRoute] String id)
        {
            Prosledjeno = id;
            return Page();
        }

        public IActionResult OnPostDodaj([FromRoute] String pom)
        {
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");

            collection = database.GetCollection<Lek>("lekovi");

            //fali da se doda referenca kod apoteke na lek, treba iz id-a farmaceuta koji se nalazi u 
            //promenljivoj prosledjeno da se izvuce referenca na apoteku, pa da se ta apoteka doda leku
            //kao referenca 

            collection.InsertOne(Lek);

            return RedirectToPage();
        }
    }
}
