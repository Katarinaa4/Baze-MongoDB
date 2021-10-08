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
    public class IzmeniKorisnikaModel : PageModel
    {
        [BindProperty]
        public Korisnik Korisnik { get; set; }
        [BindProperty]
        public Korisnik Korisnik2 { get; set; }
        public IMongoCollection<Korisnik> collection { get; set; }
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

            collection = database.GetCollection<Korisnik>("korisnici");

            Korisnik2 = collection.Find(x => x.BrojZdravstveneKnjizice == Korisnik.BrojZdravstveneKnjizice).FirstOrDefault();

            if (Korisnik2 != null)
            {
                var res = Builders<Korisnik>.Filter.Eq(pd => pd.Id, Korisnik2.Id);
                if (Korisnik.Ime != null)
                {
                    var operation = Builders<Korisnik>.Update.Set(u => u.Ime, Korisnik.Ime);
                    database.GetCollection<Korisnik>("korisnici").UpdateOne(res, operation);
                }
                if (Korisnik.Prezime != null)
                {
                    var operation = Builders<Korisnik>.Update.Set(u => u.Prezime, Korisnik.Prezime);
                    database.GetCollection<Korisnik>("korisnici").UpdateOne(res, operation);
                }
                if (Korisnik.Adresa != null)
                {
                    var operation = Builders<Korisnik>.Update.Set(u => u.Adresa, Korisnik.Adresa);
                    database.GetCollection<Korisnik>("korisnici").UpdateOne(res, operation);
                }
                if (Korisnik.BrojZdravstveneKnjizice != null)
                {
                    var operation = Builders<Korisnik>.Update.Set(u => u.BrojZdravstveneKnjizice, Korisnik.BrojZdravstveneKnjizice);
                    database.GetCollection<Korisnik>("korisnici").UpdateOne(res, operation);
                }
            }
            Korisnik2 = collection.Find(x => x.BrojZdravstveneKnjizice == Korisnik.BrojZdravstveneKnjizice).FirstOrDefault();
            ok = true;
            return Page();
        }
    }
}
