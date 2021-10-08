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
    public class ObrisiKorisnikaModel : PageModel
    {
        [BindProperty]
        public String Prosledjeno { get; set; }
        public IMongoCollection<Korisnik> collection { get; set; }
        [BindProperty]
        public List<Korisnik> korisnici { get; set; }
        [BindProperty]
        public Korisnik Korisnik { get; set; }
        public void OnGet()
        {
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");


            collection = database.GetCollection<Korisnik>("korisnici");

            korisnici = collection.Find(FilterDefinition<Korisnik>.Empty).ToList();
        }
        public IActionResult OnPost()
        {
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");


            collection = database.GetCollection<Korisnik>("korisnici");
            korisnici = collection.Find(FilterDefinition<Korisnik>.Empty).ToList();
            return Page();
        }
        public IActionResult OnPostObrisi()
        {

            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");
            collection = database.GetCollection<Korisnik>("korisnici");
            korisnici = collection.Find(x => x.Ime == Prosledjeno).ToList();
            foreach (Korisnik a in korisnici)
            {
                String idobrisi = a.Id.ToString();
                ObjectId objectId = ObjectId.Parse(idobrisi);
                collection.DeleteOne(x => x.Id == objectId);
            }

            return RedirectToPage("./ObrisiKorisnika");
        }
    }
}
