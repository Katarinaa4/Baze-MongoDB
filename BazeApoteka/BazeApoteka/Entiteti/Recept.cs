using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BazeApoteka.Entiteti
{
    public class Recept
    {
        public ObjectId Id { get; set; }
        public MongoDBRef Pacijent { get; set; }
        public MongoDBRef Lekar { get; set; }
        public MongoDBRef Faramceut { get; set; }
        public MongoDBRef Lek { get; set; }
        public bool Obradjeno { get; set; }
        ////delovi recepta 
        //public String Inscriptio { get; set; } //naziv i adresa ustanove u kojoj je lek propisan
        //public Korisnik NomenAegroti { get; set; } //podaci o pacijentu 
        public String Ordinatio { get; set; } //koji lek se propisuje
        public String Subscriptio { get; set; } //uputstvo farmaceutu, oblik, kolicina 
        public String Signatura { get; set; } //uputstvo za pacijenta
        //public String NomenMedici { get; set; } //ime lekara
    }
}
