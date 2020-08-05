using MaterialDesignColors.WpfExample.Domain;
using MaterialDesignThemes.Wpf;
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

namespace KOST_APP.Dialog
{
    /// <summary>
    /// Interaction logic for dialogTambahKamar.xaml
    /// </summary>
    public partial class dialogTambahKamar : UserControl
    {
        koneksi k = new koneksi();
        public dialogTambahKamar()
        {
            InitializeComponent();

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("id-ID");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("id-ID");

            txt_biaya.Text = Convert.ToDecimal(0).ToString("c");
        }

        private void btn_simpan_Click(object sender, RoutedEventArgs e)
        {
            if (txt_nama.Text == "" || txt_biaya.Text == "")
            {
                var sampleMessageDialog = new SampleMessageDialog
                {
                    Message = { Text = "Lengkapi Dulu Data" }
                };
                DialogHost.Show(sampleMessageDialog, "SubDialogCKamar");
            }
            else
            {
                try
                {
                    int biaya = int.Parse(txt_biaya.Text, System.Globalization.NumberStyles.Currency);

                    k.sql = "insert into kamar values(@1,@2,@3,@4,@5,@6)";
                    k.setparam();
                    k.perintah.Parameters.AddWithValue("@1", null);
                    k.perintah.Parameters.AddWithValue("@2", txt_nama.Text);
                    k.perintah.Parameters.AddWithValue("@3", biaya);
                    k.perintah.Parameters.AddWithValue("@4", "1");
                    k.perintah.Parameters.AddWithValue("@5", "0");
                    k.perintah.Parameters.AddWithValue("@6", txt_luas.Text);

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

        private void txt_biaya_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1)) e.Handled = true;
        }

        private void txt_biaya_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void txt_biaya_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            UInt32 a = 0;
            txt_biaya.SelectionStart = txt_biaya.Text.Length - 3;
            if (txt_biaya.Text.Length <= 2) // jika panjang karakter pada textbox1 <= 2
            {
                txt_biaya.Text = Convert.ToDecimal(0).ToString("c");
            }
            else
            {
                txt_biaya.Text = decimal.Parse(txt_biaya.Text, System.Globalization.NumberStyles.Currency).ToString("c");
                //a = UInt32.Parse(txt_biaya.Text, System.Globalization.NumberStyles.Currency);

                txt_biaya.SelectionStart = txt_biaya.Text.Length - 3; // menetapkan titik awal dari teks yang dipilih pada textbox
            }
        }

        private void btn_tambahfas_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
