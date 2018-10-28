using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Xml;
using System.Xml.XmlConfiguration;
using DbAdapterIgor.Logic;

namespace DbAdapterIgor
{
    public partial class Form2 : Form
    {
        //
        XmlFilesUI _Path = new XmlFilesUI();

        public Form2()
        {
            InitializeComponent();

           // _Path.ProizvodiPath = @"C:\procject\new2Proizvodi_org.xml";
           _Path.ProizvodiPath = @"C:\procject\new2Proizvodi.xml";
            _Path.CijenePath = @"C:\procject\cjene.xml";
            _Path.UpdateProizvod = @"C:\procject\Proizvodi_skrUpdate.xml";

        }


        //proizvodi load
        private void LoadXmlFile()
        {
            //StringBuilder sbuild_ = new StringBuilder();

            //foreach (XElement level1Element in XElement.Load(@"C:\Proizvodi_skr.xml").Elements("product"))
            //{
            //    sbuild_.


            //}

            try
            {


                //XmlDataDocument xmldoc = new XmlDataDocument();
                //XmlNodeList xmlnode; int i = 0;
                //string path = @"C:\Proizvodi_skr.xml";
                //string str = null;
                //FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                //xmldoc.Load(fs);
                //xmlnode = xmldoc.GetElementsByTagName("product");
                //for (i = 0; i <= xmlnode.Count - 1; i++)
                //{ xmlnode[i].ChildNodes.Item(0).InnerText.Trim();
                //    str = xmlnode[i].ChildNodes.Item(0).InnerText.Trim() + " " + xmlnode[i].ChildNodes.Item(1).InnerText.Trim() + " " + xmlnode[i].ChildNodes.Item(2).InnerText.Trim();
                //    MessageBox.Show(str);
                //}



                DataSet dataSet = new DataSet();
                // dataSet.ReadXml(@"C:\procject\Proizvodi_skr.xml");
                dataSet.ReadXml(_Path.ProizvodiPath);
                dataGridView1.DataSource = dataSet.Tables[0];




            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška " + ex.Message);
            }




        }

        //cijene
        private void LoadXmlFileCijene()
        {
         

            try
            {



                DataSet dataSet = new DataSet();
                dataSet.ReadXml(_Path.CijenePath);
                dataGridView1.DataSource = dataSet.Tables[0];




            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška " + ex.Message);
            }




        }



        //update cijene

        private void LoadXmlFileUpdateSCijenama()
        {


            try
            {



                DataSet dataSet = new DataSet();
                //dataSet.ReadXml(@"C:\procject\Proizvodi_skrUpdate.xml");
                dataSet.ReadXml(_Path.UpdateProizvod);
                dataGridView1.DataSource = dataSet.Tables[1];




            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška " + ex.Message);
            }




        }





        private void UpdateMe()
        {


            //string proizvodi = "";
            //string cijene = "";
            //proizvodi = @"C:\Users\developer\Documents\PROMODAR XML\procject\Proizvodi_skr.xml";
            //cijene = @"C:\Users\developer\Documents\PROMODAR XML\procject\price.xml";

            //proizvodi = @"C:\procject\Proizvodi_skr.xml";
            //cijene = @"C:\procject\cjene2.xml";



            XmlDocument xdoc = new XmlDocument();
            XmlDocument xdoc1 = new XmlDocument();


           


            if (!File.Exists(_Path.ProizvodiPath.Trim()))
            {
                string xmlContent = "";
                // StreamWriter sw = new StreamWriter(proizvodi.Trim(), false, System.Text.Encoding.Default);
                StreamWriter sw = new StreamWriter(_Path.ProizvodiPath.Trim(), false, System.Text.Encoding.Default);
                //  xdoc.Load(cijene.Trim());
                xdoc.Load(_Path.CijenePath.Trim());
                xmlContent = xdoc.InnerXml;

                sw.Write(xmlContent.Trim());
                sw.Close();
                MessageBox.Show("file exists i update je izvršen");

            }


            else
            {
                //xdoc1.Load(proizvodi.Trim());
                //string oldContent = xdoc1.InnerXml;
                //xdoc.Load(cijene.Trim());
                //xdoc.InnerXml = oldContent;

                //xdoc.Save(cijene.Trim());
                //xdoc1.Save(proizvodi.Trim());

                //MessageBox.Show("else");
            }
        




    }


        private void UpdateMeNew1()
        {

            //string proizvodi = "";
            //string cijene = "";
            //string updateProizvodi = "";


            //proizvodi = @"C:\procject\Proizvodi_skr.xml";
            //updateProizvodi = @"C:\procject\Proizvodi_skrUpdate.xml";
            //cijene = @"C:\procject\cjene.xml";


 

            XmlDocument xmlDocCijene = new XmlDocument();
            xmlDocCijene.Load(_Path.CijenePath);


            XmlDocument xmlDocProizvodi = new XmlDocument();
            xmlDocProizvodi.Load(_Path.ProizvodiPath);

            XmlNodeList itemNumber = xmlDocCijene.GetElementsByTagName("itemNumber");
            XmlNodeList amount = xmlDocCijene.GetElementsByTagName("currency");
            XmlNodeList itemNumberPr = xmlDocProizvodi.GetElementsByTagName("itemNumber");



            #region read xml and store variables

            XDocument doc = XDocument.Load(_Path.CijenePath);
            var rows = doc.Descendants("price").Select(
                e => new
                {
                    cijena_ = e.Element("amount").Value,
                    id_ = e.Element("itemNumber").Value,
                    valuta = e.Element("currency").Value,
                    tip = e.Element("type").Value

                });



            XDocument docProduct = XDocument.Load(_Path.ProizvodiPath);

            var rows_proizvod = docProduct.Descendants("product").Select(
                e => new
                {
                    id_ = e.Element("itemNumber").Value
                });

            #endregion




            // double exchange = 0.0d;

            #region WORKING PROJECT
            //XDocument docWrite = XDocument.Load(_Path.ProizvodiPath);
            //int counter = 0;
            ////cijene
            //foreach (var item_cijene in rows)
            //{
            //    counter++;

            //    foreach (var item_proizvodK in rows_proizvod)
            //    {
            //        if (item_cijene.id_ == item_proizvodK.id_)
            //        {
            //            if (counter == 2)
            //            {
            //                foreach (var i in docWrite.Descendants("product"))
            //                {

            //                    i.Add(new XElement("price", item_cijene.cijena_));
            //                    i.Add(new XElement("currency", item_cijene.valuta));
            //                    i.Add(new XElement("type", item_cijene.tip));
            //                    //stani
            //                    // break;
            //                    //rows = null;

            //                    //exchange = Convert.ToDouble(item.cijena_);
            //                    //exchange = double.Parse(item.cijena_);
            //                    //exchange =  7.4 * exchange;

            //                    CalculateAll(progressBar1);
            //                    counter = 0;


            //                }
            //            }








            //            //MessageBox.Show("Prošlo je" + item2.id_.ToString());


            //        }

            //    }

            //}

            //docWrite.Save(_Path.UpdateProizvod);
            //MessageBox.Show("Finish");

            #endregion



            #region WORKING PROJECT2
            XDocument docWrite = XDocument.Load(_Path.ProizvodiPath);
            int ij = 0;

            if(ij == 0)
            {

                foreach (var item_cijene in rows)
                {

                    foreach (var item_proizvodK in rows_proizvod)
                    {
                        if (item_cijene.id_ == item_proizvodK.id_)
                        {
                            foreach (var i in docWrite.Descendants("product"))
                            {
                                if (ij <= 2)
                                {

                                    if (item_cijene.id_ == item_proizvodK.id_)
                                    {
                                        i.Add(new XElement("price", item_cijene.cijena_));
                                        i.Add(new XElement("currency", item_cijene.valuta));
                                        i.Add(new XElement("type", item_cijene.tip));
                                        MessageBox.Show("Brojać :" + ij.ToString() + "  proizvod id " + item_proizvodK.id_.ToString());
                                        CalculateAll(progressBar1);
                                        docWrite.Save(_Path.UpdateProizvod);
                                        ij++;
                                        continue;


                                    }
                                }

                                else
                                {
                                    ij = 0; break;
                                }

                            }



                        }

                        //else
                        //{ break; }
                    }

                }
 

            }



            MessageBox.Show("Finish");
        






            #endregion




        }




        private void button1_Click(object sender, EventArgs e)
        {
            LoadXmlFile();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadXmlFileCijene();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //UpdateMe();
            // UpdateMeNew1();
            UpdateMe3();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LoadXmlFileUpdateSCijenama();
        }


        //Reži XMl products NE RADI
        private void button5_Click(object sender, EventArgs e)
        {

            string proizvodi = "";
            string cijene = "";
            string updateProizvodi = "";


            proizvodi = @"C:\procject\product2018-10-01.xml";
            updateProizvodi = @"C:\procject\Product_UTT_1.xml";
          //  cijene = @"C:\procject\cjene2.xml";


            XmlDocument xmlDocProizvodi = new XmlDocument();
            xmlDocProizvodi.Load(proizvodi);

            XmlNodeList item = xmlDocProizvodi.GetElementsByTagName("item");


            XDocument docProduct = XDocument.Load(proizvodi);
            var rows_proizvod = docProduct.Descendants("item").Select(
              e1 => new
              {
                  id_ = e1.Element("sku").Value
              });

            

            XDocument docWrite = XDocument.Load(proizvodi);

            int counter = 0;


            XDocument docA = XDocument.Load(proizvodi);
           int ScalarVariab = docA.Root.Element("item").Elements("sku").Count();



            foreach (var item2 in rows_proizvod)
            {
                if(item2.id_ != null)
                {
                    MessageBox.Show(item2.id_.ToString() + "//nb "+ counter.ToString());

                    if (counter == 3)
                    {
                        docWrite.Save(updateProizvodi);
                        // MessageBox.Show("Prošlo je x" + counter.ToString());
                        MessageBox.Show(counter.ToString());
                        break;
                    }

                    else
                        counter++;


                }
            



              



                //for (int i = 3; i < counter; i++)
                //{

                //    docWrite.Save(updateProizvodi);
                //    MessageBox.Show("Prošlo je :" + i.ToString() + "proizvoda");
                //    break;
                //}






            }



           
          






        }


        private void CalculateAll(System.Windows.Forms.ProgressBar progressBar)
        {
            progressBar1.Maximum = 100000;
            progressBar.Step = 1;

            for (int i = 0; i < 100000; i++)
            {
                double pow = Math.Pow(i, i);
                progressBar.PerformStep();
            }

        }


        private void UpdateMe3()
        {



            XDocument doc = XDocument.Load(_Path.UpdateProizvod);

            var list = doc.Element("Products").Elements("Product");

            foreach (var node in list)
            {
                foreach (Item item in stockArray)
                {
                    if (node.Element("recordNumber").Value == Convert.ToString(item.Id))
                        node.SetElementValue("count", Convert.ToString(item.count));
                }
            }




            //https://stackoverflow.com/questions/25480445/updating-xml-nodes-from-an-object-list-in-c-sharp

        }


    }

}
//https://www.c-sharpcorner.com/article/xml-manipulation-in-c-sharp/