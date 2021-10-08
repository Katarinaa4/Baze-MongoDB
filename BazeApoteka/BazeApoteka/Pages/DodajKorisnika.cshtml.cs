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
    public class DodajKorisnikaModel : PageModel
    {
        [BindProperty]
        public Korisnik Korisnik { get; set; }
        public IMongoCollection<Korisnik> collection { get; set; }
        public IMongoCollection<Lekar> l { get; set; }
        [BindProperty]
        public List<Lekar> lekari { get; set; }
        [BindProperty]
        public String idL { get; set; }

        public IActionResult OnGet()
        {
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");


            l = database.GetCollection<Lekar>("lekari");
            lekari = l.Find(FilterDefinition<Lekar>.Empty).ToList();
            return Page();
        }
        public IActionResult OnPost()
        {
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");


            l = database.GetCollection<Lekar>("lekari");
            lekari = l.Find(FilterDefinition<Lekar>.Empty).ToList();

            return Page();
        }

        public IActionResult OnPostDodaj()
        {
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");


            collection = database.GetCollection<Korisnik>("korisnici");
            collection.InsertOne(Korisnik);

            return RedirectToPage("./IzaberiLekara", new { id = Korisnik.Id });
        }
    }
}
