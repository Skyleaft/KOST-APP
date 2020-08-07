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
    /// Interaction logic for dialogBayar.xaml
    /// </summary>
    public partial class dialogBayar : UserControl
    {
        public event Action<int> Check;
        public event Action<int> Check2;
        public event Action<int> Check3;

        private decimal kembali;

        private int total;
        DateTime tglBayar;
        String noSewa;

        koneksi k = new koneksi();
        public dialogBayar(int _total,String _invoice,DateTime _tglBayar,String _noSewa)
        {
            InitializeComponent();
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("id-ID");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("id-ID");

            tglBayar = _tglBayar;
            total = _total;
            noSewa = _noSewa;
            txt_total.Text= Convert.ToDecimal(_total).ToString("c");
            txt_invoice.Text = _invoice;
            txt_tunai.Text = Convert.ToDecimal(0).ToString("c");
            txt_kembali.Text = Convert.ToDecimal(0).ToString("c");



        }

        private void btn_bayar_Click(object sender, RoutedEventArgs e)
        {
            int tunai = int.Parse(txt_tunai.Text, System.Globalization.NumberStyles.Currency);
            kembali = int.Parse(txt_kembali.Text, System.Globalization.NumberStyles.Currency);

            try
            {
                k.sql = "insert into pembayaran values(@1,@2,@3,@4,@5,@6,@7)";
                k.setparam();
                k.perintah.Parameters.AddWithValue("@1", txt_invoice.Text);
                k.perintah.Parameters.AddWithValue("@2", tglBayar.ToString("yyyy-MM-dd"));
                k.perintah.Parameters.AddWithValue("@3", total);
                k.perintah.Parameters.AddWithValue("@4", noSewa);
                k.perintah.Parameters.AddWithValue("@5", 1);
                k.perintah.Parameters.AddWithValue("@6", tunai);
                k.perintah.Parameters.AddWithValue("@7", kembali);

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

        private void txt_tunai_TextChanged(object sender, TextChangedEventArgs e)
        {
            UInt32 a = 0;
            txt_tunai.SelectionStart = txt_tunai.Text.Length - 3;
            if (txt_tunai.Text.Length <= 2) // jika panjang karakter pada textbox1 <= 2
            {
                txt_tunai.Text = Convert.ToDecimal(0).ToString("c");
            }
            else
            {
                txt_tunai.Text = decimal.Parse(txt_tunai.Text, System.Globalization.NumberStyles.Currency).ToString("c");
                //a = UInt32.Parse(txt_biaya.Text, System.Globalization.NumberStyles.Currency);

                txt_tunai.SelectionStart = txt_tunai.Text.Length - 3; // menetapkan titik awal dari teks yang dipilih pada textbox
                kembali = decimal.Parse(txt_tunai.Text, System.Globalization.NumberStyles.Currency) - decimal.Parse(txt_total.Text, System.Globalization.NumberStyles.Currency);
                if (kembali >= decimal.Parse(txt_total.Text, System.Globalization.NumberStyles.Currency))
                {
                    txt_kembali.Text = Convert.ToDecimal(kembali).ToString("c");
                }
                else
                {
                    txt_kembali.Text = Convert.ToDecimal(0).ToString("c");
                }
            }
        }

        private void txt_tunai_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1)) e.Handled = true;
        }
    }
}
