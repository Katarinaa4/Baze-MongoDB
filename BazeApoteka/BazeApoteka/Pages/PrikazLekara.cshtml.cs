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
using Microsoft.AspNetCore.Components.Server;

namespace BazeApoteka.Pages
{
    public class PrikazLekaraModel : PageModel
    {
        [BindProperty]
        public String Prosledjeno { get; set; }
        public IMongoCollection<Lekar> collection { get; set; }
        public IMongoCollection<Korisnik> Korisnici { get; set; }
        [BindProperty]
        public Lekar lekar { get; set; }
        [BindProperty]
        public List<Korisnik> pacijenti { get; set; }
        public void OnGet([FromRoute] String id)
        {
            Prosledjeno = id;
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");


            collection = database.GetCollection<Lekar>("lekari");
            Korisnici = database.GetCollection<Korisnik>("korisnici");

            ObjectId iid = ObjectId.Parse(Prosledjeno);
            lekar = collection.Find(x => x.Id == iid).FirstOrDefault();
            pacijenti = Korisnici.Find(x => x.Doktor.Id == lekar.Id).ToList();

        }
        public IActionResult OnPost([FromRoute] String id)
        {
            Prosledjeno = id;
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");


            collection = database.GetCollection<Lekar>("lekari");
            Korisnici = database.GetCollection<Korisnik>("korisnici");
           

            ObjectId iid = ObjectId.Parse(Prosledjeno);
            lekar = collection.Find(x => x.Id == iid).FirstOrDefault();
            pacijenti = Korisnici.Find(x => x.Doktor.Id == lekar.Id).ToList();

            return Page();
        }
        public IActionResult OnPostPrepisiRecept(string pom, string pom2)
        {
            ObjectId iid = ObjectId.Parse(pom);
            ObjectId idlekara = ObjectId.Parse(pom2);
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");


            collection = database.GetCollection<Lekar>("lekari");
            Korisnici = database.GetCollection<Korisnik>("korisnici");
            IMongoCollection<Recept> recepti = database.GetCollection<Recept>("recepti");

            lekar = collection.Find(x => x.Id == idlekara).FirstOrDefault();
            Korisnik pacijent = Korisnici.Find(x => x.Id == iid).FirstOrDefault();

            Recept recept = new Recept();
            recept.Lekar = new MongoDBRef("recepti", lekar.Id);
            recept.Pacijent = new MongoDBRef("recepti", pacijent.Id);
            recepti.InsertOne(recept);


            


            return RedirectToPage("./PrepisiRecept", new { id = recept.Id });
        }
    }
}
