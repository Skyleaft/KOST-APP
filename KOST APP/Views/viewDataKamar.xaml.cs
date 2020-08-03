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
    /// Interaction logic for viewBeranda.xaml
    /// </summary>
    public partial class viewDataKamar : UserControl
    {
        koneksi k = new koneksi();
        public viewDataKamar()
        {
            InitializeComponent();

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("id-ID");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("id-ID");

            showdata();
        }

        
        public void showdata()
        {
            k.sql = "select * from kamar";
            k.setdt();
            lv_kamar.ItemsSource = k.dt.DefaultView;

        }

        private void btn_refresh_Click(object sender, RoutedEventArgs e)
        {
            showdata();
        }

        private void lv_kamar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        //{
        //    showdata();
        //}

        private void btn_tambah_Click(object sender, RoutedEventArgs e)
        {
            var showdialog = new dialogTambahKamar();
            DialogHost.Show(showdialog, "MainDialog", ClosingEventHandler);
        }
        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            showdata();
        }

        private void txt_cari_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
