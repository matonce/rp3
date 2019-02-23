using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projekt_maja_proba // možemo postaviti da su prve vježbe u svim levelima uvijek otključane 
                             // jer se njima ionako može pristupiti tek kad otključamo level u kojem se nalaze
{
    public class RadSBazom
    {
        OleDbConnection connection = null;
        OleDbDataReader reader = null;
        int procitano;

        string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" +
            // @"Data source= C:\Users\Ana\Desktop\Faks\rp3\projekt_maja_proba\baza.mdb";
        @"Data source= C:\Users\Maja Tonček\source\repos\rp3\projekt_maja_proba\baza.mdb";

        public RadSBazom()
        {
            connection = new OleDbConnection(connectionString);
        }

        public List<Tuple<string, int>> popisLevelaIzBaze()
        {
            List<Tuple<string, int>> naziviLevela = new List<Tuple<string, int>>();

            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand("select * from Leveli", connection);

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    naziviLevela.Add(new Tuple<string, int>(reader["Naziv"].ToString(), (int)reader["Otključan"]));
                    //Console.WriteLine("iz baze se cita id " + indeksiVjezbi.Last());
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                if (reader != null)
                    reader.Close();

                if (connection != null)
                    connection.Close();
            }

            return naziviLevela;
        }

        public List<Tuple<int, string, string, int, string>> popisVjezbiSLevelaIzBaze(int indeksLevela)
        {
            List<Tuple<int, string, string, int, string>> indeksiVjezbi = new List<Tuple<int, string, string, int, string>>();

            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand("select * from VjezbeSLevela where ID_levela = @indeksLevela order by ID", connection);
                command.Parameters.AddWithValue("@indeksLevela", indeksLevela);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    indeksiVjezbi.Add(new Tuple<int, string, string, int, string>((int)reader["ID"], reader["Naziv"].ToString(), reader["Stringovi"].ToString(), (int)reader["Otključana"], "(" + reader["Naj_brzina"].ToString() + " wpm, " + reader["Naj_preciznost"].ToString() + "%)"));
                    //Console.WriteLine("iz baze se cita id " + indeksiVjezbi.Last());
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                if (reader != null)
                    reader.Close();

                if (connection != null)
                    connection.Close();
            }

            return indeksiVjezbi;
        }

        internal Tuple<String, String> otkljucajNovuVjezbu(int indeksVjezbe, int indeksLevela)
        {
            Tuple<String, String> rezultat = null;
            try
            {
                connection.Open();

                // ucini upravo prijedenu vjezbu dostupnom
                /*OleDbCommand command = new OleDbCommand("update VjezbeSLevela set Otključana = 1 where ID = @indeksVjezbe", connection);
                command.Parameters.AddWithValue("@indeksVjezbe", indeksVjezbe);
                command.ExecuteNonQuery();*/

                // ucini sljedecu vjezbu dostupnom, ak je ima
                OleDbCommand command = new OleDbCommand("update VjezbeSLevela set Otključana = 1 where ID = @indeksVjezbe and ID_levela=@indeksLevela", connection);
                command.Parameters.AddWithValue("@indeksVjezbe", indeksVjezbe + 1);
                command.Parameters.AddWithValue("@indeksLevela", indeksLevela);

                if (command.ExecuteNonQuery() != 0)
                {
                    command = new OleDbCommand("select Naziv, Stringovi from VjezbeSLevela where ID = @indeksVjezbe", connection);
                    command.Parameters.AddWithValue("@indeksVjezbe", indeksVjezbe + 1);
                    reader = command.ExecuteReader();

                    reader.Read();
                    rezultat = new Tuple<String, String>(reader["Naziv"].ToString(), reader["Stringovi"].ToString());
                }
                // ako nema sljedece vjezbe, zavrsen je ovaj level i otkljucava se drugi ako ga ima
                else
                {
                    command = new OleDbCommand("update Leveli set Otključan = 1 where ID = @indeksLevela", connection);
                    command.Parameters.AddWithValue("@indeksLevela", indeksLevela + 1);

                    if (command.ExecuteNonQuery() == 1)
                        rezultat = new Tuple<String, String>("", " "); // samo želim razlikovati ovaj slučaj od slučaja kada nemamo više levela
                    else
                        rezultat = new Tuple<String, String>("", "");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }

            return rezultat;
        }

        internal bool imaLiUvjeta(int indeksVjezbe)
        {
            bool rezultat = false;

            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand("select Ima_uvjete from VjezbeSLevela where ID_levela = @indeksVjezbe order by ID", connection);
                command.Parameters.AddWithValue("@indeksVjezbe", indeksVjezbe);
                reader = command.ExecuteReader();

                reader.Read();
                rezultat = (bool)reader["Ima_uvjete"];
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                if (reader != null)
                    reader.Close();

                if (connection != null)
                    connection.Close();
            }

            return rezultat;
        }

        internal int obnoviPodatke(int indeksVjezbe, double brzina, double preciznost)
        {
            int rezultat = 0;

            try
            {
                connection.Open();

                Console.WriteLine("indeks vjezbe, brzina, preciznost: " + indeksVjezbe + ", " + brzina + ", " + preciznost);

                OleDbCommand command = new OleDbCommand("update VjezbeSLevela set Naj_brzina = @brzina, Naj_preciznost = @preciznost "
                    + "where ID = @indeksVjezbe and Naj_brzina < @brzina and Naj_preciznost < @preciznost", connection);
                command.Parameters.AddWithValue("@brzina", brzina);
                command.Parameters.AddWithValue("@preciznost", preciznost);
                command.Parameters.AddWithValue("@indeksVjezbe", indeksVjezbe);

                rezultat = command.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }

            return rezultat;
        }

        public List<String> odaberiRijeci(int broj) // slucajnim odabirom izabere razlicite rijeci iz baze
        {
            List<String> nizStringova = new List<String>();
            HashSet<int> skup = new HashSet<int>();

            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand("select count(*) from Rijeci", connection);
                int i = (int)command.ExecuteScalar();

                while (skup.Count() < broj)
                {
                    Random r = new Random();
                    int rand_id = r.Next(1, i);

                    if (!skup.Contains(rand_id))
                    {
                        skup.Add(rand_id);
                        OleDbCommand command1 = new OleDbCommand("select * from Rijeci where ID = @id", connection);
                        OleDbParameter parameter = new OleDbParameter();
                        parameter.ParameterName = "@id";
                        parameter.Value = rand_id;

                        command1.Parameters.Add(parameter);
                        reader = command1.ExecuteReader();
                        while (reader.Read()) nizStringova.Add(reader["rijec"].ToString());
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                if (reader != null)
                    reader.Close();

                if (connection != null)
                    connection.Close();
            }
            return nizStringova;
        }

        internal void povecaj(string slovo, string stupac)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand("select * from Slova where Slovo = @slovo", connection);
                OleDbParameter parameter = new OleDbParameter();
                parameter.ParameterName = "@slovo";
                if (slovo == " ") parameter.Value = "space";
                else parameter.Value = slovo;

                command.Parameters.Add(parameter);
                reader = command.ExecuteReader();
                while (reader.Read()) procitano = (int)reader[stupac];

                procitano++;

                OleDbCommand command1 = new OleDbCommand("update Slova set " + stupac + " = @broj where Slovo = @slovo", connection);

                OleDbParameter parameter2 = new OleDbParameter();
                parameter2.ParameterName = "@broj";
                parameter2.Value = procitano;
                command1.Parameters.Add(parameter2);

                OleDbParameter parameter1 = new OleDbParameter();
                parameter1.ParameterName = "@slovo";
                if (slovo == " ") parameter1.Value = "space";
                else parameter1.Value = slovo;
                command1.Parameters.Add(parameter1);

                command1.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                if (reader != null)
                    reader.Close();

                if (connection != null)
                    connection.Close();
            }

            //trebam srediti i omjer sada
            try
            {
                int tocno = 0, netocno = 0;
                double omjer;
                connection.Open();
                OleDbCommand command = new OleDbCommand("select * from Slova where Slovo = @slovo", connection);
                OleDbParameter parameter = new OleDbParameter();
                parameter.ParameterName = "@slovo";
                if (slovo == " ") parameter.Value = "space";
                else parameter.Value = slovo;

                command.Parameters.Add(parameter);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    tocno = (int)reader["Tocno"];
                    netocno = (int)reader["Netocno"];
                }

                if (tocno + netocno != 0) omjer = Math.Round((double)(tocno * 100) / (tocno + netocno), 2);
                else omjer = -1;

                OleDbCommand command2 = new OleDbCommand("update Slova set omjer = @omjer where Slovo = @slovo", connection);

                OleDbParameter parameter3 = new OleDbParameter();
                parameter3.ParameterName = "@omjer";
                parameter3.Value = omjer;
                command2.Parameters.Add(parameter3);

                OleDbParameter parameter4 = new OleDbParameter();
                parameter4.ParameterName = "@slovo";
                if (slovo == " ") parameter4.Value = "space";
                else parameter4.Value = slovo;
                command2.Parameters.Add(parameter4);

                command2.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                if (reader != null)
                    reader.Close();

                if (connection != null)
                    connection.Close();
            }
        }

        internal void dodajVjezbu(String ime, int br_slova, int br_rijeci, String slova)
        {
            try
            {
                connection.Open();

                //Console.WriteLine("indeks vjezbe, brzina, preciznost: " + indeksVjezbe + ", " + brzina + ", " + preciznost);

                OleDbCommand command = new OleDbCommand("insert into SpremljeneVjezbe ([Ime],[Duljina_rijeci],[Broj_rijeci],[Slova]) " +
                    "values (@ime, @br_rijeci, @br_slova, @slova) ", connection);
                command.Parameters.AddWithValue("@ime", ime);
                command.Parameters.AddWithValue("@br_rijeci", br_rijeci);
                command.Parameters.AddWithValue("@br_slova", br_slova);
                command.Parameters.AddWithValue("@slova", slova);

                command.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
            MessageBox.Show("Spremili ste vježbu!");
        }

        public List<String> dohvatiVjezbe()
        {
            List<String> nizStringova = new List<String>();
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand("select * from SpremljeneVjezbe", connection);
                int i = (int)command.ExecuteScalar();
                reader = command.ExecuteReader();
                while (reader.Read()) nizStringova.Add(reader["Ime"].ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                if (reader != null)
                    reader.Close();

                if (connection != null)
                    connection.Close();
            }
            return nizStringova;
        }

        internal List<Tuple<int, int, string>> nadiSpremljenuVjezbu(String ime)
        {
            List<Tuple<int, int, string>> ret = new List<Tuple<int, int, string>>();

            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand("select * from SpremljeneVjezbe where Ime = @ime", connection);
                command.Parameters.AddWithValue("@ime", ime);

                reader = command.ExecuteReader();

                while (reader.Read())
                    ret.Add(new Tuple<int, int, string>((int)reader["Duljina_rijeci"], (int)reader["Broj_rijeci"], (String)reader["Slova"]));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                if (reader != null)
                    reader.Close();

                if (connection != null)
                    connection.Close();
            }
            return ret;
        }

        internal List<Tuple<double, double>> dohvatiZadnjeRezultate()
        {
            List<Tuple<double, double>> ret = new List<Tuple<double, double>>();

            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand("select * from Statistika", connection);

                reader = command.ExecuteReader();

                while (reader.Read())
                    ret.Add(new Tuple<double, double>((double)reader["Brzina"], (double)reader["Preciznost"]));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                if (reader != null)
                    reader.Close();

                if (connection != null)
                    connection.Close();
            }
            return ret;
        }

        internal void dodajRezultat(double brzina, double preciznost)
        {
            try
            {
                connection.Open();

                OleDbCommand command = new OleDbCommand("insert into Statistika ([Brzina],[Preciznost]) " +
                    "values (@brzina, @preciznost)", connection);
                command.Parameters.AddWithValue("@brzina", brzina);
                command.Parameters.AddWithValue("@preciznost", preciznost);

                command.ExecuteNonQuery();
            }

            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        public List<Tuple<String, double>> dohvatiNajgoraSlova()
        {
            List<Tuple<String, double>> flopSlova = new List<Tuple<String, double>>();
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand("select * from Slova order by Omjer asc", connection);
                int i = (int)command.ExecuteScalar();
                reader = command.ExecuteReader();
                int br = 0;
                while (reader.Read())
                {
                    br++;
                    if ((double)reader["Omjer"] != -1) flopSlova.Add(new Tuple<String, double>(reader["Slovo"].ToString(), (double)reader["Omjer"]));
                    if (br == 5) break;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                if (reader != null)
                    reader.Close();

                if (connection != null)
                    connection.Close();
            }
            return flopSlova;
        }

        public List<Tuple<String, double>> dohvatiNajboljaSlova()
        {
            List<Tuple<String, double>> topSlova = new List<Tuple<String, double>>();
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand("select * from Slova order by Omjer desc", connection);
                int i = (int)command.ExecuteScalar();
                reader = command.ExecuteReader();
                int br = 0;
                while (reader.Read())
                {
                    br++;
                    if ((double)reader["Omjer"] != -1) topSlova.Add(new Tuple<String, double>(reader["Slovo"].ToString(), (double)reader["Omjer"]));
                    if (br == 5) break;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                if (reader != null)
                    reader.Close();

                if (connection != null)
                    connection.Close();
            }
            return topSlova;
        }
    }
}
