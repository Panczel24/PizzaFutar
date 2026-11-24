using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaFutar
{
    internal class Rendeles
    {
        private int id;
        private Pizza pizza;
        private int darab;
        private Cim cim;
        private TimeSpan szallitas;

        public Rendeles(int id, Pizza pizza, int darab, Cim cim, TimeSpan szallitas)
        {
            this.id = id;
            this.pizza = pizza;
            this.darab = darab;
            this.cim = cim;
            this.szallitas = szallitas;
        }

        public int Id { get => id; set => id = value; }
        public int Darab { get => darab; set => darab = value; }
        public TimeSpan Szallitas { get => szallitas; set => szallitas = value; }
        internal Pizza Pizza { get => pizza; set => pizza = value; }
        internal Cim Cim { get => cim; set => cim = value; }

        public override string ToString()
        {
            return $"{this.pizza},{this.darab}, {this.cim}, {this.szallitas}";
        }
    }
}
