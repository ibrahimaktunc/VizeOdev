using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VizeOdev
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
          
        }
        public static void dosyadanOku()
        {
            string dosya_yolu = @"abc.txt";
           
            FileStream fs = new FileStream(dosya_yolu, FileMode.Open, FileAccess.Read);
           
            StreamReader sw = new StreamReader(fs);
            
            string yazi = sw.ReadLine();
            while (yazi != null)
            {
                Console.WriteLine(yazi);
                yazi = sw.ReadLine();
            }
            
            sw.Close();
            fs.Close();
          
        }
        void writeText(string P_NAME, string P_MFGR, string P_BRAND,string P_TYPE,string P_SIZE,string P_CONTAINER,string P_RETAILPRICE,string P_COMMENT)
        {
            using (StreamWriter file = new StreamWriter("WriteLines2.txt", append: true))
            {
               
                file.WriteLine("P_NAME "+P_NAME);
                file.WriteLine("P_MFGR "+P_MFGR);
                file.WriteLine("P_BRAND "+P_BRAND);
                file.WriteLine("P_TYPE "+P_TYPE);
                file.WriteLine("P_SIZE "+P_SIZE);
                file.WriteLine("P_CONTAINER "+P_CONTAINER);
                file.WriteLine("P_RETAILPRICE "+P_RETAILPRICE);
                file.WriteLine("P_COMMENT"+P_COMMENT);
              
            }
            string dosyaAdi = @"abc.txt";
            string yazis = "deneme123";
            FileStream fss = new FileStream(dosyaAdi, FileMode.OpenOrCreate, FileAccess.Write);
            fss.Close();
            File.AppendAllText(dosyaAdi, Environment.NewLine+ yazis);
        }

        void fetchxmldata()

        {

            try
            {
                string BASE_URL = "http://aiweb.cs.washington.edu/research/projects/xmltk/xmldata/data/tpc-h/part.xml";

                var xmlDoc = new XmlDocument();
                xmlDoc.Load(BASE_URL);

                string P_NAME = xmlDoc.SelectSingleNode("table[@ID='part']/T/P_NAME").InnerXml;
                string P_MFGR = xmlDoc.SelectSingleNode("table[@ID='part']/T/P_MFGR").InnerXml;
                string P_BRAND = xmlDoc.SelectSingleNode("table[@ID='part']/T/P_BRAND").InnerXml;
                string P_TYPE = xmlDoc.SelectSingleNode("table[@ID='part']/T/P_TYPE").InnerXml;
                string P_SIZE = xmlDoc.SelectSingleNode("table[@ID='part']/T/P_SIZE").InnerXml;
                string P_CONTAINER = xmlDoc.SelectSingleNode("table[@ID='part']/T/P_CONTAINER").InnerXml;
                string P_RETAILPRICE = xmlDoc.SelectSingleNode("table[@ID='part']/T/P_RETAILPRICE").InnerXml;
                string P_COMMENT = xmlDoc.SelectSingleNode("table[@ID='part']/T/P_COMMENT").InnerXml;
                writeText(P_NAME, P_MFGR,P_BRAND,P_TYPE,P_SIZE,P_CONTAINER,P_RETAILPRICE,P_COMMENT);
                MessageBox.Show(P_NAME+"\n"+P_MFGR+"\n", "baslık", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
           
            fetchxmldata();
         
           
        }
    }
}
