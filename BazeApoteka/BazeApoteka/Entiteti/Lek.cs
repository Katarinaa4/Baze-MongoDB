using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BazeApoteka.Entiteti
{
    public class Lek
    {
        public ObjectId Id { get; set; }
        public string GenerickiNaziv { get; set; }
        public string KomercijaniNaziv { get; set; }
        public string Dejstvo { get; set; }
        public string Cena { get; set; }
        public string Indikacije { get; set; }
        public string Kontraindikacije { get; set; }
        public string DaLiJeNaRecept { get; set; }
        public string Kolicina { get; set; }
        public string Doza { get; set; }
        public Apoteka MojaApoteka { get; set; }
    }
}
