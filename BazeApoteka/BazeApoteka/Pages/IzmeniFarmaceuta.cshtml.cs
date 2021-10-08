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
    public class IzmeniFarmaceutaModel : PageModel
    {
        [BindProperty]
        public String IdF { get; set; }
        [BindProperty]
        public Farmaceut Farmaceut { get; set; }
        [BindProperty]
        public Farmaceut Farmaceut2 { get; set; }
        public IMongoCollection<Farmaceut> collection { get; set; }
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

            collection = database.GetCollection<Farmaceut>("farmaceuti");

            Farmaceut2 = collection.Find(x => x.Id == ObjectId.Parse(IdF)).FirstOrDefault();

            if (Farmaceut2 != null)
            {
                var res = Builders<Farmaceut>.Filter.Eq(pd => pd.Id, Farmaceut2.Id);
                if (Farmaceut.Ime != null)
                {
                    var operation = Builders<Farmaceut>.Update.Set(u => u.Ime, Farmaceut.Ime);
                    database.GetCollection<Farmaceut>("farmaceuti").UpdateOne(res, operation);
                }
                if (Farmaceut.Prezime != null)
                {
                    var operation = Builders<Farmaceut>.Update.Set(u => u.Prezime, Farmaceut.Prezime);
                    database.GetCollection<Farmaceut>("farmaceuti").UpdateOne(res, operation);
                }
                if (Farmaceut.Plata != null)
                {
                    var operation = Builders<Farmaceut>.Update.Set(u => u.Plata, Farmaceut.Plata);
                    database.GetCollection<Farmaceut>("farmaceuti").UpdateOne(res, operation);
                }
            }
            Farmaceut2 = collection.Find(x => x.Id == ObjectId.Parse(IdF)).FirstOrDefault();
            ok = true;
            return Page();
        }
    }
}
