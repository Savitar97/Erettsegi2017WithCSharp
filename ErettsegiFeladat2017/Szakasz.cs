using System;
using System.Collections.Generic;
using System.Text;

namespace ErettsegiFeladat2017
{
    public class Szakasz
    {
        public string Kiindulopont { get; set; }
        public string Vegpont { get; set; }
        public float Hossz { get; set; }
        public int Emelkedes { get; set; }
        public int Lejtes { get; set; }
        public char SzakaszVege { get; set; }

        public Szakasz()
        {
        }

        public Szakasz(string kiindulopont, string vegpont, float hossz, int emelkedes, int lejtes, char szakaszVege)
        {
            Kiindulopont = kiindulopont;
            Vegpont = vegpont;
            Hossz = hossz;
            Emelkedes = emelkedes;
            Lejtes = lejtes;
            SzakaszVege = szakaszVege;
        }

    }
}
