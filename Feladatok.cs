using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaFutar
{
    internal class Feladatok
    {
        List<Rendeles> lista;
        public Feladatok()
        {
            Adatbazis a = new Adatbazis();
            lista = a.Feltolt();
            Kiir();
            

        }
        private void Kiir()
        {
            foreach(var item in lista)
            {
                Console.WriteLine(item);
            }

            foreach (var item in lista)
            {
                Console.WriteLine($"{item.Pizza.Nev} - {item.Cim.Nev} - {item.Darab} ");
            }
        }
    }
}
