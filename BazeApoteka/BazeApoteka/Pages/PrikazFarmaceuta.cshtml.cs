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
    public class PrikazFarmaceutaModel : PageModel
    {
        [BindProperty]
        public String Prosledjeno { get; set; }
        public IMongoCollection<Farmaceut> collection { get; set; }
        [BindProperty]
        public List<Farmaceut> l { get; set; }
        public void OnGet([FromRoute] String id)
        {
            Prosledjeno = id;
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");


            collection = database.GetCollection<Farmaceut>("farmaceuti");

            //farmaceuti = collection.Find(FilterDefinition<Farmaceut>.Empty).ToList();

            ObjectId iid = ObjectId.Parse(Prosledjeno);
            l = collection.Find(x => x.Id == iid).ToList();
        }
        public IActionResult OnPost([FromRoute] String id)
        {
            Prosledjeno = id;
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");


            collection = database.GetCollection<Farmaceut>("farmaceuti");

            //farmaceuti = collection.Find(FilterDefinition<Farmaceut>.Empty).ToList();

            ObjectId iid = ObjectId.Parse(Prosledjeno);
            l = collection.Find(x => x.Id == iid).ToList();

            return Page();
        }
    }
}
