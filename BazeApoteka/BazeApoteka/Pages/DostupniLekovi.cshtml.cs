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
    public class DostupniLekoviModel : PageModel
    {
        [BindProperty]
        public String Prosledjeno { get; set; }
        public IMongoCollection<Farmaceut> collection { get; set; }
        public IMongoCollection<Lek> collectionL { get; set; }
        [BindProperty]
       // public List<Lek> SviLekovi { get; set; }
        public List<Lek> LekoviApoteke { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost([FromRoute] String id)
        {
            Prosledjeno = id; //idfarmaceuta
            ObjectId iid = ObjectId.Parse(Prosledjeno);

            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");


            collection = database.GetCollection<Farmaceut>("farmaceuti");
            Farmaceut f = collection.Find(x => x.Id == iid).FirstOrDefault(); //nadjemo farmaceuta pa sve lekove koji imaju isti id apoteke kao farmaceut

            collectionL = database.GetCollection<Lek>("lekovi");
            LekoviApoteke = collectionL.Find(x => x.MojaApoteka.Id == f.MojaApoteka.Id).ToList();
            // lekari = collection.Find(FilterDefinition<Lekar>.Empty).ToList();
            return Page();
        }
        public IActionResult OnPostObrisi([FromRoute] String id)
        {
            
            ObjectId iid = ObjectId.Parse(id);

            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");

            collectionL = database.GetCollection<Lek>("lekovi");
            collectionL.DeleteOne(x => x.Id == iid);
            return RedirectToPage();
        }
     
    }
}
