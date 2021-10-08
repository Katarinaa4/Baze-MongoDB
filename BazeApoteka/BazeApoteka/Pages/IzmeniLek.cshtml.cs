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
    public class IzmeniLekModel : PageModel
    {
        [BindProperty]
        public String IdL { get; set; }
        [BindProperty]
        public Lek Lek { get; set; }
        [BindProperty]
        public Lek Lek2 { get; set; }
        public IMongoCollection<Lek> collection { get; set; }
        [BindProperty]
        public bool ok { get; set; }

        public void OnGet()
        {
        }
        public IActionResult OnPostIZ(String id)
        {
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");
        
            collection = database.GetCollection<Lek>("lekovi");
            IdL = id;
            return Page();
        }
        public IActionResult OnPostIzmena()
        {
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");

            collection = database.GetCollection<Lek>("lekovi");

            Lek2 = collection.Find(x => x.Id == ObjectId.Parse(IdL)).FirstOrDefault();

            if (Lek2 != null)
            {
                var res = Builders<Lek>.Filter.Eq(pd => pd.Id, Lek2.Id);
                if (Lek.GenerickiNaziv != null)
                {
                    var operation = Builders<Lek>.Update.Set(u => u.GenerickiNaziv, Lek.GenerickiNaziv);
                    database.GetCollection<Lek>("lekovi").UpdateOne(res, operation);
                }
                if (Lek.KomercijaniNaziv != null)
                {
                    var operation = Builders<Lek>.Update.Set(u => u.KomercijaniNaziv, Lek.KomercijaniNaziv);
                    database.GetCollection<Lek>("lekovi").UpdateOne(res, operation);
                }
                if (Lek.Doza != null)
                {
                    var operation = Builders<Lek>.Update.Set(u => u.Doza, Lek.Doza);
                    database.GetCollection<Lek>("lekovi").UpdateOne(res, operation);
                }
                if (Lek.Dejstvo != null)
                {
                    var operation = Builders<Lek>.Update.Set(u => u.Dejstvo, Lek.Dejstvo);
                    database.GetCollection<Lek>("lekovi").UpdateOne(res, operation);
                }
                if (Lek.Indikacije != null)
                {
                    var operation = Builders<Lek>.Update.Set(u => u.Indikacije, Lek.Indikacije);
                    database.GetCollection<Lek>("lekovi").UpdateOne(res, operation);
                }
                if (Lek.Kontraindikacije != null)
                {
                    var operation = Builders<Lek>.Update.Set(u => u.Kontraindikacije, Lek.Kontraindikacije);
                    database.GetCollection<Lek>("lekovi").UpdateOne(res, operation);
                }
                if (Lek.Cena != null)
                {
                    var operation = Builders<Lek>.Update.Set(u => u.Cena, Lek.Cena);
                    database.GetCollection<Lek>("lekovi").UpdateOne(res, operation);
                }
                if (Lek.Kolicina != null)
                {
                    var operation = Builders<Lek>.Update.Set(u => u.Kolicina, Lek.Kolicina);
                    database.GetCollection<Lek>("lekovi").UpdateOne(res, operation);
                }
            }
            ok = true;
            return Page();
        }
    }
}
