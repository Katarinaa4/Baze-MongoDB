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
    public class DodajLek2Model : PageModel
    {
        [BindProperty]
        public Lek Lek { get; set; }
        [BindProperty]
        public Lek Lek2 { get; set; }
        [BindProperty]
        public Apoteka Apoteka { get; set; }
        [BindProperty]
        public Apoteka Apoteka2 { get; set; }
        public IMongoCollection<Lek> collectionL { get; set; }
        public IMongoCollection<Apoteka> collectionA { get; set; }
        [BindProperty]
        public List<MongoDBRef> lekovii { get; set; }
        [BindProperty]
        public bool ok { get; set; }
        public IActionResult OnGet([FromRoute] String id)
        {
            return Page();
        }
        public IActionResult OnPost([FromRoute] String id)
        {
            return Page();
        }

        public IActionResult OnPostDodaj([FromRoute] String pom)
        {
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            var database = client.GetDatabase("Apoteka3");

            collectionL = database.GetCollection<Lek>("lekovi");
            collectionA = database.GetCollection<Apoteka>("apoteke");
            Apoteka2 = collectionA.Find(x => x.RegistarskiBroj == Apoteka.RegistarskiBroj).FirstOrDefault();
            Lek.MojaApoteka = Apoteka2;
            collectionL.InsertOne(Lek);

            Lek2 = collectionL.Find(x => x.KomercijaniNaziv == Lek.KomercijaniNaziv).FirstOrDefault();

            lekovii = new List<MongoDBRef>();
            lekovii = Apoteka2.Lekovi;
            lekovii.Add(new MongoDBRef("lekovi",Lek.Id));
            var res = Builders<Apoteka>.Filter.Eq(pd => pd.Id, Apoteka2.Id);
            var operation = Builders<Apoteka>.Update.Set(u => u.Lekovi, lekovii);
            database.GetCollection<Apoteka>("apoteke").UpdateOne(res, operation);
            ok = true;
            return Page(); 
        }
    }
}
