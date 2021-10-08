using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BazeApoteka.Entiteti
{
    public class Apoteka
    {
        public ObjectId Id { get; set; }
        public string Naziv { get; set; }
        public string Adresa { get; set; }
        public string RegistarskiBroj { get; set; }
        public List<MongoDBRef> Lekovi { get; set; }
        public List<MongoDBRef> Farmaceuti { get; set; }

        public Apoteka()
        {
            Lekovi = new List<MongoDBRef>();
            Farmaceuti = new List<MongoDBRef>();
        }
    }
}
