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
            k.sql = "select * from sewa";
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

        private void lv_customer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}
