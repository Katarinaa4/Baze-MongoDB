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
using MongoDB.Driver.Builders;

namespace BazeApoteka.Pages
{
   
    public class ObradjeniReceptiModel : PageModel
    {
        [BindProperty]
        public String Prosledjeno { get; set; }
        [BindProperty]
        public List<Recept> recepti { get; set; }
        public IMongoCollection<Recept> collectionR { get; set; }
        public IMongoCollection<Farmaceut> collectionF { get; set; }
        public IActionResult OnPost([FromRoute] String id)
        {
            Prosledjeno = id; //idfarmaceuta
            ObjectId iid = ObjectId.Parse(Prosledjeno);

            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");


            collectionF = database.GetCollection<Farmaceut>("farmaceuti");
            collectionR = database.GetCollection<Recept>("recepti");
            Farmaceut f = collectionF.Find(x => x.Id == iid).FirstOrDefault();

            recepti = collectionR.Find(x => x.Obradjeno == true && x.Faramceut.Id == f.Id).ToList();
            return Page();
        }

    }
}