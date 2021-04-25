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
            string dosya_yolu = @"C:\Users\DeadGhost\Desktop\ntp ödev\VizeOdev\VizeOdev\bin\Debug\abc.txt";
           
            FileStream fs = new FileStream(dosya_yolu, FileMode.Open, FileAccess.Read);
           
            StreamReader sw = new StreamReader(fs);
            
            string yazi = sw.ReadLine();
            while (yazi != null)
            {
                Console.WriteLine(yazi);
                yazi = sw.ReadLine();               
            }
            MessageBox.Show(yazi, "deneme");
        }
        void writeText(string[] data)
        {
            File.WriteAllLines("WriteLines.txt", data);
        }

        string[] lines =
        {
            "P_NAME", "P_MFGR", "P_BRAND","P_TYPE","P_SIZE","P_CONTAINER","P_RETAILPRICE","P_COMMENT"
        };

        string[] data = new string[8];
        void fetchXmlData()
        {
            try
            {
                string BASE_URL = "http://aiweb.cs.washington.edu/research/projects/xmltk/xmldata/data/tpc-h/part.xml";

                var xmlDoc = new XmlDocument();
                xmlDoc.Load(BASE_URL);

                for (int i = 0; i < lines.Length; i++)
                {
                    data[i] = xmlDoc.SelectSingleNode("table[@ID='part']/T/" + lines[i]).InnerXml;
                }
                writeText(data);
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
            dosyadanOku();         
        }
    }
}
