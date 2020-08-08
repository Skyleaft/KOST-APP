using MaterialDesignColors.WpfExample.Domain;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KOST_APP.Views
{
    /// <summary>
    /// Interaction logic for viewBeranda.xaml
    /// </summary>
    public partial class viewDataKost : UserControl
    {
        koneksi k = new koneksi();
        public viewDataKost()
        {
            InitializeComponent();
            loadDataKost();
            loadDataPemilik();
        }

        private void loadDataKost()
        {
            try
            {
                k.sql = "select *from kosan";
                k.setdt();
                foreach (DataRow baris in k.dt.Rows)
                {
                    txt_nama.Text = baris[1].ToString();
                    txt_telp.Text = baris[6].ToString();
                    txt_kota.Text = baris[3].ToString();
                    txt_alamat.Text = baris[2].ToString();
                    txt_kdpos.Text = baris[4].ToString();
                    txt_deskripsi.Text = baris[5].ToString();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("error di open " + ex);
            }
        }

        private void loadDataPemilik()
        {
            try
            {
                k.sql = "select *from pemilik";
                k.setdt();
                foreach (DataRow baris in k.dt.Rows)
                {
                    txt_nik.Text = baris[1].ToString();
                    txt_namaPemilik.Text = baris[2].ToString();
                    if (baris[3].ToString() == "P")
                    {
                        rb_laki.IsChecked = true;
                    }
                    else
                    {
                        rb_perempuan.IsChecked = true;
                    }
                    txt_telp2.Text = baris[4].ToString();
                    txt_alamat2.Text = baris[5].ToString();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("error di open " + ex);
            }
        }

        private void btn_simpan1_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                k.sql = "update kosan set nama_kosan=@1,no_telp=@2,kota=@3,alamat=@4,kode_pos=@5, deskripsi_kost=@6 where id_pemilik = 1";
                k.setparam();
                k.perintah.Parameters.AddWithValue("@1", txt_nama.Text);
                k.perintah.Parameters.AddWithValue("@2", txt_telp.Text);
                k.perintah.Parameters.AddWithValue("@3", txt_kota);
                k.perintah.Parameters.AddWithValue("@4", txt_alamat.Text);
                k.perintah.Parameters.AddWithValue("@5", txt_kdpos.Text);
                k.perintah.Parameters.AddWithValue("@6", txt_deskripsi.Text);

                k.perintah.ExecuteNonQuery();
                k.close();


                var sampleMessageDialog = new SampleMessageDialog
                {
                    Message = { Text = "Data Berhasil Diubah" }
                };
                DialogHost.Show(sampleMessageDialog, "MainDialog");


            }
            catch (Exception ex)
            {
                MessageBox.Show("error " + ex.ToString());
            }
        }

        private void btn_simpan2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String jk = "";
                if (rb_laki.IsChecked == true)
                {
                    jk = "P";
                }
                else
                {
                    jk = "W";
                }

                k.sql = "update pemilik set nama_pemilik=@1,no_hp=@2,jenis_kelamin=@3,nik=@5,alamat=@6 where id_pemilik = 1";
                k.setparam();
                k.perintah.Parameters.AddWithValue("@1", txt_nama.Text);
                k.perintah.Parameters.AddWithValue("@2", txt_telp.Text);
                k.perintah.Parameters.AddWithValue("@3", jk);
                k.perintah.Parameters.AddWithValue("@5", txt_nik.Text);
                k.perintah.Parameters.AddWithValue("@6", txt_alamat.Text);

                k.perintah.ExecuteNonQuery();
                k.close();


                var sampleMessageDialog = new SampleMessageDialog
                {
                    Message = { Text = "Data Berhasil Diubah" }
                };
                DialogHost.Show(sampleMessageDialog, "MainDialog");


            }
            catch (Exception ex)
            {
                MessageBox.Show("error " + ex.ToString());
            }
        }
    }
}
