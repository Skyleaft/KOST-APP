using MaterialDesignColors.WpfExample.Domain;
using MaterialDesignThemes.Wpf;
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

namespace KOST_APP.Dialog
{
    /// <summary>
    /// Interaction logic for dialogOpenKamar.xaml
    /// </summary>
    public partial class dialogOpenKamar : UserControl
    {
        koneksi k = new koneksi();
        private String idKamar;
        public dialogOpenKamar(String id_kamar)
        {
            InitializeComponent();
            this.idKamar = id_kamar;

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("id-ID");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("id-ID");
            txt_biaya.Text = Convert.ToDecimal(0).ToString("c");

            loadData();

            txt_biaya.Text = Convert.ToDecimal(0).ToString("c");
        }

        private async void loadData()
        {
            await Task.Delay(200);
            try
            {
                k.sql = "select *from kamar where id_kamar ='" + idKamar + "'";
                k.setdt();
                foreach (DataRow baris in k.dt.Rows)
                {
                    txt_nama.Text = baris[1].ToString();
                    txt_biaya.Text = baris[2].ToString();
                    cmb_status.SelectedIndex = int.Parse(baris[4].ToString());
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show("error di open " + ex);
            }
        }

        private void btn_ubah_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int biaya = int.Parse(txt_biaya.Text, System.Globalization.NumberStyles.Currency);

                k.sql = "update kamar set nomor_kamar=@1,biaya=@2,status=@3 where id_kamar = '" + idKamar + "'";
                k.setparam();
                k.perintah.Parameters.AddWithValue("@1", txt_nama.Text);
                k.perintah.Parameters.AddWithValue("@2", biaya);
                k.perintah.Parameters.AddWithValue("@3", cmb_status.SelectedIndex);

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

        private void btn_hapus_Click(object sender, RoutedEventArgs e)
        {
            Message.Text = "Yakin mau Hapus Customer : " + txt_nama.Text;
        }

        private void btn_yes_Click(object sender, RoutedEventArgs e)
        {
            k.sql = "delete from kamar where id_kamar = '" + idKamar + "'";
            k.setdt();

            var sampleMessageDialog = new SampleMessageDialog
            {
                Message = { Text = "Data Berhasil Di Hapus" }
            };
            DialogHost.CloseDialogCommand.Execute(null, this);

            DialogHost.Show(sampleMessageDialog, "MainDialog");
        }

        private void txt_biaya_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1)) e.Handled = true;
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
    }
}
