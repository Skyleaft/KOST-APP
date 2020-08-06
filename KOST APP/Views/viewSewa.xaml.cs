using KOST_APP.Dialog;
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
using DataGridTextColumn = System.Windows.Controls.DataGridTextColumn;

namespace KOST_APP.Views
{
    /// <summary>
    /// Interaction logic for viewSewa.xaml
    /// </summary>
    public partial class viewSewa : UserControl
    {
        koneksi k = new koneksi();
        public viewSewa()
        {
            InitializeComponent();
            showdata();
        }

        private void btn_tambah_Click(object sender, RoutedEventArgs e)
        {
            var showdialog = new dialogTambahSewa();
            DialogHost.Show(showdialog, "MainDialog", ClosingEventHandler);
        }

        public void showdata()
        {
            k.sql = "select sewa.id_sewa,customer.nama,sewa.tgl_checkin,sewa.lama_sewa,customer.no_hp,customer.kota,kamar.nomor_kamar from sewa " +
                    "inner join customer on sewa.id_customer = customer.id_customer inner join kamar on sewa.id_kamar=kamar.id_kamar";
            k.setdt();
            dg_sewa.ItemsSource = k.dt.DefaultView;

        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            showdata();
        }

        private void txt_cari_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btn_refresh_Click(object sender, RoutedEventArgs e)
        {
            showdata();
        }


        private void dg_sewa_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(System.DateTime))
                (e.Column as DataGridTextColumn).Binding.StringFormat = "dd-MM-yyyy";
            switch (e.Column.Header.ToString())
            {
                case "id_sewa":
                    e.Column.Header = "Nomor Sewa";
                    break;
                case "nama":
                    e.Column.Header = "Nama";
                    break;
                case "tgl_checkin":
                    e.Column.Header = "Tanggal Checkin";
                    break;
                case "lama_sewa":
                    e.Column.Header = "Lama Sewa";
                    break;
                case "no_hp":
                    e.Column.Header = "Telp";
                    break;
                case "kota":
                    e.Column.Header = "Asal Kota";
                    break;
                case "nomor_kamar":
                    e.Column.Header = "Nomor Kamar";
                    break;
            }
        }

        private void dg_sewa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
