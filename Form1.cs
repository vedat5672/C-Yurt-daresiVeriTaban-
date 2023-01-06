using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseHomework
{
    public partial class FormApp : Form
    {
        public FormApp()
        {
            InitializeComponent();
        }

        void  Listele()
        {
            string sorgu = "select * from ogrenci order by ogrencino asc";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
       

        private void Form1_Load(object sender, EventArgs e)
        {
        }

       

        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost; port=5432; Database=DatabaseOdevi; user id=postgres; password=root");
        private void button1_Click(object sender, EventArgs e)
        {

            Listele();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand ekle = new NpgsqlCommand("insert into ogrenci (ogrencino,adi,soyadi,bolumno,yurtno,odano) values (@p1,@p2,@p3,@p4,@p5,@p6)", baglanti);
            ekle.Parameters.AddWithValue("@p1", Convert.ToInt32(textBox1.Text)) ;
            ekle.Parameters.AddWithValue("@p2", textBox2.Text);
            ekle.Parameters.AddWithValue("@p3", textBox3.Text);
            ekle.Parameters.AddWithValue("@p4", Convert.ToInt32(textBox4.Text));
            ekle.Parameters.AddWithValue("@p5", Convert.ToInt32(textBox5.Text));
            ekle.Parameters.AddWithValue("@p6", Convert.ToInt32(textBox6.Text));
            ekle.ExecuteNonQuery();
            Listele();
            baglanti.Close();

            MessageBox.Show("Öğrenci ekleme işlemi tamamlandı.");


        }

        private void button3_Click(object sender, EventArgs e)
        {
            

            DialogResult dr = new DialogResult();
            dr = MessageBox.Show("Silmek istediğinize emin misiniz?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                baglanti.Open();
                NpgsqlCommand sil = new NpgsqlCommand("delete from ogrenci where ogrencino=@p1", baglanti);
                sil.Parameters.AddWithValue("@p1", Convert.ToInt32(textBox1.Text));
                sil.ExecuteNonQuery();
                Listele();
                baglanti.Close();
                MessageBox.Show("Öğrenci silme işlemi tamamlandı.");
            }


        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand guncelle = new NpgsqlCommand("update ogrenci set adi=@p1,soyadi=@p2,bolumno=@p3,yurtno=@p4,odano=@p5 where ogrencino=@p6",baglanti);
            guncelle.Parameters.AddWithValue("@p1", textBox2.Text);
            guncelle.Parameters.AddWithValue("@p2", textBox3.Text);
            guncelle.Parameters.AddWithValue("@p3", Convert.ToInt32( textBox4.Text));
            guncelle.Parameters.AddWithValue("@p4", Convert.ToInt32(textBox5.Text));
            guncelle.Parameters.AddWithValue("@p5", Convert.ToInt32(textBox6.Text));
            guncelle.Parameters.AddWithValue("@p6", Convert.ToInt32(textBox1.Text));
            guncelle.ExecuteNonQuery();
            Listele();
            baglanti.Close();
            MessageBox.Show("Öğrenci güncelleme işlemi tamamlandı.");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("Select * From ogrenci Where adi like '%" + textBox2.Text + "%' ", baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            baglanti.Close();
        }

    }
}
