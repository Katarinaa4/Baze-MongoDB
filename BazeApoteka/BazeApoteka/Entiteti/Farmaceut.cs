using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BazeApoteka.Entiteti
{
    public class Farmaceut
    {
        public ObjectId Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public float Plata { get; set; }
        public MongoDBRef MojaApoteka { get; set; }
    }
}
