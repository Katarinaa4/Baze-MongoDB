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
    public class IzaberiLekaraModel : PageModel
    {
        [BindProperty]
        public String Prosledjeno { get; set; }
        public IMongoCollection<Lekar> collection { get; set; }
        public IMongoCollection<Korisnik> collectionK { get; set; }
        [BindProperty]
        public List<MongoDBRef> pacijenti { get; set; }
        [BindProperty]
        public List<Lekar> lekari { get; set; }
        [BindProperty]
        public Korisnik Korisnik { get; set; }
        [BindProperty]
        public Lekar Lekar { get; set; }
        [BindProperty]
        public bool ok { get; set; }
        public IActionResult OnGet([FromRoute] String id)
        {
            ok = false;
            Prosledjeno = id;
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");


            collection = database.GetCollection<Lekar>("lekari");

            lekari = collection.Find(FilterDefinition<Lekar>.Empty).ToList();
            return Page();
        }
        public IActionResult OnPost([FromRoute] String id)
        {
            ok = false;
            Prosledjeno = id;
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");


            collection = database.GetCollection<Lekar>("lekari");

            lekari = collection.Find(FilterDefinition<Lekar>.Empty).ToList();
            return Page();
        }

        public IActionResult OnPostIzaberi(String id)
        {
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");

            collection = database.GetCollection<Lekar>("lekari");
            collectionK = database.GetCollection<Korisnik>("korisnici");

            Korisnik = collectionK.Find(x => x.Id == ObjectId.Parse(Prosledjeno)).FirstOrDefault();
            Lekar = collection.Find(x => x.Id == ObjectId.Parse(id)).FirstOrDefault();

            MongoDBRef lekar = new MongoDBRef("lekari", Lekar.Id);
            var res = Builders<Korisnik>.Filter.Eq(pd => pd.Id, Korisnik.Id);
            var operation = Builders<Korisnik>.Update.Set(u => u.Doktor, lekar);
            database.GetCollection<Korisnik>("korisnici").UpdateOne(res, operation);

            List<MongoDBRef> pacijenti = new List<MongoDBRef>();
            pacijenti = Lekar.Pacijenti;
            pacijenti.Add(new MongoDBRef("korisnici", Korisnik.Id));
            var res1 = Builders<Lekar>.Filter.Eq(pd => pd.Id, Lekar.Id);
            var operation1 = Builders<Lekar>.Update.Set(u => u.Pacijenti, pacijenti);
            database.GetCollection<Lekar>("lekari").UpdateOne(res1, operation1);
            ok = true;
            return Page();
        }
       
    }
}
