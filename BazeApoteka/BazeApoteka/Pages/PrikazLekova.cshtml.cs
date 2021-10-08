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
    public class PrikazLekovaModel : PageModel
    {
        [BindProperty]
        public String Korisnik { get; set; }
        [BindProperty]
        public String Prosledjeno { get; set; }
        [BindProperty]
        public List<Lek> lekovi { get; set; }
        
        public IMongoCollection<Lek> collection { get; set; } 

        public IActionResult OnGet([FromRoute] String id)
        {
            
            Prosledjeno = id;
            ObjectId idd = ObjectId.Parse(id);
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");


            collection = database.GetCollection<Lek>("lekovi");
            lekovi = collection.Find(x =>x.MojaApoteka.Id == idd).ToList();
            

            return Page();

        }

        public IActionResult OnPost([FromRoute] String id)
        {
            Prosledjeno = id;
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");


            collection = database.GetCollection<Lek>("lekovi");
            lekovi = collection.Find(FilterDefinition<Lek>.Empty).ToList();
            
            return Page();
        }

        public IActionResult OnPostKupiLek(String idL)
        {
            String iid = idL; //id leka
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");


            //treba da mu prosledim i id korisnika i id leka 
            return RedirectToPage("./KupiLek2", new { id=iid});
        }
    }
}
