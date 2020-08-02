using System;
using System.Collections.Generic;
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
using KOST_APP.Class;
using MaterialDesignColors.WpfExample.Domain;
using MaterialDesignThemes.Wpf;

namespace KOST_APP.Dialog
{
    /// <summary>
    /// Interaction logic for dialogTambahCustomer.xaml
    /// </summary>
    public partial class dialogTambahCustomer : UserControl
    {
        public dialogTambahCustomer()
        {
            InitializeComponent();
        }

        koneksi k = new koneksi();

        private void btn_simpan_Click(object sender, RoutedEventArgs e)
        {
            if (txt_nama.Text == "" || txt_telp.Text == "" || txt_asalkota.Text == "")
            {
                var sampleMessageDialog = new SampleMessageDialog
                {
                    Message = { Text = "Lengkapi Dulu Data" }
                };
                DialogHost.Show(sampleMessageDialog, "SubDialogCustomer");
            }
            else
            {
                try
                {
                    String jk = "";
                    if (rb_laki.IsChecked == true)
                    {
                        jk = "Laki - Laki";
                    }
                    else
                    {
                        jk = "Perempuan";
                    }
                    var img = ByteImageConverter.ConvertBitmapSourceToByteArray(img_foto.Source);

                    k.sql = "insert into customer values(@1,@2,@3,@4,@5,@6,@7)";
                    k.setparam();
                    k.perintah.Parameters.AddWithValue("@1", null);
                    k.perintah.Parameters.AddWithValue("@2", txt_nama.Text);
                    k.perintah.Parameters.AddWithValue("@3", txt_telp.Text);
                    k.perintah.Parameters.AddWithValue("@4", jk);
                    k.perintah.Parameters.AddWithValue("@5", txt_asalkota.Text);
                    k.perintah.Parameters.AddWithValue("@6", txt_alamat.Text);
                    k.perintah.Parameters.AddWithValue("@7", cmb_pekerjaan.Text);
                    k.perintah.Parameters.AddWithValue("@8", txt_instansi.Text);
                    k.perintah.Parameters.AddWithValue("@9", img);

                    k.perintah.ExecuteNonQuery();
                    k.close();

                    DialogHost.CloseDialogCommand.Execute(null, this);
                    var sampleMessageDialog = new SampleMessageDialog
                    {
                        Message = { Text = "Data Berhasil Tersimpan" }
                    };
                    DialogHost.Show(sampleMessageDialog, "MainDialog");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Data Gagal Didaftarkan " + ex);
                }
            }
            
        }

        private void btn_ambil_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
