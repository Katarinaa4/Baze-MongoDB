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
    public class DodajLekaraModel : PageModel
    {
        [BindProperty]
        public Lekar Lekar { get; set; }
        public IMongoCollection<Lekar> collection { get; set; }
        [BindProperty]
        public bool ok { get; set; }

        public IActionResult OnGet()
        {
            
            return Page();
        }
        public IActionResult OnPost([FromRoute] String id)
        {
            
            return Page();
        }

        public IActionResult OnPostDodaj([FromRoute] String pom)
        {
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");


            collection = database.GetCollection<Lekar>("lekari");

            collection.InsertOne(Lekar);

            ok = true;
            return Page();
        }
    }
}
