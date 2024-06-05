using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SZGYA_WPF_Tavolugras
{
    internal class Versenyzo
    {
        public string Nev { get; set; }
        public int Kor { get; set;}
        public string Telepules { get; set; }
        public List<int> Ugrasok {  get; set; } // -1 -> belépett

        public Versenyzo(string sor) 
        {
            string[] adatok = sor.Split(';');
            this.Nev = adatok[0];
            this.Kor = int.Parse(adatok[1]);
            this.Telepules = adatok[2];
            this.Ugrasok = new List<int>();
            for (int i = 3; i < adatok.Length; i++)
                this.Ugrasok.Add(adatok[i] == "belépett" ? -1 : int.Parse(adatok[i]));
        }

        public override string ToString()
        {
            return $"{this.Nev} ({this.Kor}): {(string.Join(" - ", Ugrasok).Replace("-1", "belépett"))}";
        }
    }
}
