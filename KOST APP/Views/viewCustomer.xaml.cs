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
using KOST_APP.Dialog;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;

namespace KOST_APP.Views
{
    /// <summary>
    /// Interaction logic for viewBeranda.xaml
    /// </summary>
    public partial class viewCustomer : UserControl
    {
        public viewCustomer()
        {
            InitializeComponent();
            showdata();
        }

        private String alamat_foto;
        koneksi k = new koneksi();

        public void showdata()
        {
            k.sql = "select * from customer";
            k.setdt();
            lv_customer.ItemsSource = k.dt.DefaultView;

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
                //img_foto.Source = new BitmapImage(new Uri(op.FileName));

            }
        }

        private void lv_customer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lv_customer.SelectedIndex >= 0)
            {
                DataRowView dataRow = (DataRowView)lv_customer.SelectedItem;
                string index = dataRow.Row[0].ToString();

                k.sql = "select *from customer  where id_customer = '" + index + "'";
                k.setdt();

                String idcust = k.dt.Rows[0][0].ToString();

                var showdialog = new dialogOpenCustomer(idcust);
                DialogHost.Show(showdialog, "MainDialog", ClosingEventHandler);

            }
        }

        private void btn_refresh_Click(object sender, RoutedEventArgs e)
        {
            showdata();
        }

        private void btn_tambah_Click(object sender, RoutedEventArgs e)
        {
            var showdialog = new dialogTambahCustomer();
            DialogHost.Show(showdialog, "MainDialog", ClosingEventHandler);
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            showdata();
        }

        private void txt_cari_TextChanged(object sender, TextChangedEventArgs e)
        {
            k.sql = "select * from customer where nama like'%" + txt_cari.Text + "%' or kota like'%" + txt_cari.Text + "%'";
            k.setdt();
            lv_customer.ItemsSource = k.dt.DefaultView;
        }
    }
}
