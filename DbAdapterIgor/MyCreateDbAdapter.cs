using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace DbAdapterIgor
{
    public partial class MyCreateDbAdapter : Form
    {
        Logic.Konnekcija _konekcija = new Logic.Konnekcija();


        public MyCreateDbAdapter()
        {
            InitializeComponent();
            // Grid();
            MessageBox.Show("Grid se puni po varijablama","On load message", MessageBoxButtons.OK);
        }



        void Grid()
        {
            try
            {

        
                     using (MySqlConnection con = new MySqlConnection(_konekcija.mysqlKoneckija))
                            {
                                con.Open();
                    //string sql = "SELECT * FROM vivoostalo order by ID ASC";
                    //string sql = "SELECT * FROM tekstovi where spojenoNa like 'vivoostalo-%'";
                    //string sql = "SELECT * FROM slike";
                    //  string sql = "SELECT slike, id FROM vivoostalo WHERE slike IS NOT NULL";

                    // string sql = "SELECT * FROM tekstovi where spojenoNa like 'vivozemljista-%'";
                    //string sql = "SELECT slike, id FROM vivozemljista WHERE slike IS NOT NULL";



                    //novo
                    //  string sql = "SELECT slike, id FROM " + txt_tablica.Text + "  WHERE slike IS NOT NULL";


                    string sql = "SELECT slike, id FROM " + txt_tablica.Text + "  WHERE slike IS NOT NULL AND id > "+ txt_id.Text + " order by id asc";

                  

                    MySqlCommand cmd = new MySqlCommand(sql, con);

                                MySqlDataAdapter sqlDataAdap = new MySqlDataAdapter(cmd);
                                DataTable dt = new DataTable();
                                sqlDataAdap.Fill(dt);
                                dataGridView1.DataSource = dt;
                            }

                }
                catch (SqlException sex)
                {

                         MessageBox.Show(sex.Message);
                }
        }


        //prenesi iz Grida u novu tablicu
        private void button1_Click(object sender, EventArgs e)
        {
            int brojac = 0;

     
                try
                {


                    using (MySqlConnection con = new MySqlConnection(_konekcija.mysqlKoneckija))
                    {
                        con.Open();
                   

                            string StrQuery;
                            Regex re = new Regex(@"\d+");

                             for (int i = 0; i < dataGridView1.Rows.Count; i++)
                            {
                                brojac =  i++;  

                              //izvačim samo broj iz stringa
                              Match m = re.Match(dataGridView1.Rows[i].Cells["spojenoNa"].Value.ToString().ToString());

                        //StrQuery = @"INSERT INTO TextTransfer (tekst, jezik, spojenoNa) VALUES ("
                        //+ "'" + dataGridView1.Rows[i].Cells["tekst"].Value.ToString() + "', "
                        //+ "'" + dataGridView1.Rows[i].Cells["jezik"].Value.ToString() + "', "
                        //+ "'" +  m+ "'); ";

                        StrQuery = @"INSERT INTO tekstTransferZemlja (tekst, jezik, spojenoNa) VALUES ("
                        + "'" + dataGridView1.Rows[i].Cells["tekst"].Value.ToString() + "', "
                        + "'" + dataGridView1.Rows[i].Cells["jezik"].Value.ToString() + "', "
                        + "'" + m + "'); ";




                        using (MySqlCommand cmd = new MySqlCommand(StrQuery, con))
                                        {
                                            cmd.ExecuteNonQuery();
                                        }

                         
                            }
                    Lbl_counter.Text = brojac.ToString();
                    con.Close();
                    con.Dispose();

                    //https://www.daniweb.com/programming/software-development/threads/371677/c-datagridview-only-display-number-for-certain-column


                }

            }



       

                catch (MySqlException sex)
                {

                    MessageBox.Show(sex.Message);
                }


            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }


        }




        private void button2_Click(object sender, EventArgs e)
        {

        }



        public static string SafeSubstring( string value, int startIndex, int length)
        {

            return new string((value ?? string.Empty).Skip(startIndex).Take(length).ToArray());


        }




        //unesi slike
        private void button3_Click(object sender, EventArgs e)
        {
            int brojac = 0;

            try
            {


                using (MySqlConnection con = new MySqlConnection(_konekcija.mysqlKoneckija))
                {
                    con.Open();
                    string StrQuery;
                    Regex re = new Regex(@"\d+");

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {

                        //filtriam slike do zareza
                        string m = dataGridView1.Rows[i].Cells["slike"].Value.ToString();

                        //string m1 = m.Substring(6, 5);  //slk2
                        //string m1 = m.Substring(12, 5);  //slk3
                        // string m1 = m.Substring(18, 5);  //slk4
                        // string m1 = m.Substring(24, 5);  //slk5

                        // string m1 = SafeSubstring(m, 0, 5); //slk 1

                        // string m1 = SafeSubstring(m, 30, 5); //slk 6
                        // string m1 = SafeSubstring(m, 36, 5); //slk 7

                        int start = int.Parse(txt_from.Text);
                        int end = int.Parse(txt_To.Text);

                        string m1 = SafeSubstring(m, start, end); //prema UI izvana



                        string url;
                        string slika = "";
                        string querytransport = "";

                        

                        if (m1 != "" || !string.IsNullOrEmpty(m1))
                        {
                            if (m1.Contains(","))
                            {
                                // int index = m1.IndexOf(",");
                                // string str = m1.Substring(0, index);
                                // MessageBox.Show("Poruka"+str);

                                m1 = m1.Replace(",", "");
                                querytransport = "SELECT datoteka FROM slike where id = " + m1;
                            }

                            else
                            {

                                querytransport = "SELECT datoteka FROM slike where id = " + m1;
                            }
                          
                        }

                        else
                        {
                            if (m1.Contains(string.Empty))
                            {
                                querytransport = "SELECT datoteka FROM slike where id = 0";
                            }

                            
                        }

                  
     


                        using (MySqlCommand cmd = new MySqlCommand(querytransport, con))
                        {

                            if (m1 != "" )
                            {
                                //slika = "1";
                                slika = cmd.ExecuteScalar().ToString();
                                url = "http://nekretnine-tomislav.hr/slike/" + slika;

                                //if(m1.Count() <= 3)
                                //{
                                //    slika = cmd.ExecuteScalar().ToString();
                                //    url = "http://nekretnine-tomislav.hr/slike/" + slika;
                                //}

                            }

                            else
                            {
                                //  slika = m1;
                              
                               // slika = cmd.ExecuteScalar().ToString();
                                url = "http://nekretnine-tomislav.hr/slike/" + slika;
                               // url = "http://nekretnine-tomislav.hr/elementi/pageBack.png";
                            }


                           

                            brojac = brojac+1;
                        }


                        // insert novi
                        //    StrQuery = @"INSERT INTO sliketransfer (ID_nekrenina, slk1) VALUES ("
                        //+ "'" + dataGridView1.Rows[i].Cells["id"].Value.ToString() + "', "
                        //+ "'" + url + "');";



                        //StrQuery = @"UPDATE sliketransfer SET slk2 = "
                        //         + "'" + url + "'  WHERE ID_nekrenina = " + "'" + dataGridView1.Rows[i].Cells["id"].Value.ToString() + "';";


                        //StrQuery = @"UPDATE sliketransfer SET slk3 = "
                        //         + "'" + url + "'  WHERE ID_nekrenina = " + "'" + dataGridView1.Rows[i].Cells["id"].Value.ToString() + "';";



                        //StrQuery = @"UPDATE sliketransfer SET slk4 = "
                        //     + "'" + url + "'  WHERE ID_nekrenina = " + "'" + dataGridView1.Rows[i].Cells["id"].Value.ToString() + "';";

                        //    StrQuery = @"UPDATE sliketransfer SET slk5 = "
                        //+ "'" + url + "'  WHERE ID_nekrenina = " + "'" + dataGridView1.Rows[i].Cells["id"].Value.ToString() + "';";





                        //  ako ih nema
                        //StrQuery = @"UPDATE sliketransfer SET slk6 = "
                        //  + "'" + url + "'  WHERE ID_nekrenina = " + "'" + dataGridView1.Rows[i].Cells["id"].Value.ToString() + "';";



                        StrQuery = @"UPDATE sliketransfer SET slk7 = "
                             + "'" + url + "'  WHERE ID_nekrenina = " + "'" + dataGridView1.Rows[i].Cells["id"].Value.ToString() + "';";




                        using (MySqlCommand cmd = new MySqlCommand(StrQuery, con))
                        {
                            cmd.ExecuteNonQuery();
                            brojac = brojac+1;

                        }


                    }

                    Lbl_counter.Text = "Količina : " + brojac.ToString();
                    con.Close();
                    con.Dispose();

                   
                


                }

            }





            catch (MySqlException sex)
            {

                MessageBox.Show(sex.Message+" Prenio si : "+brojac.ToString());
                GridClick();
                Lbl_counter.Text = "Količina : " + brojac.ToString();
            }


            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + " Prenio si : " + brojac.ToString());
                GridClick();
                Lbl_counter.Text = "Količina : " + brojac.ToString();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }



        private void GridClick()
        {

            try
            {


                using (MySqlConnection con = new MySqlConnection(_konekcija.mysqlKoneckija))
                {
                    con.Open();
                    //string sql = "SELECT * FROM vivoostalo order by ID ASC";
                    // string sql = "SELECT * FROM tekstovi where spojenoNa like 'vivoostalo-%'";
                    //  string sql = "SELECT * FROM slike";
                    string sql = "SELECT * FROM sliketransfer order by ID_nekrenina asc ";


                    MySqlCommand cmd = new MySqlCommand(sql, con);

                    MySqlDataAdapter sqlDataAdap = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sqlDataAdap.Fill(dt);
                    dataGridView2.DataSource = dt;
                }

            }
            catch (SqlException sex)
            {

                MessageBox.Show(sex.Message);
            }


        }

        private void button5_Click(object sender, EventArgs e)
        {
            Grid();
        }
    }

}
