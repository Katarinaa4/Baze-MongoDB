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

namespace BazeApoteka.Pages
{
    public class KupiLekModel : PageModel
    {
        [BindProperty]
        public Korisnik Korisnik { get; set; }
        [BindProperty]
        public Lek Lek { get; set; }
        public IMongoCollection<Korisnik> collectionK { get; set; }
        [BindProperty]
        public List<Korisnik> korisnici { get; set; }
        public IMongoCollection<Lek> collectionL { get; set; }
        [BindProperty]
        public List<Lek> lekovi { get; set; }
        public IActionResult OnGet([FromRoute] String id)
        {
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");

            collectionL = database.GetCollection<Lek>("lekovi");
           // collectionK = database.GetCollection<Korisnik>("korisnici");

            lekovi = collectionL.Find(x => x.Id == Lek.Id).ToList();
           // korisnici = collectionK.Find(x => x.Id == Korisnik.Id).ToList();

            return Page();
        }
        public IActionResult OnPost([FromRoute]String id)
        {
            Lek.Id = ObjectId.Parse(id);
            //Korisnik.BrojZdravstveneKnjizice = float.Parse(id);

            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");

            collectionL = database.GetCollection<Lek>("lekovi");
            //collectionK = database.GetCollection<Korisnik>("korisnici");

            lekovi = collectionL.Find(x => x.Id == Lek.Id).ToList();
            //korisnici = collectionK.Find(x => x.BrojZdravstveneKnjizice == Korisnik.BrojZdravstveneKnjizice).ToList();

            return Page();
        }

        public IActionResult OnPostKupi()
        {
            
            //Korisnik.BrojZdravstveneKnjizice = float.Parse(id);

            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");

            collectionL = database.GetCollection<Lek>("lekovi");
            collectionK = database.GetCollection<Korisnik>("korisnici");

            lekovi = collectionL.Find(x => x.Id == Lek.Id).ToList();
            korisnici = collectionK.Find(x => x.BrojZdravstveneKnjizice == Korisnik.BrojZdravstveneKnjizice).ToList();

            Lek lek = lekovi.FirstOrDefault();

            if (lek.DaLiJeNaRecept == "da")
            {
                //ako je na recept proveri da li postoji recept kod korisnika
            }
            else
            {
                if (int.Parse(lek.Kolicina)>0)
                {
                    int novaKolicina = int.Parse(lek.Kolicina) - 1;
                    lek.Kolicina = novaKolicina.ToString();
                    var res = Builders<Lek>.Filter.Eq(pd => pd.Id, lek.Id);
                    var operation = Builders<Lek>.Update.Set(u => u.Kolicina, lek.Kolicina);
                    database.GetCollection<Lek>("lekovi").UpdateOne(res, operation);
                }
            }

            return Page();
        }
    }
}
