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
    public class FarmaceutModel : PageModel
    {
        public IMongoCollection<Farmaceut> collection { get; set; }
        [BindProperty]
        public List<Farmaceut> farmaceuti { get; set; }
        public void OnGet()
        {
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");


            collection = database.GetCollection<Farmaceut>("farmaceuti");

            farmaceuti = collection.Find(FilterDefinition<Farmaceut>.Empty).ToList();
        }
        public IActionResult OnPost()
        {
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");


            collection = database.GetCollection<Farmaceut>("farmaceuti");
            farmaceuti = collection.Find(FilterDefinition<Farmaceut>.Empty).ToList();
            return Page();
        }

        public IActionResult OnPostUlogujSe(String tt)
        {
            return RedirectToPage("./PrikazFarmaceuta", new { id = tt });
        }
    }
}