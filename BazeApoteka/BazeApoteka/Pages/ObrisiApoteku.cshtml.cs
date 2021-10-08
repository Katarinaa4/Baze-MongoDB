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
    public class ObrisiApotekuModel : PageModel
    {

        [BindProperty]
        public String Prosledjeno { get; set; }
        public IMongoCollection<Apoteka> collection { get; set; }
        [BindProperty]
        public List<Apoteka> apoteke { get; set; }
        [BindProperty]
        public Apoteka Apoteka { get; set; }
        public void OnGet()
        {
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");


            collection = database.GetCollection<Apoteka>("apoteke");

            apoteke = collection.Find(FilterDefinition<Apoteka>.Empty).ToList();
        }
        public IActionResult OnPost()
        {
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");


            collection = database.GetCollection<Apoteka>("apoteke");
            apoteke = collection.Find(FilterDefinition<Apoteka>.Empty).ToList();
            return Page();
        }
        public IActionResult OnPostObrisi()
        {

            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");
            collection = database.GetCollection<Apoteka>("apoteke");
            Apoteka = collection.Find(x => x.RegistarskiBroj == Prosledjeno).FirstOrDefault();
            
                String idobrisi = Apoteka.Id.ToString();
                ObjectId objectId = ObjectId.Parse(idobrisi);
                collection.DeleteOne(x => x.Id == objectId);
            

            return RedirectToPage("./ObrisiApoteku");
        }
    }
}
