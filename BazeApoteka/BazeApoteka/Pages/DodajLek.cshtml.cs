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
    public class DodajLekModel : PageModel
    {
        [BindProperty]
        public Lek Lek { get; set; }
        [BindProperty]
        public String Prosledjeno { get; set; }
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
            ok = false;
            Prosledjeno = id;
            return Page();
        }
        public IActionResult OnPost([FromRoute] String id)
        {
            ok = false;
            Prosledjeno = id;
            return Page();
        }

        public IActionResult OnPostDodaj([FromRoute] String pom)
        {
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");

            collectionL = database.GetCollection<Lek>("lekovi");
            collectionA = database.GetCollection<Apoteka>("apoteke");
            Apoteka2 = collectionA.Find(x => x.Id == ObjectId.Parse(Prosledjeno)).FirstOrDefault();
           
            Lek.MojaApoteka = Apoteka2;
            collectionL.InsertOne(Lek);

            lekovii = new List<MongoDBRef>();
            lekovii = Apoteka2.Lekovi;
            lekovii.Add(new MongoDBRef("lekovi", Lek.Id));
            var res = Builders<Apoteka>.Filter.Eq(pd => pd.Id, Apoteka2.Id);
            var operation = Builders<Apoteka>.Update.Set(u => u.Lekovi, lekovii);
            database.GetCollection<Apoteka>("apoteke").UpdateOne(res, operation);
            ok = true;
            return Page();
        }
    }
}
