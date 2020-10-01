using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace ErettsegiFeladat2017
{

    class Program
    {
        public static float TuraTeljesHossza(List<Szakasz> szakaszok)
        {
            return szakaszok.Sum(element=>element.Hossz);
        }

        public static Szakasz Legrovidebb(List<Szakasz> szakaszok)
        {
            return szakaszok.Find(element => element.Hossz == szakaszok.Min(element => element.Hossz));
        }

        public static bool HianyosNev(Szakasz sz)
        {
            bool ret = false;
            if (sz.SzakaszVege.Equals('i'))
            {
                if (!sz.Vegpont.Contains("pecsetelohely"))
                {
                    ret = true;
                }
            }
            return ret;
        }

        public static string KiirFormat(Szakasz sz)
        {
            string temp = "";
            if (HianyosNev(sz))
            {
                temp = sz.Vegpont + " pecsetelohely";
            }
            else
            {
                temp = sz.Vegpont;
            }
            string visszaad = String.Format($"{sz.Kiindulopont};{temp};{sz.Hossz};{sz.Emelkedes};{sz.Lejtes};{sz.SzakaszVege}");
            return visszaad;
        }

        public static List<String> LegmagasabbVegpontesMagassaga(List<Szakasz> szakaszok,float kezdomagassag)
        {
            List<string> eredmeny = new List<string>();
            Szakasz sz = szakaszok.Find(e => (e.Emelkedes - e.Lejtes) == szakaszok.Max(element => element.Emelkedes - element.Lejtes));
            eredmeny.Add(sz.Vegpont);
            float maxmagassag = 0;
            foreach(var element in szakaszok)
            {
                maxmagassag += (element.Emelkedes - element.Lejtes);
                if (element == sz)
                {
                    break;
                }
            }
            eredmeny.Add((maxmagassag+kezdomagassag).ToString());
            return eredmeny;
        }

        public static List<String> HianyosNevek(List<Szakasz> szakaszok)
        {
            List<String> pecsetelohely = new List<String>(); ;
            foreach(var element in szakaszok)
            {
                if (HianyosNev(element))
                {
                    pecsetelohely.Add(element.Vegpont);
                }
            }
            return pecsetelohely;
        }


        static void Main(string[] args)
        {
            List<Szakasz> adatok = new List<Szakasz>();
            float tengerszint;
            try
            {
                var kezdomagassag = Convert.ToInt32(File.ReadLines("kektura.csv").First());
                tengerszint = kezdomagassag;
                Console.WriteLine(kezdomagassag);
                var beolvasott = File.ReadAllLines("kektura.csv");
                foreach (var element in beolvasott.Skip(1))
                {
                    var sor = element.Split(";");
     
                    adatok.Add(new Szakasz(sor[0], sor[1], float.Parse(sor[2]), Int32.Parse(sor[3]), Int32.Parse(sor[4]), Char.Parse(sor[5])));

                }
            }
            catch(Exception)
            {
                throw new FileNotFoundException();
            }
            
            Console.WriteLine("3.feladat: Szakaszok száma:{0}",adatok.Count);
            Console.WriteLine("4.feladat: A túra teljes hossza:{0:0.000} km",TuraTeljesHossza(adatok));
            Console.WriteLine("5.feladat: A legrövidebb szakasz adatai:");
            Console.WriteLine("Kezdete:{0}",Legrovidebb(adatok).Kiindulopont);
            Console.WriteLine("Vége:{0}", Legrovidebb(adatok).Vegpont);
            Console.WriteLine("Távolság:{0}", Legrovidebb(adatok).Hossz);
            Console.WriteLine("7.feladat: Hiányos állománynevek:");
            if (HianyosNevek(adatok).Count == 0)
            {
                Console.WriteLine("Nincs hiányos állománynév!");
            }
            else
            {
                foreach(var element in HianyosNevek(adatok))
                {
                    Console.WriteLine("{0}",element);
                }
            }
            Console.WriteLine("8.feladat: A túra legmagasabban fekvő végpontja:");
            Console.WriteLine("A végpont neve:{0}",LegmagasabbVegpontesMagassaga(adatok,tengerszint)[0]);
            Console.WriteLine("A végpont tengerszintfeletti magassága:{0} m", LegmagasabbVegpontesMagassaga(adatok,tengerszint)[1]);
            List<String> adatotkiir = new List<string>();
            adatotkiir.Add(tengerszint.ToString());
            foreach(var element in adatok)
            {
                adatotkiir.Add(KiirFormat(element));
            }
            File.WriteAllLines("kektura2.csv",adatotkiir);
        }
    }
}
