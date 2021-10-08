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
    public class KupiLek2Model : PageModel
    {
        [BindProperty]
        public String Prosledjeno { get; set; }
        [BindProperty]
        public Korisnik Korisnik { get; set; }
        [BindProperty]
        public Lek Lek { get; set; }
        public Lek Lek2 { get; set; }
        public IMongoCollection<Korisnik> collectionK { get; set; }
        [BindProperty]
        public Korisnik korisnik { get; set; }
        public IMongoCollection<Lek> collectionL { get; set; }
        [BindProperty]
        public Lek lek { get; set; }
        [BindProperty]
        public String PorukaKorisniku { get; set; }
        public IActionResult OnGet([FromRoute] String id)
        {
            Prosledjeno = id;
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");

            collectionL = database.GetCollection<Lek>("lekovi");
            // collectionK = database.GetCollection<Korisnik>("korisnici");

            lek = collectionL.Find(x => x.Id == ObjectId.Parse(id)).FirstOrDefault();
            // korisnici = collectionK.Find(x => x.Id == Korisnik.Id).ToList();

            return Page();
        }
        public IActionResult OnPost([FromRoute] String id)
        {
            //Lek.Id = ObjectId.Parse(id);
            //Korisnik.BrojZdravstveneKnjizice = float.Parse(id);
            Prosledjeno = id;
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");

            collectionL = database.GetCollection<Lek>("lekovi");
            //collectionK = database.GetCollection<Korisnik>("korisnici");
            ObjectId LekId = ObjectId.Parse(id);
            lek = collectionL.Find(x => x.Id == LekId).FirstOrDefault();
            //korisnici = collectionK.Find(x => x.BrojZdravstveneKnjizice == Korisnik.BrojZdravstveneKnjizice).ToList();

            return Page();
        }

        public IActionResult OnPostKupi(String id)
        {

            //Korisnik.BrojZdravstveneKnjizice = float.Parse(id);
            Prosledjeno = id;
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");

            collectionL = database.GetCollection<Lek>("lekovi");
            collectionK = database.GetCollection<Korisnik>("korisnici");

            lek = collectionL.Find(x => x.Id == ObjectId.Parse(Prosledjeno)).FirstOrDefault();
            korisnik = collectionK.Find(x => x.BrojZdravstveneKnjizice == Korisnik.BrojZdravstveneKnjizice).FirstOrDefault();

            

            if (lek.DaLiJeNaRecept == "da")
            {
                //ako je na recept proveri da li postoji recept kod korisnika
                if(korisnik.Recepti.Count!=0)
                {
                    foreach(Recept r in korisnik.Recepti)
                    {
                        if(r.Ordinatio==lek.GenerickiNaziv)
                        {
                            PorukaKorisniku = "Ovo je lek koji se izdaje na recept, Vi imate recept za njega ali je neophodno da ga preuzmete u apoteci";
                        }
                    }
                    if (PorukaKorisniku==null)
                    {
                        PorukaKorisniku = "Nemate recept za ovaj lek";
                    }
                }
                else
                {
                    PorukaKorisniku = "Nemate recept za ovaj lek";
                }
            }
            else
            {
                if (int.Parse(lek.Kolicina) > 0)
                {
                    int novaKolicina = int.Parse(lek.Kolicina) - 1;
                    lek.Kolicina = novaKolicina.ToString();
                    var res = Builders<Lek>.Filter.Eq(pd => pd.Id, lek.Id);
                    var operation = Builders<Lek>.Update.Set(u => u.Kolicina, lek.Kolicina);
                    database.GetCollection<Lek>("lekovi").UpdateOne(res, operation);
                }
                PorukaKorisniku = "Uspesno ste kupili lek, bice poslat na Vasu adresu!";
            }

            return Page();
        }
    }
}
