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
    public class IzmeniLekaraModel : PageModel
    {
        [BindProperty]
        public String IdL { get; set; }
        [BindProperty]
        public Lekar Lekar { get; set; }
        [BindProperty]
        public Lekar Lekar2 { get; set; }
        public IMongoCollection<Lekar> collection { get; set; }
        [BindProperty]
        public bool ok { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPostIzmena()
        {
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");

            collection = database.GetCollection<Lekar>("lekari");

            Lekar2 = collection.Find(x => x.Id == ObjectId.Parse(IdL)).FirstOrDefault();

            if (Lekar2 != null)
            {
                var res = Builders<Lekar>.Filter.Eq(pd => pd.Id, Lekar2.Id);
                if (Lekar.Ime != null)
                {
                    var operation = Builders<Lekar>.Update.Set(u => u.Ime, Lekar.Ime);
                    database.GetCollection<Lekar>("lekari").UpdateOne(res, operation);
                }
                if (Lekar.Prezime != null)
                {
                    var operation = Builders<Lekar>.Update.Set(u => u.Prezime, Lekar.Prezime);
                    database.GetCollection<Lekar>("lekari").UpdateOne(res, operation);
                }
                if (Lekar.UstanovaGdeRadi != null)
                {
                    var operation = Builders<Lekar>.Update.Set(u => u.UstanovaGdeRadi, Lekar.UstanovaGdeRadi);
                    database.GetCollection<Lekar>("lekari").UpdateOne(res, operation);
                }
            }
            Lekar2 = collection.Find(x => x.Id == ObjectId.Parse(IdL)).FirstOrDefault();
            ok = true;
            return Page();
        }
    }
}
