using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;
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
