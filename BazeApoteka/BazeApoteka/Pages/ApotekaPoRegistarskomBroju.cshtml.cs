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
    public class ApotekaPoRegistarskomBrojuModel : PageModel
    {
        [BindProperty]
        public String Prosledjeno { get; set; }

        [BindProperty]
        public IMongoCollection<Apoteka> collection { get; set; }

        [BindProperty]
        public List<Apoteka> apoteke { get; set; }

        public IActionResult OnGet([FromRoute] String id)
        {
            Prosledjeno = id;
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");
            collection = database.GetCollection<Apoteka>("apoteke");
            apoteke = collection.Find(x => x.RegistarskiBroj == Prosledjeno).ToList();
            return Page();
        }

        public IActionResult OnPost([FromRoute] String id)
        {
            Prosledjeno = id;
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");
            collection = database.GetCollection<Apoteka>("apoteke");
            apoteke = collection.Find(x => x.RegistarskiBroj == Prosledjeno).ToList();
            return Page();
        }
    }
}
