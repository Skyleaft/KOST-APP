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
    public partial class viewPembayaran : UserControl
    {
        koneksi k = new koneksi();
        private int lama, biaya,total,tunai,kembali;
        private String invoice,idSewa,idCust,idKamar;
        int status;
        public viewPembayaran()
        {
            InitializeComponent();
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("id-ID");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("id-ID");

            showdata();
            kodeotomatis();
            tgl_bayar.SelectedDate = DateTime.Now;
        }

        public void showdata()
        {
            k.sql = "select sewa.id_sewa,customer.nama,sewa.tgl_checkin,sewa.lama_sewa,customer.no_hp,customer.kota,kamar.nomor_kamar from sewa " +
                    "inner join customer on sewa.id_customer = customer.id_customer inner join kamar on sewa.id_kamar=kamar.id_kamar";
            k.setdt();
            dg_sewa.ItemsSource = k.dt.DefaultView;

        }

        private void kodeotomatis()
        {
            k.sql = "select * from pembayaran order by invoice asc";
            k.setdt();
            
            int cekbaris = k.dt.Rows.Count;
            String baru;
            int tambah;
            if (cekbaris == 0)
            {
                baru = "INV"+DateTime.Now.ToString("dd")+"-001-"+"M"+ DateTime.Now.ToString("MM");
            }
            else
            {
                tambah = Convert.ToInt32(k.dt.Rows[cekbaris - 1][0].ToString().Split('-')[1]) + 1;
                if (tambah < 10)
                {
                    baru = "INV" + DateTime.Now.ToString("dd") + "-00" + tambah + "-M" + DateTime.Now.ToString("MM");
                }
                else if (tambah < 100)
                {
                    baru = "INV" + DateTime.Now.ToString("dd") + "-0" + tambah + "-M" + DateTime.Now.ToString("MM");
                }
                else
                {
                    baru = "INV" + DateTime.Now.ToString("dd") + "-" + tambah + "-M" + DateTime.Now.ToString("MM");
                }
            }
            txt_invoice.Text = baru;
            invoice = baru;
        }

        private void btn_carisewa_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_refresh_Click(object sender, RoutedEventArgs e)
        {
            showdata();
            kodeotomatis();
        }

        private void txt_cari_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btn_bayar_Click(object sender, RoutedEventArgs e)
        {
            var showdialog = new dialogBayar(total,invoice,tgl_bayar.SelectedDate.Value,idSewa);
            DialogHost.Show(showdialog, "MainDialog", ClosingEventHandler);
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            
            showdata();
        }

        private void dg_sewa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_sewa.SelectedIndex >= 0)
            {
                DataRowView dataRow = (DataRowView)dg_sewa.SelectedItem;
                string index = dataRow.Row[0].ToString();

                k.sql = "select sewa.id_sewa,customer.nama,sewa.tgl_checkin,sewa.lama_sewa,customer.no_hp,customer.kota,kamar.nomor_kamar,kamar.biaya, customer.id_customer, kamar.id_kamar from sewa " +
                    "inner join customer on sewa.id_customer = customer.id_customer inner join kamar on sewa.id_kamar=kamar.id_kamar where sewa.id_sewa='"+index+"'";
                k.setdt();

                lama = int.Parse(k.dt.Rows[0][3].ToString());
                biaya = int.Parse(k.dt.Rows[0][7].ToString());
                idSewa = k.dt.Rows[0][0].ToString();
                txt_nomorsewa.Text = k.dt.Rows[0][0].ToString();
                lb_nama.Text = k.dt.Rows[0][1].ToString();
                idCust = k.dt.Rows[0][8].ToString();
                idKamar = k.dt.Rows[0][9].ToString();
                lb_lama.Text = k.dt.Rows[0][3].ToString();
                lb_kamar.Text = k.dt.Rows[0][6].ToString();
                lb_biaya.Text = Convert.ToDecimal(k.dt.Rows[0][7].ToString()).ToString("c");
                total = lama * biaya;
                lb_total.Text = Convert.ToDecimal(total).ToString("c");
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
