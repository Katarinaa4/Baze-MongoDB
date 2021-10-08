using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BazeApoteka.Entiteti
{
    public class Lekar
    {
        public ObjectId Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string UstanovaGdeRadi { get; set; }
        public List<MongoDBRef> Pacijenti { get; set; }

        public Lekar()
        {
            Pacijenti = new List<MongoDBRef>();
        }
    }
}
