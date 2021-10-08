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
    public class UcitajSveLekoveModel : PageModel
    {
        public IMongoCollection<Lek> collection { get; set; }
        public IMongoCollection<Apoteka> collection2 { get; set; }
        [BindProperty]
        public List<Lek> lekovi { get; set; }
        [BindProperty]
        public Apoteka apoteka { get; set; }
        [BindProperty]
        public List<Apoteka> apoteke { get; set; }
        [BindProperty]
        public List<Apoteka> apoteke2 { get; set; }
        public void OnGet()
        {
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");
            collection = database.GetCollection<Lek>("lekovi");

            lekovi = collection.Find(FilterDefinition<Lek>.Empty).ToList();

            collection2 = database.GetCollection<Apoteka>("apoteke");

            apoteke = collection2.Find(FilterDefinition<Apoteka>.Empty).ToList();

            foreach (Lek l in lekovi)
            {
                Apoteka a = collection2.Find(x => x.Id == l.MojaApoteka.Id).FirstOrDefault();
                apoteka.Naziv = a.Naziv;
            }
        }
        public IActionResult OnPost()
        {
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");


            collection = database.GetCollection<Lek>("lekovi");
            lekovi = collection.Find(FilterDefinition<Lek>.Empty).ToList();
          
            return Page();
        }
    }
}