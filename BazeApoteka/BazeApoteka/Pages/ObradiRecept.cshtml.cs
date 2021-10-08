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
    public class ObradiReceptModel : PageModel
    {
        [BindProperty]
        public String Prosledjeno { get; set; }
        public bool ok { get; set; }
        public IMongoCollection<Farmaceut> collection { get; set; }
        public IMongoCollection<Korisnik> collectionK { get; set; }
        [BindProperty]
         public Korisnik Pacijent { get; set; }
        [BindProperty]
        public Recept ReceptZaObradu { get; set; }
        [BindProperty]
        public List<Recept> recepti { get; set; }
        public List<Lek> LekoviApoteke { get; set; }
        public IActionResult OnPost([FromRoute] String id)
        {
            Prosledjeno = id; //idfarmaceuta
            ObjectId iid = ObjectId.Parse(Prosledjeno);

            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");


            collection = database.GetCollection<Farmaceut>("farmaceuti");
            Farmaceut f = collection.Find(x => x.Id == iid).FirstOrDefault(); 
            return Page();
        }
        public IActionResult OnPostPretrazi(String id)
        {
            LekoviApoteke = new List<Lek>();
            Prosledjeno = id;
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");

            
            collectionK = database.GetCollection<Korisnik>("korisnici");
          //  if(Pacijent.BrojZdravstveneKnjizice.Equals(null))
            //{
                Pacijent =collectionK.Find(x => x.BrojZdravstveneKnjizice== Pacijent.BrojZdravstveneKnjizice).FirstOrDefault();
           // }
            recepti = database.GetCollection<Recept>("recepti").Find(x => x.Pacijent.Id == Pacijent.Id && x.Obradjeno == false).ToList();
            ok = true;
            return Page();
        }
        public IActionResult OnPostLek(String id, String idF)
        {
            LekoviApoteke = new List<Lek>();
            Prosledjeno = idF; //id farmaceuta
            ObjectId iid = ObjectId.Parse(id);//id recepta
            ObjectId idFarmaceuta = ObjectId.Parse(idF);
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");

            collection = database.GetCollection<Farmaceut>("farmaceuti");
            Farmaceut f = collection.Find(x => x.Id == idFarmaceuta).FirstOrDefault();

            ReceptZaObradu = database.GetCollection<Recept>("recepti").Find(x => x.Id == iid && x.Obradjeno== false).FirstOrDefault();
            LekoviApoteke = database.GetCollection<Lek>("lekovi").Find(x => x.MojaApoteka.Id == f.MojaApoteka.Id).ToList(); //treba i ovo dole da se stavi u uslov samo sam sklonila da ne menjam bazu nego ovako prikazuje sve iz jedne apoteke 
            //&& x.GenerickiNaziv==ReceptZaObradu.Ordinatio

            var res = Builders<Recept>.Filter.Eq(pd => pd.Id, ReceptZaObradu.Id);//nadjem recept
            var operation = Builders<Recept>.Update.Set(u => u.Faramceut, new MongoDBRef("recepti", f.Id));//dodelim id farmaceuta  i update da li je dobro??????????/
            database.GetCollection<Recept>("recepti").UpdateOne(res, operation);
            ok = true;
            return Page();
        }
        public IActionResult OnPostIzdaj(String id, String idR)
        {
            Prosledjeno = idR; //id recepta
            ObjectId iid = ObjectId.Parse(id);//id leka
            ObjectId idRecepta = ObjectId.Parse(idR);
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");

            IMongoCollection <Recept> rec = database.GetCollection<Recept>("recepti");
            Recept r = rec.Find(x => x.Id == idRecepta).FirstOrDefault();
            ReceptZaObradu = database.GetCollection<Recept>("recepti").Find(x => x.Id == iid).FirstOrDefault();

            Lek lek = database.GetCollection<Lek>("lekovi").Find(x => x.Id == iid).FirstOrDefault();

            var res = Builders<Recept>.Filter.Eq(pd => pd.Id, r.Id);//nadjem recept
            var operation = Builders<Recept>.Update.Set(u => u.Lek, new MongoDBRef("recepti", lek.Id));
            database.GetCollection<Recept>("recepti").UpdateOne(res, operation);

            var res1 = Builders<Recept>.Filter.Eq(pd => pd.Id, r.Id); //pamtim da je obradjen recept
            var operation1 = Builders<Recept>.Update.Set(u => u.Obradjeno, true);
            database.GetCollection<Recept>("recepti").UpdateOne(res1, operation1);
            LekoviApoteke = new List<Lek>();
            ok = true;
            return Page();
        }
    }
}