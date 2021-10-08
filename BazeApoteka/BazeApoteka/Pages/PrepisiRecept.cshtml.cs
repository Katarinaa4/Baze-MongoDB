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
    public class PrepisiReceptModel : PageModel
    {
        [BindProperty]
        public String Prosledjeno { get; set; }
        public IMongoCollection<Recept> collection { get; set; }
        public IMongoCollection<Korisnik> Korisnici { get; set; }
        public IMongoCollection<Lekar> Lekari { get; set; }
        [BindProperty]
        public Recept recept { get; set; }
        [BindProperty]
        public Korisnik pacijent { get; set; }
        [BindProperty]
        public Lek lek { get; set; }
        [BindProperty]
        public Lekar lekar { get; set; }

        public IActionResult OnGet([FromRoute] String id)
        {
            Prosledjeno = id; //id recepta
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");

            //ucitamo recepte i nadjemo prosledjen 
            collection = database.GetCollection<Recept>("recepti");
            ObjectId iid = ObjectId.Parse(Prosledjeno);
            recept = collection.Find(x => x.Id == iid).FirstOrDefault();

            Korisnici = database.GetCollection<Korisnik>("korisnici");
            pacijent = Korisnici.Find(x => x.Id == recept.Pacijent.Id).FirstOrDefault();

            Lekari = database.GetCollection<Lekar>("lekari");
            lekar = Lekari.Find(x => x.Id == recept.Lekar.Id).FirstOrDefault();

            return Page();
        }
        public IActionResult OnPost([FromRoute] String id)
        {
            Prosledjeno = id; //id recepta
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");

            //ucitamo recepte i nadjemo prosledjen 
            collection = database.GetCollection<Recept>("recepti");
            ObjectId iid = ObjectId.Parse(Prosledjeno);
            recept = collection.Find(x => x.Id == iid).FirstOrDefault();

            Korisnici = database.GetCollection<Korisnik>("korisnici");
            pacijent = Korisnici.Find(x => x.Id == recept.Pacijent.Id).FirstOrDefault();

            return Page();
        }
        public IActionResult OnPostDodaj(String id)
        {
            Prosledjeno = id; //id recepta
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");

            //ucitamo recepte i nadjemo prosledjen 
            collection = database.GetCollection<Recept>("recepti");
           ObjectId iid = ObjectId.Parse(Prosledjeno);
          
            // recept = collection.Find(x => x.Id == iid).FirstOrDefault();

            var res = Builders<Recept>.Filter.Eq(pd => pd.Id, iid);
            var operation = Builders<Recept>.Update.Set(u => u.Ordinatio, recept.Ordinatio);
            var operation1 = Builders<Recept>.Update.Set(u => u.Signatura, recept.Signatura);
            var operation2 = Builders<Recept>.Update.Set(u => u.Subscriptio, recept.Subscriptio);
           collection.UpdateOne(res, operation);
           collection.UpdateOne(res, operation1);
           collection.UpdateOne(res, operation2);

            //treba i korisniku da dodamo recept
            recept = collection.Find(x => x.Id == iid).FirstOrDefault();
            Korisnici = database.GetCollection<Korisnik>("korisnici");
            pacijent = Korisnici.Find(x => x.Id == recept.Pacijent.Id).FirstOrDefault();

            pacijent.Recepti.Add(recept);
            var resK = Builders<Korisnik>.Filter.Eq(pd => pd.Id, pacijent.Id);
            
            var operationK = Builders<Korisnik>.Update.Set(u => u.Recepti, pacijent.Recepti);
            database.GetCollection<Korisnik>("korisnici").UpdateOne(resK, operationK);

                return RedirectToPage("./UspesnoReceptLekar", new { id = iid });
        }
    }
}