using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KOST_APP.Class;
using MaterialDesignColors.WpfExample.Domain;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;

namespace KOST_APP.Dialog
{
    /// <summary>
    /// Interaction logic for dialogOpenCustomer.xaml
    /// </summary>
    public partial class dialogOpenCustomer : UserControl
    {
        koneksi k = new koneksi();
        private String alamat_foto;
        private String idCustomer;
        public dialogOpenCustomer(String id_customer)
        {
            InitializeComponent();
            idCustomer = id_customer;
            loadData();
            
        }

        private async void loadData()
        {
            await Task.Delay(300);
            try
            {
                k.sql = "select *from customer where id_customer ='" + idCustomer + "'";
                k.setdt();
                Byte[] tmpfoto = new Byte[0];
                foreach (DataRow baris in k.dt.Rows)
                {
                    txt_nik.Text = baris[1].ToString();
                    txt_nama.Text = baris[2].ToString();
                    txt_telp.Text = baris[3].ToString();
                    if (baris[4].ToString() == "P")
                    {
                        rb_laki.IsChecked = true;
                    }
                    else
                    {
                        rb_perempuan.IsChecked = true;
                    }
                    String tgllahir = baris[5].ToString();
                    DateTime dt = Convert.ToDateTime(tgllahir);
                    tgl_lahir.SelectedDate = dt;
                    txt_asalkota.Text = baris[6].ToString();
                    txt_alamat.Text = baris[7].ToString();
                    cmb_pekerjaan.Text = baris[8].ToString();
                    txt_instansi.Text = baris[9].ToString();
                    
                    if (!baris[10].Equals(null))
                    {
                        tmpfoto = (Byte[])baris[10];
                        img_foto.Source = ByteImageConverter.ByteToImage(tmpfoto);
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("error di open " + ex);
            }
        }

        private void btn_hapus_Click(object sender, RoutedEventArgs e)
        {
            Message.Text = "Yakin mau Hapus Customer : " + txt_nama.Text;
        }

        private void btn_yes_Click(object sender, RoutedEventArgs e)
        {
            k.sql = "delete from customer where id_customer = '" + idCustomer + "'";
            k.setdt();

            var sampleMessageDialog = new SampleMessageDialog
            {
                Message = { Text = "Data Berhasil Di Hapus" }
            };
            DialogHost.CloseDialogCommand.Execute(null, this);

            DialogHost.Show(sampleMessageDialog, "MainDialog");
        }

        private void btn_ubah_Click(object sender, RoutedEventArgs e)
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
                DateTime tgllahir = tgl_lahir.SelectedDate.Value;
                var img = ByteImageConverter.ConvertBitmapSourceToByteArray(img_foto.Source);

                k.sql = "update customer set nama=@1,no_hp=@2,jenis_kelamin=@3,tgl_lahir=@4,kota=@5,alamat=@6,pekerjaan=@7,nama_instansi=@8,foto=@9, nik=@10 where id_customer = '" + idCustomer + "'";
                k.setparam();
                k.perintah.Parameters.AddWithValue("@1", txt_nama.Text);
                k.perintah.Parameters.AddWithValue("@2", txt_telp.Text);
                k.perintah.Parameters.AddWithValue("@3", jk);
                k.perintah.Parameters.AddWithValue("@4", tgllahir.ToString("yyyy-MM-dd"));
                k.perintah.Parameters.AddWithValue("@5", txt_asalkota.Text);
                k.perintah.Parameters.AddWithValue("@6", txt_alamat.Text);
                k.perintah.Parameters.AddWithValue("@7", cmb_pekerjaan.Text);
                k.perintah.Parameters.AddWithValue("@8", txt_instansi.Text);
                k.perintah.Parameters.AddWithValue("@9", img);
                k.perintah.Parameters.AddWithValue("@10", txt_nik.Text);

                k.perintah.ExecuteNonQuery();
                k.close();

                DialogHost.CloseDialogCommand.Execute(null, this);

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

        private void btn_ambil_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Masukan Foto";
            op.Filter = "Semua Gambar|*.jpg;*.jpeg;*.png|" +
                        "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                        "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                alamat_foto = op.FileName;
                img_foto.Source = new BitmapImage(new Uri(op.FileName));

            }
        }
    }
}
