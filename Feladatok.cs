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
            harmadik();
            negyedik();
            otodik();
            hatodik();
            hetedik();
            nyolcadik();


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

        private void harmadik()
        {

            var pizzak = lista
                .Where(r => r.Pizza.Ar < 1500)
                .Select(r => r.Pizza)
                .Distinct()
                .OrderByDescending(p => p.Meret)
                .ThenBy(p => p.Nev);

            foreach (var pizza in pizzak)
            {
                Console.WriteLine(
                    $"{pizza.Nev}, {pizza.Meret} cm, {pizza.Ar} Ft");
            }
        }

        private void negyedik()
        {

            var pizzak = lista
                .GroupBy(r => r.Pizza.Id)
                .Select(csoport => new
                {
                    Pizza = csoport.First().Pizza,
                    Darab = csoport.Sum(r => r.Darab)
                })
                .OrderByDescending(p => p.Darab);

            int legtobb = pizzak.First().Darab;

            foreach (var pizza in pizzak)
            {
                if (pizza.Darab == legtobb)
                {
                    Console.WriteLine(
                        $"{pizza.Pizza.Nev}, {pizza.Pizza.Meret} cm, " +
                        $"{pizza.Darab} db");
                }
            }
        }

        private void otodik()
        {

            var rendelesek = lista
                .Where(r =>
                    r.Szallitas >= new TimeSpan(18, 0, 0) &&
                    r.Szallitas <= new TimeSpan(19, 0, 0))
                .GroupBy(r => new
                {
                    r.Pizza.Nev,
                    r.Pizza.Meret
                })
                .Select(csoport => new
                {
                    csoport.Key.Nev,
                    csoport.Key.Meret,
                    Darab = csoport.Sum(r => r.Darab)
                })
                .OrderBy(r => r.Nev)
                .ThenBy(r => r.Meret);

            foreach (var rendeles in rendelesek)
            {
                Console.WriteLine(
                    $"{rendeles.Nev}, {rendeles.Meret} cm, " +
                    $"{rendeles.Darab} db");
            }
        }

   
        private void hatodik()
        {

            var pizzak = lista
                .GroupBy(r => r.Pizza.Nev)
                .Where(csoport =>
                    csoport.Select(r => r.Pizza.Meret)
                    .Distinct()
                    .Count() == 3)
                .Select(csoport => csoport.Key);

            foreach (var pizza in pizzak)
            {
                Console.WriteLine(pizza);
            }
        }

      
        private void hetedik()
        {

            var rendelesek = lista
                .GroupBy(r => new
                {
                    r.Cim.Id,
                    r.Szallitas
                })
                .Where(csoport =>
                    csoport.Sum(r => r.Darab) > 3)
                .Select(csoport => new
                {
                    Szallitas = csoport.Key.Szallitas,
                    Cim = csoport.First().Cim
                })
                .OrderBy(r => r.Szallitas);

            foreach (var rendeles in rendelesek)
            {
                Console.WriteLine(
                    $"{rendeles.Szallitas}, {rendeles.Cim.Nev}, " +
                    $"{rendeles.Cim.Utca}, {rendeles.Cim.Hazszam}");
            }
        }

      
        private void nyolcadik()
        {

            var rendelesek = lista
                .GroupBy(r => new
                {
                    r.Cim.Id,
                    r.Szallitas
                })
                .Select(csoport => new
                {
                    Szallitas = csoport.Key.Szallitas,
                    Cim = csoport.First().Cim,
                    Osszeg = csoport.Sum(r =>
                        r.Pizza.Ar * r.Darab)
                })
                .OrderBy(r => r.Szallitas);

            foreach (var rendeles in rendelesek)
            {
                Console.WriteLine(
                    $"{rendeles.Szallitas}\t" +
                    $"{rendeles.Cim.Nev}\t" +
                    $"{rendeles.Cim.Utca}\t" +
                    $"{rendeles.Cim.Hazszam}\t" +
                    $"{rendeles.Osszeg}");
            }
        }

    }
}
