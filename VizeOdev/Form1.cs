using System;
using System.Linq;
using System.Xml;
using System.IO;
using System.Threading;
using System.Windows.Forms;


namespace VizeOdev
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string[] lines =
        {
            "P_NAME", "P_MFGR", "P_BRAND","P_TYPE","P_SIZE","P_CONTAINER","P_RETAILPRICE","P_COMMENT"
        };

        string[] xmldata = new string[8];
        string[] txtData = new string[8];
        void xmlOkuma()
        {
            try         //try-catch bloku içerisinde xml den veri çekiyoruz
            {
                string BASE_URL = "http://aiweb.cs.washington.edu/research/projects/xmltk/xmldata/data/tpc-h/part.xml";     //xml linki

                var xmlDoc = new XmlDocument();
                xmlDoc.Load(BASE_URL);

                for (int i = 0; i < lines.Length; i++)
                {
                    xmldata[i] = xmlDoc.SelectSingleNode("table[@ID='part']/T/" + lines[i]).InnerXml; //xml tablosundaki belirli alanları döngü ile listeye kayıt ettik
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        Thread arkaplanCalısma;
        private void Form1_Load(object sender, EventArgs e)
        {
            arkaplanCalısma = new Thread(() =>
            {        //yeni thread olusturup işlemleri thread içerisinde yaptık
                while (true)
                {
                    xmlOkuma();
                    dosyaokuma();
                    karsilastirma();
                    Thread.Sleep(1000);    //verileri kontrol etme aralıgını burada belirlioruz
                }
            });
            arkaplanCalısma.Start();
        }

        void dosyaokuma()
        {
            if (File.Exists("veriler.txt"))    //okuma yapacagımız dosyanın var olup olmadıgını kontrol ediyoruz
            {
                string[] okuma = File.ReadAllLines(@"veriler.txt");     //txt dosyasındaki verileri liste atıyoruz
                int sayac = 0;
                foreach (var item in okuma)
                {
                    txtData[sayac] = item;      //döngü ile verileri satır satır listeye atıyoruz
                    sayac++;
                }
            }
        }
        void karsilastirma()
        {
            bool esitmi = xmldata.SequenceEqual(txtData);       //internetten çektiğimiz veri listesi ile txt dosyasındaki veri listesini karşılaştırıyourz farklılık varsa false değer donduruyoruz.
            if (!esitmi)
            {
                MessageBox.Show("Yeni veri bulundu", "UYARI", MessageBoxButtons.OK);
                File.WriteAllLines("veriler.txt", xmldata);      //farklı veri varsa txt ye kaydettik
            }
        }
    }
}
