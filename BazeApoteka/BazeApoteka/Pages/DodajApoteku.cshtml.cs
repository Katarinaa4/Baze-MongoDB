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
    public class DodajApotekuModel : PageModel
    {
        [BindProperty]
        public Apoteka Apoteka { get; set; }
        public IMongoCollection<Apoteka> collection { get; set; }
        public void OnGet()
        {
          
        }
        public IActionResult OnPostDodaj()
        {
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");


            collection = database.GetCollection<Apoteka>("apoteke");
           
            collection.InsertOne(Apoteka);
            String jj = Apoteka.Id.ToString();

            return RedirectToPage("./DodajApoteku2", new { id = jj} );
        }
    }
}