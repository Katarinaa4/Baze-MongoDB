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
    public class KorisnikModel : PageModel
    {
        [BindProperty]
        public String Komercijani { get; set; }
        [BindProperty]
        public String Genericki { get; set; }
        
        public IMongoCollection<Lek> collection { get; set; }

        [BindProperty]
        public List<Lek> LekoviZaPrikaz { get; set; }
        [BindProperty]
        public bool Nadjeno { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPostKomercijalni()
        {
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");

            collection = database.GetCollection<Lek>("lekovi");
            LekoviZaPrikaz = collection.Find(x => x.KomercijaniNaziv == Komercijani).ToList();
            Nadjeno = true;
            return Page();
           // Lekar2 = collection.Find(x => x.Id == ObjectId.Parse(IdL)).FirstOrDefault();
        }
        public IActionResult OnPostGenericki()
        {
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");

            collection = database.GetCollection<Lek>("lekovi");
            LekoviZaPrikaz = collection.Find(x => x.GenerickiNaziv == Genericki).ToList();
            Nadjeno = true;
            return Page();
            // Lekar2 = collection.Find(x => x.Id == ObjectId.Parse(IdL)).FirstOrDefault();
        }
    }
}
