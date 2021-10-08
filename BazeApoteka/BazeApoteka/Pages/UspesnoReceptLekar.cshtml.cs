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
    public class UspesnoReceptLekarModel : PageModel
    {
        public IMongoCollection<Recept> collection { get; set; }
        [BindProperty]
        public Recept recept { get; set; }
       
        public string Prosledjeno { get; set; }
        public IActionResult OnGet([FromRoute] String id)
        {
            Prosledjeno = id; //id recepta
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");

            //ucitamo recepte i nadjemo prosledjen 
            collection = database.GetCollection<Recept>("recepti");
            ObjectId iid = ObjectId.Parse(Prosledjeno);
            recept = collection.Find(x => x.Id == iid).FirstOrDefault();

            return Page();
        }
        public IActionResult OnPostJosJedan([FromRoute] String id)
        {
            Prosledjeno = id; //id recepta
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");

            //ucitamo recepte i nadjemo prosledjen 
            collection = database.GetCollection<Recept>("recepti");
            ObjectId iid = ObjectId.Parse(Prosledjeno);
            recept = collection.Find(x => x.Id == iid).FirstOrDefault();


            Recept novi = new Recept();
            novi.Lekar = recept.Lekar;
            novi.Pacijent = recept.Pacijent;
            collection.InsertOne(novi);


            return RedirectToPage("./PrepisiRecept", new { id= novi.Id});
        }
    }
}