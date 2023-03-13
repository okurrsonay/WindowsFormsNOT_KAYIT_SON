using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsNOT_KAYIT_SON

{
    public partial class FrmOgrenciDetay : Form
    {

        public FrmOgrenciDetay()

        {
            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
        //Data Source=DESKTOP-DLSRRMG\SQLEXPRESS;Initial Catalog=DbNotKayit;Integrated Security=SSPI;
        public string numara;

        //SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-DLSRRMG\SQLEXPRESS;Initial Catalog=DbNotKayit;Integrated Security=true;");


        private void FrmOgrenciDetay_Load(object sender, EventArgs e)
        {


            LblNumara.Text = numara;

            SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-DLSRRMG\SQLEXPRESS;Initial Catalog=DbNotKayit;Integrated Security=true; TrustServerCertificate=true;");
            //DATA SOURCE=> KENDİ SQL SUNUCUMUZUN ADINI TUTAR
            //INITIAL CATALOG=> SQL SUNUCUMUZ ÜZRİNDE KULLADIĞIMIZ VERİ TABANIMIZIN ADI
            //INTEGRATED SECURİTY=> TRUE SEÇİLİRSE  SQL ÜZERİNDE GÜVENLİ BAĞLANTILARIN AÇILMASINA İZİN VERİR FALSE OLDUĞUNDA TAM TERSİ
            //TrustServerCertificate=true DEĞERİ GÜVENLİK SERTİFİKASININ KULLANILIP KULLANILMAYACAĞINA İZİN VERİR

            try
            {
                baglanti.Open();

                SqlCommand komut = new SqlCommand("SELECT * FROM tblDers WHERE OGRNUMARA=@p1", baglanti);
                komut.Parameters.AddWithValue("@p1", numara);
                SqlDataReader dr = komut.ExecuteReader();

                while (dr.Read())
                {
                    LblAdSoyad.Text = dr[2].ToString() + " " + dr[3].ToString();
                    LblSınav1.Text = dr[4].ToString();
                    LblSınav2.Text = dr[5].ToString();
                    LblSınav3.Text = dr[6].ToString();
                    LblOrtalama.Text = dr[7].ToString();
                    LblDurum.Text = dr[8].ToString();
                }
            }
            catch (Exception ex)
            {
                // Hata oluştuğunda burada işlem yapabilirsiniz
                MessageBox.Show("Bağlantı hatası: " + ex.Message);
            }
            finally
            {
                baglanti.Close();
            }
        }
    }
}
