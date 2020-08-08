using KOST_APP.Dialog;
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
using DataGridTextColumn = System.Windows.Controls.DataGridTextColumn;

namespace KOST_APP.Views
{
    /// <summary>
    /// Interaction logic for viewPembayaran.xaml
    /// </summary>
    public partial class viewLaporan : UserControl
    {
        koneksi k = new koneksi();
        private int lama, biaya,total,tunai,kembali;
        private String invoice,idSewa,idCust,idKamar;
        int status;
        public viewLaporan()
        {
            InitializeComponent();
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("id-ID");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("id-ID");

            showdata();
        }

        public void showdata()
        {
            k.sql = "select *from viewpenjualan";
            k.setdt();
            dg_sewa.ItemsSource = k.dt.DefaultView;

        }


        private void btn_carisewa_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_refresh_Click(object sender, RoutedEventArgs e)
        {
            showdata();
        }

        private void txt_cari_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btn_bayar_Click(object sender, RoutedEventArgs e)
        {
            //var showdialog = new dialogBayar(total,invoice,tgl_bayar.SelectedDate.Value,idSewa);
            //DialogHost.Show(showdialog, "MainDialog", ClosingEventHandler);
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            
            //showdata();
        }

        private void dg_sewa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_sewa.SelectedIndex >= 0)
            {
                DataRowView dataRow = (DataRowView)dg_sewa.SelectedItem;
                string index = dataRow.Row[0].ToString();

                k.sql = "select * from viewpenjualan where invoice='"+index+"'";
                k.setdt();
                foreach (DataRow baris in k.dt.Rows)
                {
                    txt_invoice.Text = index;
                    txt_nomorsewa.Text = baris[6].ToString();
                    idCust = baris[10].ToString();
                    idKamar = baris[9].ToString();
                    tgl_bayar.Text = baris[1].ToString();
                }
                    
            }
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
    }
}
