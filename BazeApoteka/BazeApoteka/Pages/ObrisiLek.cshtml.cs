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
using MongoDB.Driver.Builders;

namespace BazeApoteka.Pages
{
    public class ObrisiLekModel : PageModel
    {

        [BindProperty]
        public String Prosledjeno { get; set; }
        public IMongoCollection<Lek> collection { get; set; }
        [BindProperty]
        public List<Lek> lekovi { get; set; }
        //[BindProperty]
        // public Lek Lek { get; set; }
        public void OnGet()
        {
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");


            collection = database.GetCollection<Lek>("lekovi");

            lekovi = collection.Find(FilterDefinition<Lek>.Empty).ToList();
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
        public IActionResult OnPostObrisi()
        {

            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");
            collection = database.GetCollection<Lek>("lekovi");
            lekovi = collection.Find(x => x.GenerickiNaziv == Prosledjeno).ToList();
            foreach (Lek l in lekovi)
            {
                String idobrisi = l.Id.ToString();
                ObjectId objectId = ObjectId.Parse(idobrisi);
                collection.DeleteOne(x => x.Id == objectId);
            }

            return RedirectToPage("./ObrisiLek");
        }
    }
}
