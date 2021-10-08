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

//Trenutno se za broj farmaceuta i broj lekova prikazuje 0, dok ne uspemo da 
//dodamo kod apoteke reference

namespace BazeApoteka.Pages
{
    public class UcitajSveApotekeModel : PageModel
    {
        public IMongoCollection<Apoteka> collection { get; set; }
        [BindProperty]
        public List<Apoteka> apoteke { get; set; }
        public void OnGet()
        {
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");


            collection = database.GetCollection<Apoteka>("apoteke");

            apoteke = collection.Find(FilterDefinition<Apoteka>.Empty).ToList();
        }
        public IActionResult OnPost()
        {
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");


            collection = database.GetCollection<Apoteka>("apoteke");
            apoteke = collection.Find(FilterDefinition<Apoteka>.Empty).ToList();
            return Page();
        }
    }
}
