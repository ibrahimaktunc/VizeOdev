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
        void writeText(string[] data)
        {
            if (!File.Exists("veriler.txt"))    //txt dosyasının olup olmadığını kontrol ediyoruz
            {
                File.WriteAllLines("veriler.txt", data);     //dosya yok ise oluşturup data listesine verileri kaydediyrouz
            }          
        }

        string[] lines =
        {
            "P_NAME", "P_MFGR", "P_BRAND","P_TYPE","P_SIZE","P_CONTAINER","P_RETAILPRICE","P_COMMENT"
        };

        string[] xmldata = new string[8];
       string[] txtData = new string[8];
        void fetchXmlData()
        {
            try
            {
                string BASE_URL = "http://aiweb.cs.washington.edu/research/projects/xmltk/xmldata/data/tpc-h/part.xml";

                var xmlDoc = new XmlDocument();
                xmlDoc.Load(BASE_URL);

                for (int i = 0; i < lines.Length; i++)
                {
                    xmldata[i] = xmlDoc.SelectSingleNode("table[@ID='part']/T/" + lines[i]).InnerXml;
                }
                writeText(xmldata);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {           
            fetchXmlData();      
        }
        void dosyaokuma()
        {
            string[] okuma = File.ReadAllLines(@"veriler.txt");
            int sayac = 0;
            foreach (var item in okuma)
            {
                txtData[sayac] = item;
                sayac++;
            }
        }
        void karsilastirma()
        {
            bool esitmi = xmldata.SequenceEqual(txtData);
            if (!esitmi)
            {
                MessageBox.Show("Yeni veri bulundu", "UYARI", MessageBoxButtons.OK);
                writeText(xmldata);
            }
        }
    }
}
