using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BazeApoteka.Entiteti
{
    public class Korisnik
    {
        public ObjectId Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Adresa { get; set; }
        public double BrojZdravstveneKnjizice { get; set; }
        public MongoDBRef Doktor { get; set; }
        public List<Recept> Recepti { get; set; }

        public Korisnik()
        {
            Recepti = new List<Recept>();
        }
    }
}
