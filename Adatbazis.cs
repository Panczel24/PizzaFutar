using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaFutar

{

    internal class Adatbazis
    {

        private readonly string connectionString;

        public Adatbazis()
        {
            this.connectionString = "server=localhost;database=pizzafutár;user=root;password='';";
        }

        public List<Rendeles> Feltolt()

        {

            List<Rendeles> lista = new List<Rendeles>();
            Dictionary<int, Pizza> pizzak = new Dictionary<int, Pizza>();
            Dictionary<int, Cim> cimek = new Dictionary<int, Cim>();

            try
            {

                using (MySqlConnection conn = new MySqlConnection(this.connectionString))
                {
                    conn.Open();
                    Console.WriteLine("Sikeres kapcsolódás az adatbázishoz!");
                    string query = "SELECT * FROM pizza";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {

                            while (reader.Read())
                            {

                                int id = reader.GetInt32("id");
                                Pizza pizza = new Pizza(
                                    id,
                                    reader.GetString("nev"),
                                    reader.GetInt32("meret"),
                                    reader.GetInt32("ar")
                                    );

                                pizzak[id] = pizza;
                            }
                        }
                    }



                    query = "SELECT * FROM cim";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {

                            while (reader.Read())
                            {

                                int id = reader.GetInt32("id");
                                Cim cim = new Cim(
                                    id,
                                    reader.GetString("nev"),
                                    reader.GetString("utca"),
                                    reader.GetString("hsz")
                                    );

                                cimek[id] = cim;
                            }
                        }
                    }





                    query = "SELECT * FROM rendeles";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {

                            while (reader.Read())
                            {

                                int id = reader.GetInt32("id");
                                Pizza p = pizzak[reader.GetInt32("pizzaid")];
                                Cim c = cimek[reader.GetInt32("cimid")];
                                int db = reader.GetInt32("darab");
                                TimeSpan t = reader.GetTimeSpan("szallitas");
                                lista.Add(new Rendeles(id, p, db, c, t));
                                   
                            }
                        }
                    }




                }

            }

            catch (MySqlException ex)
            {
                Console.WriteLine("hiba az afatbaziskapcsolattal");
                Environment.Exit(0);
            }
            catch (Exception e)
            {
                Console.WriteLine("Általános hiba:" +e);
                Environment.Exit(0);

            }

            Console.WriteLine(pizzak[1]);
            Console.WriteLine(cimek[1]);
            return lista;


        }

    }

}