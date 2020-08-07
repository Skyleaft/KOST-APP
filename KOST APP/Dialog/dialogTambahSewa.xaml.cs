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
using KOST_APP.Class;
using MaterialDesignColors.WpfExample.Domain;

namespace KOST_APP.Dialog
{
    /// <summary>
    /// Interaction logic for dialogTambahSewa.xaml
    /// </summary>
    public partial class dialogTambahSewa : UserControl
    {
        public String idCust,namaCust;
        public String idKamar,namaKamar;
        koneksi k = new koneksi();
        public dialogTambahSewa()
        {
            InitializeComponent();
            tgl_checkin.SelectedDate = DateTime.Now;
        }

        private void btn_caricust_Click(object sender, RoutedEventArgs e)
        {
            var showdialog = new dialogPilihCust();
            showdialog.Check += value => idCust = value;
            showdialog.Check2 += value => namaCust = value;
            DialogHost.Show(showdialog, "SubDialogSewa", ClosingEventHandler);
        }
        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            //showdata();
            txt_nama.Text = namaCust;
            txt_kamar.Text = namaKamar;
        }

        private void btn_simpan_Click(object sender, RoutedEventArgs e)
        {
            if (txt_nama.Text == "" || txt_kamar.Text == "")
            {
                var sampleMessageDialog = new SampleMessageDialog
                {
                    Message = { Text = "Lengkapi Dulu Data" }
                };
                DialogHost.Show(sampleMessageDialog, "SubDialogSewa");
            }
            else
            {
                try
                {
                    DateTime tglCheckin = tgl_checkin.SelectedDate.Value;
                    String idSewa = "KMR-" + idKamar+"M"+tglCheckin.ToString("MMdd")+"C"+idCust;
                    k.sql = "insert into sewa values(@1,@2,@3,@4,@5,@6,@7)";
                    k.setparam();
                    k.perintah.Parameters.AddWithValue("@1", idSewa);
                    k.perintah.Parameters.AddWithValue("@2", tglCheckin.ToString("yyyy-MM-dd"));
                    k.perintah.Parameters.AddWithValue("@3", slider_lama.Value);
                    k.perintah.Parameters.AddWithValue("@4", idCust);
                    k.perintah.Parameters.AddWithValue("@5", "1");
                    k.perintah.Parameters.AddWithValue("@6", idKamar);
                    k.perintah.Parameters.AddWithValue("@7", "1");

                    k.perintah.ExecuteNonQuery();
                    k.close();

                    k.sql = "update customer set status= 1 where id_customer='"+idCust+"'";
                    k.setdt();

                    k.sql = "update kamar set status= 1 where id_kamar='" + idKamar + "'";
                    k.setdt();

                    DialogHost.CloseDialogCommand.Execute(null, this);
                    var sampleMessageDialog = new SampleMessageDialog
                    {
                        Message = { Text = "Data Berhasil Tersimpan"}
                    };
                    DialogHost.Show(sampleMessageDialog, "MainDialog");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Data Gagal Didaftarkan " + ex);
                }
            }
        }

        

        private void btn_carikamar_Click(object sender, RoutedEventArgs e)
        {
            var showdialog = new dialogPilihKamar();
            showdialog.Check += value => idKamar = value;
            showdialog.Check2 += value => namaKamar = value;
            DialogHost.Show(showdialog, "SubDialogSewa", ClosingEventHandler);
        }
    }
}
