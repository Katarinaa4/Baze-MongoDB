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
    public class IzmeniApotekuModel : PageModel
    {
        [BindProperty]
        public Apoteka Apoteka { get; set; }
        [BindProperty]
        public Apoteka Apoteka2 { get; set; }
        public IMongoCollection<Apoteka> collection { get; set; }
        [BindProperty]
        public bool ok { get; set; }
        public void OnGet()
        {
            ok = true;
        }

        public IActionResult OnPostIzmena()
        {
            
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");

            collection = database.GetCollection<Apoteka>("apoteke");

            Apoteka2 = collection.Find(x => x.RegistarskiBroj == Apoteka.RegistarskiBroj).FirstOrDefault();

            if (Apoteka2 != null)
            {
                var res = Builders<Apoteka>.Filter.Eq(pd => pd.Id, Apoteka2.Id);
                if (Apoteka.Naziv != null)
                {
                    var operation = Builders<Apoteka>.Update.Set(u => u.Naziv, Apoteka.Naziv);
                    database.GetCollection<Apoteka>("apoteke").UpdateOne(res, operation);
                }
                if (Apoteka.Adresa != null)
                {
                    var operation2 = Builders<Apoteka>.Update.Set(u => u.Adresa, Apoteka.Adresa);
                    database.GetCollection<Apoteka>("apoteke").UpdateOne(res, operation2);
                }
            }
            ok = true;
            return Page();
        }
    }
}
