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


namespace WindowsFormsNOT_KAYIT_SON
{
    public partial class FrmOgretmenDetay : Form
    {
        public FrmOgretmenDetay()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-DLSRRMG\SQLEXPRESS;Initial Catalog=DbNotKayit;Integrated Security=true; TrustServerCertificate=true;");
        private void FrmOgretmenDetay_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dbNotKayitDataSet.tblDers' table. You can move, or remove it, as needed.
            this.tblDersTableAdapter.Fill(this.dbNotKayitDataSet.tblDers);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TblDers (OGRNUMARA, OGRAD, OGRSOYAD) values(@P1,@P2,@P3)", baglanti);
            komut.Parameters.AddWithValue("@p1", mskNumara.Text);
            komut.Parameters.AddWithValue("@p2", textAd.Text);
            komut.Parameters.AddWithValue("@p3", textSoyad.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Öğrenci Sisteme Başarılı Bie Şekilde Eklenmiştir.");

            this.tblDersTableAdapter.Fill(this.dbNotKayitDataSet.tblDers); // otomatik doldurma komutu
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            mskNumara.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            textAd.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            textSoyad.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            textSinav1.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            textSinav2.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            textSinav3.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double ortalama, s1, s2, s3;
            string durum;
            s1 = Convert.ToDouble(textSinav1.Text);
            s2 = Convert.ToDouble(textSinav2.Text);
            s3 = Convert.ToDouble(textSinav3.Text);
            ortalama = (s1 + s2 + s3 ) / 3;
            LblOrtalama .Text =ortalama.ToString();

            if (ortalama >= 50)
            {
                durum = "True";
            }
            else
            {
                durum = "False";
            }

            baglanti.Open();
            SqlCommand komut = new SqlCommand("update tblDers set OGRS1=@p1, OGRS2=@p2, OGRS3=@p3, ORTALAMA=@p4, DURUM=@p5 where OGRNUMARA=@p6", baglanti);
            komut.Parameters.AddWithValue ("@p1", textSinav1 .Text);
            komut.Parameters.AddWithValue("@p2", textSinav2.Text);
            komut.Parameters.AddWithValue("@p3", textSinav3.Text);
            komut.Parameters.AddWithValue("@p4", decimal.Parse (LblOrtalama  .Text));
            komut.Parameters.AddWithValue("@p5", durum);
            komut.Parameters.AddWithValue("@p6", mskNumara .Text);

            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Öğrenci Notları Güncellendi");

            this.tblDersTableAdapter.Fill(this.dbNotKayitDataSet.tblDers);
        }
    }
}
